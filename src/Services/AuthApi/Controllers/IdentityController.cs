using AuthApi.Models;
using AuthApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers;

[ApiController]
[Route("[controller]")]
public class IdentityController : ControllerBase
{

    private readonly ILogger<IdentityController> _logger;
    private readonly IIdentityService _identityService;
    public IdentityController(ILogger<IdentityController> logger, IConfiguration configuration, IIdentityService identityService)
    {
        _logger = logger;
        _identityService = identityService;
    }

    [HttpPost("SignIn")]
    public IActionResult SignIn(UserFormInfo userForm)
    {
        User? user = _identityService.GetUser(userForm);
        if(user == null)
        {
            return NotFound("User not found");
        }

        string jwt = _identityService.GetToken(user);
        return Ok(jwt);
    }

    [HttpPost("Register")]
    public IActionResult Register(UserFormInfo userForm)
    {
        string? jwt = _identityService.RegisterUser(userForm);
        if(jwt == null)
        {
            return BadRequest();
        }

        return Ok(jwt);
    }
}
