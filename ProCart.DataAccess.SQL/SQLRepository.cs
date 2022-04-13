using ProCart.core.Contracts;
using ProCart.core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProCart.DataAccess.SQL
{
    public class SQLRepository<T> : IRepository<T> where T : BaseEntity
    {

        internal DataContext _context;
        internal DbSet<T> dbSet;

        public SQLRepository(DataContext context)
        {
            _context = context;
            dbSet = context.Set<T>();
        }
        public IQueryable<T> Collections()
        {
            return dbSet;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            var t = Find(id);
            if (_context.Entry(t).State == EntityState.Detached)
                dbSet.Attach(t);
            dbSet.Remove(t);
        }

        public T Find(string id)
        {
            return dbSet.Find(id);
        }

        public void Insert(T t)
        {
            dbSet.Add(t);
        }

        public void Update(T t)
        {
            dbSet.Attach(t);
            _context.Entry(t).State = EntityState.Modified;
        }
    }
}
