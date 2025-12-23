using Dtos;

namespace Services
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetCategory();
    }
}