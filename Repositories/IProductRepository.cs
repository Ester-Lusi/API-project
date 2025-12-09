using Entities;

namespace Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts(string? name, int[]? categories, int? nimPrice, int? maxPrice, int? limit, string? orderBy, int? offset);
    }
}