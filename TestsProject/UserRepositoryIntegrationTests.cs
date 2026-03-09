using Dtos;
using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class UserRepositoryIntegrationTests : IClassFixture<DBFixture>
    {
            private readonly ShopContext _context;
            private readonly UserRepository _userRepository;

            public UserRepositoryIntegrationTests(DBFixture fixture)
            {
                _context = fixture.Context; // Use the context from the fixture
                _userRepository = new UserRepository(_context);
            }

            // Test for AddUser method
            [Fact]
            public async Task AddUser_ShouldAddUser()
            {
                // Arrange
                var user = new User { Email = "testuser@test.com", Password = "password123" };

                // Act
                var addedUser = await _userRepository.AddUser(user);

                // Assert
                Assert.NotNull(addedUser);
                Assert.Equal("testuser@test.com", addedUser.Email);
                Assert.Equal("password123", addedUser.Password);

                // Verify user is added to the database
                var dbUser = await _context.Users.FindAsync(addedUser.Id);
                Assert.NotNull(dbUser);
                Assert.Equal("testuser@test.com", dbUser.Email);
            }

            // Test for GetById method
            [Fact]
            public async Task GetById_ShouldReturnUser_WhenUserExists()
            {
                // Arrange
                var user = new User { Email = "user1@test.com", Password = "password1" };
                await _userRepository.AddUser(user); // Add user to DB

                // Act
                var result = await _userRepository.GetById(user.Id);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(user.Id, result.Id);
                Assert.Equal(user.Email, result.Email);
            }

            // Test for GetById method when user does not exist
            [Fact]
            public async Task GetById_ShouldReturnNull_WhenUserDoesNotExist()
            {
                // Act
                var result = await _userRepository.GetById(999); // ID that does not exist

                // Assert
                Assert.Null(result);
            }

            // Test for FindUser method (successful login)
            [Fact]
            public async Task FindUser_ShouldReturnUser_WhenCredentialsMatch()
            {
                // Arrange
                var user = new User { Email = "user1@test.com", Password = "password1" };
                await _userRepository.AddUser(user); // Add user to DB

                var loginUser = new LoginUser ("user1@test.com", "password1" );

                // Act
                var result = await _userRepository.FindUser(loginUser);

                // Assert
                Assert.NotNull(result);
                Assert.Equal("user1@test.com", result.Email);
                Assert.Equal("password1", result.Password);
            }

            // Test for FindUser method (unsuccessful login)
            [Fact]
            public async Task FindUser_ShouldReturnNull_WhenCredentialsDoNotMatch()
            {
                // Arrange
                var user = new User { Email = "user1@test.com", Password = "password1" };
                await _userRepository.AddUser(user); // Add user to DB

                var loginUser = new LoginUser("user1@test.com","wrongpassword" );

                // Act
                var result = await _userRepository.FindUser(loginUser);

                // Assert
                Assert.Null(result);
            }

            // Test for UpdateUser method
            [Fact]
            public async Task UpdateUser_ShouldUpdateUser()
            {
                // Arrange
                var user = new User { Email = "user2@test.com", Password = "oldpassword" };
                await _userRepository.AddUser(user); // Add user to DB

                user.Password = "newpassword"; // Change password

                // Act
                await _userRepository.UpdateUser(user);

                // Assert
                var updatedUser = await _userRepository.GetById(user.Id);
                Assert.NotNull(updatedUser);
                Assert.Equal("newpassword", updatedUser.Password);
            }
        }
}
