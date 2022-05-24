using AutoMapper;

using MediatR;

using Microsoft.AspNetCore.Identity;
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
    public class GetRoomMaterialQueryHandler : IRequestHandler<GetRoomMaterialQuery, ResponseModel<FileManagerResult>>
    {
        private readonly DbContextOptions<AppDbContext> dbContextOptions;
        private readonly IMapper mapper;
        private readonly IFileManager fileManager;

        public GetRoomMaterialQueryHandler(DbContextOptions<AppDbContext> dbContextOptions, IMapper mapper, IFileManager fileManager)
        {
            this.dbContextOptions = dbContextOptions;
            this.mapper = mapper;
            this.fileManager=fileManager;
        }

        public async Task<ResponseModel<FileManagerResult>> Handle(GetRoomMaterialQuery request, CancellationToken cancellationToken)
        {
            using (var context = new AppDbContext(dbContextOptions))
            {
                var material = await context.RoomMaterials.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.MaterialId && x.RoomId == request.RoomId);
                if (material ==null)
                {
                    return ResponseModel.Fail<FileManagerResult>(ConstantMessageCodes.FILE_NOT_FOUND,default,null,ResponseCodeEnum.NOT_FOUND.GetStatusCode());

                }
                string path = material.FilePath;
                var fileResult = await fileManager.Get(path);
                if(!fileResult.IsSuccess)
                {
                    return ResponseModel.Fail<FileManagerResult>(fileResult.Message, default, null, fileResult.ResponseCode.GetStatusCode());
                }
                
                return ResponseModel.Success(ConstantMessageCodes.OPERATION_SUCCESS, fileResult.Data);
            }
        }
    }
}
