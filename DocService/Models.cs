using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace DocService
{
    class Models
    {
        public class StatusObj
        {
            [JsonProperty(PropertyName = "status")]
            public string Status { get; set; }

            [JsonProperty(PropertyName = "detail")]
            public string Detail { get; set; }
            
            [JsonProperty(PropertyName = "body")]
            public string Body { get; set; }
        }

        public class BodyObj
        {
            [JsonProperty(PropertyName = "body")]
            public string Body { get; set; }
        }
    }
}
