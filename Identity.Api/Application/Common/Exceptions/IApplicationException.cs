using System.Net;
namespace Identity.Api.Application.Common.Exceptions;

public interface IApplicationException
{
    public string ErrorMessage { get; }
    public HttpStatusCode StatusCode { get; }
}