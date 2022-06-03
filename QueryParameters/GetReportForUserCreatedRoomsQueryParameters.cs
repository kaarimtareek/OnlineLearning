using OnlineLearning.Common;

using System;

namespace OnlineLearning.QueryParameters
{
    public class GetReportForUserCreatedRoomsQueryParameters : QueryStringParameters
    {
        public string[] Statusess { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
