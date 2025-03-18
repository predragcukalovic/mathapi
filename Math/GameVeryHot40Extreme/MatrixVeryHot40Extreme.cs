using GameVeryHot5Extreme;
using MathBaseProject.StructuresV3;
using MathForGames.BasicGameData;

namespace GameVeryHot40Extreme
{
    public class MatrixVeryHot40Extreme : MatrixVeryHot5Extreme
    {
        #region Public properties

        public static readonly int[,] WinForLinesVeryHot40Extreme =
        {
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 1, 5, 20, 300},
            {0, 0, 4, 10, 50},
            {0, 0, 4, 10, 50},
            {0, 0, 2, 5, 20},
            {0, 0, 1, 3, 10},
            {0, 0, 1, 3, 10},
            {0, 0, 1, 3, 10},
            {0, 0, 1, 3, 10},
            {0, 0, 0, 0, 0},
        };

        public new static int[] PlayLines = { 4 };


        #endregion
        #region Public methods
        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber">Broj linije.</param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            var m = 1;
            var l = GetLine(lineNumber, GlobalData.GameLineExtra);
            if (l.GetElement(2) == 1)
            {
                l.SetElement(2, 0);
                m = 2;
            }
            return l.CalculateLineWin(WinForLinesVeryHot40Extreme, WinForWildVeryHotExtreme, 0, m);
        }

        /// <summary>
        /// Vraća niz koeficijenata za id simbola.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public new static int[] GetSymbolCoefficients(int id)
        {
            if (id < 2)
            {
                return WinForWildVeryHotExtreme;
            }
            if (id == 10)
            {
                return WinForScatterVeryHotExtreme;
            }
            var coefficients = new int[5];
            for (var i = 0; i < 5; i++)
            {
                coefficients[i] = WinForLinesVeryHot40Extreme[id, i];
            }
            return coefficients;
        }

        #endregion

        #region V3 structs


        public new static HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)96.45,
                symbols = GetHelpSymbolConfigV3(),
                lines = GetHelpLineConfigV3()
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

        private static HelpLineConfigV3[] GetHelpLineConfigV3()
        {
            const int numberOfLines = 40;
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
