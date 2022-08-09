using System.Net;

namespace Identity.Api.Domain.Common.Errors;

public class DuplicateUsernameException : Exception, IDomainException
{
    public string ErrorMessage => "Username already exists";

    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;
}