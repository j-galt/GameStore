﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GameStore.BLL.Interfaces
{
    public interface IRepository<T> where T : class
    {       
        T Get(int id);
        IEnumerable<T> GetWithIncludes(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includes);

        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        T SingleOrDefault(Expression<Func<T, bool>> predicate);

        void Add(T entity);
        void AddRange(IEnumerable<T> entities);

        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
