using Microsoft.EntityFrameworkCore;

using OnlineLearning.Models;

using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
