using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Threading.Tasks;
using TestProject;
using Xunit;

namespace Repositories.Tests
{
    public class RatingRepositoryIntegrationTests : IClassFixture<DBFixture>
    {
        private readonly ShopContext _context;
        private readonly RatingRepository _ratingRepository;

        public RatingRepositoryIntegrationTests(DBFixture fixture)
        {
            _context = fixture.Context;
            _ratingRepository = new RatingRepository(_context);
        }

        [Fact]
        public async Task AddRating_ShouldAddRatingToDatabase()
        {
            // Arrange
            var rating = new Rating
            {
                Host = "localhost",
                Method = "POST",
                Path = "/api/orders",
                Referer = "integration-test",
                UserAgent = "test-agent",
                RecordDate = DateTime.Now
            };

            // Act
            var result = await _ratingRepository.AddRating(rating);

            // Assert
            Assert.NotNull(result);

            var ratingFromDb = await _context.Ratings
                .FirstOrDefaultAsync(r => r.RatingId == result.RatingId);

            Assert.NotNull(ratingFromDb);
            Assert.Equal("localhost", ratingFromDb.Host);
            Assert.Equal("POST", ratingFromDb.Method);
            Assert.Equal("/api/orders", ratingFromDb.Path);
            Assert.Equal("integration-test", ratingFromDb.Referer);
            Assert.Equal("test-agent", ratingFromDb.UserAgent);
        }
    }
}