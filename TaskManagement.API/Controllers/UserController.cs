using Microsoft.AspNetCore.Mvc;
using TaskManagement.Common.Dtos.User;
using TaskManagement.Domain.Entities;
using TaskManagement.Application.Interface.Services;

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
        try
        {
            var id = await UserService.CreateUserAsync(userCreate);
            return Ok(new { Status = "Success", Message = "User created successfully", Id = id });
        }
        catch (Exception ex)
        {

            return BadRequest(new { Status = "Error", Message = "Error creating user: " + ex.Message });
        }
    }

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> GetUsers()
    {
        try
        {
            var users = await UserService.GetUsersAsync();
            if (users.Any())
                return Ok(new { Status = "Success", Message = "Users retrieved successfully", Users = users });
            else
                return NotFound(new { Status = "Error", Message = "No users found" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Status = "Error", Message = "An error occurred while retrieving users: " + ex.Message });
        }
    }

    [HttpGet]
    [Route("Get/{id}")]
    public async Task<IActionResult> GetUser(Guid id)
    {
        try
        {
            var user = await UserService.GetUserAsync(id);
            if (user != null)
                return Ok(new { Status = "Success", Message = "User retrieved successfully", User = user });
            else
                return NotFound(new { Status = "Error", Message = "User not found" });
        }
        catch (Exception ex)
        {

            return StatusCode(500, new { Status = "Error", Message = "An error occurred while retrieving the user: " + ex.Message });
        }
    }

    [HttpPut]
    [Route("Update/{id}")]
    public async Task<IActionResult> UpdateUser([FromRoute]Guid id, UserUpdate userUpdate)
    {
        try
        {
            await UserService.UpdateUserAsync(id, userUpdate);
            return Ok(new { Status = "Success", Message = "User updated successfully" });
        }
        catch (Exception ex)
        {

            return StatusCode(500, new { Status = "Error", Message = "An error occurred while updating the user: " + ex.Message });
        }
    }

    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<IActionResult> DeleteUser([FromRoute]Guid id)
    {
        try
        {
            await UserService.DeleteUserAsync(id);
            return Ok(new { Status = "Success", Message = "User deleted successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Status = "Error", Message = "An error occurred while deleting the user: " + ex.Message });
        }
    }
    

}
