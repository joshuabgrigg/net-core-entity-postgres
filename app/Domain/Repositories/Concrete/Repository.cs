using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public abstract class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppPostgresContext _context;

    public Repository(AppPostgresContext context)
    {
        this._context = context;
    }

    public T Add(T item)
    {
        this._context.Set<T>().Add(item);
        this._context.SaveChanges();

        return item;
    }
    public IEnumerable<T> FindAll(Func<T, bool> predicate)
    {
        return _context.Set<T>().AsNoTracking().Where(predicate).ToList();
    }
    public IEnumerable<T> FindAll()
    {
        return _context.Set<T>().AsNoTracking().ToList();
    }
    public void Remove(T item) {
        this._context.Remove(item);
        this._context.SaveChanges();
    }
    public T Update(T item)
    {
        this._context.Update(item);
        this._context.SaveChanges();

        return item;
    }
}