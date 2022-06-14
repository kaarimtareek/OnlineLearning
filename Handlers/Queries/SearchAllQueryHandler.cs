using MediatR;

using Microsoft.EntityFrameworkCore;

using OnlineLearning.Common;
using OnlineLearning.Constants;
using OnlineLearning.DTOs;
using OnlineLearning.Models;
using OnlineLearning.Models.OutputModels;
using OnlineLearning.Queries;
using OnlineLearning.Services;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineLearning.Handlers.Queries
{
    public class SearchAllQueryHandler : IRequestHandler<SearchAllQuery, ResponseModel<SearchAllOutputModel>>
    {
        private readonly DbContextOptions<AppDbContext> dbContextOptions;

        public SearchAllQueryHandler(DbContextOptions<AppDbContext> dbContextOptions)
        {
            this.dbContextOptions=dbContextOptions;
        }

        public async Task<ResponseModel<SearchAllOutputModel>> Handle(SearchAllQuery request, CancellationToken cancellationToken)
        {
            using (AppDbContext context = new AppDbContext(dbContextOptions))
            {
                List<RoomDto> rooms = new List<RoomDto>();
                if(request.SkipRooms)
                rooms = await context.Rooms.Include(x => x.Owner).Include(x=>x.Status).IncludeUserRoomStatus(request.UserId).AsNoTracking().Where(x => (x.Name.Contains( request.SearchValue ) || x.Owner.Name.Contains( request.SearchValue)) && !x.IsDeleted ).Select(x=> new RoomDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    ExpectedEndDate = x.ExpectedEndDate,
                    FinishDate = x.FinishDate,
                    IsPublic = x.IsPublic,
                    NumberOfJoinedUsers = x.NumberOfJoinedUsers,
                    NumberOfLeftUsers = x.NumberOfLeftUsers,
                    NumberOfRejectedUsers = x.NumberOfRejectedUsers,
                    NumberOfRequestedUsers = x.NumberOfRequestedUsers,
                    Price = x.Price,
                    OwnerId = x.OwnerId,
                    OwnerName = x.Owner.Name,
                    StartDate = x.StartDate,
                    StatusId = x.StatusId,
                    Status = x.Status == null?null: new RoomStatusDto
                    {
                        Id = x.Status.Id,
                        NameArabic = x.Status.NameArabic,
                        NameEnglish = x.Status.NameEnglish
                    },
                    UserRoomStatus = x.RequestedUsers ==null? null: x.RequestedUsers.FirstOrDefault(x=> x.UserId == request.UserId) ==null? null : new UserRoomStatusDto
                    {
                        Id = x.RequestedUsers.First(x => x.UserId == request.UserId).StatusId,
                        NameArabic = x.RequestedUsers.First(x => x.UserId == request.UserId).Status.NameArabic,
                        NameEnglish = x.RequestedUsers.First(x => x.UserId == request.UserId).Status.NameArabic
                    }
                    
                }).ToListAsync();
                List<UserDto> users = new List<UserDto>();
                if(request.SkipUsers)
                    users = await context.Users.Include(x=>x.Status).AsNoTracking().Where(x => x.Name.Contains(request.SearchValue) && !x.IsDeleted).Select(x=> new UserDto
                {
                    Id=x.Id,
                    UserName = x.UserName,
                    StatusId =x.StatusId,
                    CreatedAt = x.CreatedAt,
                    Name = x.Name,
                    Birthdate = x.Birthdate,
                    Status = x.Status ==null? null : new UserStatusDto
                    {
                        Id = x.Status.Id,
                        NameArabic = x.Status.NameArabic,
                        NameEnglish = x.Status.NameEnglish
                    }
                }).ToListAsync();
                List<InterestDto> interests = new List<InterestDto>();
                if (request.SkipInterests)
                    interests = await context.Interests.AsNoTracking().Where(x => x.Id.Contains(request.SearchValue) && !x.IsDeleted).Select(x=> new InterestDto
                {
                    Id = x.Id,
                    NumberOfInterestedUsers = x.NumberOfInterestedUsers,
                }).ToListAsync();
                var result = new SearchAllOutputModel
                {
                    Interests = interests,
                    Rooms = rooms,
                    Users = users,
                };
                return ResponseModel.Success(ConstantMessageCodes.OPERATION_SUCCESS, result);
            }
        }
    }
}
