using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class RatingRepository : IRatingRepository
    {
        ShopContext _dbcontext;
        public RatingRepository(ShopContext context)
        {
            _dbcontext = context;
        }

        public async Task<Rating> AddRating(Rating newRating)
        {
            await _dbcontext.Ratings.AddAsync(newRating);
            await _dbcontext.SaveChangesAsync();
            return newRating;
        }
    }
}
