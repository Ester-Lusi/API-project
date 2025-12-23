using Dtos;
using Entities;

namespace Services
{
    public interface IUserService
    {
        Task<UserDto> AddUser(User user);
        void DeleteUser(int id);
        Task<User> FindUser(User user);
        Task<UserDto> GetById(int id);
        Task<IEnumerable<UserDto>> GetUsers();
        Task<int> UpdateUser(int id, User user);
    }
}