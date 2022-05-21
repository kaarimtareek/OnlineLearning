using System.Collections.Generic;
using System.Linq;

namespace OnlineLearning.Constants
{
    public static class ConstantUserRoomStatus
    {
        public const string EMPTY = "";
        public const string PENDING = "PENDING";
        public const string JOINED = "JOINED";
        public const string ACCEPTED = "ACCEPTED";
        public const string REJECTED = "REJECTED";
        public const string SUSPENDED = "SUSPENDED";
        public const string LEFT = "LEFT";
        public const string CANCELED = "CANCELED";
        public static readonly string[] ALL = new string[] { PENDING, JOINED, ACCEPTED, REJECTED, SUSPENDED, LEFT, CANCELED };
        public static bool IsValidStatus(string status) => ALL.Contains(status);

        public static bool IsJoined(string status) => status == JOINED;

        public static bool IsAccepted(string status) => status == ACCEPTED;

        public static bool IsRejected(string status) => status == REJECTED;

        public static bool IsPending(string status) => status == PENDING;

        public static bool IsSuspended(string status) => status == SUSPENDED;
        public static bool IsLeft(string status) => status == LEFT;



        //public static Dictionary<string, List<string>> AllowedStatus = new Dictionary<string, List<string>>
        //{
        //    {PENDING ,new List<string>{ JOINED, ACCEPTED , REJECTED, CANCELED } },
        //    {ACCEPTED ,new List<string>{ JOINED , SUSPENDED , CANCELED } },
        //    {REJECTED ,new List<string>{}},
        //    {JOINED ,new List<string>{ SUSPENDED, LEFT }},
        //    {SUSPENDED ,new List<string>{ JOINED }},
        //    {LEFT ,new List<string>{ PENDING }},
        //    {CANCELED ,new List<string>{ PENDING }},
        //};

        public static Dictionary<string, List<string>> RoomOwnerAllowedStatus = new Dictionary<string, List<string>>
        {
            { EMPTY ,new List<string>{ } },
            { PENDING ,new List<string>{ JOINED, ACCEPTED , REJECTED } },
            { ACCEPTED ,new List<string>{  SUSPENDED , REJECTED } },
            { REJECTED ,new List<string>{ ACCEPTED }},
            { JOINED ,new List<string>{ SUSPENDED  }},
            { SUSPENDED ,new List<string>{ JOINED }},
            { LEFT ,new List<string>{ }},
            { CANCELED ,new List<string>{ }},
        };
        public static Dictionary<string, List<string>> UserAllowedStatus = new Dictionary<string, List<string>>
        {
            { EMPTY ,new List<string>{ PENDING } },
            { PENDING ,new List<string>{ CANCELED } },
            { ACCEPTED ,new List<string>{  LEFT ,CANCELED } },
            { REJECTED ,new List<string>{  }},
            { JOINED ,new List<string>{ LEFT  }},
            { SUSPENDED ,new List<string>{  }},
            { LEFT ,new List<string>{ PENDING }},
            { CANCELED ,new List<string>{ PENDING }},
        };
    }
}