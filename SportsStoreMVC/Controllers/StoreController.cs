using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStoreMVC.Data.Repositories;
using SportsStoreMVC.Models.Domain;

namespace SportsStoreMVC.Controllers
{
    public class StoreController : Controller
    {
        private IRepository<Product> _productRepository;
        private CategoryRepository _categoryRepository;

        public StoreController(IRepository<Product> productRepository, CategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        public IActionResult Index()
        {
            return View(_productRepository.GetAll());
        }
    }
}