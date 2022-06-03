using MediatR;
using OnlineLearning.Common;
using OnlineLearning.DTOs.Reports;

using System;

namespace OnlineLearning.Queries.Reports
{
    public class GetUserCreatedRoomsReportQuery : IRequest<ResponseModel<UserCreatedRoomsActivityDto>>
    {
        public string UserId { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string[] Statusess { get; set; }
    }
}
