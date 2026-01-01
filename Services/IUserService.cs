using Dtos;

namespace Services
{
    public interface IUserService
    {
        Task<UserDto> AddUser(UserDto user, string Password);
        void DeleteUser(int id);
        Task<UserDto> FindUser(LoginUser user);
        Task<UserDto> GetById(int id);
        Task<IEnumerable<UserDto>> GetUsers();
        Task<bool> UpdateUser(int id, UserDto user, string Password);
    }
}