using Dtos;

namespace Services
{
    public interface IPasswordService
    {
        Password GetStrength(string password);
    }
}