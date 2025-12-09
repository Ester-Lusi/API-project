using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ProductRepository : IProductRepository
    {
        shopContext _dbContext;
        public ProductRepository(shopContext context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<Product>> GetProducts(string? name,
            int[]? categories, int? nimPrice, int? maxPrice, int? limit,
            string? orderBy, int? offset)
        {
            return _dbContext.Products;
        }
    }
}
