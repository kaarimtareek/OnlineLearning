using Microsoft.EntityFrameworkCore;

using OnlineLearning.Constants;
using OnlineLearning.Models;

using System.Threading;
using System.Threading.Tasks;

namespace OnlineLearning.EntitiesValidators
{
    public class UserValidator : IUserValidator
    {
        private readonly DbContextOptions<AppDbContext> contextOptions;

        public UserValidator(DbContextOptions<AppDbContext> contextOptions)
        {
            this.contextOptions = contextOptions;
        }
        public async Task<bool> IsAvailableEmail(string email)
        {
            using (var context = new AppDbContext(contextOptions))
            {
                return !await context.ApplicationUsers.AnyAsync(x => x.Email == email);

            }
        }
        public async Task<bool> IsAvailableEmail(string email, CancellationToken cancellationToken)
        {
            using (var context = new AppDbContext(contextOptions))
            {
                return !await context.ApplicationUsers.AnyAsync(x => x.Email == email, cancellationToken);

            }
        }
        public async Task<bool> IsAvailablePhoneNumber(string phonenumber)
        {
            using (var context = new AppDbContext(contextOptions))
            {
                return !await context.ApplicationUsers.AnyAsync(x => x.PhoneNumber == phonenumber);

            }
        }
        public async Task<bool> IsAvailablePhoneNumber(string phonenumber, CancellationToken cancellationToken)
        {
            using (var context = new AppDbContext(contextOptions))
            {
                return !await context.ApplicationUsers.AnyAsync(x => x.PhoneNumber == phonenumber, cancellationToken);

            }
        }
        public async Task<bool> IsExistingUserId(string id)
        {
            using (var context = new AppDbContext(contextOptions))
            {
                return !await context.ApplicationUsers.AnyAsync(x => x.Id == id && !x.IsDeleted);

            }
        }
        public async Task<bool> IsExistingUserId(string id, CancellationToken cancellationToken)
        {
            using (var context = new AppDbContext(contextOptions))
            {
                return await context.ApplicationUsers.AnyAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);

            }
        }
        public async Task<bool> IsActiveUserId(string id)
        {
            using (var context = new AppDbContext(contextOptions))
            {
                return !await context.ApplicationUsers.AnyAsync(x => x.Id == id && !x.IsDeleted && x.StatusId == ConstantUserStatus.ACTIVE);

            }
        }
        public async Task<bool> IsActiveUserId(string id, CancellationToken cancellationToken)
        {
            using (var context = new AppDbContext(contextOptions))
            {
                return await context.ApplicationUsers.AnyAsync(x => x.Id == id && !x.IsDeleted && x.StatusId == ConstantUserStatus.ACTIVE, cancellationToken);

            }
        }
    }
}
