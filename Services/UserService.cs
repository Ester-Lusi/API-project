using Repositories;
using Entities;

namespace Services
{
    public class UserService : IUserService
    {
        IUserRepository _userRepository;
        IPasswordService _passwordService;
        public UserService(IUserRepository iUserRepository, IPasswordService iPasswordService)
        {
            _userRepository = iUserRepository;
            _passwordService = iPasswordService;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }
        public async Task<User> GetById(int id)
        {
            return await _userRepository.GetById(id);
        }
        public async Task<User> AddUser(User user)
        {
            if (_passwordService.CheckStrength(user.Password).Strength < 2)
                return null;
            return await _userRepository.AddUser(user);
        }
        public async Task<User> FindUser(User user)
        {
            return await _userRepository.FindUser(user);
        }
        public async Task<int> UpdateUser(int id, User user)
        {
            var result = Zxcvbn.Core.EvaluatePassword(user.Password);
            if (result.Score >= 2)
                await _userRepository.UpdateUser(id, user);
            return result.Score;
        }
        public void DeleteUser(int id)
        {
            _userRepository.DeleteUser(id);
        }

    }
}
