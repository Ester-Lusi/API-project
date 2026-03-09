using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject;

namespace Repositories.TestProject
{
    public class ProductsRepositoryUnitTests : IClassFixture<DBFixture>
    {
        private readonly ShopContext _context;

        public ProductsRepositoryUnitTests(DBFixture fixture)
        {
            _context = fixture.Context;  // Use the context from the DBFixture
            SeedDatabase();
        }

        private void SeedDatabase()
        {
            // Seed initial data for the database
            _context.Categories.AddRange(
                new Category { CategoryId = 1, CategoryName = "Cat1" },
                new Category { CategoryId = 2, CategoryName = "Cat2" }
            );

            _context.Products.AddRange(
                new Product { ProductId = 1, ProductName = "Apple", Price = 10, CategoryId = 1, Description = "Red" },
                new Product { ProductId = 2, ProductName = "Banana", Price = 5, CategoryId = 1, Description = "Yellow" },
                new Product { ProductId = 3, ProductName = "Orange", Price = 8, CategoryId = 2, Description = "Orange" }
            );

            _context.SaveChanges();
        }

        [Fact]
        public async Task GetProducts_NoFilters_ReturnsAll()
        {
            var repo = new ProductRepository(_context);

            var result = await repo.GetProducts(
                null, null, null, null,
                position: 1, skip: 0,
                orderBy: null, description: null
            );

            Assert.Equal(3, result.TotalCount);
            Assert.Equal(3, result.Items.Count());
        }

        [Fact]
        public async Task GetProducts_FilterByName_ReturnsMatching()
        {
            var repo = new ProductRepository(_context);

            var result = await repo.GetProducts(
                name: "App",
                categories: null,
                minPrice: null,
                maxPrice: null,
                position: null,
                skip: 0,
                orderBy: null,
                description: null
            );

            Assert.Single(result.Items);
            Assert.Equal("Apple", result.Items.First().ProductName);
        }

        [Fact]
        public async Task GetProducts_FilterByCategory_ReturnsCorrectItems()
        {
            var repo = new ProductRepository(_context);

            var result = await repo.GetProducts(
                name: null,
                categories: new int?[] { 1 },
                minPrice: null,
                maxPrice: null,
                position: null,
                skip: 0,
                orderBy: null,
                description: null
            );

            Assert.Equal(2, result.TotalCount);
            Assert.All(result.Items, p => Assert.Equal(1, p.CategoryId));
        }

        [Fact]
        public async Task GetProducts_OrderByPriceAsc_Works()
        {
            var repo = new ProductRepository(_context);

            var result = await repo.GetProducts(
                null, null, null, null,
                position: 1, skip: 0,
                orderBy: "price_asc", description: null
            );

            var prices = result.Items.Select(p => p.Price).ToList();
            Assert.Equal(prices.OrderBy(p => p), prices);
        }

        [Fact]
        public async Task GetProducts_Pagination_Works()
        {
            var repo = new ProductRepository(_context);

            var result = await repo.GetProducts(
                null, null, null, null,
                position: 1, skip: 0,
                orderBy: "price_asc", description: null
            );

            Assert.Equal(3, result.TotalCount);
            Assert.Single(result.Items);
        }
    }
}
