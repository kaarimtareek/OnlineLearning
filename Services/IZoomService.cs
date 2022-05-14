using OnlineLearning.Common;
using OnlineLearning.Models.NetworkModels;

using System.Threading.Tasks;

namespace OnlineLearning.Services
{
    public interface IZoomService
    {
        Task<OperationResult<CreatedZoomMeetingResponse>> CreateZoomMeeting(string token, UpsertZoomMeetingRequest request);
        Task<OperationResult> DeleteZoomMeeting(string token, long meetingId);
        Task<OperationResult> UpdateZoomMeeting(string token, long meetingId, UpsertZoomMeetingRequest request);
    }
}