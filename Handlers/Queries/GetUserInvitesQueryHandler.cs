using MediatR;
using Microsoft.EntityFrameworkCore;

using OnlineLearning.Common;
using OnlineLearning.DTOs;
using OnlineLearning.Models;
using OnlineLearning.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineLearning.Handlers.Queries
{
    public class GetUserInvitesQueryHandler : IRequestHandler<GetUserInvitesQuery, ResponseModel<List<InviteDto>>>
    {
        private readonly DbContextOptions<AppDbContext> dbContextOptions;

        public GetUserInvitesQueryHandler(DbContextOptions<AppDbContext> dbContextOptions)
        {
            this.dbContextOptions=dbContextOptions;
        }

        public async Task<ResponseModel<List<InviteDto>>> Handle(GetUserInvitesQuery request, CancellationToken cancellationToken)
        {
            using(AppDbContext context = new AppDbContext(dbContextOptions))
            {
                var userInvites = await context.UserInvites.Include(x => x.Owner).Include(x => x.Room).AsNoTracking().Where(x => x.UserId == request.UserId).Select(x => new InviteDto
                {
                    UserId = request.UserId,
                    Id = x.Id,
                    OwnerId = x.OwnerId,
                    OwnerName = x.Owner.Name,
                    RoomId = x.RoomId,
                    RoomName = x.Room.Name,
                    StatusId = x.StatusId
                }).ToListAsync();
                return ResponseModel.Success(Constants.ConstantMessageCodes.OPERATION_SUCCESS, userInvites);
            }
        }

    }
}
