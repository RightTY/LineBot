using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SevenDeadlySins.Models.Line.LineBotMessage.Element
{
    public class Bubble
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("body")]
        public FlexBody Body { get; set; }

        [JsonProperty("footer")]
        public FlexFooter Footer { get; set; }
    }
}
