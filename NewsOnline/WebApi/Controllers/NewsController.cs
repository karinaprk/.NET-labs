using BusinessLogic.Services;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.ViewModels.NewsViewModel;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class NewsController : ControllerBase
{
     private readonly NewsService _newsService;
    
    public NewsController(NewsService newsService)
    {
        _newsService = newsService;
    }
    
    [HttpPost("/CreateNews")]
    public IActionResult CreateNews(CreateNewsViewModel model)
    {
        if (string.IsNullOrEmpty(model.Title)
            || string.IsNullOrEmpty(model.Content)
            || string.IsNullOrEmpty(model.HeadingName)
            || !model.TagNames.Any())
            return BadRequest("Fields are empty");

        try
        {
            _newsService.CreateNews(model.AuthorName, model.Title, model.Content, model.HeadingName, model.TagNames);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }

        return Ok(model);
    }
    
    [HttpGet("/GetByTitle")]
    public IActionResult GetByTitle(string title)
    {
        if (string.IsNullOrEmpty(title))
            return BadRequest("Field is empty");
    
        var news = _newsService.GetByTitle(title);
        if (news is null || !news.Any())
        {
            return Ok("No news with this title");
        }

        var response = news.Select(CreateResponse).ToList();

        return Ok(response);
    }
    
    [HttpGet("/GetByUserName")]
    public IActionResult GetByUserName(string userName)
    {
        if (string.IsNullOrEmpty(userName))
            return BadRequest("Field is empty");
    
        var news = _newsService.GetByUserName(userName);
        if (news is null || !news.Any())
        {
            return Ok("No news by this author");
        }

        var response = news.Select(CreateResponse).ToList();
        
        return Ok(response);
    }
    
    [HttpGet("/GetByHeading")]
    public IActionResult GetByHeading(string headingName)
    {
        if (string.IsNullOrEmpty(headingName))
            return BadRequest("Field is empty");
    
        var news = _newsService.GetByHeading(headingName);
        if (news is null || !news.Any())
        {
            return Ok("No news with this heading");
        }
    
        var response = news.Select(CreateResponse).ToList();
        
        return Ok(response);
    }
    
    [HttpGet("/GetByPeriod")]
    public IActionResult GetByPeriod(GetNewsByPeriodViewModel model)
    {
        var news = _newsService.GetByPeriod(model.StartTime, model.EndTime);
        if (news is null || !news.Any())
        {
            return Ok("No news in this period of time");
        }
    
        var response = news.Select(CreateResponse).ToList();
        
        return Ok(response);
    }

    private static GetNewsResponseViewModel CreateResponse(News news)
    {
        var response = new GetNewsResponseViewModel
        {
            Title = news.Title,
            AuthorName = $"{news.Author.FirstName} {news.Author.LastName}",
            Content = news.Content,
            PublicationTime = news.PublicationTime,
            HeadingName = news.Heading.Title,
            Tags = news.Tags.Select(i => i.Title)
        };

        return response;
    }
}