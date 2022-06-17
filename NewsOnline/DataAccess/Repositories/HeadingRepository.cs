using DataAccess.Models;

namespace DataAccess.Repositories;

public class HeadingRepository : Repository<Heading>
{
    public HeadingRepository(NewsContext context) : base(context) { }
    
    public Heading GetByName(string name) =>
        DbSet.FirstOrDefault(t => t.Title.Equals(name));
}