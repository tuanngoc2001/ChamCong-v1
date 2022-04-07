using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ChamCong.Common.Utils
{
    public class Response
    {
        public Response(HttpStatusCode code, string message)
        {
            Code = code;
            Message = message;
        }
        public Response(string message)
        {
            Message = message;
        }
        public Response()
        {
        }
        public HttpStatusCode Code { get; set; } = HttpStatusCode.OK;
        public string Message { get; set; } = "Success";
        public bool IsSuccess
        {
            get { return Code == HttpStatusCode.OK; }
        }
    }
    public class Response<T> : Response
    {
        [JsonConstructor]
        public Response(T data)
        {
            Data = data;
            Code = HttpStatusCode.OK;
        }
        public Response(HttpStatusCode code, T data)
        {
            Data = data;
            Message = "OK";
        }
        public Response(HttpStatusCode code, T data, string message)
        {
            Code = code;
            Data = data;
            Message = message;
        }
        public Response()
        {

        }
        /// <summary>
        ///     Returned data
        /// </summary>
        public T Data { get; set; }
    }
    //trả về một mảng data
    public class ResponseList<T> : Response
    {

        public ResponseList(List<T> data)
        {
            Data = data;
        }

        public ResponseList()
        {
        }


        public List<T> Data { get; set; }
    }


    //error
    public class ResponseError : Response
    {
        public ResponseError(HttpStatusCode code, string message, List<Dictionary<string, string>> errorDetail = null) : base(
            code,
            message)
        {
            ErrorDetail = errorDetail;
        }

        public List<Dictionary<string, string>> ErrorDetail { get; set; }
    }



    //APIReponsitory
    public class APIReponsitory
    {
        public APIReponsitory() { }
        public APIReponsitory(bool success, string message)
        {
            Success = success;
            Message = message;
        }
        public APIReponsitory(bool success, string message,string data)
        {
            Success = success;
            Message = message;
            Data = data;
        }
        public Boolean Success { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }
    }
    
}
