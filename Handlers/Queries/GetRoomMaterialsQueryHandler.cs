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
    public class GetRoomMaterialsQueryHandler : IRequestHandler<GetRoomMaterialsQuery, ResponseModel<List<RoomMaterialDto>>>
    {
        private readonly DbContextOptions<AppDbContext> dbContextOptions;
        private readonly IMapper mapper;

        public GetRoomMaterialsQueryHandler(DbContextOptions<AppDbContext> dbContextOptions, IMapper mapper)
        {
            this.dbContextOptions = dbContextOptions;
            this.mapper = mapper;
        }


        public async Task<ResponseModel<List<RoomMaterialDto>>> Handle(GetRoomMaterialsQuery request, CancellationToken cancellationToken)
        {
            using(var context = new AppDbContext(dbContextOptions))
            {
                var materials = await context.RoomMaterials.Where(x => x.RoomId == request.RoomId && x.IsActive && !x.IsDeleted).Select(x => new RoomMaterialDto
                {
                    RoomId = x.RoomId,
                    IsActive = x.IsActive,
                    FileName = x.FileName,
                    FilePath = x.FilePath,
                    Id = x.Id
                }).ToListAsync();
                return ResponseModel.Success(ConstantMessageCodes.OPERATION_SUCCESS, materials);
            }
        }
    }
}
