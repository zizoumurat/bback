using Buyersoft.Application.Services;
using System.Globalization;
using System.Resources;

namespace Buyersoft.Infrastructure.Services;
public class LocalizationService : ILocalizationService
{
    private readonly ResourceManager _resourceManager;

    public LocalizationService()
    {
        _resourceManager = new ResourceManager("Buyersoft.Infrastructure.Resources.Messages", typeof(LocalizationService).Assembly);
    }

    public string GetLocalizedString(string key)
    {
        return key;
    }
}