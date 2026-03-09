using Entities;

namespace Repositories
{
    public interface IProductRepository
    {
        public Task<Product> GetProductById(int id);
        Task<(List<Product> Items, int TotalCount)> GetProducts(string? name, int?[] categories, int? minPrice, int? maxPrice, int? position, int? skip, string? orderBy, string? description);
    }
}