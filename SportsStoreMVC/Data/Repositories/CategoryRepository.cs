using Microsoft.EntityFrameworkCore;
using SportsStoreMVC.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStoreMVC.Data.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        private ApplicationDbContext _context;
        private DbSet<Category> _categories;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
            _categories = context.Categories;
        }
        public void Add(Category obj)
        {
            _categories.Add(obj);
        }

        public void Delete(Category obj)
        {
            _categories.Remove(obj);
        }

        public IEnumerable<Category> GetAll()
        {
            return _categories.AsNoTracking().ToList();
        }

        public Category GetById(int id)
        {
            return _categories.SingleOrDefault(c => c.CategoryId == id);
        }

        public Category GetByIdWithProducts(int id)
        {
            return _categories.Include(c => c.Products).SingleOrDefault(c => c.CategoryId == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
