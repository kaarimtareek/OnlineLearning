using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.EntityFrameworkCore;

using OnlineLearning.Commands;
using OnlineLearning.Common;
using OnlineLearning.Constants;
using OnlineLearning.Models;
using OnlineLearning.Services;

namespace OnlineLearning.Handlers.Commands
{
    public class AddRoomMaterialCommandHandler : IRequestHandler<AddRoomMaterialCommand, ResponseModel<int>>
    {
        private readonly DbContextOptions<AppDbContext> dbContextOptions;
        private readonly IRoomService roomService;

        public AddRoomMaterialCommandHandler(DbContextOptions<AppDbContext> dbContextOptions,IRoomService roomService)
        {
            this.dbContextOptions = dbContextOptions;
            this.roomService = roomService;
        }
        public async Task<ResponseModel<int>> Handle(AddRoomMaterialCommand request, CancellationToken cancellationToken)
        {
            using (var context = new AppDbContext(dbContextOptions))
            {
                using var transactionScope = await context.Database.BeginTransactionAsync();
                var addMaterialResult = await roomService.AddMaterial(context, request.RoomId, request.File);
                if(!addMaterialResult.IsSuccess)
                {
                    await transactionScope.RollbackAsync();
                }
                else
                {
                    await transactionScope.CommitAsync();
                }
                return new ResponseModel<int>
                {
                    IsSuccess = addMaterialResult.IsSuccess,
                    HttpStatusCode = addMaterialResult.ResponseCode.GetStatusCode(),
                    MessageCode = addMaterialResult.Message,
                    Result = addMaterialResult.Data
                };
            }
        }
    }
}
