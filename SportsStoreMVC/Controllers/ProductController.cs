using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportsStoreMVC.Data.Repositories;
using SportsStoreMVC.Models.Domain;
using SportsStoreMVC.Models.ProductViewModels;

namespace SportsStoreMVC.Controllers
{
    public class ProductController : Controller
    {
        private IRepository<Product> _productRepository;
        private CategoryRepository _categoryRepository;

        public ProductController(IRepository<Product> productRepository, CategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        public IActionResult Index(int categoryId = 0)
        {
            ViewData["Categories"] = GetCategoriesSelectList(categoryId);
            var products = _productRepository.GetAll().OrderBy(p => p.Name);
            if (categoryId != 0)
            {
                products = _categoryRepository.GetByIdWithProducts(categoryId).Products.OrderBy(p => p.Name);
            }
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["IsEdit"] = false;
            ViewData["Categories"] = GetCategoriesSelectList();
            return View(viewName: "Edit");
        }

        [HttpPost]
        public IActionResult Create(EditViewModel editViewModel)
        {
            try
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
            }
            catch
            {
                TempData["error"] = "Something went wrong, product was not created";
            }
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
            ViewData["Categories"] = GetCategoriesSelectList(product.Category.CategoryId);
            return View(new EditViewModel(product));
        }

        [HttpPost]
        public IActionResult Edit(int id, EditViewModel editViewModel)
        {
            try
            {
                Product product = _productRepository.GetById(id);
                product.EditProduct(editViewModel, _categoryRepository.GetById(editViewModel.CategoryId));
                _productRepository.SaveChanges();
                TempData["message"] = $"You successfully updated product {product.Name}";
            }
            catch
            {
                TempData["error"] = "Something went wrong, product was not updated";
            }
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
        private SelectList GetCategoriesSelectList(int selected = 0)
        {
            return new SelectList(_categoryRepository.GetAll().OrderBy(c => c.Name), nameof(Category.CategoryId),nameof(Category.Name),selected);
        }
    }
}