using DataAccess.Repositories;

namespace DataAccess;

public class UnitOfWork : IDisposable
{
    private readonly NewsContext _context;

    public UserRepository UserRepository { get; set; }
    public TagRepository TagRepository { get; set; }
    public NewsRepository NewsRepository { get; set; }
    public HeadingRepository HeadingRepository { get; set; }

    public UnitOfWork (NewsContext context)
    {
        _context = context;
        UserRepository = new UserRepository(_context);
        TagRepository = new TagRepository(_context);
        NewsRepository = new NewsRepository(_context);
        HeadingRepository = new HeadingRepository(_context);
    }

    public void SaveChanges() => _context.SaveChanges();
    
    public void Dispose() => _context.Dispose();
}