using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService : IProductService
    {
        IProductRepository _iProductRepository;
        public ProductService(IProductRepository iProductRepository)
        {
            _iProductRepository = iProductRepository;
        }
        public async Task<IEnumerable<Product>> GetProducts(string? name,
            int[]? categories, int? nimPrice, int? maxPrice, int? limit,
            string? orderBy, int? offset)
        {
            return await _iProductRepository.GetProducts(name, categories, nimPrice, maxPrice, limit, orderBy, offset);
        }
    }
}
