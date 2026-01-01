using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class ProductsRepositoryIntegrationTests: IDisposable
    {

        private readonly DBFixture _fixture;
        private readonly shopContext _dbContext;
        private readonly ProductRepository _productRepository;

        public ProductsRepositoryIntegrationTests()
        {
            _fixture = new DBFixture();
            _dbContext = _fixture.Context;
            _productRepository = new ProductRepository(_dbContext);
        }
        public void Dispose()
        {
            _fixture.Dispose();
        }

        [Fact]
        public async Task GetProducts_WhenProductsExist_ReturnsAllProductsWithCategory()
        {
            // Arrange
            var category = new Category { CategoryName = "Electronics" };
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            var testProducts = new List<Product>
            {
                new Product { ProductName = "Laptop", CategoryId = category.CategoryId, Price = 3500 },
                new Product { ProductName = "Mouse", CategoryId = category.CategoryId, Price = 150 }
            };

            await _dbContext.Products.AddRangeAsync(testProducts);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _productRepository.GetProducts(
                position: 1,
                skip: 10,
                name: null,
                description: null,
                categories: null,
                minPrice: null,
                maxPrice: null,
                orderBy: null
            );

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Items.Count());
            Assert.All(result.Items, p => Assert.NotNull(p.Categoty));
            Assert.Contains(result.Items, p => p.ProductName == "Laptop" && p.Categoty.CategoryName == "Electronics");
        }

        [Fact]
        public async Task GetProducts_WhenNoProductsExist_ReturnsEmptyList()
        {
            // Act
            var result = await _productRepository.GetProducts(
                position: 1,
                skip: 10,
                name: null,
                description: null,
                categories: null,
                minPrice: null,
                maxPrice: null,
                orderBy: null
            );

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result.Items);
        }
    }
}