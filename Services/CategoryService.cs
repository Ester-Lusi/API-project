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
        public async Task<IEnumerable<CategoryDto>> GetCategory()
        {
            IEnumerable<Category> cayegoryList = await _iCategoryRepository.GetCategory();
            IEnumerable<CategoryDto> categoryDtos = _imapper.Map<IEnumerable<Category>, IEnumerable<CategoryDto>>(cayegoryList);
            return categoryDtos;
        }
    }
}
