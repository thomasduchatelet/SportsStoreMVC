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
            _categories.Add()
        }

        public void Delete(Category obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetAll()
        {
            throw new NotImplementedException();
        }

        public Category GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
