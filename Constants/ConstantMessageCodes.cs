﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearning.Constants
{
    public static class ConstantMessageCodes
    {
        public const string OPERATION_SUCCESS = "OPERATION_SUCCESS";
        public const string OPERATION_FAILED = "OPERATION_FAILED";
        public const string VALIDATION_ERROR = "VALIDATION_ERROR";
        public const string USER_NOT_FOUND = "USER_NOT_FOUND";
        public const string FILE_NOT_FOUND = "FILE_NOT_FOUND";
        public const string FILE_NOT_SUPPORTED = "FILE_NOT_SUPPORTED";
        public const string ROOM_NOT_FOUND = "ROOM_NOT_FOUND";
        public const string ROOM_NOT_VAlID_TO_JOIN = "ROOM_NOT_VAlID_TO_JOIN";
        public const string ROOM_STATUS_NOT_FOUND = "ROOM_STATUS_NOT_FOUND";
        public const string INTEREST_ALREADY_EXIST = "INTEREST_ALREADY_EXIST";
        public const string THERE_ARE_SIMILAR_INTEREST = "THERE_ARE_SIMILAR_INTEREST";
        public const string CANT_JOIN_OWNED_ROOM = "CANT_JOIN_OWNED_ROOM";
        public const string CANT_CHANGE_STATUS = "CANT_CHANGE_STATUS";
        public const string INVALID_STATUS = "INVALID_STATUS";
    }
}
