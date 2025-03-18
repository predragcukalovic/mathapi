using MathBaseProject.StructuresV3;
using MathForGames.BasicGameData;
using MathForGames.GameTurboHot40;

namespace MathForGames.GameWildClover40
{
    public class MatrixWildClover40 : MatrixTurboHot40
    {
        #region Public properties

        public static readonly int[] GratisNumber = { 10, 20, 30 };
        public static int[] PlayLines = { 40 };

        #endregion

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLineTurbo).CalculateLineWin(LineWinsForGames.WinForLinesWildClover40, LineWinsForGames.WinForWildsWildClover40, 0, 1);
        }

        #region V3 structs

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 10, 10, 10, 10, 0, 0, 0, 0, 8, 8, 8, 8, 2, 2, 2, 2, 7, 7, 7, 7, 1, 1, 1, 1, 9, 9, 9, 9, 4, 4, 4, 4, 3, 3, 3, 3, 11, 6, 6, 6, 6, 5, 5, 5, 5 };
            fakeReels[1] = new[] { 8, 8, 8, 8, 0, 0, 0, 0, 7, 7, 7, 7, 5, 5, 5, 5, 2, 2, 2, 2, 1, 1, 1, 1, 4, 4, 4, 4, 9, 9, 9, 9, 3, 3, 3, 3, 11, 10, 10, 10, 10, 6, 6, 6, 6 };
            fakeReels[2] = new[] { 7, 7, 7, 7, 0, 0, 0, 0, 9, 9, 9, 9, 2, 2, 2, 2, 1, 1, 1, 1, 10, 10, 10, 10, 4, 4, 4, 4, 8, 8, 8, 8, 3, 3, 3, 3, 11, 6, 6, 6, 6, 5, 5, 5, 5 };
            fakeReels[3] = new[] { 9, 9, 9, 9, 0, 0, 0, 0, 10, 10, 10, 10, 6, 6, 6, 6, 2, 2, 2, 2, 7, 7, 7, 7, 1, 1, 1, 1, 4, 4, 4, 4, 3, 3, 3, 3, 11, 5, 5, 5, 5, 8, 8, 8, 8 };
            fakeReels[4] = new[] { 10, 10, 10, 10, 0, 0, 0, 0, 8, 8, 8, 8, 2, 2, 2, 2, 1, 1, 1, 1, 4, 4, 4, 4, 7, 7, 7, 7, 3, 3, 3, 3, 9, 9, 9, 9, 11, 6, 6, 6, 6, 5, 5, 5, 5 };

            return fakeReels;
        }

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja u bonusu
        /// </summary>
        /// <returns></returns>
        public static int[][] GetGratisFakeReels()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 10, 10, 10, 10, 0, 0, 0, 0, 8, 8, 8, 8, 2, 2, 2, 2, 7, 7, 7, 7, 0, 1, 1, 1, 1, 9, 9, 9, 9, 4, 4, 4, 4, 3, 3, 3, 3, 11, 6, 6, 6, 6, 5, 5, 5, 5, 0 };
            fakeReels[1] = new[] { 8, 8, 8, 8, 0, 0, 0, 0, 7, 7, 7, 7, 5, 5, 5, 5, 2, 2, 2, 2, 0, 1, 1, 1, 1, 4, 4, 4, 4, 9, 9, 9, 9, 3, 3, 3, 3, 11, 10, 10, 10, 10, 6, 6, 6, 6, 0 };
            fakeReels[2] = new[] { 7, 7, 7, 7, 0, 0, 0, 0, 9, 9, 9, 9, 2, 2, 2, 2, 1, 1, 1, 1, 0, 10, 10, 10, 10, 4, 4, 4, 4, 8, 8, 8, 8, 3, 3, 3, 3, 11, 6, 6, 6, 6, 5, 5, 5, 5, 0 };
            fakeReels[3] = new[] { 9, 9, 9, 9, 0, 0, 0, 0, 10, 10, 10, 10, 6, 6, 6, 6, 2, 2, 2, 2, 0, 7, 7, 7, 7, 1, 1, 1, 1, 4, 4, 4, 4, 3, 3, 3, 3, 11, 5, 5, 5, 5, 8, 8, 8, 8, 0 };
            fakeReels[4] = new[] { 10, 10, 10, 10, 0, 0, 0, 0, 8, 8, 8, 8, 2, 2, 2, 2, 1, 1, 1, 1, 0, 4, 4, 4, 4, 7, 7, 7, 7, 3, 3, 3, 3, 9, 9, 9, 9, 11, 6, 6, 6, 6, 5, 5, 5, 5, 0 };

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
                return LineWinsForGames.WinForWildsWildClover40;
            }
            var coefficients = new int[5];
            for (var i = 0; i < 5; i++)
            {
                coefficients[i] = LineWinsForGames.WinForLinesWildClover40[id, i];
            }
            return coefficients;
        }

        public static HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)95.3,
                symbols = GetHelpSymbolConfigV3(),
                lines = GetHelpLineConfigV3()
            };

            return helpV3;
        }

        private static HelpSymbolConfigV3<object>[] GetHelpSymbolConfigV3()
        {
            var symbols = new HelpSymbolConfigV3<object>[12];
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
            symbols[11] = new HelpSymbolConfigV3<object>
            {
                id = 11,
                extra = new HelpSymbolExtraV3(),
                coefficients = GetSymbolCoefficients(11),
                features = new[] { HelpSymbolFeatureV3.FreeSpin }
            };

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
