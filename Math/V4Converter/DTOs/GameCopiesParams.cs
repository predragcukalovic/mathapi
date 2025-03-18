
using Newtonsoft.Json;
using System.ComponentModel;

namespace V4Converter.DTOs
{
    public class GameCopiesParams
    {
        public string GameName { get; set; }
        public string CopyOf { get; set; }
        [DefaultValue(-1)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public int NumberOfLines { get; set; }
        [DefaultValue(-1)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public int PlayLines { get; set; }
    }
}
