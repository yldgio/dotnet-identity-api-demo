using System.Net;

namespace Identity.Api.Application.Common.Errors;

public class DuplicateUsernameError
{
    public string ErrorMessage => "Username already exists";

    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

}