using Dtos;
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


namespace Repositories.TestProject
{
    public class UserRepositoyUnitTests
    {
        public class UserRepositoryUnitTests
        {
 
            private readonly shopContext _shopContext;
            private readonly IUserRepository _userRepository;

            public UserRepositoryUnitTests(DBFixture fixture)
            {
                _shopContext = fixture.Context;  // Use the context from the fixture
                _userRepository = new UserRepository(_shopContext);
            }

            [Fact]
            public async Task GetById_ReturnsUser_WhenUserExists()
            {
                // Arrange
                var user = new User { Id = 1, Email = "user1@test.com", Password = "password1" };
                await _userRepository.AddUser(user);

                // Act
                var result = await _userRepository.GetById(1);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(1, result.Id);
                Assert.Equal("user1@test.com", result.Email);
            }

            [Fact]
            public async Task GetById_ReturnsNull_WhenUserDoesNotExist()
            {
                // Act
                var result = await _userRepository.GetById(999); // ID that doesn't exist

                // Assert
                Assert.Null(result);
            }

            [Fact]
            public async Task AddUser_ReturnsUser_WhenUserIsAdded()
            {
                // Arrange
                var user = new User { Email = "newuser@test.com", Password = "newpassword" };

                // Act
                var result = await _userRepository.AddUser(user);

                // Assert
                Assert.NotNull(result);
                Assert.Equal("newuser@test.com", result.Email);
                Assert.Equal("newpassword", result.Password);
            }

            [Fact]
            public async Task FindUser_ReturnsUser_WhenCredentialsMatch()
            {
                // Arrange
                var user = new User { Email = "user1@test.com", Password = "password1" };
                await _userRepository.AddUser(user);

                var loginUser = new LoginUser { Email = "user1@test.com", Password = "password1" };

                // Act
                var result = await _userRepository.FindUser(loginUser);

                // Assert
                Assert.NotNull(result);
                Assert.Equal("user1@test.com", result.Email);
                Assert.Equal("password1", result.Password);
            }

            [Fact]
            public async Task FindUser_ReturnsNull_WhenCredentialsDoNotMatch()
            {
                // Arrange
                var user = new User { Email = "user1@test.com", Password = "password1" };
                await _userRepository.AddUser(user);

                var loginUser = new LoginUser { Email = "user1@test.com", Password = "wrongpassword" };

                // Act
                var result = await _userRepository.FindUser(loginUser);

                // Assert
                Assert.Null(result);
            }

            [Fact]
            public async Task UpdateUser_UpdatesUser_WhenUserExists()
            {
                // Arrange
                var user = new User { Id = 1, Email = "user1@test.com", Password = "password1" };
                await _userRepository.AddUser(user);

                user.Password = "newpassword";

                // Act
                await _userRepository.UpdateUser(user); // Ensure the correct method signature is used
                var updatedUser = await _userRepository.GetById(1);

                // Assert
                Assert.NotNull(updatedUser);
                Assert.Equal("newpassword", updatedUser.Password);
            }
        }
    }
}
