using System.Collections.Generic;

namespace Papi.GameServer.Math.Contracts.Responses
{
    public class CombinationModel
    {
        public byte[][] Matrix { get; set; }
        public bool GratisGame { get; set; }
        public int NumberOfGratisGames { get; set; }
        public int WinFor2 { get; set; }
        public byte[] MultiplyFor2 { get; set; }
        public byte[] MultiplyFor2Alpinist { get; set; }
        public byte[] PositionFor2 { get; set; }
        public byte NumberOfWinningLines { get; set; }
        public LineInfoModel[] LinesInformation { get; set; }
        public byte[] GratisGamesValues { get; set; }
        public byte[] GratisGamesPositions { get; set; }
        public byte AdditionalInformation { get; set; }
        public int TotalWin { get; set; }
        public byte[] AdditionalArray { get; set; }
        public List<CombinationModel> CascadeList { get; set; }
    }
}
