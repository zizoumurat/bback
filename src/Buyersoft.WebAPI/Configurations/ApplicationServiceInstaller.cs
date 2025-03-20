using FluentValidation;
using MediatR;
using Buyersoft.Application;
using Buyersoft.Application.Behavior;
using Buyersoft.Application.Features.CommonFeatures.Queries.GetSelectListItemsQuery;

namespace Buyersoft.WebApi.Configurations;

public class ApplicationServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(typeof(AssemblyReference).Assembly);

        services.AddTransient(typeof(IRequestHandler<,>), typeof(GetSelectListItemsQueryHandler<>));

        services.AddTransient(typeof(IPipelineBehavior<,>), (typeof(ValidationBehavior<,>)));

        services.AddValidatorsFromAssembly(typeof(AssemblyReference).Assembly);
    }
}
