using MathBaseProject.StructuresV3;
using MathForGames.BasicGameData;
using MathForGames.GameTurboHot40;

namespace GameWildHeat40
{
    public class MatrixWildHeat40 : MatrixTurboHot40
    {
        public static int[] PlayLines = { 40 };

        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLineTurbo).CalculateLineWin(LineWinsForGames.WinForLinesTurboHot40, LineWinsForGames.WinForWildsTurboHot40, 0, 1);
        }

        public void FixWilds()
        {
            if (GetNumberOfElement(2) == 0)
            {
                return;
            }
            var arr = new int[5, 6];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 6; j++)
                {
                    var elem = GetElement(i, j + 5);
                    arr[i, j] = elem == 2 ? 0 : elem;
                }
            }
            FromMatrixArray(arr);
        }

        #region V3 structs

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 5, 5, 5, 5, 4, 4, 4, 4, 7, 7, 7, 7, 0, 0, 0, 0, 6, 6, 6, 6, 1, 1, 1, 1, 2, 2, 2, 2, 5, 5, 5, 5, 1, 1, 1, 1, 7, 7, 7, 7, 4, 4, 4, 4, 3, 3, 3, 3, 6, 6, 6, 6, 5, 5, 5, 5, 6, 6, 6, 6, 0, 0, 0, 0, 5, 5, 5, 5, 7, 7, 7, 7, 4, 4, 4, 4, 2, 2, 2, 2, 3, 3, 3, 3, 6, 6, 6, 6, 1, 1, 1, 1 };
            fakeReels[1] = new[] { 6, 6, 6, 6, 5, 5, 5, 5, 1, 1, 1, 1, 4, 4, 4, 4, 7, 7, 7, 7, 3, 3, 3, 3, 4, 4, 4, 4, 0, 0, 0, 0, 5, 5, 5, 5, 3, 3, 3, 3, 2, 2, 2, 2, 5, 5, 5, 5, 6, 6, 6, 6, 3, 3, 3, 3, 0, 0, 0, 0, 6, 6, 6, 6, 5, 5, 5, 5, 7, 7, 7, 7, 1, 1, 1, 1, 5, 5, 5, 5, 2, 2, 2, 2, 7, 7, 7, 7, 1, 1, 1, 1, 5, 5, 5, 5, 2, 2, 2, 2, 4, 4, 4, 4 };
            fakeReels[2] = new[] { 4, 4, 4, 4, 7, 7, 7, 7, 2, 2, 2, 2, 3, 3, 3, 3, 5, 5, 5, 5, 7, 7, 7, 7, 2, 2, 2, 2, 4, 4, 4, 4, 0, 0, 0, 0, 3, 3, 3, 3, 7, 7, 7, 7, 5, 5, 5, 5, 4, 4, 4, 4, 7, 7, 7, 7, 0, 0, 0, 0, 6, 6, 6, 6, 1, 1, 1, 1, 5, 5, 5, 5, 2, 2, 2, 2, 1, 1, 1, 1, 4, 4, 4, 4, 6, 6, 6, 6, 7, 7, 7, 7, 5, 5, 5, 5, 1, 1, 1, 1 };
            fakeReels[3] = new[] { 7, 7, 7, 7, 5, 5, 5, 5, 4, 4, 4, 4, 0, 0, 0, 0, 7, 7, 7, 7, 1, 1, 1, 1, 4, 4, 4, 4, 5, 5, 5, 5, 2, 2, 2, 2, 6, 6, 6, 6, 7, 7, 7, 7, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 0, 0, 0, 0, 6, 6, 6, 6, 3, 3, 3, 3, 7, 7, 7, 7, 1, 1, 1, 1, 3, 3, 3, 3, 5, 5, 5, 5, 6, 6, 6, 6, 1, 1, 1, 1, 5, 5, 5, 5, 6, 6, 6, 6, 2, 2, 2, 2 };
            fakeReels[4] = new[] { 3, 3, 3, 3, 2, 2, 2, 2, 5, 5, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7, 0, 0, 0, 0, 4, 4, 4, 4, 5, 5, 5, 5, 7, 7, 7, 7, 4, 4, 4, 4, 6, 6, 6, 6, 5, 5, 5, 5, 2, 2, 2, 2, 4, 4, 4, 4, 6, 6, 6, 6, 0, 0, 0, 0, 5, 5, 5, 5, 3, 3, 3, 3, 2, 2, 2, 2, 6, 6, 6, 6, 1, 1, 1, 1, 5, 5, 5, 5, 0, 0, 0, 0, 4, 4, 4, 4, 7, 7, 7, 7, 3, 3, 3, 3, 5, 5, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7 };

            return fakeReels;
        }

        /// <summary>
        /// Vraća niz koeficijenata za id simbola.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int[] GetSymbolCoefficients(int id)
        {
            if (id == 0 || id == 2)
            {
                return LineWinsForGames.WinForWildsTurboHot40;
            }
            var coefficients = new int[5];
            for (var i = 0; i < 5; i++)
            {
                coefficients[i] = LineWinsForGames.WinForLinesTurboHot40[id, i];
            }
            return coefficients;
        }

        public static HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)97.0,
                symbols = GetHelpSymbolConfigV3(),
                lines = GetHelpLineConfigV3()
            };

            return helpV3;
        }

        private static HelpSymbolConfigV3<object>[] GetHelpSymbolConfigV3()
        {
            var symbols = new HelpSymbolConfigV3<object>[8];
            for (var i = 0; i < 8; i++)
            {
                symbols[i] = new HelpSymbolConfigV3<object>
                {
                    id = i,
                    extra = new HelpSymbolExtraV3(),
                    coefficients = GetSymbolCoefficients(i),
                    features = new[] { HelpSymbolFeatureV3.Regular }
                };
            }

            return symbols;
        }

        private static HelpLineConfigV3[] GetHelpLineConfigV3()
        {
            var lines = new HelpLineConfigV3[40];
            for (var i = 0; i < 40; i++)
            {
                var pos = new int[5];
                for (var j = 0; j < 5; j++)
                {
                    pos[j] = GlobalData.GameLineTurbo[i, j];
                }
                lines[i] = new HelpLineConfigV3 { id = i, positions = pos };
            }

            return lines;
        }

        #endregion
    }
}
