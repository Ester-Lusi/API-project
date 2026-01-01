using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject;

namespace Repository.TestProject
{
    public class CategoryRepositoryIntegrationTests
    {
        private readonly DBFixture _fixture;
        private readonly shopContext _dbContext;
        private readonly CategoryRepository _categoryRepository;

        public CategoryRepositoryIntegrationTests()
        {
            _fixture = new DBFixture();
            _dbContext = _fixture.Context;
            _categoryRepository = new CategoryRepository(_dbContext);
        }
        public void Dispose()
        {
            _fixture.Dispose();
        }

        [Fact]
        public async Task GetCategories_ReturnsAllCategories()
        {
            // Arrange
            var category1 = new Category { /*CategoryId = 1,*/ CategoryName = "Category1" };
            var category2 = new Category { /*CategoryId = 2,*/ CategoryName = "Category2" };

            await _dbContext.Categories.AddAsync(category1);
            await _dbContext.Categories.AddAsync(category2);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _categoryRepository.GetCategory();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetCategories_ReturnsEmpty_WhenNoCategoriesExist()
        {
            // Act
            var result = await _categoryRepository.GetCategory();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result); // Ensure the result is empty
        }
    }
}

