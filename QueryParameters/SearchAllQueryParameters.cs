using OnlineLearning.Common;

namespace OnlineLearning.QueryParameters
{
    public class SearchAllQueryParameters : QueryStringParameters
    {
        public bool SkipRooms { get; set; }
        public bool SkipInterests { get; set; }
        public bool SkipUsers { get; set; }
    }
}
