using Repositories;
using Entities;

namespace Services
{
    public class UserService : IUserService
    {
        IUserRepository _iUserRepository;
        PasswordService _iPasswordService;
        public UserService(IUserRepository iUserRepository, PasswordService iPasswordService)
        {
            _iUserRepository = iUserRepository;
            _iPasswordService = iPasswordService;
        }

        public IEnumerable<string> Get()
        {
            return _iUserRepository.Get();
        }
        public User GetById(int id)
        {
            return _iUserRepository.GetById(id);
        }
        public User AddUser(User user)
        {
            if (_iPasswordService.CheckStrength(user.Password).Strength < 2)
                return null;
            return _iUserRepository.AddUser(user);
        }
        public User FindUser(User user)
        {
            return _iUserRepository.FindUser(user);
        }
        public int UpdateUser(int id, User user)
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
