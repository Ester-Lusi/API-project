using Dtos;
using Entities;

namespace Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts(string? name, int[]? categories, int? nimPrice, int? maxPrice, int? limit, string? orderBy, int? offset);
    }
}