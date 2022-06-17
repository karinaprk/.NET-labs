using BusinessLogic.Services;
using ConsoleUI.Printers;

namespace ConsoleUI.Controllers;

public class UserController
{
    private readonly UserService _userService;

    public string CurrentUserName { get; set; }

    public UserController(UserService userService)
    {
        _userService = userService;
    }
    
    public void RegisterUser(string firstName, string lastName, string userName, string password)
    {
        if (firstName is null || lastName is null || userName is null || password is null) 
            Console.WriteLine(CommonPrinter.FillEmptyLines);
        
        try
        {
            CurrentUserName = _userService.RegisterUser(firstName, lastName, userName, password);
        }
        catch (ArgumentException exception)
        {
            Console.WriteLine(exception.Message);
            return;
        }
        
        Console.WriteLine("You have successfully registered and logged in");
    }
    
    public void Login(string userName, string password)
    {
        if (userName is null || password is null) 
            Console.WriteLine(CommonPrinter.FillEmptyLines);

        try
        {
            CurrentUserName = _userService.Login(userName, password);
        }
        catch (ArgumentException exception)
        {
            Console.WriteLine(exception.Message);
            return;
        }
        
        Console.WriteLine("You have successfully logged in");
    }

    public void Logout()
    {
        CurrentUserName = null;
        Console.WriteLine("You have successfully logged out");
    }
}