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
using OnlineLearning.Utilities;

namespace OnlineLearning.Handlers.Commands
{
    public class AddRoomMaterialCommandHandler : IRequestHandler<AddRoomMaterialCommand, ResponseModel<int>>
    {
        private readonly DbContextOptions<AppDbContext> dbContextOptions;
        private readonly IRoomService roomService;
        private readonly ILoggerService<AddRoomCommandHandler> logger;

        public AddRoomMaterialCommandHandler(DbContextOptions<AppDbContext> dbContextOptions,IRoomService roomService,ILoggerService<AddRoomCommandHandler> logger)
        {
            this.dbContextOptions = dbContextOptions;
            this.roomService = roomService;
            this.logger = logger;
        }
        public async Task<ResponseModel<int>> Handle(AddRoomMaterialCommand request, CancellationToken cancellationToken)
        {
            try
            {


                using (var context = new AppDbContext(dbContextOptions))
                {
                    using var transactionScope = await context.Database.BeginTransactionAsync();
                    var addMaterialResult = await roomService.AddMaterial(context, request.RoomId, request.File);
                    if (!addMaterialResult.IsSuccess)
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
            catch(Exception e)
            {
                logger.LogError($"Error in add room material command handler {e}");
                return ResponseModel.Fail<int>();
            }
        }
    }
}
