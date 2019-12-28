using SportsStoreMVC.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStoreMVC.Models.ProductViewModels
{
    public class EditViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool InStock { get; set; }
        public int CategoryId { get; set; }

        public EditViewModel()
        {
        }
        public EditViewModel(Product product)
        {
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
            InStock = product.InStock;
            CategoryId = product.Category?.CategoryId ?? 0;
        }


    }
}
