using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SevenDeadlySins.Models.Line.LineBotMessage.Element
{
    public class FlexFooter
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("layout")]
        public string Layout { get; set; }

        [JsonProperty("spacing")]
        public string Spacing { get; set; }

        [JsonProperty("contents")]
        public Content[] Contents { get; set; }

        [JsonProperty("flex")]
        public long Flex { get; set; }
    }
}
