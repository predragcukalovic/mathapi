using GameTopHot5;
using MathCombination.CombinationData;

namespace CombinationExtras.ConversionData.V3Conversion.V3ConversionTeam3
{
    public class GameTopHot5Conversion : GameWild27Conversion
    {
        public new static Combination3 GetNonWinningCombination(int bet)
        {
            var matrixArray = new[,] { { 0, 1, 4, 7, 8 }, { 8, 5, 0, 6, 8 }, { 8, 3, 2, 8, 8 } };
            var matrix = new MatrixTopHot5();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationTopHot5();
            combination.MatrixToCombination(matrix, 5, bet);
            return combination;
        }
    }
}
