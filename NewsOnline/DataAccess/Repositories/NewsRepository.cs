using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class NewsRepository : Repository<News>
{
    public NewsRepository(NewsContext context) : base(context) { }

    public override IEnumerable<News> GetAll()
        => DbSet
            .Include(n => n.Heading)
            .Include(n => n.Tags)
            .Include(n => n.Author)
            .ToList();
}