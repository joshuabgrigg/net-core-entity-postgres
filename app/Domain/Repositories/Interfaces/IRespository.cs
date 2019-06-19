using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRepository<T> where T : class
{
    Task<T> Add(T item);
    Task Remove(T item);
    Task<T>  Update(T item);
    Task<IEnumerable<T>> FindAll();
}

public interface IPrimaryIDRepository<ID, Model> {
    Task<Model> Find(ID id);
    Task Remove(ID id);
}