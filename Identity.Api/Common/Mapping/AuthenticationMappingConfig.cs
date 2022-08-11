using Identity.Api.Authentication.Commands.Register;
using Identity.Api.Authentication.Common;
using Identity.Api.Authentication.Queries.Login;
using Identity.Contracts.Auth;

using Mapster;

namespace Identity.Api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AuthenticationResult, AuthResponse>()
        .Map(dest => dest.Token, src => src.Token)
        .Map(dest => dest, src => src.User);
        config.NewConfig<RegisterRequest, RegisterCommand>();
        config.NewConfig<LoginRequest, LoginQuery>();
    }
}