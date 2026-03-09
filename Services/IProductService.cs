using Dtos;

namespace Services
{
    public interface IProductService
    {
        public Task<ProductDto> GetProductById(int id);
        Task<PageResponseDto<ProductDto>> GetProducts(string? name, int?[] categories, int? minPrice, int? maxPrice, int? position, int skip, string? orderBy, string? description);
    }
}