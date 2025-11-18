using Entities;

namespace Services
{
    public interface IUserService
    {
        User AddUser(User user);
        void DeleteUser(int id);
        User FindUser(User user);
        IEnumerable<string> Get();
        User GetById(int id);
        int UpdateUser(int id, User user);
    }
}