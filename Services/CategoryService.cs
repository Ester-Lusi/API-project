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
    public class CategoryService : ICategoryService
    {
        ICategoryRepository _iCategoryRepository;
        //AutoMapper _imapper;
        IMapper _imapper;
        public CategoryService(ICategoryRepository iCategoryRepository, IMapper mapper)
        {
            _iCategoryRepository = iCategoryRepository;
            _imapper = mapper;
        }
        public async Task<List<CategoryDto>> GetCategory()
        {
            List<Category> cayegoryList = await _iCategoryRepository.GetCategory();
            List<CategoryDto> categoryDtos = _imapper.Map<List<Category>, List<CategoryDto>>(cayegoryList);
            return categoryDtos;
        }
    }
}
