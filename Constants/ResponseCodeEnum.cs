using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OnlineLearning.Constants
{
    public enum ResponseCodeEnum
    {
        SUCCESS = 1,
        FAILED = -1,
        DUPLICATE_DATA = 2,
        NOT_FOUND = 3,
        BAD_INPUT = 4,
        INVALID_DATA = 5,

    }
    public static class ResponseCodeEnumExtension
    {
        public static HttpStatusCode GetStatusCode(this ResponseCodeEnum codeEnum)

        {
            switch (codeEnum)
            {
                case ResponseCodeEnum.SUCCESS:
                    return HttpStatusCode.OK;
                case ResponseCodeEnum.DUPLICATE_DATA:
                    return HttpStatusCode.Conflict;
                case ResponseCodeEnum.NOT_FOUND:
                    return HttpStatusCode.NotFound;
                case ResponseCodeEnum.BAD_INPUT:
                case ResponseCodeEnum.INVALID_DATA:
                    return HttpStatusCode.BadRequest;
                default:
                    return HttpStatusCode.InternalServerError;

            }
        }
    }
}
