using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Features.DepartmentFeatures.Queries.GetAllDepartments;
public sealed record GetAllDepartmentsQuery(DepartmentFilterDto filter, PageRequest pagination) : IQuery<GetAllDepartmentsQueryResponse>;
