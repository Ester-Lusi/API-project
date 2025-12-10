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
        public CategoryService(ICategoryRepository iCategoryRepository)
        {
            _iCategoryRepository = iCategoryRepository;
        }
        public async Task<List<Category>> GetCategory()
        {
            return await _iCategoryRepository.GetCategory();
        }
    }
}
