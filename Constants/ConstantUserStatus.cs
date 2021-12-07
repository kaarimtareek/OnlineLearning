using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearning.Constants
{
    public static class ConstantUserStatus
    {
        public const string ACTIVE = "ACTIVE";
        public const string IN_ACTIVE = "IN_ACTIVE";
        public const string BLOCKED= "BLOCKED";
        public static List<string> ALL = new List<string> { ACTIVE, IN_ACTIVE, BLOCKED };
        public static bool IsStatusExist(string status) => ALL.Contains(status);
    }
}
