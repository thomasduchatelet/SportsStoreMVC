using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStoreMVC.Models.Domain;

namespace SportsStoreMVC.Controllers
{
    public class ProductController : Controller
    {
        private IRepository<Product> _productRepository;
        private IRepository<Category> _categoryRepository;

        private IEnumerable<Product> Products => _productRepository.GetAll().OrderBy(p => p.Name);

        public ProductController(IRepository<Product> productRepository, IRepository<Category> categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        public IActionResult Index()
        {
            return View(Products);
        }

        public IActionResult Create()
        {
            return View(viewName:"Index", Products);
        }
        public IActionResult Edit()
        {
            return View(viewName: "Index", Products);
        }
        public IActionResult Delete()
        {
            return View(viewName: "Index", Products);
        }
    }
}