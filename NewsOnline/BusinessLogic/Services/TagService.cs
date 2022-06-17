using DataAccess;
using DataAccess.Models;

namespace BusinessLogic.Services;

public class TagService
{
    private readonly UnitOfWork _uow;

    public TagService(UnitOfWork uow)
    {
        _uow = uow;
    }

    public void CreateTag(string name)
    {
        if (_uow.TagRepository.GetByName(name) is not null)
            throw new ArgumentException("User already exists", nameof(name));
        
        _uow.TagRepository.Create(new Tag { Title = name });
        _uow.SaveChanges();
    }
}