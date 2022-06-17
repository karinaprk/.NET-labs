using DataAccess;
using DataAccess.Models;

namespace BusinessLogic.Services;

public class UserService
{
    private readonly UnitOfWork _uow;

    public UserService(UnitOfWork uow)
    {
        _uow = uow;
    }

    public string RegisterUser(string firstName, string lastName, string userName, string password)
    {
        if (_uow.UserRepository.GetByUserName(userName) is not null)
            throw new ArgumentException("User already exists", nameof(userName));

        var user = new User { FirstName = firstName, LastName = lastName, 
            UserName = userName, Password = password };
        _uow.UserRepository.Create(user);
        _uow.SaveChanges();

        return userName;
    }

    public string Login(string userName, string password)
    {
        var user = _uow.UserRepository.GetByUserName(userName);
        
        if (user is null || !user.Password.Equals(password))
            throw new ArgumentException("Invalid credentials", nameof(userName));

        return userName;
    }
}