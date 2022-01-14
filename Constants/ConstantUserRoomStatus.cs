using System.Linq;

namespace OnlineLearning.Constants
{
    public static class ConstantUserRoomStatus
    {
        public const string PENDING = "PENDING";
        public const string JOINED = "JOINED";
        public const string ACCEPTED = "ACCEPTED";
        public const string REJECTED = "REJECTED";
        public static readonly string[] ALL = new string[] { PENDING, JOINED, ACCEPTED, REJECTED };
        public static bool IsValidStatus(string status) => ALL.Contains(status);
        public static bool IsJoined(string status) => status == JOINED;
        public static bool IsAccepted(string status) => status == ACCEPTED;
        public static bool IsRejected(string status) => status == REJECTED;
        public static bool IsPending(string status) => status == PENDING;

    }
}
