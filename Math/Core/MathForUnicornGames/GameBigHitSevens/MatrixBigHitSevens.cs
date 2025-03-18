using MathBaseProject.BaseMathData;
using MathBaseProject.StructuresV3;
using MathForUnicornGames.BasicUnicornData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathForUnicornGames.GameBigHitSevens
{
    public class MatrixBigHitSevens : Matrix
    {
        public MatrixBigHitSevens() : base(5) { }

        #region Public properties

        public static int[] PlayLines = { 10 };
        public static readonly int[,] WinForLinesBigHitSevens =
        {
            {0, 0, 400, 1000, 10000 },
            {0, 0, 100, 400, 1000},
            {0, 0, 100, 400, 1000},
            {0, 0, 25, 100, 250},
            {0, 0, 25, 100, 250 },
            {0, 0, 10, 50, 200 },
            {0, 0, 10, 50, 200}
        };

        #endregion

        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, UnicornGlobalData.GameLineShifted).CalculateLineWin(WinForLinesBigHitSevens, null, -1, 1);
        }

        /// <summary>
        /// Konstruiše matricu na osnovu dvodimenzionalnog niza za igru BigHitSevens.
        /// </summary>
        /// <param name="matrix"></param>
        public void FromMatrixArrayBigHitSevens(int[,] matrix)
        {
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    SetElement(i, j, matrix[i, j]);
                }
            }
        }

        #region Struct V3

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 0, 5, 5, 5, 5, 0, 1, 1, 3, 1, 0, 4, 4, 4, 4, 0, 3, 3, 1, 3, 0, 5, 5, 2, 5, 0, 3, 1, 3, 3, 0, 2, 2, 3, 3, 0, 4, 4, 4, 4, 1, 1, 6, 6, 5, 6, 5, 5, 3, 3, 3, 4, 4, 4, 4 };
            fakeReels[1] = new[] { 0, 4, 4, 4, 4, 0, 5, 2, 5, 5, 0, 6, 6, 3, 6, 6, 4, 4, 4, 4, 5, 5, 2, 5, 2, 2, 3, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5, 6, 5, 6, 6, 2, 2, 2, 5, 5, 5, 6, 5, 6, 6, 1, 3, 1 };
            fakeReels[2] = new[] { 0, 6, 5, 6, 6, 0, 5, 5, 5, 5, 0, 6, 6, 2, 6, 0, 4, 4, 4, 4, 0, 2, 2, 3, 2, 0, 1, 1, 3, 1, 0, 3, 3, 2, 2, 0, 2, 1, 1, 6, 6, 6, 6, 6, 6, 2, 2, 4, 4, 4, 4, 4, 4 };
            fakeReels[3] = new[] { 0, 6, 6, 2, 6, 0, 5, 5, 5, 5, 0, 6, 6, 1, 6, 0, 4, 4, 4, 4, 0, 5, 5, 5, 5, 0, 6, 3, 6, 6, 0, 1, 3, 1, 1, 0, 3, 3, 1, 3, 3, 6, 6, 6, 6, 6, 6, 4, 4, 4, 4, 1, 2, 2 };
            fakeReels[4] = new[] { 0, 4, 4, 4, 4, 0, 6, 2, 6, 6, 0, 5, 5, 5, 5, 0, 6, 6, 1, 6, 0, 2, 2, 3, 2, 0, 1, 1, 3, 1, 0, 2, 3, 2, 2, 0, 5, 5, 1, 1, 5, 5, 3, 4, 4, 4, 4, 5, 5, 2, 5, 2, 5, 5, 5, 4, 4, 4, 4 };

            return fakeReels;
        }

        /// <summary>
        /// Vraća niz koeficijenata za id simbola.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int[] GetSymbolCoefficients(int id)
        {
            var coefficients = new int[5];
            for (var i = 0; i < 5; i++)
            {
                coefficients[i] = WinForLinesBigHitSevens[id, i];
            }
            return coefficients;
        }

        public static HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)96.0,
                symbols = GetHelpSymbolConfigV3(),
                lines = GetHelpLineConfigV3()
            };

            return helpV3;
        }

        private static HelpSymbolConfigV3<object>[] GetHelpSymbolConfigV3()
        {
            var symbols = new HelpSymbolConfigV3<object>[7];

            for (var i = 0; i < 7; i++)
            {
                symbols[i] = new HelpSymbolConfigV3<object>
                {
                    id = i,
                    features = new[] { HelpSymbolFeatureV3.Regular },
                    extra = new HelpSymbolExtraV3(),
                    coefficients = GetSymbolCoefficients(i)
                };
            }
            return symbols;
        }

        public static HelpLineConfigV3[] GetHelpLineConfigV3()
        {
            var lines = new HelpLineConfigV3[10];
            for (var i = 0; i < 10; i++)
            {
                var pos = new int[5];
                for (var j = 0; j < 5; j++)
                {
                    pos[j] = UnicornGlobalData.GameLineShifted[i, j] - 1;
                }
                lines[i] = new HelpLineConfigV3 { id = i, positions = pos };
            }

            return lines;
        }

        #endregion
    }
}
