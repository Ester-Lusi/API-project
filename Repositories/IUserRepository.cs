using Dtos;
using Entities;

namespace Repositories
{
    public interface IUserRepository
    {
        Task<User> AddUser(User user);
        void DeleteUser(int id);
        Task<User?> FindUser(LoginUser user);
        Task<User> GetById(int id);
        Task<IEnumerable<User>> GetUsers();
        Task UpdateUser(User user);
    }
}