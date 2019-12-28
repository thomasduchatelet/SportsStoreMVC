using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportsStoreMVC.Models.Domain;
using SportsStoreMVC.Models.ProductViewModels;

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

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["IsEdit"] = false;
            ViewData["Categories"] = GetCategoiesSelectList();
            return View(viewName: "Edit");
        }

        [HttpPost]
        public IActionResult Create(EditViewModel editViewModel)
        {
            Product product = new Product
                (
                    editViewModel.Name,
                    editViewModel.Price,
                    _categoryRepository.GetById(editViewModel.CategoryId),
                    editViewModel.Description,
                    editViewModel.InStock
                );
            _productRepository.Add(product);
            _productRepository.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Product product = _productRepository.GetById(id);
            if (product is null)
                return NotFound();
            ViewData["IsEdit"] = true;
            ViewData["Categories"] = GetCategoiesSelectList(product.Category.CategoryId);
            return View(new EditViewModel(product));
        }

        [HttpPost]
        public IActionResult Edit(int id, EditViewModel editViewModel)
        {
            Product product = _productRepository.GetById(id);
            product.EditProduct(editViewModel, _categoryRepository.GetById(editViewModel.CategoryId));
            _productRepository.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            Product product = _productRepository.GetById(id);
            return View(product);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Product product = _productRepository.GetById(id);
            _productRepository.Delete(product);
            _productRepository.SaveChanges();
            return RedirectToAction("Index");
        }
        private SelectList GetCategoiesSelectList(int selected = 0)
        {
            return new SelectList(_categoryRepository.GetAll().OrderBy(c => c.Name), nameof(Category.CategoryId),nameof(Category.Name),selected);
        }
    }
}