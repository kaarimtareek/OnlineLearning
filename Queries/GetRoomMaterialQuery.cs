using MediatR;

using OnlineLearning.Common;
using OnlineLearning.DTOs;
using OnlineLearning.Services;

using System.IO;

namespace OnlineLearning.Queries
{
    public class GetRoomMaterialQuery : IRequest<ResponseModel<FileManagerResult>>
    {
        public int RoomId { get; set; }
        public int MaterialId { get; set; }
        public string UserId { get; set; }
    }
}
