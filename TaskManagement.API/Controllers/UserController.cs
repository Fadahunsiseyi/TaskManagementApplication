using Microsoft.AspNetCore.Mvc;
using TaskManagement.Domain.Dtos.User;
using TaskManagement.Domain.Interface.Services;

namespace TaskManagement.API.Controllers;


[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    public IUserService UserService { get; }

    public UserController(IUserService userService)
    {
        UserService = userService;
    }

    [HttpPost]
    [Route("Create")]

    public async Task<IActionResult> CreateUser(UserCreate userCreate)
    {
        var id = await UserService.CreateUserAsync(userCreate);
        return Ok(id);
    }

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await UserService.GetUsersAsync();
        return Ok(users);
    }
    

}
