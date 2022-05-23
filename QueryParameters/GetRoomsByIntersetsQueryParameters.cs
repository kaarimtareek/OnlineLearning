using OnlineLearning.Common;
using OnlineLearning.Settings;

using RestSharp;

using System;

namespace OnlineLearning.QueryParameters
{
    public class GetRoomsByIntersetsQueryParameters : QueryStringParameters
    {
        public string[] Interests { get; set; }
        public override void HandleSettings(PaginationSettings paginationSettings)
        {
            base.HandleSettings(paginationSettings);
            if (Interests == null)
                Interests = Array.Empty<string>();
        }
    }
}
