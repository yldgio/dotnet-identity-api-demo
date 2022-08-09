using System.Net;
namespace Identity.Api.Domain.Common.Errors;

public interface IDomainException
{
    public string ErrorMessage { get; }
    public HttpStatusCode StatusCode { get; }
}