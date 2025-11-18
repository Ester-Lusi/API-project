using Entities;

namespace Services
{
    public interface IPasswordService
    {
        Password checkStrength(string password);
    }
}