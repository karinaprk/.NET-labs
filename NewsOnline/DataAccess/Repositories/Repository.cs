using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public abstract class Repository<T> : IRepository<T> where T: class
{
    private readonly NewsContext _context;
    protected readonly DbSet<T> DbSet;

    public Repository(NewsContext context)
    {
        _context = context;
        DbSet = context.Set<T>();
    }

    public virtual IEnumerable<T> GetAll() => DbSet.AsNoTracking().ToList();

    public T Get(int id) => DbSet.Find(id);

    public void Create(T item)
    {
        DbSet.Add(item);
    }

    public void Update(T item)
    {
        _context.Entry(item).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
        DbSet.Remove(Get(id));
    }
}