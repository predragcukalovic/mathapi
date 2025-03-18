using MathCombination.CombinationData;
using System.Collections.Generic;

namespace Game20FireCash
{
    public class Combination20FireCash : Combination
    {
        /// <summary>
        /// Transformiše matricu za igru 'FireCash20' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="bet">Ulog</param>
        /// <param name="numberOfLines"></param>
        public void MatrixToCombination20FireCash(Matrix20FireCash matrix, int bet, int numberOfLines)
        {
            GratisGame = false;
            NumberOfGratisGames = 0;
            Matrix = new byte[5, 5];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }

            CreateEmptyArray(MultiplyFor2);
            CreateEmptyArray(GratisGamesValues);
            CreateEmptyArray(GratisGamesPositions);

            TotalWin = 0;
            var linesInfo = new List<LineInfo>();
            for (var i = 1; i <= numberOfLines; i++)
            {
                var win = matrix.CalculateWinLine(i);
                if (win == 0)
                {
                    continue;
                }
                var lineInfo = new LineInfo
                {
                    Id = (byte)(i - 1),
                    Win = win * bet,
                    WinningElement = (byte)matrix.GetWinningElementForLine(i, 1, Matrix20FireCash.WinForWild20FireCash, win, Matrix20FireCash.GameLine20FireCash)
                };
                lineInfo.WinningPosition = matrix.GetLinePositions(i, lineInfo.WinningElement);
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            var scatterWin = matrix.GetScatterWin();
            if (scatterWin > 0)
            {
                var lineInfo = new LineInfo
                {
                    WinningPosition = matrix.GetScatterPositionsArray(),
                    Id = EXTRA_LINE,
                    Win = scatterWin * bet * numberOfLines,
                    WinningElement = 0
                };
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
        }

        public static Combination GetCombination20FireCash(int numberOfLines, int bet)
        {
            var matrixArray = Matrix20FireCash.GetMatixArray();
            var matrix = new Matrix20FireCash();
            matrix.FromMatrixArray20FireCash(matrixArray);
            var combination = new Combination20FireCash();
            combination.MatrixToCombination20FireCash(matrix, bet, numberOfLines);
            return combination;
        }

        /// <summary>
        /// Transformiše matricu za igru 'Redstone20FruitsFrenzy' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombination20FruitFrenzy(Matrix20FireCash matrix, int bet)
        {
            GratisGame = false;
            NumberOfGratisGames = 0;
            Matrix = new byte[5, 5];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }

            CreateEmptyArray(MultiplyFor2);
            CreateEmptyArray(GratisGamesValues);
            CreateEmptyArray(GratisGamesPositions);

            TotalWin = 0;
            var linesInfo = new List<LineInfo>();
            for (var i = 1; i <= 20; i++)
            {
                var win = matrix.CalculateWinLine(i);
                if (win == 0)
                {
                    continue;
                }
                var lineInfo = new LineInfo
                {
                    Id = (byte)(i - 1),
                    Win = win * bet / 10,
                    WinningElement = (byte)matrix.GetWinningElementForLine(i, 1, Matrix20FireCash.WinForWild20FireCash, win, Matrix20FireCash.GameLine20FireCash)
                };
                lineInfo.WinningPosition = matrix.GetLinePositions(i, lineInfo.WinningElement);
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            var scatterWin = matrix.GetScatterWin();
            if (scatterWin > 0)
            {
                var lineInfo = new LineInfo
                {
                    WinningPosition = matrix.GetScatterPositionsArray(),
                    Id = EXTRA_LINE,
                    Win = scatterWin * bet * 2,
                    WinningElement = 0
                };
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
        }

        public static Combination GetCombination20FruitFrenzy(int bet)
        {
            var matrixArray = Matrix20FireCash.GetMatixArray();
            var matrix = new Matrix20FireCash();
            matrix.FromMatrixArray20FireCash(matrixArray);
            var combination = new Combination20FireCash();
            combination.MatrixToCombination20FruitFrenzy(matrix, bet);
            return combination;
        }
    }
}
