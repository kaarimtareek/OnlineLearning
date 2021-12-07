using Microsoft.EntityFrameworkCore;

using OnlineLearning.Models;

using System.Threading;
using System.Threading.Tasks;

namespace OnlineLearning.EntitiesValidators
{
    public class RoomValidator : IRoomValidator
    {
        private readonly DbContextOptions<AppDbContext> contextOptions;

        public RoomValidator(DbContextOptions<AppDbContext> contextOptions)
        {
            this.contextOptions = contextOptions;
        }

        public async Task<bool> IsRoomStatusExist(string id)
        {
            using (var context = new AppDbContext(contextOptions))
            {
                return await context.LookupRoomStatuses.AsNoTracking().AnyAsync(x => x.Id == id && !x.IsDeleted);
            }
        }
        public async Task<bool> IsRoomStatusExist(string id, CancellationToken cancellationToken)
        {
            using (var context = new AppDbContext(contextOptions))
            {
                return await context.LookupRoomStatuses.AsNoTracking().AnyAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
            }
        }
    }
}