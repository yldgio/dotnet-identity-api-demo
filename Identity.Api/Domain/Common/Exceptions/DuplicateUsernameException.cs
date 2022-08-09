using System.Net;

namespace Identity.Api.Domain.Common.Exceptions;

public class DuplicateUsernameException : Exception, IDomainException
{
    public string ErrorMessage => "Username already exists";

    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;
}