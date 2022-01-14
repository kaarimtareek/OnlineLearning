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
        public async Task<bool> IsRoomExist(int id)
        {
            using (var context = new AppDbContext(contextOptions))
            {
                return await context.Rooms.AsNoTracking().AnyAsync(x => x.Id == id && !x.IsDeleted);
            }
        }
        public async Task<bool> IsRoomExist(int id, CancellationToken cancellationToken)
        {
            using (var context = new AppDbContext(contextOptions))
            {
                return await context.Rooms.AsNoTracking().AnyAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
            }
        }
        public async Task<bool> IsUserRoomOwner(int roomId,string userId)
        {
            using (var context = new AppDbContext(contextOptions))
            {
                return await context.Rooms.AsNoTracking().AnyAsync(x => x.Id == roomId && x.OwnerId == userId && !x.IsDeleted);
            }
        }
        public async Task<bool> IsUserRoomOwner(int roomId, string userId, CancellationToken cancellationToken)
        {
            using (var context = new AppDbContext(contextOptions))
            {
                return await context.Rooms.AsNoTracking().AnyAsync(x => x.Id == roomId && x.OwnerId == userId && !x.IsDeleted, cancellationToken);
            }
        }

    }
}