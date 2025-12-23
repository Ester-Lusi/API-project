using Dtos;
using Entities;

namespace Services
{
    public interface IUserService
    {
        Task<UserDto> AddUser(UserDto user);
        void DeleteUser(int id);
        Task<User> FindUser(UserDto user);
        Task<UserDto> GetById(int id);
        Task<IEnumerable<UserDto>> GetUsers();
        Task<int> UpdateUser(int id, UserDto user);
    }
}