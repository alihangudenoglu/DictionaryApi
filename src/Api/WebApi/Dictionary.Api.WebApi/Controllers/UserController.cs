using Dictionary.Common.Models.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dictionary.Api.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        var result =await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost]
    [Route("Update")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

}
