using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace com.ai.ext.validataTest.Models
{
    public interface IRepository<T> where T : class
    {
        //Expression<> used for LINQ: Now the 'IQueryable' version of the 'Where' method will be used.
        //If not: Now the 'IEnumerable' version of the 'Where' method will be used. Here all table data is gathered
        IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate = null);
        T Get(Expression<Func<T, bool>> predicate);
        void Save(T item, ref List<T> updateItemList);
        void Attach(T oldItem, T newItem, ref List<T> updateItemList);
        bool Remove(T item, ref List<T> updateItemList);
    }
}
