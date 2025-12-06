using Entities;

namespace Repositories
{
    public interface IUserRepository
    {
        Task<User> AddUser(User user);
        void DeleteUser(int id);
        Task<User> FindUser(User user);
        Task<User> GetById(int id);
        Task UpdateUser(int id, User user);
        Task<IEnumerable<User>> GetUsers();
    }
}