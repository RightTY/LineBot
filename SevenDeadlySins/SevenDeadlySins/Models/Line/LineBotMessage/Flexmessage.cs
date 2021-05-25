using Newtonsoft.Json;
using SevenDeadlySins.Models.Line.LineBotMessage.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SevenDeadlySins.Models.Line.LineBotMessage
{
    public class Flexmessage
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("altText")]
        public string altText { get; set; }
        [JsonProperty("contents")]
        public Carousel contents { get; set; }
    }
}
