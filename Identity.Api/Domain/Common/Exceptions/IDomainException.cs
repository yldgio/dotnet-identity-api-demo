using System.Net;
namespace Identity.Api.Domain.Common.Exceptions;

public interface IDomainException
{
    public string ErrorMessage { get; }
    public HttpStatusCode StatusCode { get; }
}