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
                default:
                    return HttpStatusCode.InternalServerError;

            }
        }
    }
}
