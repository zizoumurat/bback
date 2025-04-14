using Microsoft.AspNetCore.Authorization;

namespace Buyersoft.Presentation.Attributes;


[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class AuthorizeWithBearerPolicyAttribute : AuthorizeAttribute
{
    public AuthorizeWithBearerPolicyAttribute(string policy) : base()
    {
        AuthenticationSchemes = "Bearer";
        Policy = policy;
    }
}
