using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class UserRepository : Repository<User>
{
    public UserRepository(NewsContext context) : base(context) { }

    public User GetByUserName(string userName) =>
        DbSet
            .Include(u => u.News)
                .ThenInclude(n => n.Heading)
            .Include(u => u.News)
                .ThenInclude(n => n.Tags)
            .FirstOrDefault(u => u.UserName.Equals(userName));
}