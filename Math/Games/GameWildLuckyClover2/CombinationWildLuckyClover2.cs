using GameWildLuckyClover;
using MathCombination.CombinationData;
using RNGUtils.RandomData;
using System.Collections.Generic;

namespace GameWildLuckyClover2
{
    public class CombinationWildLuckyClover2 : Combination
    {
        /// <summary>
        /// Transformiše matricu za igru 'WildLuckyClover2' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGame"></param>
        /// <param name="addInfo"></param>
        public void MatrixToCombination(MatrixWildLuckyClover2 matrix, int numberOfLines, int bet, bool gratisGame, byte addInfo)
        {
            Matrix = new byte[5, 6];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 6; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }

            GratisGame = false;
            NumberOfGratisGames = 0;
            AdditionalInformation = 0;

            var scatNum = matrix.GetNumberOfElement(7);

            if (!gratisGame && scatNum >= 3)
            {
                GratisGame = true;
                NumberOfGratisGames = MatrixWildLuckyClover.FreeSpinsCount[scatNum - 3];
                AdditionalInformation = 0;
                var dist = new[] { 18, 17, 17, 16, 16, 16 };
                var rand = SoftwareRng.Next(100);
                var sum = 0;
                for (var i = 0; i < 6; i++)
                {
                    sum += dist[i];
                    if (sum > rand)
                    {
                        AdditionalInformation = (byte)(i + 1);
                        break;
                    }
                }
            }
            if (gratisGame)
            {
                AdditionalInformation = addInfo;
            }

            TotalWin = 0;
            var linesInfo = new List<LineInfo>();
            for (var i = 1; i <= numberOfLines; i++)
            {
                var win = matrix.CalculateWinLine(i, gratisGame ? AdditionalInformation : 0);
                if (win == 0)
                {
                    continue;
                }
                var lineInfo = new LineInfo
                {
                    Id = (byte)(i - 1),
                    Win = win * bet,
                    WinningElement = (byte)matrix.GetWinningElementForLine(i, gratisGame ? AdditionalInformation : 0)
                };
                lineInfo.WinningPosition = matrix.GetLinesPositions(i, gratisGame ? AdditionalInformation : 0, lineInfo.WinningElement);
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            if (GratisGame)
            {
                var lineInfo = new LineInfo
                {
                    WinningPosition = matrix.GetPositionsArray(7),
                    Id = EXTRA_LINE,
                    Win = 0,
                    WinningElement = 7
                };
                linesInfo.Add(lineInfo);
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
        }
    }
}
