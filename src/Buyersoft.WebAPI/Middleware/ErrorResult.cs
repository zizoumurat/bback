using System.Text.Json;

namespace Buyersoft.WebApi.Middleware;

public class ErrorResult : ValidationErrorDetails
{
    public string Message { get; set; }
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}

public class ValidationErrorDetails
{
    public int StatusCode { get; set; }
    public List<ValidationError> Errors { get; set; }
}

public class ValidationError
{
    public string PropertyName { get; set; }
    public string ErrorMessage { get; set; }
}
