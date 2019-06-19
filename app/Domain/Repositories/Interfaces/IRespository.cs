using System;
using System.Collections.Generic;

public interface IRepository<T> where T : class
{
    T Add(T item);
    void Remove(T item);
    T Update(T item);
    IEnumerable<T> FindAll();
}

public interface IPrimaryIDRepository<ID, Model> {
    Model Find(ID id);
    void Remove(ID id);
}