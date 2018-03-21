using Newtonsoft.Json;

namespace FXZServer
{

    public class Response
    {
        public int ErrorCode { get; set; }

        public bool IsError { get; set; }

        public string ErrorMessage { get; set; }

        public override string ToString()
        {
            if (IsError)
            {
                return $"errcode:{ErrorCode}, errmsg:{ErrorMessage}";
            }
            return JsonConvert.SerializeObject(this);
        }
    }

    public class Response<T> : Response
    {
        public T Data { get; set; }
    }
}
