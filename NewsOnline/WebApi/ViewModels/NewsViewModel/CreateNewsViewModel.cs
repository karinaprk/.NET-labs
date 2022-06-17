namespace WebApi.ViewModels.NewsViewModel;

public class CreateNewsViewModel
{
    public string AuthorName { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string HeadingName { get; set; }
    public IEnumerable<string> TagNames { get; set; }
}