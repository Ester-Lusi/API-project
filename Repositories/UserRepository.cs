using System.Linq;
using System.Text.Json;
using Entities;
using Foundatio.Utility;
using Microsoft.EntityFrameworkCore;


namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        ShopContext _dbContext;
        public UserRepository(ShopContext context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await Task.FromResult(_dbContext.Users);
        }
        public async Task<User> GetById(int id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public async Task<User> AddUser(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> FindUser(User user)
        {
            try
            {
                return await _dbContext.Users.FindAsync(user);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task UpdateUser(int id, User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }
        public void DeleteUser(int id)
        {

        }
    }
}
