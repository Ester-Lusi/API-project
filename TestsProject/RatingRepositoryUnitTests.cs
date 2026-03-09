using Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Repositories.Tests
{
    public class RatingRepositoryUnitTests
    {
        [Fact]
        public async Task AddRating_ShouldAddRatingAndSaveChanges()
        {
            // Arrange
            var rating = new Rating
            {
                RatingId = 1,
                Host = "localhost",
                Method = "GET",
                Path = "/api/products",
                Referer = "test",
                UserAgent = "unit-test",
                RecordDate = DateTime.Now
            };

            var mockSet = new Mock<DbSet<Rating>>();

            var mockContext = new Mock<ShopContext>();
            mockContext.Setup(m => m.Ratings).Returns(mockSet.Object);
            mockContext.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
                       .ReturnsAsync(1);

            var repository = new RatingRepository(mockContext.Object);

            // Act
            var result = await repository.AddRating(rating);

            // Assert
            mockSet.Verify(m => m.AddAsync(rating, It.IsAny<CancellationToken>()), Times.Once);
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

            Assert.Equal(rating, result);
        }
    }
}