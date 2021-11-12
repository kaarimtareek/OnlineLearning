using Microsoft.EntityFrameworkCore;
using OnlineLearning.Models;
using OnlineLearning.Services;
using OnlineLearning.Utilities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineLearning.EntitiesValidators
{
    public class InterestValidator
    {
        private readonly DbContextOptions<AppDbContext> contextOptions;
        private readonly IInterestService interestService;

        public InterestValidator(DbContextOptions<AppDbContext> contextOptions, IInterestService interestService)
        {
            this.contextOptions = contextOptions;
            this.interestService = interestService;
        }
        public async Task<bool> IsInterestExist(string interestId)
        {
            using (var context = new AppDbContext(contextOptions))
            {
                return await context.Interests.AsNoTracking().AnyAsync(x => x.Id == interestId && !x.IsDeleted);
            }
        }
        public async Task<bool> IsInterestExist(string interestId,CancellationToken cancellationToken)
        {
            using (var context = new AppDbContext(contextOptions))
            {
                return await context.Interests.AsNoTracking().AnyAsync(x => x.Id == interestId && !x.IsDeleted,cancellationToken);
            }
        }
        public async Task<bool> IsUserInterestExist(string userId,string interestId)
        {
            using (var context = new AppDbContext(contextOptions))
            {
                  return await context.UserInterests.AsNoTracking().AnyAsync(x => x.InterestId == interestId && x.UserId == userId && !x.IsDeleted);
            }
        }
        public async Task<bool> IsUserInterestExist(string userId, string interestId, CancellationToken cancellationToken)
        {
            using (var context = new AppDbContext(contextOptions))
            {
                return await context.UserInterests.AsNoTracking().AnyAsync(x => x.InterestId == interestId && x.UserId == userId && !x.IsDeleted,cancellationToken);
            }
        }
        //public async Task<bool> HasSimilarInterest(string interest)
        //{
        //    using (var context = new AppDbContext(contextOptions))
        //    {
        //        string normalizedInterest = StringNormalizer.Normalize(interest);
        //          return await context.UserInterests.AsNoTracking().AnyAsync(x => x.InterestId == interestId && x.UserId == userId && !x.IsDeleted);
        //    }
        //}
        //public async Task<bool> HasSimilarInterest(string userId, string interestId, CancellationToken cancellationToken)
        //{
        //    using (var context = new AppDbContext(contextOptions))
        //    {
        //        return await context.UserInterests.AsNoTracking().AnyAsync(x => x.InterestId == interestId && x.UserId == userId && !x.IsDeleted,cancellationToken);
        //    }
        //}
    }
}
