using MathCombination.CombinationData;
using MathForGames.BasicGameData;
using System.Collections.Generic;

namespace GameFruityForce
{
    public class CombinationFruityForce : Combination
    {
        /// <summary>
        /// Transformiše matricu za igru 'FruityForce40' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombinationFruityForce(MatrixFruityForce matrix, int numberOfLines, int bet, int level, byte addInfo, bool init = false, bool buyBonus = false)
        {
            var wildId = level > 0 ? 8 + level : 0;
            Matrix = new byte[5, 6];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 6; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                    if (Matrix[i, j] == 0)
                    {
                        Matrix[i, j] = (byte)wildId;
                    }
                }
            }

            GratisGame = false;
            NumberOfGratisGames = 0;

            TotalWin = 0;
            var linesInfo = new List<LineInfo>();
            for (var i = 1; i <= numberOfLines; i++)
            {
                var win = matrix.CalculateWinLine(i, level);
                if (win == 0)
                {
                    continue;
                }
                var lineInfo = new LineInfo
                {
                    Id = (byte)(i - 1),
                    Win = win * bet,
                    WinningElement = (byte)matrix.GetWinningElementForLine(i, 0, MatrixFruityForce.WinForWildFruityForce[level], win, GlobalData.GameLineTurbo)
                };
                lineInfo.WinningPosition = matrix.GetLine(i, GlobalData.GameLineTurbo).GetLinesPositions(GlobalData.GameLineTurbo, i, 0, lineInfo.WinningElement);
                TotalWin += lineInfo.Win;
                if (lineInfo.WinningElement == 0)
                {
                    lineInfo.WinningElement = (byte)wildId;
                }
                linesInfo.Add(lineInfo);
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
            WinFor2 = init ? 0 : addInfo;
            AdditionalInformation = (byte)(init ? 0 : ((addInfo + 1) % 230));
            if (buyBonus)
            {
                AdditionalInformation = (byte)MatrixFruityForce.GetGamesNeededForLevel(level);
            }
        }
    }
}
