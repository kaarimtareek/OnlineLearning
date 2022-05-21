using System;

namespace OnlineLearning.Models.NetworkModels
{
    // CreateZoomMeetingRequest myDeserializedClass = JsonConvert.DeserializeObject<CreateZoomMeetingRequest>(myJsonResponse);
    public class UpsertZoomMeetingRequest
    {
        /// <summary>
        /// Topic name
        /// </summary>
        public string agenda { get; set; }
        public bool? default_password { get; set; } = false;
        public int? duration { get; set; }
        public string? password { get; set; }
        public bool? pre_schedule { get; set; }
        public DateTime? start_time { get; set; }
        public string? timezone { get; set; } = "Africa/Cairo";
        public int type { get; set; } = 2;
        public string topic { get; set; }
    }
}
