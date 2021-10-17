using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using OnlineLearning.Constants;

namespace OnlineLearning.Common
{
    public class OperationResult : IOperationResult
    {
        public static OperationResult<T> Fail<T>(string message, T data = default, ResponseCodeEnum responseCodeEnum = default)
        {
            return new OperationResult<T>(data, false, responseCodeEnum, message);
        }
        public static OperationResult<T> Success<T>(string message, T data = default, ResponseCodeEnum responseCodeEnum = default)
        {
            return new OperationResult<T>(data, true, responseCodeEnum, message);
        }
        public bool IsSuccess { get; set; }
        [JsonIgnore]
        public ResponseCodeEnum ResponseCode { get; set; }
        public string Message { get; set; }
    }
    public class OperationResult<T> : IOperationResult
    {
        public OperationResult()
        {

        }
        public OperationResult(T result,bool IsSuccess,ResponseCodeEnum codeEnum,string message)
        {
            Data = result;
            this.IsSuccess = IsSuccess;
            ResponseCode = codeEnum;
            Message = message;
        }
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        [JsonIgnore]
        public ResponseCodeEnum ResponseCode { get; set; }
        public string Message { get; set; }
    }
    public interface IOperationResult
    {
        public bool IsSuccess { get; set; }
        [JsonIgnore]
        public ResponseCodeEnum ResponseCode { get; set; }
        public string Message { get; set; }
    }
}
