using ErrorOr;
using Identity.Api.Application.Authentication.Common;
using Identity.Api.Application.Authentication.Queries.Identity;
using Identity.Contracts.Auth;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace Identity.Api.Presentation.Controllers;

[Route("[controller]")]
public class IdentityController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public IdentityController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("get/{username}")]
    public async Task<IActionResult> Get(string Username)
    {
        var identityQuery = new IdentityQueryGet(Username);
        ErrorOr<IdentityResult> result = await _mediator.Send(identityQuery);

        return result.Match(
            result => Ok(_mapper.Map<IdentityResponse>(result)),
            errors => Problem(errors)
        );
    }
}