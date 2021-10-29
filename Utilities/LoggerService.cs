using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearning.Utilities
{
    public class LoggerService<T> : ILoggerService<T>
    {
        private readonly ILogger<T> logger;

        public LoggerService(ILogger<T> logger)
        {
            this.logger = logger;
        }
        public void LogError(string msg)
        {
                logger.LogError(msg);
        }

        public void LogInfo(string msg)
        {
          
                logger.LogInformation(msg);
        }
    }
}
