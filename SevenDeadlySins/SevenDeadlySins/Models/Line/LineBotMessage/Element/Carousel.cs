using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SevenDeadlySins.Models.Line.LineBotMessage.Element
{
    public class Carousel
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("contents")]
        public Bubble[] Contents { get; set; }
    }
}
