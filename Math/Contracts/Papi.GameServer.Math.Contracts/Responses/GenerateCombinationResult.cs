using System;

namespace Papi.GameServer.Math.Contracts.Responses
{
    public class GenerateCombinationResult
    {
        public byte[] AdditionalArray { get; set; }
        public bool IsBonusGame { get; set; }
        public object FrontendData { get; set; }
        public int NextNumberOfGratisGames { get; set; }
        public int NumberOfGratisGames { get; set; }
        public string SmBetResult { get; set; }
        public byte AdditionalInformation { get; set; }
        public bool HasWinFor2 { get; set; }
        public long Win { get; set; }
        public bool IsGratisGame { get; set; }
        public int NumberOfWinningLines { get; set; }
        public object AdditionalGameData { get; set; }
    }

    public class GenerateCombinationResultLog
    {
        public byte[] AdditionalArray { get; set; }
        public bool IsBonusGame { get; set; }
        public int NextNumberOfGratisGames { get; set; }
        public int NumberOfGratisGames { get; set; }
        public string SmBetResult { get; set; }
        public byte AdditionalInformation { get; set; }
        public bool HasWinFor2 { get; set; }
        public long Win { get; set; }
        public bool IsGratisGame { get; set; }
        public int NumberOfWinningLines { get; set; }

        public GenerateCombinationResultLog(GenerateCombinationResult generateCombinationResult)
        {
            this.AdditionalArray = generateCombinationResult.AdditionalArray;
            this.IsBonusGame = generateCombinationResult.IsBonusGame;
            this.NextNumberOfGratisGames = generateCombinationResult.NextNumberOfGratisGames;
            this.NumberOfGratisGames = generateCombinationResult.NumberOfGratisGames;
            this.SmBetResult = generateCombinationResult.SmBetResult;
            this.AdditionalInformation = generateCombinationResult.AdditionalInformation;
            this.HasWinFor2 = generateCombinationResult.HasWinFor2;
            this.Win = generateCombinationResult.Win;
            this.IsGratisGame = generateCombinationResult.IsGratisGame;
            this.NumberOfWinningLines = generateCombinationResult.NumberOfWinningLines;
        }
    }

}