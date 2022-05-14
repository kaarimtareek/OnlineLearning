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
        public async Task<bool> IsUserCanCreateMeeting(string userId,DateTime startDate, DateTime endDate,CancellationToken cancellationToken = default,int meetingId =0)
        {
            using (var context = new AppDbContext(contextOptions))
            {
                var exceptMeetingId = meetingId == 0;
                
                var result = await context.RoomMeetings.AsNoTracking()
                    .AnyAsync(x => x.OwnerId == userId && ( x.StatusId == ConstantRoomMeetingStatus.ACTIVE  || x.StatusId == ConstantRoomMeetingStatus.WAITING ) && !(( x.EndDate < startDate && x.StartDate < startDate) || (endDate < x.StartDate && startDate <x.StartDate)) && (exceptMeetingId || x.Id != meetingId) && !x.IsDeleted,cancellationToken);
                return !result;
            }
        }
        public string GetOverlappedMeetings( string userId, DateTime startDate, DateTime endDate, int meetingId = 0, CancellationToken cancellationToken = default)
        {
            using (var context = new AppDbContext(contextOptions))
            {
                var exceptMeetingId = meetingId == 0;

                var overlappedIntervals =  context.RoomMeetings.AsNoTracking().Where(x => x.OwnerId == userId && !x.IsDeleted &&  ( x.StatusId ==ConstantRoomMeetingStatus.ACTIVE || x.StatusId ==ConstantRoomMeetingStatus.WAITING) && !((x.EndDate < startDate && x.StartDate < startDate) || (endDate < x.StartDate && startDate <x.StartDate)) && (exceptMeetingId || x.Id != meetingId ) ).Select(x => new { x.StartDate, x.EndDate, x.Id, x.RoomId, x.MeetingName }).ToArray();
           // var overlappedIntervals = intervalsOfOtherMeetings.Where(x => DatetimeHelper.IsIntervalsOverlap(startDate, endDate, x.StartDate, x.EndDate)).ToArray();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var interval in overlappedIntervals)
                stringBuilder.AppendLine($"interval id : {interval.Id} , Start date: {interval.StartDate} , End Date: {interval.EndDate}");
            return stringBuilder.ToString();
            }
        }


    }
}