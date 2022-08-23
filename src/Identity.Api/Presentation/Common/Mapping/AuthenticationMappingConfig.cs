using Identity.Api.Application.Authentication.Commands;
using Identity.Api.Application.Authentication.Common;
using Identity.Api.Application.Authentication.Queries;
using Identity.Contracts.Auth;

using Mapster;

namespace Identity.Api.Presentation.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AuthenticationResult, AuthResponse>()
        .Map(dest => dest.Token, src => src.Token)
        .Map(dest => dest, src => src.User); //can be skipped
        config.NewConfig<RegisterRequest, RegisterCommand>();
        config.NewConfig<LoginRequest, LoginQuery>();
    }
}