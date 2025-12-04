using Repositories;
using Entities;

namespace Services
{
    public class UserService : IUserService
    {
        IUserRepository _iUserRepository;
        IPasswordService _iPasswordService;
        public UserService(IUserRepository iUserRepository, IPasswordService iPasswordService)
        {
            _iUserRepository = iUserRepository;
            _iPasswordService = iPasswordService;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _iUserRepository.GetUsers();
        }
        public async Task<User> GetById(int id)
        {
            return await _iUserRepository.GetById(id);
        }
        public async Task<User> AddUser(User user)
        {
            if (_iPasswordService.CheckStrength(user.Password).Strength < 2)
                return null;
            return await _iUserRepository.AddUser(user);
        }
        public async Task<User> FindUser(User user)
        {
            return await _iUserRepository.FindUser(user);
        }
        public async Task<int> UpdateUser(int id, User user)
        {
            var result = Zxcvbn.Core.EvaluatePassword(user.Password);
            if (result.Score > 2)
                _iUserRepository.UpdateUser(id, user);
            return result.Score;
        }
        public void DeleteUser(int id)
        {
            _iUserRepository.DeleteUser(id);
        }

    }
}
