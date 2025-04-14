using Buyersoft.Application.Features.AppFeatures.AuthFeatures.Commands.ForgotPassword;
using Buyersoft.Application.Features.AppFeatures.AuthFeatures.Commands.Login;
using Buyersoft.Application.Features.AppFeatures.AuthFeatures.Commands.ResetPassword;
using Buyersoft.Application.Features.AuthFeatures.Commands.GetTokenByRefreshToken;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Presentation.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Buyersoft.Presentation.Controller;
public class AuthController : ApiController
{
    private readonly ISupplierService _supplierService;

    public AuthController(IMediator mediator, ISupplierService supplierService) : base(mediator)
    {
        _supplierService = supplierService;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Login(LoginCommand request)
    {
        LoginCommandResponse response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> GetTokenByRefreshToken(GetTokenByRefreshTokenCommand request)
    {
        GetTokenByRefreshTokenCommandResponse response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPost("create-supplier")]
    public async Task<IActionResult> CreateSupplier(SupplierCreateDto model)
    {
        await _supplierService.CreateSupplier(model);
        return Ok();
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordCommand request)
    {
        ForgotPasswordCommandResponse response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResettPassword(ResetPasswordCommand request)
    {
        ResetPasswordCommandResponse response = await _mediator.Send(request);
        return Ok(response);
    }
}
