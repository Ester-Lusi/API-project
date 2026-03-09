using Dtos;
using Entities;
using Foundatio.Utility;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text.Json;



namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ShopContext _dbContext;
        public UserRepository(ShopContext context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return _dbContext.Users;
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

        public async Task<User?> FindUser(LoginUser user)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == user.Email && x.Password == user.Password);
        }

        public async Task UpdateUser(User user)
        {
            _dbContext.Users.Update(user);
            _dbContext.SaveChangesAsync();
        }
        public void DeleteUser(int id)
        {

        }

    }
}
