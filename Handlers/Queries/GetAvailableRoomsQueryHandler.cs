using MediatR;

using Microsoft.EntityFrameworkCore;

using OnlineLearning.Common;
using OnlineLearning.Constants;
using OnlineLearning.DTOs;
using OnlineLearning.Models;
using OnlineLearning.Models.OutputModels;
using OnlineLearning.Queries;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineLearning.Handlers.Queries
{
    public class GetAvailableRoomsQueryHandler : IRequestHandler<GetAvailableRoomsQuery, ResponseModel<AvailableRoomsOutputModel>>
    {
        private readonly DbContextOptions<AppDbContext> dbContextOptions;
        public GetAvailableRoomsQueryHandler(DbContextOptions<AppDbContext> dbContextOptions)
        {
            this.dbContextOptions = dbContextOptions;
        }
        public async Task<ResponseModel<AvailableRoomsOutputModel>> Handle(GetAvailableRoomsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                using (var context = new AppDbContext(dbContextOptions))
                {
                    var userInterests = await context.UserInterests.Where(x => x.UserId == request.UserId && !x.IsDeleted).Select(x => x.InterestId).ToListAsync();
                    //how to get the top n numbers from each interest
                    //this query get the first 5 rooms of the first 3 interests in the user interests where the room start date is greater than now
                    var roomsForEachUserInterest = (from userinterest in context.Interests
                                                    let rooms = (from room in context.Rooms
                                                                 join owner in context.Users on room.OwnerId equals owner.Id
                                                                 let roomInterests = (from roomInterest in context.RoomInterests where roomInterest.RoomId == room.Id && !roomInterest.IsDeleted select roomInterest).Take(3).ToList()
                                                                 where !room.IsDeleted && roomInterests.Any(x => x.InterestId == userinterest.Id)
                                                                  && room.StartDate > DateTime.Now && room.StatusId != ConstantRoomStatus.FINISHED && room.StatusId != ConstantRoomStatus.CANCELED
                                                                 select new RoomDto
                                                                 {
                                                                     Id = room.Id,
                                                                     Description = room.Description,
                                                                     ExpectedEndDate = room.ExpectedEndDate,
                                                                     FinishDate = room.FinishDate,
                                                                     Interests = roomInterests.Select(x => new InterestDto
                                                                     {
                                                                         Id = x.InterestId,
                                                                         IsDeleted = x.IsDeleted
                                                                     }).ToList(),
                                                                     OwnerId = room.OwnerId,
                                                                     Name = room.Name,
                                                                     OwnerName = owner.Name,
                                                                     Price = room.Price,
                                                                     StartDate = room.StartDate,
                                                                     StatusId = room.StatusId,
                                                                 }).Take(5).ToList()
                                                    where userInterests.Contains(userinterest.Id) && !userinterest.IsDeleted
                                                    select new { userinterest.Id, rooms }
                                      ).Take(3).ToList();
                    var roomsResult = roomsForEachUserInterest.ToDictionary(x => x.Id, x => x.rooms.AsEnumerable());
                    return new ResponseModel<AvailableRoomsOutputModel>
                    {
                        HttpStatusCode = System.Net.HttpStatusCode.OK,
                        IsSuccess = true,
                        Result = new AvailableRoomsOutputModel
                        {
                            Values = roomsResult,
                        }
                    };
                }
            }
            catch (Exception e)
            {
                return new ResponseModel<AvailableRoomsOutputModel>
                {
                    HttpStatusCode = Constants.ResponseCodeEnum.FAILED.GetStatusCode(),
                    IsSuccess = false,
                    MessageCode = ConstantMessageCodes.OPERATION_FAILED,
                };
            }
        }
    }
}