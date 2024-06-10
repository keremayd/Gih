using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories;

public class RepositoryBase<T>: IRepositoryBase<T> where T: class
{
    protected readonly ApplicationDbContext _context;

    public RepositoryBase(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<T> FindAll()
    {
        return  _context.Set<T>();
    }
    
    public IQueryable<T> FindAllSortByScore()
    {
       return _context.Set<T>().OrderByDescending(x => EF.Property<int>(x, "Score"));
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
    {
        return  _context.Set<T>().Where(expression);
    }

    public void Create(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }
}