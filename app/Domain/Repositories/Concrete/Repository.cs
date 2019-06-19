using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions;

public abstract class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppPostgresContext _context;

    public Repository(AppPostgresContext context)
    {
        this._context = context;
    }

    public async Task<T> Add(T item)
    {
        await this._context.Set<T>().AddAsync(item);
        await this._context.SaveChangesAsync();

        return item;
    }
    protected async Task<IEnumerable<T>> FindAll(Func<T, bool> predicate)
    {
        var messages = _context.Set<T>().AsQueryable().Where(predicate).ToAsyncEnumerable();
        return await messages.ToList();
    }
    public async Task<IEnumerable<T>> FindAll()
    {
        return await _context.Set<T>().ToListAsync();
    }
    public async Task Remove(T item) {
        this._context.Remove(item);
        await this._context.SaveChangesAsync();
    }

    public async Task<T> Update(T item)
    {
        this._context.Update(item);
        await this._context.SaveChangesAsync();

        return item;
    }
}