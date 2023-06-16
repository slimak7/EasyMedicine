namespace MedicinesManagement.ResponseModels
{
    public abstract class BaseResponse
    {
        public bool Success { get; set; }
        public string Errors { get; set; }

        public BaseResponse(bool success, string errors)
        {
            Success = success;
            Errors = errors;
        }
        public BaseResponse()
        {
            Success = true;
            Errors = null;
        }

    }
}
