using Microsoft.EntityFrameworkCore;
using SportsStoreMVC.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStoreMVC.Data.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private ApplicationDbContext _context;
        private DbSet<Product> _products;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
            _products = context.Products;
        }
        public void Add(Product obj)
        {
            _products.Add(obj);
        }

        public void Delete(Product obj)
        {
            _products.Remove(obj);
        }

        public IEnumerable<Product> GetAll()
        {
            return _products.AsNoTracking().ToList();
        }

        public Product GetById(int id)
        {
            return _products.Include(p => p.Category).SingleOrDefault(p => p.ProductId == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
