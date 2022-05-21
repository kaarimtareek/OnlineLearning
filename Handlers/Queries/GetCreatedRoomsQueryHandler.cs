using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineLearning.Common;
using OnlineLearning.Models.OutputModels;
using OnlineLearning.Models;
using OnlineLearning.Queries;
using OnlineLearning.DTOs;
using System.Threading.Tasks;
using System.Threading;
using OnlineLearning.Services;
using System.Linq;
using AutoMapper;
using System.Collections.Generic;
using OnlineLearning.Constants;

namespace OnlineLearning.Handlers.Queries
{
    public class GetCreatedRoomsQueryHandler : IRequestHandler<GetCreatedRoomsQuery, ResponseModel<PagedList<RoomDto>>>
    {
        private readonly DbContextOptions<AppDbContext> dbContextOptions;
        private readonly IMapper mapper;

        public GetCreatedRoomsQueryHandler(DbContextOptions<AppDbContext> dbContextOptions, IMapper mapper)
        {
            this.dbContextOptions=dbContextOptions;
            this.mapper=mapper;
        }

        public async Task<ResponseModel<PagedList<RoomDto>>> Handle(GetCreatedRoomsQuery request, CancellationToken cancellationToken)
        {
            using (var context = new AppDbContext(dbContextOptions))
            {
                var rooms = await context.Rooms.IsNotDeleted().IncludeOwner().IncludeInterests().IncludeUserRoomStatus(request.UserId).AsNoTracking().Where(x => x.OwnerId == request.UserId && !x.IsDeleted).ToListAsync();
                var roomsDto = mapper.Map<List<RoomDto>>(rooms);
                return new ResponseModel<PagedList<RoomDto>>
                {
                    HttpStatusCode = ResponseCodeEnum.SUCCESS.GetStatusCode(),
                    IsSuccess = true,
                    MessageCode = ConstantMessageCodes.OPERATION_SUCCESS,
                    Result = new PagedList<RoomDto>(roomsDto, roomsDto.Count, 1, roomsDto.Count)
                };
            }
        }
    }
}
