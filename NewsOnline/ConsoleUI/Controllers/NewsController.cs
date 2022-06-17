using BusinessLogic.Services;
using ConsoleUI.Printers;
using DataAccess.Models;

namespace ConsoleUI.Controllers;

public class NewsController
{
    private readonly NewsService _newsService;
    
    public NewsController(NewsService newsService)
    {
        _newsService = newsService;
    }
    
    public void CreateNews(string authorName, string title, string content, string headingName, IEnumerable<string> tagNames)
    {
        if (authorName is null || title is null || content is null 
            || headingName is null || tagNames is null || !tagNames.Any())
            Console.WriteLine(CommonPrinter.FillEmptyLines);

        try
        {
            _newsService.CreateNews(authorName, title, content, headingName, tagNames);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
            return;
        }
        
        Console.WriteLine("You have successfully created the news");
    }
    
    public void GetByTitle(string title)
    {
        if (title is null) Console.WriteLine(CommonPrinter.FillEmptyLines);

        var news = _newsService.GetByTitle(title);
        if (news is null || !news.Any())
        {
            Console.WriteLine("No such news");
            return;
        }

        PrintNews(news);
    }
    
    public void GetByUserName(string userName)
    {
        if (userName is null) Console.WriteLine(CommonPrinter.FillEmptyLines);

        var news = _newsService.GetByUserName(userName);
        if (news is null || !news.Any())
        {
            Console.WriteLine("No news by this author");
            return;
        }

        PrintNews(news);
    }
    
    public void GetByHeading(string headingName)
    {
        if (headingName is null) Console.WriteLine(CommonPrinter.FillEmptyLines);

        var news = _newsService.GetByHeading(headingName);
        if (news is null || !news.Any())
        {
            Console.WriteLine("No news under this heading");
            return;
        }

        PrintNews(news);
    }

    public void GetByPeriod(DateTime start, DateTime end)
    {
        var news = _newsService.GetByPeriod(start, end);
        if (news is null || !news.Any())
        {
            Console.WriteLine("No news between this period");
            return;
        }

        PrintNews(news);
    }
    
    private static void PrintNews(News news)
    {
        Console.WriteLine($"Heading: {news.Heading}");
        Console.WriteLine($"Author: {news.Author.FirstName} {news.Author.LastName}");
        Console.WriteLine($"Title: {news.Title}");
        Console.WriteLine($"Tags: {string.Join(", ", news.Tags.Select(t => t.Title))}");
        Console.WriteLine($"Publication time: {news.PublicationTime}");
        Console.WriteLine($"Content: {news.Content}");
    }
    
    private static void PrintNews(IEnumerable<News> news)
    {
        foreach (var item in news)
        {
            PrintNews(item);
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}