using Entities;

namespace Repositories
{
    public interface IUserRepository
    {
        User AddUser(User user);
        void DeleteUser(int id);
        User FindUser(User user);
        IEnumerable<string> Get();
        User GetById(int id);
        void UpdateUser(int id, User user);
    }
}