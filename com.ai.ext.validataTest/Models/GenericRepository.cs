using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;

namespace com.ai.ext.validataTest.Models
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private PhonebookBinFileContext context = null;
        public List<T> items = null;

        public GenericRepository(PhonebookBinFileContext _context)
        {
            context = _context;            
            items = context.Items<T>();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate = null)
        {
            if(predicate != null)
            {
                return items.AsQueryable().Where(predicate);
            }
            return items.AsEnumerable();
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            return items.AsQueryable().First(predicate);
        }

        /*
         * MyClass a = new MyClass(); //sample for finding; IEquatable<MyClass>
         * List<MyClass> list = GetInstances();
         * MyClass found = list.Find( mc => mc.Equals(a) );
         */
        public void Save(T item, ref List<T> updateItemList)
        {
            ////Get propertyName which contains ID for generic model
            //PropertyInfo _propertyID = (from prop in typeof(T).GetProperties()
            //                        where prop.Name.Contains("ID")
            //                        select prop
            //                        ).First();
            //if (_propertyID != null)
            //{
            //    //If ID value is not null means record should be updated
            //    var propIDValue = _propertyID.GetValue(_propertyID, null);
            //    if (propIDValue != null || propIDValue.ToString().Length > 0)
            //    {
            //        var _itemIndex = items.IndexOf(item);//(T)Activator.CreateInstance(typeof(T), new object {});
            //        if(_itemIndex == 1)
            //        {

            //        }
            //    }
            //}
            items.Add(item);
            updateItemList = items;
        }

        public void Attach(T oldItem, T newItem, ref List<T> updateItemList)
        {
            try
            {
                items.Remove(oldItem);
                items.Add(newItem);
                updateItemList = items;
            }
            catch(Exception ex)
            {
                //LOG ex
            }
        }

        public bool Remove(T item, ref List<T> updateItemList)
        {
            try
            {
                items.Remove(item);
                updateItemList = items;
                return true;
            }
            catch(Exception ex)
            {
                //LOG ex
                return false;
            }
        }

    }
}