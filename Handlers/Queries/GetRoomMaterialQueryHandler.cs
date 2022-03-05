using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineLearning.Common;
using OnlineLearning.Constants;
using OnlineLearning.DTOs;
using OnlineLearning.Models;
using OnlineLearning.Queries;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineLearning.Handlers.Queries
{
    public class GetRoomMaterialQueryHandler : IRequestHandler<GetRoomMaterialQuery, ResponseModel<RoomMaterialDto>>
    {
        private readonly DbContextOptions<AppDbContext> dbContextOptions;
        private readonly IMapper mapper;

        public GetRoomMaterialQueryHandler(DbContextOptions<AppDbContext> dbContextOptions, IMapper mapper)
        {
            this.dbContextOptions = dbContextOptions;
            this.mapper = mapper;
        }

        public async Task<ResponseModel<RoomMaterialDto>> Handle(GetRoomMaterialQuery request, CancellationToken cancellationToken)
        {
            using (var context = new AppDbContext(dbContextOptions))
            {
                var material = await context.RoomMaterials.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.MaterialId && x.RoomId == request.RoomId);
                if(material ==null)
                {
                    return ResponseModel.Fail<RoomMaterialDto>(ConstantMessageCodes.FILE_NOT_FOUND);

                }
                var returnedMaterial = new RoomMaterialDto
                {
                    RoomId = material.RoomId,
                    IsActive = material.IsActive,
                    FileName = material.FileName,
                    FilePath = material.FilePath,
                    Id = material.Id
                };
                return ResponseModel.Success(ConstantMessageCodes.OPERATION_SUCCESS, returnedMaterial);
            }
        }
    }
}
