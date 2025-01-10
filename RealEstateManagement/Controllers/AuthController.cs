using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<ActionResult<Result<Guid>>> Register(RegisterUserCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.IsSuccess)
        {
            return Ok(new { UserId = result.Data });
        }
        else
        {
            return BadRequest(result.ErrorMessage);
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<Result<string>>> Login(LoginUserCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.IsSuccess)
        {
            return Ok(new { Token = result.Data });
        }
        else
        {
            return BadRequest(result.ErrorMessage);
        }
    }
}
