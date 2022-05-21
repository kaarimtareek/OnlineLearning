
using MediatR;

namespace OnlineLearning.Common
{
    public interface IRequestWrapper<T> : IRequest<ResponseModel<T>> { }
    public interface IHandlerWrapper<TIn, TOut> : IRequestHandler<TIn, ResponseModel<TOut>>
        where TIn : IRequestWrapper<TOut>
    { }
    public interface IPipelineWrapper<TIn, TOut> : IPipelineBehavior<TIn, ResponseModel<TOut>>
    where TIn : IRequestWrapper<TOut>
    { }

}
