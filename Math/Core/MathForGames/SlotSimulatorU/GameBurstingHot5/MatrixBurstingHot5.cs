using MathBaseProject.BaseMathData;
using MathBaseProject.StructuresV3;
using MathForGames.BasicGameData;

namespace MathForGames.GameBurstingHot5
{
    public class MatrixBurstingHot5 : Matrix
    {
        #region Public properties

        public static int[] PlayLines = { 5 };
        public static int[] PlayLines40 = { 40 };

        #endregion

        #region Public methods

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber">Broj linije.</param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLineExtra).CalculateLineWin(LineWinsForGames.WinForLinesBurstingHot5, LineWinsForGames.WinForWildBurstingHot5, 0, 1);
        }

        #endregion

        #region V3 structs

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 8, 8, 8, 4, 5, 5, 5, 10, 8, 8, 8, 3, 2, 4, 6, 6, 6, 1, 4, 7, 7, 7, 9, 4, 5, 5, 5, 6, 6, 6, 7, 7, 7, 10, 8, 8, 8, 3, 2, 4, 6, 6, 6 };
            fakeReels[1] = new[] { 7, 8, 9, 4, 5, 6, 6, 6, 7, 8, 8, 8, 1, 4, 2, 7, 6, 8, 5, 4, 3, 0, 4, 3, 2, 1, 4, 5, 5, 5, 6, 7, 7, 4, 2, 7, 6, 8, 5, 4, 3, 0 };
            fakeReels[2] = new[] { 10, 3, 4, 7, 2, 3, 6, 2, 0, 5, 2, 3, 8, 9, 3, 2, 5, 5, 5, 7, 7, 7, 2, 4, 1, 3, 2, 6, 6, 6, 8, 8, 8, 3, 4, 7, 2, 3, 6, 2, 0, 5, 2, 3, 8 };
            fakeReels[3] = new[] { 1, 2, 5, 4, 7, 5, 3, 2, 5, 1, 6, 4, 3, 8, 0, 6, 2, 9, 3, 5, 5, 5, 8, 8, 8, 7, 7, 7, 6, 6, 6, 4, 3, 0, 6, 2, 9, 3, 5, 5 };
            fakeReels[4] = new[] { 10, 3, 1, 5, 5, 5, 2, 4, 8, 3, 4, 7, 2, 3, 8, 8, 8, 4, 1, 2, 6, 6, 6, 9, 4, 3, 7, 7, 7, 2, 1, 10, 3, 1, 5, 5, 5, 2, 4 };

            return fakeReels;
        }

        /// <summary>
        /// Vraća niz koeficijenata za id simbola.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int[] GetSymbolCoefficients(int id)
        {
            if (id == 0)
            {
                return LineWinsForGames.WinForWildBurstingHot5;
            }
            if (id == 9)
            {
                return LineWinsForGames.WinForScatter1BurstingHot5;
            }
            if (id == 10)
            {
                return new[] { 0, 0, LineWinsForGames.WIN_FOR_SCATTER2_BURSTING_HOT5, 0, 0 };
            }
            var coefficients = new int[5];
            for (var i = 0; i < 5; i++)
            {
                coefficients[i] = LineWinsForGames.WinForLinesBurstingHot5[id, i];
            }
            return coefficients;
        }

        public static HelpConfigV3<object> GetHelpConfigV3(int numberOfLines = 5)
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)96.5,
                symbols = GetHelpSymbolConfigV3(),
                lines = GetHelpLineConfigV3(numberOfLines)
            };

            return helpV3;
        }

        private static HelpSymbolConfigV3<object>[] GetHelpSymbolConfigV3()
        {
            var symbols = new HelpSymbolConfigV3<object>[11];
            for (var i = 0; i < 11; i++)
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

        private static HelpLineConfigV3[] GetHelpLineConfigV3(int numberOfLines)
        {
            var lines = new HelpLineConfigV3[numberOfLines];
            for (var i = 0; i < numberOfLines; i++)
            {
                var pos = new int[5];
                for (var j = 0; j < 5; j++)
                {
                    pos[j] = GlobalData.GameLineExtra[i, j];
                }
                lines[i] = new HelpLineConfigV3 { id = i, positions = pos };
            }

            return lines;
        }

        #endregion
    }
}
