using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearning.Utilities
{
    public interface ILoggerService<T>
    {
        void LogError(string msg);
        void LogInfo(string msg);

    }
}
