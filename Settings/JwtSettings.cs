using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearning.Settings
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public int ExpirationInDays { get; set; }
    }
}
