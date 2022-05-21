namespace OnlineLearning.Utilities
{
    public interface ILoggerService<T>
    {
        void LogError(string msg);
        void LogInfo(string msg);

    }
}
