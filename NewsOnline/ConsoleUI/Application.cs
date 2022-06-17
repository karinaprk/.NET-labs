using System.Security.Cryptography;
using ConsoleUI.Controllers;

namespace ConsoleUI;

public class Application
{
    private readonly UserController _userController;
    private readonly NewsController _newsController;
    private readonly TagController _tagController;

    public Application(UserController userController, 
        NewsController newsController, TagController tagController)
    {
        _userController = userController;
        _newsController = newsController;
        _tagController = tagController;
    }

    public void Run()
    {
        while (true)
            HomePage();
    }

    public void HomePage()
    {
        Console.WriteLine("Choose an action");

        int choice;

        if (_userController.CurrentUserName is null)
        {
            Console.WriteLine("1 - User profile");
            Console.WriteLine("2 - Search for news");
            Console.WriteLine("If you want to post news - register or login first");

            choice = GetChoice(1, 2);

            switch (choice)
            {
                case 1: 
                    UserActions();
                    break;
                case 2:
                    SearchNews();
                    break;
            }
        }
        else
        {
            Console.WriteLine("1 - User profile");
            Console.WriteLine("2 - Search for news");
            Console.WriteLine("3 - Create a tag");
            Console.WriteLine("4 - Create news");

            choice = GetChoice(1, 4);

            switch (choice)
            {
                case 1: 
                    UserActions();
                    break;
                case 2:
                    SearchNews();
                    break;
                case 3:
                    CreateTag();
                    break;
                case 4:
                    CreateNews();
                    break;
            }
        }
    }
    
    private void CreateNews()
    {
        Console.WriteLine("Enter a title");
        var title = Console.ReadLine();
        Console.WriteLine("Enter a heading name of the news");
        var headingName = Console.ReadLine();
        Console.WriteLine("Add tags seperated with enter, write \"0\" to stop enter tags");
        var tags = AddTags();
        Console.WriteLine("Enter a content you want to share");
        var content = Console.ReadLine();
        
        _newsController.CreateNews(_userController.CurrentUserName, title, content, headingName, tags);
    }

    private IEnumerable<string> AddTags()
    {
        var tags = new List<string>();
        while (true)
        {
            var tagName = Console.ReadLine();
            if (tagName is null || tagName.Equals("0"))
                break;
            tags.Add(tagName);
        }

        return tags;
    }
    
    private void CreateTag()
    {
        Console.WriteLine("Enter a name of the tag");
        var tagName = Console.ReadLine();
        _tagController.CreateTag(tagName);
    }

    private void SearchNews()
    {
        Console.WriteLine("Choose an action");
        Console.WriteLine("0 - Return");
        Console.WriteLine("1 - Search by title");
        Console.WriteLine("2 - Search by author");
        Console.WriteLine("3 - Search by heading");
        Console.WriteLine("4 - Search by period");
        
        var choice = GetChoice(0, 4);
        switch (choice)
        {
            case 0: 
                HomePage();
                break;
            case 1:
                Console.WriteLine("Enter a title");
                _newsController.GetByTitle(Console.ReadLine());
                break;
            case 2:
                Console.WriteLine("Enter an author");
                _newsController.GetByUserName(Console.ReadLine());
                break;
            case 3:
                Console.WriteLine("Enter a heading");
                _newsController.GetByHeading(Console.ReadLine());
                break;
            case 4:
                Console.WriteLine("Enter a period");
                var period = EnterPeriod();
                _newsController.GetByPeriod(period.Start, period.End);
                break;
        }
    }

    private (DateTime Start, DateTime End) EnterPeriod()
    {
        Console.WriteLine("Enter start of the period");
        DateTime start;
        while (true)
        {
            if (DateTime.TryParse(Console.ReadLine(), out start))
                break;
            Console.WriteLine("Wrong time period");
        }
            
        Console.WriteLine("Enter end of the period");
        DateTime end;
        while (true)
        {
            if (DateTime.TryParse(Console.ReadLine(), out end))
                break;
            Console.WriteLine("Wrong time period");
        }

        return (start, end);
    }

    private void UserActions()
    {
        Console.WriteLine("Choose an action");
        Console.WriteLine("0 - Return");

        int choice;
        if (_userController.CurrentUserName is not null)
        {
            Console.WriteLine("1 - Logout");
            choice = GetChoice(0, 1);
            switch (choice)
            {
                case 0: 
                    HomePage();
                    break;
                case 1: 
                    _userController.Logout();
                    break;
            }
        }
        else
        {
            Console.WriteLine("1 - Register");
            Console.WriteLine("2 - Login");
            
            choice = GetChoice(0, 2);
            switch (choice)
            {
                case 0: 
                    HomePage();
                    break;
                case 1:
                    var registerInfo = RegisterUser();
                    _userController.RegisterUser(registerInfo.FirstName, registerInfo.LastName, 
                        registerInfo.UserName, registerInfo.Password);
                    break;
                case 2:
                    var loginInfo = LoginUser();
                    _userController.Login(loginInfo.UserName, loginInfo.Password);
                    break;
            }
        }
        
        
    }

    private (string UserName, string Password) LoginUser()
    {
        Console.WriteLine("Enter your username");
        string userName = Console.ReadLine();
            
        Console.WriteLine("Enter your password");
        string password = Console.ReadLine();

        return (userName, password);
    }

    private (string FirstName, string LastName, string UserName, string Password) RegisterUser()
    {
        Console.WriteLine("Enter your first name");
        string firstName = Console.ReadLine();
            
        Console.WriteLine("Enter your last name");
        string lastName = Console.ReadLine();
        
        Console.WriteLine("Enter your user name");
        string userName = Console.ReadLine();

        string password;
        while (true)
        {
            Console.WriteLine("Enter your password");
            password = Console.ReadLine();

            Console.WriteLine("Enter your password again");
            if (Console.ReadLine().Equals(password))
                break;
            Console.WriteLine("Wrong password, try again");
        }

        return (firstName, lastName, userName, password);
    }
    
    private static int GetChoice(int lowerBound, int upperBound)
    {
        int choice;
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out choice) && choice >= lowerBound && choice <= upperBound)
                break;

            Console.WriteLine("Wrong choice, try again");
        }

        return choice;
    }
}