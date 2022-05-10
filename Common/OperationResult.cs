﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using OnlineLearning.Constants;

namespace OnlineLearning.Common
{
    public class OperationResult : IOperationResult
    {
        public static OperationResult<T> Fail<T>(string message = ConstantMessageCodes.OPERATION_FAILED, T data = default, ResponseCodeEnum responseCodeEnum = ResponseCodeEnum.FAILED)
        {
            return new OperationResult<T>(data, false, responseCodeEnum, message);
        }
        public static OperationResult<T> Success<T>( T data = default, string message = ConstantMessageCodes.OPERATION_SUCCESS, ResponseCodeEnum responseCodeEnum = ResponseCodeEnum.SUCCESS)
        {
            return new OperationResult<T>(data, true, responseCodeEnum, message);
        }
        public static OperationResult<T,E> Fail<T,E>(string message = ConstantMessageCodes.OPERATION_FAILED, T data = default, ResponseCodeEnum responseCodeEnum = ResponseCodeEnum.FAILED, E error = default)
        {
            return new OperationResult<T,E>(data, false, responseCodeEnum, message,error);
        }
        public static OperationResult<T,E> Success<T,E>( T data = default, string message = ConstantMessageCodes.OPERATION_SUCCESS, ResponseCodeEnum responseCodeEnum = ResponseCodeEnum.SUCCESS, E error = default)
        {
            return new OperationResult<T,E>(data, true, responseCodeEnum, message,error);
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
    public class OperationResult<T, E> : IOperationResult
    {
        public OperationResult()
        {

        }
        public OperationResult(T result, bool IsSuccess, ResponseCodeEnum codeEnum, string message,E error)
        {
            Data = result;
            this.IsSuccess = IsSuccess;
            ResponseCode = codeEnum;
            Message = message;
            Error = error;
        }
        public T Data { get; set; }
        public E Error { get; set; }
        public bool IsSuccess { get; set; }
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
