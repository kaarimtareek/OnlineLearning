using Microsoft.EntityFrameworkCore;

using OnlineLearning.Common;
using OnlineLearning.Constants;
using OnlineLearning.DTOs;
using OnlineLearning.Models;
using OnlineLearning.Utilities;
using OnlineLearning.Utilities.Stemmer;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearning.Services
{
    public class InterestService : IInterestService
    {
        private readonly DbContextOptions<AppDbContext> contextOptions;
        private readonly ILoggerService<InterestService> logger;
        private readonly IStemmer stemmer;

        public InterestService(ILoggerService<InterestService> logger, DbContextOptions<AppDbContext> contextOptions, IStemmer stemmer)
        {
            this.logger = logger;
            this.contextOptions = contextOptions;
            this.stemmer = stemmer;
        }

        public async Task<OperationResult<List<InterestDto>>> GetInterests()
        {
            try
            {
                using (AppDbContext context = new AppDbContext(contextOptions))
                {
                    var result = await context.Interests.AsNoTracking().IsNotDeleted().Select(x => new InterestDto
                    {
                        Id = x.Id
                    }).ToListAsync();
                    return OperationResult.Success(result);
                }
            }
            catch (Exception e)
            {
                logger.LogError($"error whilte GetInterests: {e}");
                return OperationResult.Fail<List<InterestDto>>();
            }
        }

        public async Task<OperationResult<string>> AddInterest(AppDbContext context, string newInterest, bool ignoreSimilarity = false)
        {
            try
            {
                //normalize the interest
                var normalizedInterest = StringNormalizer.Normalize(newInterest);
                var stemmedInterest = StringNormalizer.NormalizeWithStem(newInterest);

                var interest = await context.Interests.FirstOrDefaultAsync(x => x.Id == normalizedInterest);
                if (interest != null)
                {
                    if (interest.IsDeleted)
                    {
                        interest.IsDeleted = false;
                    }
                    else
                    {
                        return OperationResult.Fail<string>(ConstantMessageCodes.INTEREST_ALREADY_EXIST, default, ResponseCodeEnum.DUPLICATE_DATA);
                    }
                }
                else
                {
                    if (!ignoreSimilarity)
                    {
                        var similarInterestsResult = await GetSimilarInterests(stemmedInterest);
                        if (similarInterestsResult.Data?.Count > 0)
                        {
                            var similarInterets = string.Join(",", similarInterestsResult.Data.Select(x => x.Id));
                            return new OperationResult<string>
                            {
                                IsSuccess = false,
                                Data = similarInterets,
                                Message = ConstantMessageCodes.THERE_ARE_SIMILAR_INTEREST,
                                ResponseCode = ResponseCodeEnum.DUPLICATE_DATA,
                            };
                        }
                    }

                    interest = new Interest
                    {
                        Id = normalizedInterest,
                        StemmedName = stemmedInterest,
                    };
                    await context.Interests.AddAsync(interest);
                }
                await context.SaveChangesAsync();
                return OperationResult.Success<string>(interest.Id);
            }
            catch (Exception e)
            {
                logger.LogError($"error whilte AddInterest: {e}");
                return OperationResult.Fail<string>(ConstantMessageCodes.OPERATION_FAILED, default, ResponseCodeEnum.FAILED);
            }
        }
        public async Task<OperationResult<string>> UpdateInterestNumber(AppDbContext context,string interestId, int number = 1)
        {
            try
            {
               

                var interest = await context.Interests.FirstOrDefaultAsync(x => x.Id == interestId);
                if (interest == null)
                {
                   
                        return OperationResult.Fail<string>(ConstantMessageCodes.NOT_FOUND, default, ResponseCodeEnum.NOT_FOUND);
                }
                interest.NumberOfInterestedUsers += number;
                await context.SaveChangesAsync();
                return OperationResult.Success<string>(interest.Id);
            }
            catch (Exception e)
            {
                logger.LogError($"error whilte AddInterest: {e}");
                return OperationResult.Fail<string>(ConstantMessageCodes.OPERATION_FAILED, default, ResponseCodeEnum.FAILED);
            }
        }
        public async Task<OperationResult<string>> UpdateInterestNumber(AppDbContext context,Interest interest, int number = 1)
        {
            try
            {
                if (interest == null)
                {
                    return OperationResult.Fail<string>(ConstantMessageCodes.NOT_FOUND, default, ResponseCodeEnum.NOT_FOUND);
                }
                interest.NumberOfInterestedUsers += number;
                await context.SaveChangesAsync();
                return OperationResult.Success<string>(interest.Id);
            }
            catch (Exception e)
            {
                logger.LogError($"error whilte AddInterest: {e}");
                return OperationResult.Fail<string>(ConstantMessageCodes.OPERATION_FAILED, default, ResponseCodeEnum.FAILED);
            }
        }

        public async Task<OperationResult<List<Interest>>> GetUserIntersts(string userId)
        {
            try
            {
                using (AppDbContext context = new AppDbContext(contextOptions))
                {
                    var userInterests = await context.UserInterests.Include(x=>x.Interest).Where(x=>x.UserId == userId && !x.IsDeleted).Select(x=>new Interest { Id = x.InterestId, IsDeleted = x.IsDeleted}).ToListAsync();
                    return OperationResult.Success(userInterests);
                }
            }
            catch (Exception e)
            {
                logger.LogError($"error whilte GetUserIntersts: {e}");
                return OperationResult.Fail<List<Interest>>(ConstantMessageCodes.OPERATION_FAILED, default, ResponseCodeEnum.FAILED);
            }
        }

        public async Task<OperationResult<int>> AddUserInterest(AppDbContext context, string userId, string interestId)
        {
            try
            {
                var interest = await context.Interests.AsNoTracking().FirstOrDefaultAsync(x => x.Id == interestId && !x.IsDeleted);
                if (interest == null)
                {
                    return OperationResult.Fail<int>(ConstantMessageCodes.INTEREST_NOT_FOUND, default, ResponseCodeEnum.NOT_FOUND);
                }
                var userInterest = await context.UserInterests.FirstOrDefaultAsync(x => x.UserId == userId && x.InterestId == interestId);
                if (userInterest != null)
                {
                    if (userInterest.IsDeleted)
                    {
                        userInterest.IsDeleted = false;
                    }
                    else
                    {
                        return OperationResult.Fail<int>(ConstantMessageCodes.INTEREST_ALREADY_EXIST, default, ResponseCodeEnum.DUPLICATE_DATA);
                    }
                }
                else
                {
                    userInterest = new UserInterest
                    {
                        UserId = userId,
                        InterestId = interestId,
                    };
                    await context.UserInterests.AddAsync(userInterest);
                }
                await context.SaveChangesAsync();
                return OperationResult.Success(userInterest.Id);
            }
            catch (Exception e)
            {
                logger.LogError($"error whilte AddUserInteres: {e}");
                return OperationResult.Fail<int>(ConstantMessageCodes.OPERATION_FAILED, default, ResponseCodeEnum.FAILED);
            }
        }

        public async Task<OperationResult<int>> DeleteUserInterest(AppDbContext context,string userId, string interestId)
        {
            var userInterest = await context.UserInterests.FirstOrDefaultAsync(x=> x.UserId == userId && x.InterestId == interestId && !x.IsDeleted);
            if(userInterest == null)
            {
                return OperationResult.Fail<int>(ConstantMessageCodes.INTEREST_NOT_FOUND,default,ResponseCodeEnum.NOT_FOUND);
            }
            userInterest.IsDeleted = true;
            await context.SaveChangesAsync();
            return OperationResult.Success(userInterest.Id);
        }
        public async Task<OperationResult<List<Interest>>> GetSimilarInterests(string normalizedInterest)
        {
            try
            {
                using (AppDbContext context = new AppDbContext(contextOptions))
                {
                    //split the text by dashes to get each word alone
                    var interestWords = normalizedInterest.Split('-', StringSplitOptions.RemoveEmptyEntries);
                    //stemming all the words in the new interest
                    var interestStemmedWords = interestWords.ToList().ConvertAll(x => stemmer.Stem(x).Value);
                    //get all the interests in the db
                    var interests = await context.Interests.AsNoTracking().Where(x => !x.IsDeleted).ToListAsync();
                    var result = GetSimilarInterests(interestStemmedWords, interests);
                    return OperationResult.Success(result, ConstantMessageCodes.THERE_ARE_SIMILAR_INTEREST, ResponseCodeEnum.FAILED);
                }
            }
            catch (Exception e)
            {
                logger.LogError($"error whilte GetSimilarInterests: {e}");
                return OperationResult.Fail<List<Interest>>(ConstantMessageCodes.OPERATION_FAILED, default, ResponseCodeEnum.FAILED);
            }
        }
        private List<Interest> GetSimilarInterests(List<string> separatedInterest, List<Interest> interests)
        {
            List<Interest> similarInterests = new List<Interest>();
            foreach (var interest in interests)
            {
                bool isSimilar = false;
                //split each interest by the dashes
                foreach (var word in (string[])interest.StemmedName.Split('-'))
                {
                    ///compare each stemmed word in the new interest with each stemmed word in the interests in the db
                    ///if they are equal , add the interest in the similar interests list
                    foreach (var value in separatedInterest)
                    {
                        if (value == word)
                        {
                            similarInterests.Add(interest);
                            isSimilar = true;
                            break;
                        }
                    }
                    //if they are similar then break , we dont need to compare the rest of the senetence if there is a similarity
                    if (isSimilar)
                    {
                        break;
                    }
                }
            }
            return similarInterests;
        }

    }
}