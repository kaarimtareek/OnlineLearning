using System;
using System.Linq;

namespace OnlineLearning.Constants
{
    public static class ConstantRoomStatus
    {
        public const string ACTIVE = "ACTIVE";
        public const string CANCELED = "CANCELED";
        public const string PENDING = "PENDING";
        public const string FINISHED = "FINISHED";

        public static readonly string[] ALL = { ACTIVE, CANCELED, PENDING, FINISHED };
        public static bool IsValidStatus(string status) => ALL.Contains(status);
        public static bool IsActiveStatus(string status) => status == ACTIVE || status == PENDING;
        public static bool IsDeadStatus(string status) => status == FINISHED || status == CANCELED;
    }
}
