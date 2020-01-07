﻿using SportsStoreMVC.Models.Domain;
using SportsStoreMVC.Models.Domain.Enums;
using SportsStoreMVC.Models.ProductViewModels;
using System;
using Xunit;

namespace SportsStore.Tests.Models {
    public class ProductTest {
        private readonly Category _category;

        public ProductTest() {
            _category = new Category("Soccer");
        }

        #region Constructor
        [Fact]
        public void NewProduct_ValidAndOptionalData_CreatesProduct() {
            var product = new Product("Football", 10, _category);
            Assert.Equal("Football", product.Name);
            Assert.Equal(10, product.Price);
            Assert.Equal(_category, product.Category);
            Assert.Null(product.Description);
            Assert.True(product.InStock);
        }

        [Fact]
        public void NewProduct_ValidNoOptionalData_CreatesProduct() {
            var product = new Product("Football", 10, _category, "WK colors", false);
            Assert.Equal("Football", product.Name);
            Assert.Equal(10, product.Price);
            Assert.Equal(_category, product.Category);
            Assert.Equal("WK colors", product.Description);
            Assert.False(product.InStock);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890")]
        public void NewProduct_InvalidName_ThrowsArgumentException(string name) {
            Assert.Throws<ArgumentException>(() => new Product(name, 10, _category));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        [InlineData(3020)]
        public void NewProduct_InvalidPrice_ThrowsArgumentException(int price) {
            Assert.Throws<ArgumentException>(() => new Product("Football", price, _category));
        }

        [Fact]
        public void NewProduct_InvalidCategory_ThrowsArgumentException() {
            Assert.Throws<ArgumentException>(() => new Product("Football", 10, null));
        }

        #endregion

        #region Methods
        [Fact]
        public void EditProduct_ValidEdit_ChangesTheProduct() {
            var product = new Product("Football", 10, _category);
            var category = new Category("NewCategory");
            Product newProduct = new Product("NewName", 20, category, "NewDescription", false, (int)Availability.ShopAndOnline);
            product.EditProduct(new EditViewModel(newProduct),category);
            Assert.Equal("NewName", product.Name);
            Assert.Equal(20, product.Price);
            Assert.Equal(category, product.Category);
            Assert.Equal("NewDescription", product.Description);
            Assert.Equal(Availability.ShopAndOnline, product.Availability);
            Assert.False(product.InStock);
        }

        [Fact]
        public void EditProduct_InValidEdit_ThrowsArgumentException() {
            var product = new Product("Football", 10, _category);
            Assert.Throws<ArgumentException>(() => product.EditProduct(new EditViewModel(new Product("NewName", 20, null, "NewDescription", false, (int)Availability.ShopAndOnline)), _category));
        }
        #endregion
    }
}
