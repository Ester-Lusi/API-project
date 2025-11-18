using System.Text.Json;
using UserEntities;

namespace UserRepositories
{
    public class UserRepository
    {
        string _filePath = "users.txt";
        public IEnumerable<string> Get()
        {
            return new string[] { "User1", "User2"};
        }
        public User GetById(int id)
        {
            using (StreamReader reader = System.IO.File.OpenText(_filePath))
            {
                string? currentUserInFile;
                while ((currentUserInFile = reader.ReadLine()) != null)
                {
                    User? user = JsonSerializer.Deserialize<User>(currentUserInFile);
                    if (user.Id == id)
                        return user;
                }
            }
            return null;
        }

        public User AddUser(User user)
        {
            int numberOfUsers = System.IO.File.ReadLines(_filePath).Count();
            user.Id = numberOfUsers + 1;
            string userPost = JsonSerializer.Serialize(user);
            System.IO.File.AppendAllText(_filePath, userPost + Environment.NewLine);
            return user;
        }

        public User FindUser(User user)
        {

            using (StreamReader reader = System.IO.File.OpenText(_filePath))
            {
                string? currentUserInFile;
                while ((currentUserInFile = reader.ReadLine()) != null)
                {
                    User? fileUser = JsonSerializer.Deserialize<User>(currentUserInFile);
                    if (fileUser.Email == user.Email && fileUser.Password == user.Password)
                        return fileUser;
                }
            }
            return null;
        }

        public void UpdateUser(int id,User user)
        {
            string textToReplace = string.Empty;
            using (StreamReader reader = System.IO.File.OpenText(_filePath))
            {
                string? currentUserInFile;
                while ((currentUserInFile = reader.ReadLine()) != null)
                {
                    User? newUser = JsonSerializer.Deserialize<User>(currentUserInFile);
                    if (newUser.Id == id)
                        textToReplace = currentUserInFile;
                }
            }

            if (textToReplace != string.Empty)
            {
                string text = System.IO.File.ReadAllText(_filePath);
                text = text.Replace(textToReplace, JsonSerializer.Serialize(user));
                System.IO.File.WriteAllText(_filePath, text);
            }

        }
        public void DeleteUser(int id)
        {

        }
    }
}
