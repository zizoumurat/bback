using Buyersoft.Application.Services;
using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Enums;
using Buyersoft.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

public class SendNotificationService : ISendNotificationService
{
    private readonly IHubContext<NotificationHub> _hubContext;
    private static ConcurrentDictionary<string, CancellationTokenSource> countdownTokens = new();
    private static Dictionary<string, int> pausedSessions = new();

    public SendNotificationService(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task ChangeStatu(string requestId, ReverseAuctionStatusEnum statu, int remainingSeconds)
    {
        // İstemcilere durum değişikliğini bildir
        await _hubContext.Clients.Group(requestId).SendAsync("ChangeStatu", statu);

        switch (statu)
        {
            case ReverseAuctionStatusEnum.Started:
                await HandleStart(requestId, remainingSeconds);
                break;

            case ReverseAuctionStatusEnum.Paused:
                HandlePause(requestId, remainingSeconds);
                break;

            case ReverseAuctionStatusEnum.Ended:
                await HandleClose(requestId);
                break;
        }
    }

    private async Task HandleStart(string requestId, int remainingSeconds)
    {
        // Eğer duraklatılmış bir oturum varsa, kaydedilen süreyi kullan
        if (pausedSessions.TryGetValue(requestId, out var pausedSeconds))
        {
            pausedSessions.Remove(requestId);
            remainingSeconds = pausedSeconds;
        }

        await StartCountdown(requestId, remainingSeconds);
    }

    private void HandlePause(string requestId, int remainingSeconds)
    {
        // Geri sayımı durdur ve kalan saniyeyi kaydet
        PauseCountdown(requestId, remainingSeconds);
        pausedSessions[requestId] = remainingSeconds;
    }

    private async Task HandleClose(string requestId)
    {
        // Geri sayımı tamamen durdur
        await CloseSession(requestId);
    }

    private async Task StartCountdown(string requestId, int remainingSeconds)
    {
        // Eğer mevcut bir geri sayım varsa, önce iptal et
        if (countdownTokens.TryGetValue(requestId, out var existingTokenSource))
        {
            existingTokenSource.Cancel();
            countdownTokens.TryRemove(requestId, out _);
        }

        var tokenSource = new CancellationTokenSource();
        countdownTokens[requestId] = tokenSource;

        // Asenkron geri sayım işlemini başlat
        await Task.Run(async () =>
        {
            while (remainingSeconds > 0 && !tokenSource.Token.IsCancellationRequested)
            {
                remainingSeconds--;

                // Geri kalan süreyi istemcilere gönder
                await UpdateRemainingTime(requestId, remainingSeconds);

                await Task.Delay(1000); // 1 saniye bekle
            }

            if (remainingSeconds <= 0)
            {
                // Süre bittiğinde oturumu kapat
                await HandleClose(requestId);
            }
        });
    }

    private void PauseCountdown(string requestId, int remainingSeconds)
    {
        if (countdownTokens.TryGetValue(requestId, out var tokenSource))
        {
            tokenSource.Cancel();
            countdownTokens.TryRemove(requestId, out _);
        }
    }

    private async Task CloseSession(string requestId)
    {
        if (countdownTokens.TryGetValue(requestId, out var tokenSource))
        {
            tokenSource.Cancel();
            countdownTokens.TryRemove(requestId, out _);
        }

        // İstemcilere oturumun kapandığını bildir
        await _hubContext.Clients.Group(requestId).SendAsync("SessionClosed");
    }

    private async Task UpdateRemainingTime(string requestId, int remainingSeconds)
    {
        // İstemcilere güncel süreyi gönder
        await _hubContext.Clients.Group(requestId).SendAsync("UpdateRemainingTime", remainingSeconds);
    }


    public async Task SendNotificationToUserAsync(string userId, string message)
    {
        await _hubContext.Clients.Group(userId).SendAsync("ReceiveNotification", message);
    }

    public async Task ChangePrice(string requestId)
    {
        await _hubContext.Clients.Group(requestId).SendAsync("ChangePrice");
    }

    public async Task SendComment(string contractId, string user, string message)
    {
        await _hubContext.Clients.Group(contractId).SendAsync("ReceiveComment", user, message);
    }
}
