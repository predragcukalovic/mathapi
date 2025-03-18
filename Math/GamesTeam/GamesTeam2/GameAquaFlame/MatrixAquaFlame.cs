using MathBaseProject.BaseMathData;
using MathBaseProject.StructuresV3;
using MathForGames.BasicGameData;
using RNGUtils.RandomData;

namespace GameAquaFlame
{
    public class MatrixAquaFlame : Matrix
    {
        private static readonly int[][][] _Reels =
        {
            new[]
            {
                new[]{7,6,6,6,0,0,0,5,5,5,3,3,3,4,4,4,6,6,7,7,7,7,3,3,3,5,5,5,6,6,6,2,2,2,7,7,7,1,4,4,4,6,6,6,2,2,2,1,7,7,7,2,2,2,5,5,5,3,3,3,4,4,4,6,6,6,3,3,3,6,6,6,7,7},
                new[]{6,7,7,7,3,3,3,5,5,5,4,4,4,3,3,3,7,7,7,4,4,4,7,7,7,6,6,6,4,4,4,0,0,0,6,6,6,7,7,7,5,5,5,2,2,2,7,7,7,1,5,5,5,2,2,2,7,7,7,1,3,3,3,6,6,6,5,5,5,2,2,2,4,4,4,6,6},
                new[]{5,5,4,4,4,6,6,6,3,3,3,5,5,5,4,4,4,7,7,7,5,5,5,3,3,3,0,0,0,4,4,4,5,5,5,7,7,7,3,3,3,5,5,5,6,6,6,2,2,2,7,7,7,1,2,2,2,3,3,3,6,6,6,5,5,5,7,7,7,2,2,2,6,6,6,1,3,3,3,5},
                new[]{7,6,6,6,1,5,5,5,7,7,3,3,3,5,5,5,2,2,2,3,3,3,7,7,7,0,0,0,6,6,6,4,4,4,5,5,5,2,2,2,4,4,4,7,7,7,6,6,6,5,5,5,4,4,4,3,3,3,7,7,7,1,6,6,6,5,5,5,4,4,4,6,6,6,2,2,2,7,7},
                new[]{7,3,3,3,1,5,5,5,4,4,4,7,7,7,2,2,2,6,6,6,4,4,4,7,7,7,6,6,6,5,5,5,3,3,3,7,7,7,5,5,5,3,3,3,6,6,6,0,0,0,7,7,7,4,4,4,6,6,6,2,2,2,7,7,7,3,3,3,6,6,6,2,2,2,5,5,5,1,7,7}

            },
            new[]
            {
                new[]{10,10,10,10,10,10,10,11,11,11,11,11,11,10,10,10,10,10,10,11,11,11,11,11,11,11,11,8,8,8,8,13,13,13,13,13,13,11,11,11,11,11,11,14,14,14,14,14,14,14,9,13,13,13,13,13,13,13,14,14,14,14,14,14,14,9,11,11,11,11,14,14,14,14,14,14,13,13,13,13,13,13,13,13,13,13,13,14,14,14,14,15,15,15,15,9,12,12,12,12,12},
                new[]{13,13,13,13,13,13,13,13,15,15,15,15,15,13,13,13,13,13,8,8,8,15,15,15,15,15,15,9,13,13,13,13,13,12,12,12,12,12,12,15,15,15,15,15,15,11,11,11,11,11,12,12,12,12,12,12,14,14,14,11,11,11,11,11,11,9,12,12,12,12,14,14,14,14,14,14,9,10,10,10},
                new[]{13,13,13,13,13,13,8,8,8,8,8,15,15,15,15,15,15,9,10,10,10,10,10,10,10,15,15,15,15,15,15,12,12,12,12,12,12,10,10,10,10,10,10,15,15,15,15,15,15,15,10,10,10,10,10,10,12,12,12,12,12,12,15,15,15,15,15,15,12,12,12,10,10,10,9,12,12,12,12,10,10,10,12,12,12,12,12,11,11,11,11,9,14,14,14,14,14},
                new[]{12,12,12,12,12,12,11,11,11,11,11,9,13,13,13,13,13,13,12,12,12,12,11,11,11,11,11,12,12,12,12,12,13,13,13,13,13,15,15,15,15,15,8,8,8,13,13,13,13,13,9,15,15,15,15,15,15,15,15,15,14,14,14,14,14,15,15,15,15,15,14,14,14,14,14,9,10,10,10},
                new[]{12,12,12,12,10,10,10,10,10,13,13,13,13,13,13,13,13,13,14,14,14,14,14,14,11,11,11,11,11,11,11,10,10,10,10,10,10,10,12,12,12,12,12,12,12,12,13,13,13,13,13,13,13,8,8,8,8,8,8,10,10,10,10,10,10,10,11,11,11,11,11,11,9,10,10,10,10,10,10,14,14,14,14,14,14,14,15,15,15,15,15,15,14,14,14,14,14,9,15,15,15,15,14,14,14,14,14,14}
            }
        };

        public static readonly int[,] WinForLinesAquaFlame =
        {
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 20, 80, 400},
            {0, 0, 20, 40, 200},
            {0, 0, 20, 40, 200},
            {0, 0, 10, 20, 100},
            {0, 0, 10, 20, 100},
            {0, 0, 10, 20, 100},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 50, 200, 1000},
            {0, 0, 20, 40, 200},
            {0, 0, 20, 40, 200},
            {0, 0, 10, 20, 100},
            {0, 0, 10, 20, 100},
            {0, 0, 10, 20, 100}
        };
        public static readonly int[] WinForWilds1AquaFlame = { 0, 0, 40, 400, 1000 };
        public static readonly int[] WinForWilds2AquaFlame = { 0, 0, 50, 500, 10000 };
        public static readonly int[] WinForScatters1AquaFlame = { 0, 0, 5, 20, 500 };
        public static readonly int[] WinForScatters2AquaFlame = { 0, 0, 5, 20, 1000 };
        public static readonly int[] PlayLines = { 20 };

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber">Broj linije.</param>
        /// <param name="aquaFlame">0 za aqua, 1 za flame</param>
        /// <returns></returns>
        public int CalculateWinLine(int lineNumber, int aquaFlame)
        {
            if (aquaFlame == 1)
            {
                return GetLine(lineNumber, GlobalData.GameLineExtra).CalculateLineWin(WinForLinesAquaFlame, WinForWilds2AquaFlame, 8, 1);
            }
            return GetLine(lineNumber, GlobalData.GameLineExtra).CalculateLineWin(WinForLinesAquaFlame, WinForWilds1AquaFlame, 0, 1);
        }

        /// <summary>
        /// Daje matricu za Aqua ili Flame
        /// </summary>
        /// <param name="aquaFlame">0 za Aqua, 1 za Flame</param>
        /// <returns></returns>
        public static int[,] GetMatixArray(int aquaFlame)
        {
            var mat = new int[5, 3];
            for (var i = 0; i < 5; i++)
            {
                var l = _Reels[aquaFlame][i].Length;
                var p = SoftwareRng.Next(l);
                for (var j = 0; j < 3; j++)
                {
                    mat[i, j] = _Reels[aquaFlame][i][(p + j) % l];
                }
            }
            return mat;
        }

        #region V3 structs

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 5, 5, 5, 0, 0, 0, 7, 7, 7, 4, 4, 4, 3, 3, 3, 6, 6, 6, 5, 5, 5, 7, 7, 7, 7, 3, 3, 3, 4, 4, 4, 5, 5, 5, 2, 2, 2, 0, 0, 0, 7, 7, 7, 1, 6, 6, 6, 5, 5, 5, 2, 2, 2, 1, 7, 7, 7, 13, 13, 13, 8, 8, 8, 15, 15, 15, 12, 12, 12, 11, 11, 11, 14, 14, 14, 13, 13, 13, 15, 15, 15, 15, 11, 11, 11, 12, 12, 12, 13, 13, 13, 10, 10, 10, 8, 8, 8, 15, 15, 15, 9, 14, 14, 14, 13, 13, 13, 10, 10, 10, 9, 15, 15, 15 };
            fakeReels[1] = new[] { 7, 7, 7, 3, 3, 3, 4, 4, 4, 0, 0, 0, 6, 6, 6, 5, 5, 5, 6, 6, 6, 0, 0, 0, 5, 5, 5, 7, 7, 7, 4, 4, 4, 2, 2, 2, 7, 7, 7, 1, 4, 4, 4, 2, 2, 2, 7, 7, 7, 1, 3, 3, 3, 5, 5, 5, 15, 15, 15, 11, 11, 11, 12, 12, 12, 8, 8, 8, 14, 14, 14, 13, 13, 13, 14, 14, 14, 8, 8, 8, 13, 13, 13, 15, 15, 15, 12, 12, 12, 10, 10, 10, 15, 15, 15, 9, 12, 12, 12, 10, 10, 10, 15, 15, 15, 9, 11, 11, 11, 13, 13, 13 };
            fakeReels[2] = new[] { 6, 6, 6, 5, 5, 5, 0, 0, 0, 3, 3, 3, 1, 4, 4, 4, 6, 6, 6, 7, 7, 7, 4, 4, 4, 3, 3, 3, 0, 0, 0, 4, 4, 4, 1, 7, 7, 7, 3, 3, 3, 4, 4, 4, 5, 5, 5, 2, 2, 2, 7, 7, 7, 14, 14, 14, 13, 13, 13, 8, 8, 8, 11, 11, 11, 9, 12, 12, 12, 14, 14, 14, 15, 15, 15, 12, 12, 12, 11, 11, 11, 8, 8, 8, 12, 12, 12, 9, 15, 15, 15, 11, 11, 11, 12, 12, 12, 13, 13, 13, 10, 10, 10, 15, 15, 15 };
            fakeReels[3] = new[] { 5, 5, 5, 1, 4, 4, 4, 7, 7, 7, 3, 3, 3, 4, 4, 4, 2, 2, 2, 3, 3, 3, 1, 7, 7, 7, 0, 0, 0, 5, 5, 5, 4, 4, 4, 2, 2, 2, 6, 6, 6, 7, 7, 7, 5, 5, 5, 0, 0, 0, 13, 13, 13, 9, 12, 12, 12, 15, 15, 15, 11, 11, 11, 12, 12, 12, 10, 10, 10, 11, 11, 11, 9, 15, 15, 15, 8, 8, 8, 13, 13, 13, 12, 12, 12, 10, 10, 10, 14, 14, 14, 15, 15, 15, 13, 13, 13, 8, 8, 8 };
            fakeReels[4] = new[] { 3, 3, 3, 1, 4, 4, 4, 7, 7, 7, 0, 0, 0, 2, 2, 2, 5, 5, 5, 6, 6, 6, 6, 1, 7, 7, 7, 5, 5, 5, 4, 4, 4, 3, 3, 3, 7, 7, 7, 4, 4, 4, 3, 3, 3, 5, 5, 5, 0, 0, 0, 7, 7, 7, 11, 11, 11, 9, 12, 12, 12, 15, 15, 15, 8, 8, 8, 10, 10, 10, 13, 13, 13, 14, 14, 14, 14, 9, 15, 15, 15, 13, 13, 13, 12, 12, 12, 11, 11, 11, 15, 15, 15, 12, 12, 12, 11, 11, 11, 13, 13, 13, 8, 8, 8, 15, 15, 15 };
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
                return WinForWilds1AquaFlame;
            }
            if (id == 8)
            {
                return WinForWilds2AquaFlame;
            }
            if (id == 1)
            {
                return WinForScatters1AquaFlame;
            }
            if (id == 9)
            {
                return WinForScatters2AquaFlame;
            }
            var coefficients = new int[5];
            for (var i = 0; i < 5; i++)
            {
                coefficients[i] = WinForLinesAquaFlame[id, i];
            }
            return coefficients;
        }

        public static HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)96.5,
                symbols = GetHelpSymbolConfigV3(),
                lines = GetHelpLineConfigV3()
            };

            return helpV3;
        }

        private static HelpSymbolConfigV3<object>[] GetHelpSymbolConfigV3()
        {
            var symbols = new HelpSymbolConfigV3<object>[16];
            for (var i = 0; i < 16; i++)
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
            var lines = new HelpLineConfigV3[20];
            for (var i = 0; i < 20; i++)
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
