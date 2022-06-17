using BusinessLogic.Services;
using ConsoleUI.Printers;

namespace ConsoleUI.Controllers;

public class TagController
{
    private readonly TagService _tagService;
    
    public TagController(TagService tagService)
    {
        _tagService = tagService;
    }
    
    public void CreateTag(string name)
    {
        if (name is null) Console.WriteLine(CommonPrinter.FillEmptyLines);
        
        _tagService.CreateTag(name);
    }
}