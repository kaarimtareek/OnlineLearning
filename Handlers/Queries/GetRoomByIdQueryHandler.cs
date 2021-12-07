using MediatR;
using OnlineLearning.Commands;
using OnlineLearning.Common;
using OnlineLearning.Constants;
using OnlineLearning.Models;
using OnlineLearning.Queries;
using OnlineLearning.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineLearning.Handlers.Queries
{
    public class GetRoomByIdQueryHandler : IRequestHandler<GetRoomByIdQuery, ResponseModel<Room>>
    {
        private readonly IRoomService roomService;

        public GetRoomByIdQueryHandler(IRoomService roomService)
        {
            this.roomService = roomService;
        }
       
        public async Task<ResponseModel<Room>> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await roomService.GetRoomById(request.RoomId);

            return new ResponseModel<Room>
            {
                IsSuccess = result.IsSuccess,
                HttpStatusCode = result.ResponseCode.GetStatusCode(),
                Result = result.Data,
                MessageCode = result.Message,
            };
        }
    }
}
