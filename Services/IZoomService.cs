using OnlineLearning.Common;
using OnlineLearning.Models.NetworkModels;

using System.Threading.Tasks;

namespace OnlineLearning.Services
{
    public interface IZoomService
    {
        Task<OperationResult<CreatedZoomMeetingResponse>> CreateZoomMeeting(string token, CreateZoomMeetingRequest request);
    }
}