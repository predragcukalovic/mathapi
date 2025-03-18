using MathCombination.CombinationData;
using MathForGames.BasicGameData;
using System.Linq;

namespace GameAmazingJoker
{
    public class CombinationAmazingJoker : Combination
    {
        /// <summary>
        /// Transformiše matricu za igru 'AmazingJoker' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombinationAmazingJoker(MatrixAmazingJoker matrix, int numberOfLines, int bet, bool gratisGame)
        {
            Matrix = new byte[5, 6];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 6; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }

            var numSket = matrix.GetNumberOfElement(11);
            GratisGame = numSket > 2;
            if (GratisGame)
            {
                NumberOfGratisGames = MatrixAmazingJoker.NumberOfGratisGames[numSket - 3];
            }

            CreateLinesInformationsTurbo(matrix, numberOfLines, bet, 0, MatrixAmazingJoker.WinForWildAmazingJoker, GlobalData.GameLineTurbo);

            if (GratisGame)
            {
                var li = LinesInformation.ToList();
                li.Add(new LineInfo { Id = EXTRA_LINE, Win = 0, WinningElement = 11, WinningPosition = matrix.GetPositionsArray(11) });
                LinesInformation = li.ToArray();
            }
        }
    }
}
