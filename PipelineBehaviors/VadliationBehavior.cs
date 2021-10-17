using FluentValidation;
using MediatR;
using OnlineLearning.Common;
using OnlineLearning.Constants;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineLearning.PipelineBehaviors
{
    public class ValidationBehavior<TRequest,TResponse> : IPipelineBehavior<TRequest,TResponse>
       where TResponse: class , IResponseModel, new ()
     


    {
        private readonly IEnumerable<IValidator<TRequest>> validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            this.validators = validators;
        }


        public async Task<OperationResult<TResponse>> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<OperationResult<TResponse>> next)
        {
            var validationContext = new ValidationContext<TRequest>(request);
            var failures = validators.Select(x => x.Validate(validationContext))
                .SelectMany(x => x.Errors)
                .Where(x => x != null)
                .ToList();
            if (failures.Count > 0)
            {
                return OperationResult.Fail<TResponse>(ConstantMessageCodes.OPERATION_FAILED, default, ResponseCodeEnum.FAILED);
            }
            return await next();
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var validationContext = new ValidationContext<TRequest>(request);
            var failures = validators.Select(x => x.Validate(validationContext))
                .SelectMany(x => x.Errors)
                .Where(x => x != null)
                .ToList();
            if (failures.Count > 0)
            {
                var errors = failures.Select(x => new ErrorModel
                {
                    Code = x.ErrorCode,
                    FieldName = x.PropertyName,
                    Message = x.ErrorMessage,
                    
                }).ToList();
                return new TResponse { IsSuccess = false,HttpStatusCode= HttpStatusCode.BadRequest  ,Errors = errors, MessageCode = ConstantMessageCodes.VALIDATION_ERROR };
            }
            return await next();
        }
    }
}
