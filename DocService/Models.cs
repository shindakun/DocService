using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace DocService
{
    public class Models
    {
        public class StatusObj
        {
            [JsonProperty(PropertyName = "id")]
            public string Id { get; set; }

            [JsonProperty(PropertyName = "partid")]
            public string PartId { get; set; }

            [JsonProperty(PropertyName = "status")]
            public string Status { get; set; }

            [JsonProperty(PropertyName = "detail")]
            public string Detail { get; set; }
            
            [JsonProperty(PropertyName = "body")]
            public string Body { get; set; }
        }

        public class BodyObj
        {
            [JsonProperty(PropertyName = "id")]
            public string Id { get; set; }

            [JsonProperty(PropertyName = "partid")]
            public string PartId { get; set; }

            [JsonProperty(PropertyName = "body")]
            public string Body { get; set; }
        }

        public class PutStatus
        {
            [JsonProperty(PropertyName = "status")]
            public string Status { get; set; }

            [JsonProperty(PropertyName = "detail")]
            public string Detail { get; set; }
        }
    }
}
