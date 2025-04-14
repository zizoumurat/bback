using Buyersoft.Application.Features.CommonFeatures.Queries.GetSelectListItemsQuery;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Presentation.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Buyersoft.Presentation.Controller;

public class PublicListController : ApiController
{
    private readonly IBranchService _branchService;
    private readonly IDepartmentService _departmentService;
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;

    public PublicListController(IMediator mediator, IBranchService branchService, IUserService userService, ITokenService tokenService, IDepartmentService departmentService) : base(mediator)
    {
        _branchService = branchService;
        _userService = userService;
        _tokenService = tokenService;
        _departmentService = departmentService;
    }

    [HttpGet("{entityName}")]
    public async Task<IActionResult> GetAll([FromRoute] string entityName, [FromQuery] Dictionary<string, string> queryParameters = null)
    {
        if(entityName.ToLower() == "branch")
        {
            var list = await _branchService.GetSelectListItemsAsync(queryParameters);

            return Ok(list);
        }

        var assembly = Assembly.Load("Buyersoft.Domain");
        var entityType = assembly.GetTypes().Where(x => x.FullName.ToLower() == $"Buyersoft.Domain.Entitites.{entityName}".ToLower()).FirstOrDefault();

        if (entityType == null)
        {
            return BadRequest("Unknown entity name.");
        }

        var queryType = typeof(GetSelectListItemsQuery<>).MakeGenericType(entityType);

        var query = Activator.CreateInstance(queryType, queryParameters);
        var result = await _mediator.Send((IRequest<List<SelectListItemDto>>)query);

        return Ok(result);
    } 
}
