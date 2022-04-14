using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Domain.Common.Models.User;
using RestaurantAPI.Domain.Interfaces.Facades;

namespace RestaurantAPI.UI.ASP.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountFcd _accountFcd;

    public AccountController(IAccountFcd accountFcd)
    {
        _accountFcd = accountFcd;
    }

    [HttpPost("register")]
    public ActionResult RegisterUser([FromBody] RegisterDTO registerDTO)
    {
        return Ok(_accountFcd.RegisterUser(registerDTO));
    }

    [HttpPost("login")]
    public ActionResult LoginUser([FromBody] LoginDTO loginDTO)
    {
        return Ok(_accountFcd.LoginUser(loginDTO));
    }
}