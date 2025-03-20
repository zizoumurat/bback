using Buyersoft.Presentation.Abstraction;
using Buyersoft.Presentation.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Buyersoft.WebAPI.OptionsSetup;

public static class AuthorizationPolicyGenerator
{
    public static void AddDynamicPolicies(AuthorizationOptions options)
    {
        var assembly = AppDomain.CurrentDomain.GetAssemblies()
            .FirstOrDefault(a => a.GetName().Name == "Buyersoft.Presentation");

        var controllers = assembly?.GetTypes()
            .Where(type => typeof(ApiController).IsAssignableFrom(type) && !type.IsAbstract)
            .ToList();

        foreach (var controller in controllers)
        {
            var actions = controller.GetMethods(BindingFlags.Instance | BindingFlags.Public)
                .Where(m => m.IsDefined(typeof(AuthorizeWithBearerPolicyAttribute), true))
                .ToList();

            foreach (var action in actions)
            {
                var attributes = action.GetCustomAttributes<AuthorizeWithBearerPolicyAttribute>();

                foreach (var attribute in attributes)
                {
                    if (options.GetPolicy(attribute.Policy) == null)
                    {
                        options.AddPolicy(attribute.Policy, policy =>
                            policy.RequireClaim("Permission", attribute.Policy));
                    }
                }
            }
        }
    }
}