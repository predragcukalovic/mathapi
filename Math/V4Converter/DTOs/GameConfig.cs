using Newtonsoft.Json;
using System.ComponentModel;

namespace V4Converter.DTOs
{
    public class GameConfig
    {
        public string GameName { get; set; }
        public int NumberOfReels { get; set; }
        [DefaultValue(3)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public int NumberOfRows { get; set; }
        [DefaultValue(false)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool BuyBonusEnabled { get; set; }
        public int[][] FakeReels { get; set; }
        [DefaultValue(null)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public int[][] BonusFakeReels { get; set; }
        [DefaultValue(null)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public int[][] RespinFakeReels { get; set; }
        public int NumberOfSymbols { get; set; }
        public int[,] WinForLines { get; set; }
        public int NumberOfLines { get; set; }
        public string LineType { get; set; }
        [DefaultValue(null)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public int[] PlayLines { get; set; }
        public int[,] NonWinningCombinationMatrix { get; set; }
        [DefaultValue(null)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public int[,] NonWinningCombinationMatrixBonus { get; set; }
        [DefaultValue(0)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public int AdditionalArrayLength { get; set; }
        [DefaultValue("Default")]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string ExtraRowStrategy { get; set; }
        [DefaultValue(2)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public int ExtraRowStrategyLow { get; set; }
        [DefaultValue(6)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public int ExtraRowStrategyHigh { get; set; }
        [DefaultValue("Default")]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string WinLineType { get; set; }
        [DefaultValue("Default")]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string WinSymbolType { get; set; }
        [DefaultValue("Default")]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string MatrixType { get; set; }
        [DefaultValue(null)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string[] ExtraFields { get; set; }
        [DefaultValue(null)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string BonusSymbolStrategy { get; set; }
        [DefaultValue(null)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string DoubleWinStrategy { get; set; }
        [DefaultValue(null)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string WildExpandStrategy { get; set; }
        public string RecallStrategy { get; set; }
        [DefaultValue(false)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool OmitExtraLineNoWin { get; set; }
        [DefaultValue(null)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public int[] MissingSymbols { get; set; }
        [DefaultValue(null)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public int[] LinesToOmit { get; set; }
        [DefaultValue(false)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool UseRecallForWins { get; set; }
        [DefaultValue(null)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string GameLineModifier { get; set; }
        [DefaultValue("Default")]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string AdditionalArrayStrategy { get; set; }

        [DefaultValue("Default")]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string MatrixModifier { get; set; }

        [DefaultValue(0)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public decimal Rtp { get; set; }

        [DefaultValue("Position")]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string WildExpandSource { get; set; }

        public int[] ExtraConfigArray { get; set; }
    }
}
