using Newtonsoft.Json;

namespace MathCombination.CombinationData.CheatTool
{
    public class CheatToolData
    {
        #region Constructor
        internal CheatToolData()
        {
        }
        #endregion

        #region Public properties

        public bool UsingCheatTool { get; set; }
        public bool StoppingReelsNotUsingMatrix { get; set; }
        public int[,] NewMatrix { get ; set; }
        public int[] IndicesInReels { get; set; }

        #endregion

        #region From Json
        public static CheatToolData CheatToolFromJson(string json)
        {
            var cheatToolFromJson = JsonConvert.DeserializeObject<CheatToolData>(json);
            return cheatToolFromJson;
        }

        #endregion
    }
}
