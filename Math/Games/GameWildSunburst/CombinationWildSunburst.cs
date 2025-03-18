using MathCombination.CombinationData;
using RNGUtils.RandomData;
using System;
using System.Collections.Generic;

namespace GameWildSunburst
{
    public class CombinationWildSunburst : Combination
    {
        private void ShuffleListCheat(ref List<byte> list)
        {
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = (int)SoftwareRng.Next(n + 1);
                var t = list[k];
                list[k] = list[n];
                list[n] = t;
            }
        }

        private byte[] GetCheatOverthrows(MatrixWildSunburst matrix)
        {
            var cheatOverthrowLeft = new List<byte>();
            var cheatOverthrowRight = new List<byte>();
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    var elem = matrix.GetElement(i, j);
                    if (elem > 10 || elem == 0)
                    {
                        Matrix[i, j] = 3;
                        var countCheat = Math.Max(elem - 10, 1);
                        for (var k = 0; k < countCheat; k++)
                        {
                            if (i <= 1)
                            {
                                cheatOverthrowLeft.Add((byte)(j * 5 + i));
                            }
                            if (i >= 3)
                            {
                                cheatOverthrowRight.Add((byte)(j * 5 + i));
                            }
                        }
                    }
                }
            }
            var diff = cheatOverthrowRight.Count - cheatOverthrowLeft.Count;
            if ((diff == 0 || diff == 1) && cheatOverthrowRight.Count <= 6)
            {
                ShuffleListCheat(ref cheatOverthrowLeft);
                ShuffleListCheat(ref cheatOverthrowRight);
                var cheatOverCount = cheatOverthrowLeft.Count;
                var n = 2 * cheatOverCount + diff;
                var overthrows = new byte[n];
                for (var i = 0; i < cheatOverCount; i++)
                {
                    overthrows[2 * i] = cheatOverthrowRight[i];
                    overthrows[2 * i + 1] = cheatOverthrowLeft[i];
                }
                if (diff == 1)
                {
                    overthrows[n - 1] = cheatOverthrowRight[cheatOverCount];
                }
                return overthrows;
            }
            return null;
        }

        /// <summary>
        /// Transformiše matricu za igru 'WildSunburst' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGame"></param>
        public void MatrixToCombinationWildSunburst(MatrixWildSunburst matrix, int numberOfLines, int bet, bool gratisGame, bool cheatTool = false)
        {
            FillMatrixArray(matrix);
            var numSket = matrix.GetNumberOfElement(9);
            GratisGame = numSket > 2;
            NumberOfGratisGames = GratisGame ? MatrixWildSunburst.GRATIS_GAMES : 0;
            var overthrows = new byte[0];

            if (gratisGame)
            {
                if (cheatTool)
                {
                    overthrows = GetCheatOverthrows(matrix);
                }
                if (!cheatTool || overthrows == null)
                {
                    var n = MatrixWildSunburst.GetOverthrowsNumber();
                    overthrows = new byte[n];
                    for (var i = 0; i < n; i++)
                    {
                        var field = (int)SoftwareRng.Next(6);
                        var fieldRow = field % 2 + (i % 2 == 0 ? 3 : 0);
                        var fieldReel = field / 2;
                        var oldField = matrix.GetElement(fieldRow, fieldReel);
                        var newField = oldField < 11 ? 11 : oldField + 1;
                        matrix.SetElement(fieldRow, fieldReel, newField);
                        overthrows[i] = (byte)(fieldReel * 5 + fieldRow);
                    }
                }
            }

            TotalWin = 0;
            var linesInfo = new List<LineInfo>();
            for (var i = 1; i <= numberOfLines; i++)
            {
                var win = matrix.CalculateWinLine(i, gratisGame, out int winElem, out byte[] winPos);
                if (win == 0)
                {
                    continue;
                }
                var lineInfo = new LineInfo
                {
                    Id = (byte)(i - 1),
                    Win = win * bet,
                    WinningElement = (byte)winElem,
                    WinningPosition = winPos
                };
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            var scatWin = matrix.GetNoLineWin(9, MatrixWildSunburst.WinForScatterWildSunburs);
            if (scatWin > 0)
            {
                var lineInfo = new LineInfo
                {
                    WinningPosition = matrix.GetPositionsArray(9),
                    Id = EXTRA_LINE,
                    Win = scatWin * bet * numberOfLines,
                    WinningElement = 9
                };
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            if (gratisGame)
            {
                var lineInfo = new LineInfo
                {
                    WinningPosition = overthrows,
                    Id = 253,
                    Win = 0,
                    WinningElement = 0
                };
                linesInfo.Add(lineInfo);
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
        }
    }
}
