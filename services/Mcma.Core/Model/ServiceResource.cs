using System;

namespace Mcma.Core
{
    public class ServiceResource : McmaObject
    {
        public string ResourceType { get; set; }

        public string HttpEndpoint { get; set; }

        public string AuthType { get; set; }

        public string AuthData { get; set; }
    }
}