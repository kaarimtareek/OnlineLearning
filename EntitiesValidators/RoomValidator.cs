using Microsoft.EntityFrameworkCore;

using OnlineLearning.Constants;
using OnlineLearning.Models;
using OnlineLearning.Utilities;

using System;
using System.Linq;
using System.Text;
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

        public async Task<bool> IsRoomStatusExist(string id, CancellationToken cancellationToken = default)
        {
            using (var context = new AppDbContext(contextOptions))
            {
                return await context.LookupRoomStatuses.AsNoTracking().AnyAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
            }
        }
      
        public async Task<bool> IsRoomExist(int id, CancellationToken cancellationToken = default)
        {
            using (var context = new AppDbContext(contextOptions))
            {
                return await context.Rooms.AsNoTracking().AnyAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
            }
        }
        public async Task<bool> IsUserRoomOwner(int roomId, string userId, CancellationToken cancellationToken = default)
        {
            using (var context = new AppDbContext(contextOptions))
            {
                return await context.Rooms.AsNoTracking().AnyAsync(x => x.Id == roomId && x.OwnerId == userId && !x.IsDeleted, cancellationToken);
            }
        }
        public async Task<bool> IsActiveRoom(int roomId, CancellationToken cancellationToken = default)
        {
            using (var context = new AppDbContext(contextOptions))
            {
                return await context.Rooms.AnyAsync(x => x.Id == roomId && x.StatusId == ConstantRoomStatus.ACTIVE && !x.IsDeleted,cancellationToken);
            }
        }
        public async Task<bool> IsUserCanCreateMeeting(string userId,DateTime startDate, DateTime endDate,CancellationToken cancellationToken = default)
        {
            using (var context = new AppDbContext(contextOptions))
            {
                return await context.RoomMeetings.AsNoTracking()
                    .AnyAsync(x => x.OwnerId == userId && (x.StatusId == ConstantRoomMeetingStatus.ACTIVE || (x.EndDate < startDate && x.StartDate < startDate) || (endDate < x.StartDate && startDate < x.StartDate)) && !x.IsDeleted);
            }
        }
        public string GetOverlappedMeetings( string userId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            using (var context = new AppDbContext(contextOptions))
            {
                var intervalsOfOtherMeetings =  context.RoomMeetings.AsNoTracking().Where(x => x.OwnerId == userId && !x.IsDeleted && ( x.StatusId ==ConstantRoomMeetingStatus.ACTIVE || x.StatusId ==ConstantRoomMeetingStatus.WAITING) ).Select(x => new { x.StartDate, x.EndDate, x.Id, x.RoomId, x.MeetingName }).ToArray();
            var overlappedIntervals = intervalsOfOtherMeetings.Where(x => DatetimeHelper.IsIntervalsOverlap(startDate, endDate, x.StartDate, x.EndDate)).ToArray();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var interval in overlappedIntervals)
                stringBuilder.AppendLine($"interval id : {interval.Id} , Start date: {interval.StartDate} , End Date: {interval.EndDate}");
            return stringBuilder.ToString();
            }
        }


    }
}