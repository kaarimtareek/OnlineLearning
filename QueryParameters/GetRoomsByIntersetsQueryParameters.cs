using OnlineLearning.Common;

namespace OnlineLearning.QueryParameters
{
    public class GetRoomsByIntersetsQueryParameters : QueryStringParameters
    {
        public string[] Interests { get; set; }
    }
}
