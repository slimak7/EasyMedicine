namespace MedicinesManagement.ResponseModels
{
    public abstract class BaseResponse
    {
        bool Success { get; set; }
        string Errors { get; set; }

        public BaseResponse(bool success, string errors)
        {
            Success = success;
            Errors = errors;
        }
    }
}
