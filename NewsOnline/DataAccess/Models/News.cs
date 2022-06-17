namespace DataAccess.Models;

public class News
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime PublicationTime { get; set; }

    public User Author { get; set; }
    public Heading Heading { get; set; }
    
    public ICollection<Tag> Tags { get; set; }
}