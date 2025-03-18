using GameWild5;
using MathCombination.CombinationData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombinationExtras.ConversionData.V3Conversion.V3ConversionTeam1
{
    public class GameWild5Conversion
    {
        public static Combination3 GetNonWinningCombination(int bet)
        {
            var matrixArray = new[,] { { 8, 8, 8, 8, 8 }, { 7, 7, 7, 7, 7 }, { 6, 6, 6, 6, 6 } };
            var matrix = new MatrixWild5();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationWild5();
            combination.MatrixToCombination(matrix, bet);
            return combination;
        }
    }
}