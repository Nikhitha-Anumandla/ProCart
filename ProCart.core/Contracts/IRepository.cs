using ProCart.core.Models;
using System.Linq;

namespace ProCart.core.Constracts
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> Collections();
        void Commit();
        void Delete(string id);
        T Find(string id);
        void Insert(T t);
        void Update(T t);
    }
}