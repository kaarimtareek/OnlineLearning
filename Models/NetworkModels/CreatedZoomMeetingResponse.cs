using System;

namespace OnlineLearning.Models.NetworkModels
{
    // CreatedZoomMeetingResponse myDeserializedClass = JsonConvert.DeserializeObject<CreatedZoomMeetingResponse>(myJsonResponse);
    public class CreatedZoomMeetingResponse
    {
        public long id { get; set; }
        public string agenda { get; set; }
        public DateTime created_at { get; set; }
        public int duration { get; set; }
        public string join_url { get; set; }
        public string password { get; set; }
        public bool pre_schedule { get; set; }
    }
}
