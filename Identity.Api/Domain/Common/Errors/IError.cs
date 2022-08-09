namespace Identity.Api.Domain.Common.Errors;

public interface IDomainError
{
    public System.Net.HttpStatusCode StatusCode { get; }
    public string ErrorMessage { get; }

}