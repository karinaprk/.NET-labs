using DataAccess.Models;

namespace DataAccess.Repositories;

public class TagRepository : Repository<Tag>
{
    public TagRepository(NewsContext context) : base(context) { }

    public Tag GetByName(string name) =>
        DbSet.FirstOrDefault(t => t.Title.Equals(name));
}