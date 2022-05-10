using Microsoft.EntityFrameworkCore;

using OnlineLearning.Common;
using OnlineLearning.Constants;
using OnlineLearning.Models;
using OnlineLearning.Utilities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Services
{
    public class MeetingService : IMeetingService
    {
        public async Task<OperationResult<int>> AddMeeting(AppDbContext context, string userId, int roomId, string link, DateTime start, DateTime end, string topicName, string topiceDiscription, int zoomMeetingId, string meetingPassword, bool startNow)
        {
            try
            {
                var duration = DatetimeHelper.GetDurationFromDates(start, end);
                var meeting = new RoomMeeting
                {
                    StartDate = start,
                    EndDate = end,
                    RoomId = roomId,
                    MeetingName = topicName,
                    MeetingDescription = topiceDiscription,
                    OwnerId = userId,
                    Duration = duration,
                    MeetingUrl = link,
                    MeetingPassword = meetingPassword,
                    StatusId = startNow ? ConstantRoomMeetingStatus.ACTIVE : ConstantRoomMeetingStatus.WAITING,
                    ZoomMeetingId = zoomMeetingId,
                };
                await context.RoomMeetings.AddAsync(meeting);
                await context.SaveChangesAsync();
                return OperationResult.Success(meeting.Id);
            }
            catch (Exception ex)
            {
                return OperationResult.Fail<int>(ex.Message);
            }

        }
        //craete meeting by the details of specific class in the room
        //create meeting by the details given by the user
        //check if the room exists and available and the user is the owner,
        //check if the user has any overlapping time between the given start and end dates with any other intervals in any of his rooms
    }
}
