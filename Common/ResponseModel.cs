using OnlineLearning.Constants;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OnlineLearning.Common
{

    public class ResponseModel : IResponseModel
    {
        public static ResponseModel<T> Fail<T>(string message,T data = default, List<ValidationErrorModel> errors = default, HttpStatusCode code = default)
        {
            return new ResponseModel<T>(false, data, errors, message,code);
        }
        public static ResponseModel<T> Success<T>(string message,T data = default,List<ValidationErrorModel> errors=default,HttpStatusCode code = default)
        {
            return new ResponseModel<T>( true,data, errors, message,code);
        }
        public List<ValidationErrorModel> Errors { get; set; }
        public bool IsSuccess { get; set; }
        public string MessageCode { get; set; }
        [JsonIgnore]
        public HttpStatusCode HttpStatusCode { get; set; }

    }
    public class ResponseModel <T> : IResponseModel
    {
        public ResponseModel()
        {

        }
        public ResponseModel(bool isSuccess, T result = default, List<ValidationErrorModel> errors = default,string messageCode = "",HttpStatusCode code = default)
        {
            IsSuccess = isSuccess;
            Errors = errors;
            Result = result;
            MessageCode= messageCode;
            HttpStatusCode = code;
        }
        public T Result { get; set; }
        public List<ValidationErrorModel> Errors { get; set; }

        public bool IsSuccess { get; set; }
        public string MessageCode { get; set; }
        [JsonIgnore]
        public HttpStatusCode HttpStatusCode { get; set; }

    }
    public interface IResponseModel
    {
        public List<ValidationErrorModel> Errors { get; set; }
        public bool IsSuccess { get; set; }
        public string MessageCode { get; set; }
        [JsonIgnore]
        public HttpStatusCode HttpStatusCode { get; set; }
    }
}
