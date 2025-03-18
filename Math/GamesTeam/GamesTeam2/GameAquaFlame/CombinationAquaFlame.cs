using MathCombination.CombinationData;
using MathForGames.BasicGameData;
using System.Collections.Generic;

namespace GameAquaFlame
{
    public class CombinationAquaFlame : Combination
    {
        /// <summary>
        /// Transformiše matricu za igru 'AquaFlame' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        /// <param name="aquaFlame">0 za aqua, 1 za flame</param>
        public void MatrixToCombinationAquaFlame(MatrixAquaFlame matrix, int numberOfLines, int bet, int aquaFlame)
        {
            GratisGame = false;
            NumberOfGratisGames = 0;
            WinFor2 = aquaFlame;
            FillMatrixArray(matrix);

            var wild = aquaFlame == 1 ? 8 : 0;
            var winForWild = aquaFlame == 1 ? MatrixAquaFlame.WinForWilds2AquaFlame : MatrixAquaFlame.WinForWilds1AquaFlame;
            var scatter = aquaFlame == 1 ? 9 : 1;

            TotalWin = 0;
            var linesInfo = new List<LineInfo>();
            for (var i = 1; i <= numberOfLines; i++)
            {
                var win = matrix.CalculateWinLine(i, aquaFlame);
                if (win == 0)
                {
                    continue;
                }
                var lineInfo = new LineInfo
                {
                    Id = (byte)(i - 1),
                    Win = win * bet,
                    WinningElement = (byte)matrix.GetWinningElementForLine(i, wild, winForWild, win, GlobalData.GameLineExtra)
                };
                lineInfo.WinningPosition = matrix.GetLine(i, GlobalData.GameLineExtra).GetLinesPositions(GlobalData.GameLineExtra, i, wild, lineInfo.WinningElement);
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            var scatterWin = matrix.GetNoLineWin(scatter, scatter == 9 ? MatrixAquaFlame.WinForScatters2AquaFlame : MatrixAquaFlame.WinForScatters1AquaFlame);
            if (scatterWin > 0)
            {
                var lineInfo = new LineInfo
                {
                    WinningPosition = matrix.GetPositionsArray(scatter),
                    Id = EXTRA_LINE,
                    Win = scatterWin * bet * numberOfLines,
                    WinningElement = (byte)scatter
                };
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
        }
    }
}
