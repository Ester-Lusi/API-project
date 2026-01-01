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
        public async Task<PageResponseDto<ProductDto>> GetProducts(string? name, int?[] categories, int? minPrice, int? maxPrice, int? position, int skip, string? orderBy, string? description)
        {
            List<Product> products;
            PageResponseDto<ProductDto> pageResponse = new PageResponseDto<ProductDto>();
            (products, pageResponse.TotalItems) = await _iProductRepository.GetProducts(name, categories, minPrice, maxPrice, position, skip, orderBy, description);
            pageResponse.Data = _imapper.Map<List<Product>, List<ProductDto>>(products);
            pageResponse.CurrentPage = position ?? 1;
            pageResponse.HasPreviousPage = pageResponse.CurrentPage > 1;
            pageResponse.HasNextPage = (pageResponse.TotalItems / skip) > (pageResponse.CurrentPage - 1);
            pageResponse.PageSize = skip;
            return pageResponse;
        }

    }
}
