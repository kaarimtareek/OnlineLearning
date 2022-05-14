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
        public async Task<OperationResult<int>> AddMeeting(AppDbContext context, string userId, int roomId, string link, DateTime start, DateTime end, string topicName, string topiceDiscription, long zoomMeetingId, string meetingPassword, bool startNow)
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
        public async Task<OperationResult<int>> DeleteMeeting(AppDbContext context, int roomId, int meetingId)
        {
            try
            {
                //delete only pending meetings
                var meeting = await context.RoomMeetings.SingleOrDefaultAsync(x=>x.Id == meetingId && x.RoomId == roomId && !x.IsDeleted);
                if (meeting == null)
                    return OperationResult.Fail<int>(ConstantMessageCodes.NOT_FOUND,default,ResponseCodeEnum.NOT_FOUND);
                if(meeting.StatusId == ConstantRoomMeetingStatus.ACTIVE)
                {
                    return OperationResult.Fail<int>();
                }
                if (meeting.StatusId == ConstantRoomMeetingStatus.CLOSED)
                {
                    return OperationResult.Fail<int>(ConstantMessageCodes.NOT_FOUND,default,ResponseCodeEnum.NOT_FOUND);
                }
                meeting.IsDeleted = true;
                meeting.StatusId = ConstantRoomMeetingStatus.CANCELED;
                await context.SaveChangesAsync();
                return OperationResult.Success(meeting.Id);
            }
            catch (Exception ex)
            {
                return OperationResult.Fail<int>(ex.Message);
            }

        }
        public async Task<OperationResult<int>> DeleteMeeting(AppDbContext context, RoomMeeting roomMeeting)
        {
            try
            {
                //delete only pending meetings
                
                if (roomMeeting == null)
                    return OperationResult.Fail<int>(ConstantMessageCodes.NOT_FOUND,default,ResponseCodeEnum.NOT_FOUND);
                if(roomMeeting.StatusId == ConstantRoomMeetingStatus.ACTIVE)
                {
                    return OperationResult.Fail<int>();
                }
                if (roomMeeting.StatusId == ConstantRoomMeetingStatus.CLOSED)
                {
                    return OperationResult.Fail<int>(ConstantMessageCodes.NOT_FOUND,default,ResponseCodeEnum.NOT_FOUND);
                }
                roomMeeting.IsDeleted = true;
                roomMeeting.StatusId = ConstantRoomMeetingStatus.CANCELED;
                await context.SaveChangesAsync();
                return OperationResult.Success(roomMeeting.Id);
            }
            catch (Exception ex)
            {
                return OperationResult.Fail<int>(ex.Message);
            }

        }
        public async Task<OperationResult<int>> UpdateMeeting(AppDbContext context, int roomId, int meetingId,string topicName,string topicDescription, bool startNow, DateTime? startTime, DateTime? endTime)
        {
            try
            {
                //update only pending meetings
                var meeting = await context.RoomMeetings.SingleOrDefaultAsync(x=>x.Id == meetingId && x.RoomId == roomId && !x.IsDeleted);
                if (meeting == null)
                    return OperationResult.Fail<int>(ConstantMessageCodes.NOT_FOUND,default,ResponseCodeEnum.NOT_FOUND);
                if(meeting.StatusId != ConstantRoomMeetingStatus.WAITING)
                {
                    return OperationResult.Fail<int>();
                }
                meeting.MeetingName = topicName;
                meeting.MeetingDescription = topicDescription;
                if(startNow)
                {
                    meeting.StartDate = DateTime.Now;
                    meeting.StatusId = ConstantRoomMeetingStatus.ACTIVE;
                }
                if (startTime != null)
                    meeting.StartDate = startTime.Value;
                if (endTime != null)
                    meeting.EndDate = endTime.Value;
                await context.SaveChangesAsync();
                return OperationResult.Success(meeting.Id);
            }
            catch (Exception ex)
            {
                return OperationResult.Fail<int>(ex.Message);
            }

        }
        public async Task<OperationResult<int>> UpdateMeeting(AppDbContext context, RoomMeeting roomMeeting,string topicName,string topicDescription, bool startNow, DateTime? startTime, DateTime? endTime)
        {
            try
            {
                //update only pending meetings
                
                if (roomMeeting == null)
                    return OperationResult.Fail<int>(ConstantMessageCodes.NOT_FOUND,default,ResponseCodeEnum.NOT_FOUND);
                if(roomMeeting.StatusId != ConstantRoomMeetingStatus.WAITING)
                {
                    return OperationResult.Fail<int>();
                }
                roomMeeting.MeetingName = topicName;
                roomMeeting.MeetingDescription = topicDescription;
                if(startNow)
                {
                    roomMeeting.StartDate = DateTime.Now;
                    roomMeeting.StatusId = ConstantRoomMeetingStatus.ACTIVE;
                }
                if (startTime != null)
                    roomMeeting.StartDate = startTime.Value;
                if (endTime != null)
                    roomMeeting.EndDate = endTime.Value;
                await context.SaveChangesAsync();
                return OperationResult.Success(roomMeeting.Id);
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
