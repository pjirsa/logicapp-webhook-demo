using System;

namespace logicdemo
{
    public class ProcessRequest
    {
        public string callbackUrl { get; set; }
        public string data { get; set; }
    }

    public class ProcessResponse
    {
        public string data { get; set; }
    }

}