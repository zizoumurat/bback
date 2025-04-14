using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.DepartmentFeatures.Queries.GetAllDepartments;

public sealed record GetAllDepartmentsQueryResponse(PaginatedList<DepartmentListDto> result);
