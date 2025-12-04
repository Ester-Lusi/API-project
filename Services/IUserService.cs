using Entities;

namespace Services
{
    public interface IUserService
    {
        Task<User> AddUser(User user);
        void DeleteUser(int id);
        Task<User> FindUser(User user);
        Task<User> GetById(int id);
        Task<int> UpdateUser(int id, User user);
        Task<IEnumerable<User>> GetUsers();
    }
}