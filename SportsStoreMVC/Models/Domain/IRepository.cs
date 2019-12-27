using System.Collections.Generic;

namespace SportsStoreMVC.Models.Domain
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Add(T product);
        void Delete(T product);
        void SaveChanges();
    }
}
