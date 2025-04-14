using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;

namespace Buyersoft.Application.Features.DepartmentFeatures.Queries.GetAllDepartments;

public sealed class GetAllDepartmentsQueryHandler : IQueryHandler<GetAllDepartmentsQuery, GetAllDepartmentsQueryResponse>
{
    private readonly IDepartmentService _departmentService;
    private readonly ITokenService _tokenService;

    public GetAllDepartmentsQueryHandler(IDepartmentService departmentService, ITokenService tokenService)
    {
        _departmentService = departmentService;
        _tokenService = tokenService;
    }

    public async Task<GetAllDepartmentsQueryResponse> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
    {
        int companyId = _tokenService.GetCompanyIdByToken();

        var result = await _departmentService.GetAllAsync(companyId, request.filter, request.pagination);

        return new(result);
    }
}