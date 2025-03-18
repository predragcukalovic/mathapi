namespace Papi.GameServer.Math.Contracts.Requests
{
    public class CheatToolData
    {
        public bool UsingCheatTool { get; set; }
        public bool StoppingReelsNotUsingMatrix { get; set; }
        public int[,] NewMatrix { get; set; }
        public int[] IndicesInReels { get; set; }
        public int AdditionalInfo { get; set; }
        public bool TriggerJackpot { get; set; }
        public int JackpotType { get; set; }
        public int ExtraNumber { get; set; }
    }
}