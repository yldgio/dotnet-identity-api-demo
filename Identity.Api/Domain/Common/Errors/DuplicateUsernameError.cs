using System.Net;

using FluentResults;

namespace Identity.Api.Domain.Common.Errors;

public class DuplicateUsernameError : Error
{
    public DuplicateUsernameError() : base("Username already exists")
    { }
}