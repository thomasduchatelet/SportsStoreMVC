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

        public ProductController(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}