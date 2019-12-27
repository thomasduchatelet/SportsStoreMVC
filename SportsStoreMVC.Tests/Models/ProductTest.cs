﻿using SportsStore.Models.Domain;
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

    }
}
