using Dtos;

namespace Services
{
    public interface IProductService
    {
        Task<PageResponseDto<ProductDto>> GetProducts(string? name, int?[] categories, int? minPrice, int? maxPrice, int? position, int skip, string? orderBy, string? description);
    }
}