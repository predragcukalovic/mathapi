using MathBaseProject.BaseMathData;
using MathBaseProject.StructuresV3;
using MathForUnicornGames.BasicUnicornData;

namespace MathForUnicornGames.GameHavanaDice
{
    public class MatrixHavanaDice : Matrix
    {
        #region Constructor
        public MatrixHavanaDice()
            : base(5)
        {

        }
        #endregion

        #region Public properties

        public static int[] PlayLines = { 10 };

        public static readonly int[] WinForWildHavanaDice = { 0, 0, 50, 500, 2000 };

        public static readonly int[,] WinForLinesHavanaDice =
        {
            {0, 0, 0, 0, 0},
            {0, 0, 25, 100, 500 },
            {0, 0, 25, 100, 500 },
            {0, 0, 20, 75, 250 },
            {0, 0, 20, 75, 250 },
            {0, 0, 20, 75, 250 },
            {0, 0, 15, 50, 200 },
            {0, 0, 15, 50, 200 },
            {0, 0, 5, 25, 100 },
            {0, 0, 5, 25, 100 },
            {0, 0, 5, 25, 100 },
            {0, 0, 5, 25, 100 },
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}
        };

        #endregion

        /// <summary>
        /// Računa dobitak linije. Wild symbol za igru je  postavljen kao 0.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, UnicornGlobalData.GameLineShifted).CalculateLineWin(WinForLinesHavanaDice, WinForWildHavanaDice, 0, 1);
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
                11, 4, 11, 4, 6, 6, 1, 6, 6, 6, 13, 0, 0, 0, 0, 12, 11, 11, 11, 3, 10, 10, 10, 4, 10, 4, 7, 7, 7, 5, 2,
                10, 10, 2, 5, 9, 9, 4, 9, 4, 4, 1, 9, 1, 9, 7, 7, 7, 3, 11, 11, 11, 5, 10, 10, 10, 3, 7, 7, 7, 7, 3, 8,
                8, 8, 5, 11, 5, 11, 11, 11, 8, 8, 5, 10, 10, 10, 3
            };
            fakeReels[1] = new[]
            {
                2, 6, 6, 4, 2, 7, 7, 7, 1, 7, 1, 10, 10, 10, 5, 8, 8, 8, 5, 11, 11, 4, 11, 4, 11, 13, 0, 0, 0, 0, 12, 9,
                9, 4, 9, 4, 7, 2, 10, 10, 5, 6, 6, 6, 5, 2, 8, 8, 8, 3, 9, 9, 5, 9, 5, 5, 7, 7, 2, 1, 2, 6, 6, 6, 3, 10,
                10, 10, 13, 0, 0, 0, 12, 11, 11, 2, 8, 8, 5, 6, 6, 6, 3, 11, 11, 11, 3, 3, 7, 4, 9, 7, 3, 8, 8, 3, 5
            };
            fakeReels[2] = new[]
            {
                5, 1, 5, 9, 9, 1, 9, 1, 11, 11, 11, 4, 1, 4, 9, 9, 9, 3, 1, 4, 3, 1, 8, 8, 13, 0, 0, 0, 0, 12, 7, 7, 7,
                3, 2, 9, 9, 2, 9, 2, 3, 10, 10, 10, 2, 3, 9, 9, 9, 1, 9, 2, 3, 6, 6, 6, 3, 6, 3, 1, 3, 3, 1, 5, 3, 1, 9,
                9, 9, 1, 9, 1, 3, 4, 1, 4
            };
            fakeReels[3] = new[]
            {
                7, 5, 3, 4, 1, 8, 1, 8, 8, 3, 8, 4, 6, 6, 4, 11, 4, 11, 11, 13, 0, 0, 0, 0, 12, 9, 9, 9, 2, 4, 2, 7, 7,
                2, 5, 2, 5, 7, 2, 7, 7, 4, 2, 11, 11, 4, 2, 10, 2, 10, 10, 13, 0, 0, 0, 12, 11, 11, 5, 11, 5, 11, 3, 1,
                3, 4, 2, 4, 2, 10, 10, 10, 1, 9, 9, 4, 2, 4, 8, 8, 13, 0, 0, 12, 10, 10, 2, 1, 4, 2, 1, 9, 9, 9
            };
            fakeReels[4] = new[]
            {
                2, 4, 10, 10, 13, 0, 0, 0, 12, 7, 7, 7, 5, 1, 5, 9, 6, 6, 6, 2, 11, 11, 2, 8, 8, 8, 2, 7, 2, 7, 7, 7,
                13, 0, 0, 0, 0, 12, 9, 9, 3, 11, 11, 3, 11, 3, 1, 6, 6, 6, 3, 1, 3, 7, 5, 8, 5, 8, 8, 1, 4, 2, 4, 1, 7,
                7, 7, 2, 4, 10, 10, 5, 10, 3, 2, 1, 3, 8, 8, 8, 4, 1, 3, 9, 9, 5, 3, 2, 7, 7, 2, 6, 6, 6
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
                return WinForWildHavanaDice;
            }

            var coefficients = new int[5];

            for (var i = 0; i < 5; i++)
            {
                coefficients[i] = WinForLinesHavanaDice[id, i];
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
        /// Trenutno implementacija za Wild symbol(0) dodeljuje Regular
        /// 12 je broj simbola u ovoj igri, vrednosti simbola 0-11
        /// simboli 12 i 13 predstavljaju granicnik za simbol 0
        /// sekvenca od 6 wild simbola izgleda 13,0,0,0,0,12
        /// </summary>
        /// <returns></returns>
        private static HelpSymbolConfigV3<object>[] GetHelpSymbolConfigV3()
        {
            var symbols = new HelpSymbolConfigV3<object>[12];
            for (var i = 0; i < 12; i++)
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

        /// <summary>
        /// Konstruiše matricu na osnovu dvodimenzionalnog niza za igru Havana Dice.
        /// </summary>
        /// <param name="matrix"></param>
        public void FromMatrixArrayHavanaDice(int[,] matrix)
        {
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    SetElement(i, j, matrix[i, j]);
                }
            }
        }
    }
}

