using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using WebApi.ViewModels.TagViewModels;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TagController : ControllerBase
{
    private readonly TagService _tagService;
    
    public TagController(TagService tagService)
    {
        _tagService = tagService;
    }
    
    [HttpPost("/CreateTag")]
    public IActionResult CreateTag(CreateTagViewModel model)
    {
        if (string.IsNullOrEmpty(model.Name))
            return BadRequest("Field is empty");
        
        _tagService.CreateTag(model.Name);
        return Ok(model.Name);
    }
}