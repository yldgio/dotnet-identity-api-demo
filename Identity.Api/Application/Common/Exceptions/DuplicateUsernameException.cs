using System.Net;

namespace Identity.Api.Application.Common.Exceptions;

public class DuplicateUsernameException : Exception, IApplicationException
{
    public string ErrorMessage => "Username already exists";

    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;
}