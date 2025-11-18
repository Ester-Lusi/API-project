using UserEntities;
using UserRepositories;


namespace UserServices
{
    public class UserService
    {
        UserRepository UserRepository = new UserRepository();
        public IEnumerable<string> Get()
        {
            return UserRepository.Get();
        }
        public User GetById(int id)
        {
            return UserRepository.GetById(id);
        }
        public User AddUser(User user)
        {
            return UserRepository.AddUser(user);
        }
        public User FindUser(User user)
        {
            return UserRepository.FindUser(user);
        }
        public void UpdateUser(int id, User user)
        {
            UserRepository.UpdateUser(id, user);
        }
        public void DeleteUser(int id)
        {
            UserRepository.DeleteUser(id);
        }

    }
}
