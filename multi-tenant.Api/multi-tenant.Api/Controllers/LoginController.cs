using MediatR;
using Microsoft.AspNetCore.Mvc;
using multi_tenant.Models.Login;

namespace multi_tenant.Api.Controllers;

/// <summary>
/// Login
/// </summary>
[ApiController]
[Route("[controller]")]

public class LoginController : ControllerBase
{
    private readonly ILogger<LoginController> _logger;
    private readonly IMediator _mediator;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="mediator"></param>
    public LoginController(ILogger<LoginController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    /// <summary>
    /// Authenticate
    /// </summary>
    /// <returns></returns>
    [HttpPost("")]
    public async Task<IActionResult> Authenticate([FromBody] LoginRequest login)
    {
        var response = await _mediator.Send(login);

        if (response.Errors.Any())
            return BadRequest(response);

        return Ok(response);
    }
}