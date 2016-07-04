using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.ai.ext.validataTest.Models
{
    public class UoW : IDisposable
    {
        private PhonebookBinFileContext context = null;        

        public UoW()
        {
            context = new PhonebookBinFileContext();
        }

        public Dictionary<Type, object> repos = new Dictionary<Type, object>();

        public IRepository<T> Repository<T>() where T : class
        {
            if (repos.Keys.Contains(typeof(T)) == true)
            {
                return repos[typeof(T)] as IRepository<T>;
            }

            IRepository<T> repo = new GenericRepository<T>(context);
            repos.Add(typeof(T), repo);
            return repo;
        }

        public void SaveChanges<T>(List<T> _items)
        {            
            context.SaveChanges(_items);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context = null;                    
                    //context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}