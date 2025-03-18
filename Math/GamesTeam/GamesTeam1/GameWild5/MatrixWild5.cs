using GameWild27;
using MathBaseProject.StructuresV3;
using MathForGames.BasicGameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWild5
{
    public class MatrixWild5 : MatrixWild27
    {
        #region Private fields

        #endregion

        #region Public fields

        public static readonly int[] WinForWild5 = { 60, 40, 30, 20, 20, 10, 10, 10, 10 };

        #endregion

        #region Public methods

        public int GetLineWinForWild5(int lineNumber, out int winElem)
        {
            var line = new[] { Matrix[0, GlobalData.GameLineVegasHot[lineNumber - 1, 0] + 1], Matrix[1, GlobalData.GameLineVegasHot[lineNumber - 1, 1] + 1], Matrix[2, GlobalData.GameLineVegasHot[lineNumber - 1, 2] + 1] };
            winElem = line[0];
            if (line[1] != winElem)
            {
                return 0;
            }
            if (line[2] != winElem)
            {
                return 0;
            }

            return WinForWild5[winElem];
        }

        #endregion

        #region V3 structs

        /// <summary>
        /// Vraća niz koeficijenata za id simbola.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public new static int[] GetSymbolCoefficients(int id)
        {
            return new[] { 0, 0, WinForWild5[id] };
        }

        public byte[] GetWinningPositions(int lineNumber)
        {
            return new byte[] { (byte)(GlobalData.GameLineVegasHot[lineNumber - 1, 0] * 3), (byte)(GlobalData.GameLineVegasHot[lineNumber - 1, 1] * 3 + 1), (byte)(GlobalData.GameLineVegasHot[lineNumber - 1, 2] * 3 + 2) };
        }

        public new static HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)96.32,
                symbols = GetHelpSymbolConfigV3(),
                lines = GetHelpLineConfigV3()
            };

            return helpV3;
        }

        private static HelpSymbolConfigV3<object>[] GetHelpSymbolConfigV3()
        {
            var symbols = new HelpSymbolConfigV3<object>[9];
            for (var i = 0; i < 9; i++)
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
            var lines = new HelpLineConfigV3[5];
            for (var i = 0; i < 5; i++)
            {
                var pos = new int[3];
                for (var j = 0; j < 3; j++)
                {
                    pos[j] = GlobalData.GameLineVegasHot[i, j];
                }
                lines[i] = new HelpLineConfigV3 { id = i, positions = pos };
            }

            return lines;
        }

        #endregion
    }
}
