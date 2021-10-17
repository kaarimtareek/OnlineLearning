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

        public UserService(DbContextOptions<AppDbContext> contextOptions)
        {
            this.contextOptions = contextOptions;
        }

        public async Task<OperationResult<int>> Add(string name, string email, string phonenumber, string passwrod, DateTime? birthdate)
        {
            try
            {
                using (var context = new AppDbContext(contextOptions))
                {
                    string salt = PasswordHasher.CreateRandomSalt();
                    string passwordHashed = PasswordHasher.Create(passwrod, salt);
                    var user = new User
                    {
                        Email = email,
                        Phonenumber = phonenumber,
                        Name = name,
                        Birthdate = birthdate,
                        Salt = salt,
                        Password = passwordHashed,
                    };
                    await context.Users.AddAsync(user);
                    await context.SaveChangesAsync();
                    return OperationResult.Success(ConstantMessageCodes.OPERATION_SUCCESS, user.Id, ResponseCodeEnum.SUCCESS);
                }
            }
            catch (Exception e)
            {

                return OperationResult.Fail<int>(ConstantMessageCodes.OPERATION_FAILED, default, ResponseCodeEnum.FAILED);
            }
        }
        public async Task<OperationResult<User>> Get(int id)
        {
            try
            {
                using (var context = new AppDbContext(contextOptions))
                {
                    var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
                    if (user == null)
                    {
                        return OperationResult.Fail<User>(ConstantMessageCodes.OPERATION_FAILED, default, ResponseCodeEnum.FAILED);
                    }
                    return OperationResult.Success(ConstantMessageCodes.OPERATION_SUCCESS, user, ResponseCodeEnum.SUCCESS);
                }
            }
            catch (Exception e)
            {
                return OperationResult.Fail<User>(ConstantMessageCodes.OPERATION_FAILED, default, ResponseCodeEnum.FAILED);
            }
        }
    }


}