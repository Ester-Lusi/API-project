using Dtos;
using Entities;

namespace Repositories
{
    public interface IUserRepository
    {
        Task<User> AddUser(User user);
        void DeleteUser(int id);
        Task<User> FindUser(User user);
        Task<User> GetById(int id);
        Task<IEnumerable<User>> GetUsers();
        void UpdateUser(int id, User user);
    }
}