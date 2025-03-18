using Papi.GameServer.Utils.Enums;
using MathCombination.CombinationData;
using System.Collections.Generic;

namespace V4Converter.DTOs
{
    public class GenericCombination : ICombination
    {
        public byte[,] Matrix { get; protected set; }
        public bool GratisGame { get; protected set; }
        public int NumberOfGratisGames { get; protected set; }
        public int WinFor2 { get; protected set; }
        public byte[] MultiplyFor2 { get; protected set; }
        public byte[] MultiplyFor2Alpinist { get; protected set; }
        public byte[] PositionFor2 { get; protected set; }
        public byte NumberOfWinningLines { get; protected set; }
        public LineInfo[] LinesInformation { get; protected set; }
        public byte[] GratisGamesValues { get; protected set; }
        public byte[] GratisGamesPositions { get; protected set; }
        public byte AdditionalInformation { get; protected set; }
        public int TotalWin { get; protected set; }
        public byte[] AdditionalArray { get; protected set; }
        public List<ICombination> CascadeList { get; protected set; }

        public GenericCombination(GameConfig gameConfig, int gratisGamesLeft)
        {
            int numberOfReels = gameConfig.NumberOfReels;
            int numberOfRows;
            switch (gameConfig.ExtraRowStrategy)
            {
                case "Included":
                case "IncludedMiddle":
                case "WLC":
                case "MysticJungle":
                    numberOfRows = gameConfig.NumberOfRows + 2;
                    break;
                default:
                    numberOfRows = gameConfig.NumberOfRows;
                    break;
            }
            Matrix = new byte[numberOfReels, numberOfRows];
            PositionFor2 = new byte[numberOfReels];
            MultiplyFor2 = new byte[4];
            MultiplyFor2Alpinist = new byte[numberOfReels];
            GratisGamesValues = new byte[numberOfReels];
            GratisGamesPositions = new byte[numberOfReels];
            CascadeList = null;
            LinesInformation = new LineInfo[0];
            AdditionalArray = new byte[gameConfig.AdditionalArrayLength];
            var matrix = GetNonWinningMatrix(gameConfig, gratisGamesLeft);
            CreateEmptyArray(PositionFor2);
            CreateEmptyArray(MultiplyFor2);
            CreateEmptyArray(MultiplyFor2Alpinist);
            CreateEmptyArray(GratisGamesPositions);
            CreateEmptyArray(GratisGamesValues);
            PopulateAdditionalArray(AdditionalArray, gameConfig);
            for (var i = 0; i < numberOfReels; i++)
            {
                for (var j = 0; j < numberOfRows; j++)
                {
                    Matrix[i, j] = (byte)matrix[i, j];
                }
            }
        }

        public bool IsBonus(Games game, bool gratisGame, byte addInfo, byte[] addArray)
        {
            return false;
        }

        public byte[] ToByteArray(Games game, int numOfGratisGames, long newCreditMeter, bool isCurrentGameGratis, ICombination combination)
        {
            throw new System.NotImplementedException();
        }

        public object ToGameData(Games game, int numOfGratisGames, long newCreditMeter, bool isCurrentGameGratis, ICombination combination, bool json)
        {
            throw new System.NotImplementedException();
        }

        public object ToJson(Games game, int numOfGratisGames, long newCreditMeter, bool isCurrentGameGratis, ICombination combination)
        {
            throw new System.NotImplementedException();
        }

        private void CreateEmptyArray(byte[] array)
        {
            for (var i = 0; i < array.Length; i++)
            {
                array[i] = 255;
            }
        }

        private int[,] GetNonWinningMatrix(GameConfig gameConfig, int gratisGamesLeft)
        {
            if (gratisGamesLeft > 0)
            {
                return gameConfig.NonWinningCombinationMatrixBonus;
            }
            else
            {
                return gameConfig.NonWinningCombinationMatrix;
            }
        }
        private void PopulateAdditionalArray(byte[] array, GameConfig gameConfig)
        {
            switch (gameConfig.AdditionalArrayStrategy)
            {
                case "ZeroFill":
                    return;
                default:
                    CreateEmptyArray(array);
                    break;
            }
        }
    }
}
