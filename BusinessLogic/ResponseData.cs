using System;

namespace Configurations
{
    public class ResponseData<T>
    {
        public ResponseData()
        {
            Success = true;
        }
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public int Count { get; set; }
    }
}
