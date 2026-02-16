using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        ShopContext _dbContext;
        public CategoryRepository(ShopContext context)
        {
            _dbContext = context;
        }
        public async Task<IEnumerable<Category>> GetCategory()
        {
            return await _dbContext.Categories.ToListAsync();
        }
    }
}
