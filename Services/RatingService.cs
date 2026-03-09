using Repositories;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _iRatingRepository;
        public RatingService(IRatingRepository iRatingRepository)
        {
            _iRatingRepository = iRatingRepository;
        }
        public async Task<Rating> AddRating(Rating newRating)
        {
            return await _iRatingRepository.AddRating(newRating);
        }
    }
}
