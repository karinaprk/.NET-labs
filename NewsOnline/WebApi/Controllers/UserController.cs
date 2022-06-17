using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using WebApi.ViewModels.UserViewModels;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    

    public UserController(UserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost("/Register")]
    public IActionResult RegisterUser(RegisterUser model)
    {
        if (string.IsNullOrEmpty(model.FirstName) 
            || string.IsNullOrEmpty(model.LastName)
            || string.IsNullOrEmpty(model.UserName) 
            || string.IsNullOrEmpty(model.Password))
            return BadRequest("Empty fields");

        if (!model.Password.Equals(model.ConfirmPassword))
            return BadRequest("Passwords are not the same");
        
        try
        {
            _userService.RegisterUser(model.FirstName, model.LastName, model.UserName, model.Password);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }

        return Ok(model.UserName);
    }
    
    [HttpPost("/Login")]
    public IActionResult Login(LoginViewModel model)
    {
        if (string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.UserName))
            return BadRequest("Empty fields");

        try
        {
            _userService.Login(model.UserName, model.Password);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }

        return Ok(model.UserName);
    }
}