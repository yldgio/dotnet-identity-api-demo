namespace Identity.Api.Domain.Common.Errors;

public interface IError
{
    public System.Net.HttpStatusCode StatusCode { get; }
    public string ErrorMessage { get; }

}