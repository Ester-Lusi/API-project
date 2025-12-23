using Dtos;
using Entities;

namespace Services
{
    public interface IPasswordService
    {
        Password CheckStrength(string password);
    }
}