using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace BusinessLogic.Services;

public class NewsService
{
    private readonly UnitOfWork _uow;

    public NewsService(UnitOfWork uow)
    {
        _uow = uow;
    }

    public void CreateNews(string authorName, string title, string content, string headingName, IEnumerable<string> tagNames)
    {
        var author = _uow.UserRepository.GetByUserName(authorName);
        if (author is null)
            throw new ArgumentException("This author does not exist", nameof(authorName));
        
        var heading = _uow.HeadingRepository.GetByName(headingName);
        if (heading is null)
            throw new ArgumentException("This heading does not exist", nameof(headingName));

        var tags = new List<Tag>();
        foreach (var tagName in tagNames)
        {
            var tag = _uow.TagRepository.GetByName(tagName);
            if (tag is null)
                throw new ArgumentException("This tag does not exist", nameof(tagNames));
            
            tags.Add(tag);
        }

        var news = new News
        {
            Heading = heading, 
            Content = content, 
            Tags = tags,
            Title = title, 
            Author = author, 
            PublicationTime = DateTime.Now
        };
        
        _uow.NewsRepository.Create(news);
        _uow.SaveChanges();
    }

    public IEnumerable<News> GetByTitle(string title)
    {
        var news = _uow.NewsRepository.GetAll()
            .Where(n => n.Title.Equals(title))
            .ToList();
        return news;
    }

    public IEnumerable<News> GetByUserName(string userName)
    {
        var author = _uow.UserRepository.GetByUserName(userName);
        if (author is null) throw new ArgumentException("This author does not exists", nameof(userName));

        return author.News.ToList();
    }

    public IEnumerable<News> GetByPeriod(DateTime start, DateTime end)
    {
        var news = _uow.NewsRepository.GetAll()
            .Where(n => n.PublicationTime >= start && n.PublicationTime <= end).ToList();

        return news;
    }
    
    public IEnumerable<News> GetByHeading(string heading)
    {
        var news = _uow.NewsRepository.GetAll()
            .Where(n => n.Heading.Title.Equals(heading)).ToList();

        return news;
    }
}