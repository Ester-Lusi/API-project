using Dtos;

namespace Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetCategory();
    }
}