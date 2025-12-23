using AutoMapper;
using Dtos;
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
        //AutoMapper _imapper;
        IMapper _imapper;
        public ProductService(IProductRepository iProductRepository, IMapper mapper)
        {
            _iProductRepository = iProductRepository;
            _imapper = mapper;
        }
        public async Task<IEnumerable<ProductDto>> GetProducts(string? name,
            int[]? categories, int? nimPrice, int? maxPrice, int? limit,
            string? orderBy, int? offset)
        {
            IEnumerable<Product> products = await _iProductRepository.GetProducts(name, categories, nimPrice, maxPrice, limit, orderBy, offset);
            IEnumerable<ProductDto> productsDto = _imapper.Map< IEnumerable < Product> ,IEnumerable <ProductDto>>(products);
            return productsDto;
        }
    }
}
