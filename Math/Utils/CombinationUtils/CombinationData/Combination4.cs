using System.Collections.Generic;
using Papi.GameServer.Utils.Enums;
using Papi.GameServer.Utils.Logging;
using MathForGames.GameCrissCross;

namespace MathCombination.CombinationData
{
    public class Combination4 : ICombination
    {
        #region Constructor or Singleton implementation

        /// <summary>
        /// Konstruktor za kombinaciju
        /// </summary>
        public Combination4(byte additionalInformation)
        {
            AdditionalInformation = additionalInformation;
            Matrix = new byte[4, 3];
            PositionFor2 = new byte[5];
            MultiplyFor2 = new byte[4];
            MultiplyFor2Alpinist = new byte[5];
            GratisGamesValues = new byte[5];
            GratisGamesPositions = new byte[5];
            CascadeList = null;
        }

        #endregion

        #region Public properties

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

        #endregion

        #region Private methods

        /// <summary>
        /// Popunjava niz vrednostima 255
        /// </summary>
        /// <param name="array">Niz koji se popunjava</param>
        protected static void CreateEmptyArray(byte[] array)
        {
            for (var i = 0; i < array.Length; i++)
            {
                array[i] = 255;
            }
        }

        /// <summary>
        /// kreira dodatne informacije za linije.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="bet"></param>
        protected void CreateLinesInformations(MatrixCrissCross matrix, int bet)
        {
            TotalWin = 0;
            var linesInfo = new List<LineInfo>();
            var winningLines = matrix.GetAllWinningLines();

            foreach (var winningLine in winningLines)
            {
                var winOfLine = matrix.GetLine(winningLine).CalculateLineWin();
                var win = winOfLine * bet;
                var winningElement = (byte)matrix.GetWinningElementForLine(winningLine);
                var lineInfo = new LineInfo
                {
                    WinningPosition = new byte[5],
                    Id = (byte)winningLine,
                    Win = win,
                    WinningElement = winningElement
                };
                TotalWin += win;
                CreateWinningLinePositionsCrissCross(ref lineInfo.WinningPosition, winningElement, winningLine);
                linesInfo.Add(lineInfo);
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
        }

        /// <summary>
        /// Kreira pozicije za dobitne linije.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="winningElement"></param>
        /// <param name="lineNumber"></param>
        protected void CreateWinningLinePositionsCrissCross(ref byte[] p, int winningElement, int lineNumber)
        {
            p[0] = (byte)((lineNumber / 27) * 4);
            lineNumber %= 27;
            p[1] = (byte)((lineNumber / 9) * 4 + 1);
            lineNumber %= 9;
            p[2] = (byte)((lineNumber / 3) * 4 + 2);
            lineNumber %= 3;
            if (Matrix[3, lineNumber] != 0 && Matrix[3, lineNumber] != winningElement)
            {
                p[3] = 255;
            }
            else
            {
                p[3] = (byte)(lineNumber * 4 + 3);
            }
            p[4] = 255;
        }

        /// <summary>
        /// Kreira niz sa pozicijama elementa u matrici.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="element"></param>
        protected void CreatePositionArray(byte[] position, int element)
        {
            CreateEmptyArray(position);
            var index = 0;
            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (Matrix[i, j] == element)
                    {
                        position[index] = (byte)(j * 4 + i);
                        index++;
                    }
                }
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Transformiše matricu za igru 'CrissCross' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombination(MatrixCrissCross matrix, int bet)
        {
            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }

            GratisGame = false;
            NumberOfGratisGames = 0;
            WinFor2 = matrix.GetMysteryWin() * bet;
            CreateEmptyArray(MultiplyFor2);
            CreateEmptyArray(GratisGamesPositions);
            CreateEmptyArray(GratisGamesValues);
            CreatePositionArray(PositionFor2, 1);

            CreateLinesInformations(matrix, bet);
            TotalWin += WinFor2;
        }

        public byte[] ToByteArray(Games game, int numOfGratisGames, long newCreditMeter, bool isCurrentGameGratis, ICombination combination)
        {
            /*switch (game)
            {
                case Games.RollingDices81:
                    return GameCrissCrossConversion.ToByteArray(numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
                default:
                    Logger.LogError(ErrorCodes.Modules.ParameterLoader + ": " + "Error ToByteArray doesnt exist for: " + game);
                    return null;
            }*/
            return null;
        }

        public object ToJson(Games game, int numOfGratisGames, long newCreditMeter, bool isCurrentGameGratis, ICombination combination)
        {
            Logger.LogError("Error ToJSON doesnt exist for: " + game);
            return null;
        }

        /// <summary>
        /// Vraća objekat sa svim podacima za igru.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="numOfGratisGames"></param>
        /// <param name="newCreditMeter"></param>
        /// <param name="isCurrentGameGratis"></param>
        /// <param name="combination"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public object ToGameData(Games game, int numOfGratisGames, long newCreditMeter, bool isCurrentGameGratis,
            ICombination combination, bool json)
        {
            if (json)
            {
                return ToJson(game, numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
            }
            return ToByteArray(game, numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
        }

        /// <summary>
        /// Da li je trenutna igra bonus?
        /// </summary>
        /// <param name="game"></param>
        /// <param name="gratisGame"></param>
        /// <param name="addInfo"></param>
        /// <param name="addArray"></param>
        /// <returns></returns>
        public bool IsBonus(Games game, bool gratisGame, byte addInfo, byte[] addArray)
        {
            return false;
        }

        #endregion
    }
}
