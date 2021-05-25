using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SevenDeadlySins.Models.Line.LineBotMessage.Element
{
    public class Content
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("weight")]
        public string Weight { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("style")]
        public string Style { get; set; }

        [JsonProperty("height")]
        public string Height { get; set; }

        [JsonProperty("action")]
        public FlexAction Action { get; set; }
    }
}
