﻿namespace OnlineLearning.Settings
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public int ExpirationInDays { get; set; }
    }
}
