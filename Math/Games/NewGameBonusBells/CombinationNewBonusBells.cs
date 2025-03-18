using GameBonusBells;
using MathCombination.CombinationData;
using MathForGames.BasicGameData;

namespace NewGameBonusBells
{
    public class CombinationNewBonusBells : Combination
    {
        /// <summary>
        /// Transformiše matricu za igru 'BonusBells' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        /// <param name="gratisGame"></param>
        public void MatrixToCombinationNew(MatrixNewBonusBells matrix, int numberOfLines, int bet, bool gratisGame)
        {
            if (gratisGame)
            {
                matrix.SetElement(0, 0, 0);
                matrix.SetElement(0, 1, 0);
                matrix.SetElement(0, 2, 0);
                matrix.SetElement(4, 0, 0);
                matrix.SetElement(4, 1, 0);
                matrix.SetElement(4, 2, 0);
            }
            FillMatrixArray(matrix);

            CreateEmptyArray(PositionFor2);
            GratisGame = matrix.GetNumberOfElement(9) >= 3 && !gratisGame;
            NumberOfGratisGames = GratisGame ? MatrixBonusBells.GRATIS_GAMES : 0;
            var nextPosition = 0;
            for (var i = 1; i < 4; i++)
            {
                var haveWild = false;
                for (var j = 0; j < 3; j++)
                {
                    if (matrix.GetElement(i, j) == 0)
                    {
                        PositionFor2[nextPosition++] = (byte)(j * 5 + i);
                        haveWild = true;
                    }
                }
                if (haveWild)
                {
                    matrix.SetElement(i, 0, 0);
                    matrix.SetElement(i, 1, 0);
                    matrix.SetElement(i, 2, 0);
                }
            }

            CreateLinesInformations(matrix, numberOfLines, bet, 1, 0, MatrixBonusBells.WinForWildBonusBells, GlobalData.GameLineExtra,
                matrix.GetNoLineWin(9, MatrixBonusBells.WinForGratisBonusBells), 9);
        }
    }
}
