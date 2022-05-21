using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace OnlineLearning.PipelineBehaviors
{
    public class LogBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>

    {
        private readonly OnlineLearning.Utilities.ILoggerService<LogBehavior<TRequest, TResponse>> logger;

        public LogBehavior(OnlineLearning.Utilities.ILoggerService<LogBehavior<TRequest, TResponse>> logger)
        {
            this.logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {

            logger.LogInfo($"logging request of {request} ");

            var result = await next();
            logger.LogInfo($"logging reponse of {result}");
            return result;
        }
    }
}
