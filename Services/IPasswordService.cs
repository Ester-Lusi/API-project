using Dtos;

namespace Services
{
    public interface IPasswordService
    {
        int GetPasswordStrength(string password);
    }
}