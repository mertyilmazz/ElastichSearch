using Microsoft.OpenApi.Writers;
using System.Net;

namespace ElastichSearch.API.DTOs
{
    public record ResponseDto<T>
    {
        public T? Data { get; set; }

        public List<string>? Errors { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public static ResponseDto<T> Success(T data, HttpStatusCode statusCode)
        {
            return new ResponseDto<T> { Data = data, StatusCode = statusCode };
        }

        public static ResponseDto<T> Fail(List<string> errors, HttpStatusCode httpStatusCode)
        {
            return new ResponseDto<T> { Errors = errors, StatusCode = httpStatusCode };
        }

        public static ResponseDto<T> Fail(string error, HttpStatusCode httpStatusCode)
        {
            return new ResponseDto<T> { Errors = new List<string> { error}, StatusCode = httpStatusCode };
        }


    }
}
