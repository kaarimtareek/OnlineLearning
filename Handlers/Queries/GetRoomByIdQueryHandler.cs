using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

using OnlineLearning.Common;
using OnlineLearning.Constants;
using OnlineLearning.DTOs;
using OnlineLearning.Models;
using OnlineLearning.Queries;
using OnlineLearning.Services;

using System.Threading;
using System.Threading.Tasks;

namespace OnlineLearning.Handlers.Queries
{
    public class GetRoomByIdQueryHandler : IRequestHandler<GetRoomByIdQuery, ResponseModel<RoomDto>>
    {
        private readonly DbContextOptions<AppDbContext> dbContextOptions;
        private readonly IMapper mapper;

        public GetRoomByIdQueryHandler(DbContextOptions<AppDbContext> dbContextOptions, IMapper mapper)
        {
            this.dbContextOptions = dbContextOptions;
            this.mapper = mapper;
        }

        public async Task<ResponseModel<RoomDto>> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
        {
            using (AppDbContext context = new AppDbContext(dbContextOptions))
            {
                var room = await context.Rooms.IsNotDeleted().IncludeOwner().IncludeInterests().FirstOrDefaultAsync(x => x.Id == request.RoomId && !x.IsDeleted);
                if (room ==null)
                {
                    return new ResponseModel<RoomDto>
                    {
                        IsSuccess = false,
                        HttpStatusCode = ResponseCodeEnum.NOT_FOUND.GetStatusCode(),
                        MessageCode = ConstantMessageCodes.ROOM_NOT_FOUND,
                    };
                }
                var roomDto = mapper.Map<RoomDto>(room);
                return new ResponseModel<RoomDto>
                {
                    IsSuccess = true,
                    HttpStatusCode = ResponseCodeEnum.SUCCESS.GetStatusCode(),
                    MessageCode = ConstantMessageCodes.OPERATION_SUCCESS,
                    Result = roomDto

                };
            }
        }
    }
}
