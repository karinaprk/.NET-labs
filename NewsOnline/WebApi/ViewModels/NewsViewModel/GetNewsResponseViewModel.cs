namespace WebApi.ViewModels.NewsViewModel;

public class GetNewsResponseViewModel
{
    public string Title { get; set; }
    public string AuthorName { get; set; }
    public string Content { get; set; }
    public DateTime PublicationTime { get; set; }
    public string HeadingName { get; set; }
    public IEnumerable<string> Tags { get; set; }
}