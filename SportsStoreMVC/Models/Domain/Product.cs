﻿using System;
using System.Runtime.CompilerServices;

// Make SportsStore.tests a friendly assembly so it can access the internal properties of this class
[assembly: InternalsVisibleTo("SportsStore.Tests")]

namespace SportsStore.Models.Domain {
    public class Product {
        #region Fields
        private string _name;
        private decimal _price;
        private Category _category;
        #endregion

        #region Properties
        public int ProductId { get; internal set; }

        public string Name {
            get => _name;
            private set {
                if (string.IsNullOrWhiteSpace(value) || value.Length > 100 || value.Length < 5)
                    throw new ArgumentException("Category must have a name, and the name should contain between 5 and 100 characters");
                _name = value;
            }
        }

        public decimal Price {
            get => _price;
            private set {
                if (value < 1 || value > 3000)
                    throw new ArgumentException("Price must be in the range 1 - 3000");
                _price = value;
            }
        }

        public Category Category {
            get => _category;
            private set {
                _category = value ?? throw new ArgumentException("You must specify a category for a product");
            }
        }

        public string Description { get; private set; }
        public bool InStock { get; private set; }

        #endregion

        #region Constructors
        public Product(string name, decimal price, Category category, string description = null, bool inStock = true) {
            InStock = inStock;
            Name = name;
            Price = price;
            Description = description;
            Category = category;
            category.AddProduct(this);
        }

        // Added for EF because EF cannot set navigation properties through constructor parameters
        private Product() {
        }
        #endregion

        #region Methods
        public override bool Equals(object obj) {
            return obj is Product p && p.ProductId == ProductId;
        }

        public override int GetHashCode() {
            return HashCode.Combine(ProductId);
        }
        #endregion
    }
}