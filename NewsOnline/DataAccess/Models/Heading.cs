namespace DataAccess.Models;

public class Heading
{
    public int Id { get; set; }
    public string Title { get; set; }

    public ICollection<News> News { get; set; }
}