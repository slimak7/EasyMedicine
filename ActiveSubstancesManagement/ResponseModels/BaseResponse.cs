﻿namespace ActiveSubstancesManagement.ResponseModels
{
    public class BaseResponse
    {
        public bool Success { get; set; }
        public string Errors { get; set; }

        public BaseResponse(bool success, string errors)
        {
            Success = success;
            Errors = errors;
        }
    }
}
