using Microsoft.EntityFrameworkCore;

using OnlineLearning.Common;
using OnlineLearning.Constants;
using OnlineLearning.Models;
using OnlineLearning.Utilities;

using System;
using System.Threading.Tasks;

namespace OnlineLearning.Services
{
    public class UserService : IUserService
    {
        private readonly DbContextOptions<AppDbContext> contextOptions;
        private readonly ILoggerService<UserService> logger;

        public UserService(DbContextOptions<AppDbContext> contextOptions, ILoggerService<UserService> logger)
        {
            this.contextOptions = contextOptions;
            this.logger = logger;
        }


        public async Task<OperationResult<ApplicationUser>> Get(string id)
        {
            try
            {
                using (var context = new AppDbContext(contextOptions))
                {
                    logger.LogInfo($"trying to get user with id {id}");
                    var user = await context.ApplicationUsers.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
                    if (user == null)
                    {
                        return OperationResult.Fail<ApplicationUser>(ConstantMessageCodes.OPERATION_FAILED, default, ResponseCodeEnum.FAILED);
                    }
                    return OperationResult.Success(user);
                }
            }
            catch (Exception e)
            {
                return OperationResult.Fail<ApplicationUser>(ConstantMessageCodes.OPERATION_FAILED, default, ResponseCodeEnum.FAILED);
            }
        }


    }
}