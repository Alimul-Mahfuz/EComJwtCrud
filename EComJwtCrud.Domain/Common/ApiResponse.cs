using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EComJwtCrud.Domain.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public T Data { get; set; }
        public string Message   { get; set; }
        public List<System.String> Errors { get; set; }


        public ApiResponse()
        {
            Errors = new List<string>();
        }

        public static ApiResponse<T> SuccessResponse(T? data, string message = "", int StatusCode = 200)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data,
                StatusCode = StatusCode
            };
        }

        public static ApiResponse<T> FailResponse(List<string> errors, string message = "",int StatusCode=500)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Errors = errors,
                StatusCode = StatusCode
            };
        }

        public static ApiResponse<T> FailResponse(string error, string message = "",int StatusCode=500)
        {
            return FailResponse(new List<string> { error }, message,StatusCode);
        }
    }
}
