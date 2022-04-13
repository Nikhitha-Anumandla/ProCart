using ProCart.core.Contracts;
using ProCart.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProCart.WebUI.Tests.Mocks
{
    public class MockContext<T>: IRepository<T> where T:BaseEntity
    {
        List<T> items;
        string className;
        public MockContext()
        {
            items = new List<T>();
        }

        public void Commit()
        {
            return;
        }

        public void Insert(T t)
        {
            items.Add(t);
        }

        public void Update(T t)
        {
            var updateT = items.Find(c => c.id == t.id);
            if (updateT == null)
                throw new Exception(className + " not found!");
            updateT = t;
        }

        public T Find(string id)
        {
            var item = items.Find(c => c.id == id);
            if (item == null)
                throw new Exception(className + " not found!");
            return item;
        }

        public void Delete(string id)
        {
            var item = items.Find(c => c.id == id);
            if (item == null)
                throw new Exception(className + " not found!");
            items.Remove(item);
        }

        public IQueryable<T> Collections()
        {
            return items.AsQueryable();
        }
    }
}
