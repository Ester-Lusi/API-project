using Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject;

namespace Repository.TestProject
{
    public class CategoryRepositoryTests : IClassFixture<DBFixture>
    {
        private readonly CategoryRepository _categoryRepository;
        private readonly ShopContext _context;

        public CategoryRepositoryTests(DBFixture fixture)
        {
            _context = fixture.Context; // Use the context from the fixture
            _categoryRepository = new CategoryRepository(_context);
            SeedDatabase();
        }

        private void SeedDatabase()
        {
            // Seed initial data for the database
            _context.Categories.AddRange(new List<Category>
            {
                new Category { CategoryId = 1, CategoryName = "Category1" },
                new Category { CategoryId = 2, CategoryName = "Category2" }
            });
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetCategories_ReturnsAllCategories()
        {
            // Act
            var result = await _categoryRepository.GetCategory();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetCategories_ReturnsEmpty_WhenNoCategoriesExist()
        {
            // Arrange: Clear the categories
            _context.Categories.RemoveRange(_context.Categories);
            await _context.SaveChangesAsync();

            // Act
            var result = await _categoryRepository.GetCategory();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result); // Ensure the result is empty
        }
    }
}

