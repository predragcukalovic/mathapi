using MathBaseProject.BaseMathData;
using MathBaseProject.StructuresV3;
using MathForUnicornGames.BasicUnicornData;

namespace MathForUnicornGames.Game20MegaFlames
{
    public class Matrix20MegaFlames : Matrix
    {
        #region Constructor
        public Matrix20MegaFlames()
            : base(5)
        {

        }
        #endregion

        #region Public properties

        public static int[] PlayLines = { 20 };

        public static readonly int[,] WinForLines20MegaFlames =
        {
            {0, 0, 0, 0, 0},
            {0, 20, 160, 600, 4000 },
            {0, 0, 10, 40, 200 },
            {0, 0, 10, 40, 200 },
            {0, 0, 5, 20, 80 },
            {0, 0, 5, 20, 80 },
            {0, 0, 4, 10, 40 },
            {0, 0, 4, 10, 40 },
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}
        };

        public static readonly int[] WinForScatter20MegaFlames = { 0, 0, 2, 10, 50 };

        #endregion

        /// <summary>
        /// Računa dobitak linije. Wild symbol za igru je  postavljen kao 0.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, UnicornGlobalData.GameLineShifted).CalculateLineWin(WinForLines20MegaFlames, null, -1, 1);
        }

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[5][];

            fakeReels[0] = new[]
            {
                0,3,2,4,5,6,7,1,3,2,4,5,6,7,0,3,2,4,5,6,7,0,3,2,4,5,6,7,0,3,2,4,5,6,7,0,3,2,4,5,6,7,5,3,2,4,5,6,7,4,5
            };
            fakeReels[1] = new[]
            {
                0,3,2,4,5,6,7,1,3,2,4,5,6,7,1,3,2,4,5,6,7,1,3,2,4,5,6,7,1,3,2,4,5,6,7,0,3,2,4,5,6,7,5,3,2,4,5,6,7,4,5
            };
            fakeReels[2] = new[]
            {
                0,3,2,4,5,6,7,1,3,2,4,5,6,7,1,3,2,4,5,6,7,1,3,2,4,5,6,7,1,3,2,4,5,6,7,0,3,2,4,5,6,7,5,3,2,4,5,6,7,4,5
            };
            fakeReels[3] = new[]
            {
                7,3,2,4,5,6,7,1,3,2,4,5,6,7,1,3,2,4,5,6,7,1,3,2,4,5,6,7,1,3,2,4,5,6,7,7,3,2,4,5,6,7,5,3,2,4,5,6,7,4,5
            };
            fakeReels[4] = new[]
            {
                7,3,2,4,5,6,7,1,3,2,4,5,6,7,1,3,2,4,5,6,7,1,3,2,4,5,6,7,1,3,2,4,5,6,7,7,3,2,4,5,6,7,5,3,2,4,5,6,7,4,5
            };
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
                return WinForScatter20MegaFlames;
            }

            var coefficients = new int[5];

            for (var i = 0; i < 5; i++)
            {
                coefficients[i] = WinForLines20MegaFlames[id, i];
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

        /// <summary>
        /// Returns symbols configuration.
        /// </summary>
        /// <returns></returns>
        private static HelpSymbolConfigV3<object>[] GetHelpSymbolConfigV3()
        {
            var symbols = new HelpSymbolConfigV3<object>[8];

            symbols[0] = new HelpSymbolConfigV3<object>
            {
                id = 0,
                features = new[] { HelpSymbolFeatureV3.Scatter },
                extra = new HelpSymbolExtraV3(),
                coefficients = GetSymbolCoefficients(0)
            };

            for (var i = 1; i < 8; i++)
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

        private static HelpLineConfigV3[] GetHelpLineConfigV3()
        {
            var lines = new HelpLineConfigV3[20];
            for (var i = 0; i < 20; i++)
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

        /// <summary>
        /// Konstruiše matricu na osnovu dvodimenzionalnog niza za igru 20 Mega Flames.
        /// </summary>
        /// <param name="matrix"></param>
        public void FromMatrixArray20MegaFlames(int[,] matrix)
        {
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    SetElement(i, j, matrix[i, j]);
                }
            }
        }

        /// <summary>
        /// Daje niz pozicija za simbol.
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public new byte[] GetPositionsArray(int symbol)
        {
            var positions = new byte[5];
            var index = 0;
            for (var i = 0; i < 5; i++)
            {
                for (var j = 1; j < 4; j++)
                {
                    if (GetElement(i, j) == symbol)
                    {
                        positions[index++] = (byte)(j * 5 + i);
                    }
                }
            }
            for (; index < 5; index++)
            {
                positions[index] = 255;
            }
            return positions;
        }


        public new int GetNumberOfElement(int element)
        {
            var counter = 0;
            for (var i = 0; i < 5; i++)
            {
                for (var j = 1; j < 4; j++)
                {
                    if (element == GetElement(i, j))
                    {
                        counter++;
                    }
                }
            }
            return counter;
        }

        public int GetScatterWin(int scatterId, int[] winForScatter)
        {
            var n = GetNumberOfElement(scatterId);
            return n == 0 ? 0 : winForScatter[n - 1];
        }
    }
}

