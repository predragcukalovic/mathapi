namespace Papi.GameServer.Math.Contracts.Requests
{
    public class GenerateCombinationRequest
    {
        public long Bet { get; set; }
        public int NumberOfLines { get; set; }
        public int GratisGamesLeft { get; set; }
        public byte AdditionalInformation { get; set; }
        public string AdditionalArray { get; set; }
        public object GameDataobj { get; set; }
        public int PlayerChoice { get; set; }
        public bool ReturnJson { get; set; }
        public long Credits { get; set; }
        public bool IsCurrentGameGratis { get; set; }
        public bool ShouldGenerateSmBetResult { get; set; }
        public string SmBetResultInBonus { get; set; }
        public string Rtp { get; set; }
    }
}