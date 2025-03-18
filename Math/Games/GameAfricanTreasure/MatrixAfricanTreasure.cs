using MathBaseProject.BaseMathData;
using RNGUtils.RandomData;

namespace GameAfricanTreasure
{
    public class MatrixAfricanTreasure : Matrix
    {
        public MatrixAfricanTreasure() : base(5)
        {
        }

        private static readonly int[][] _Reels =
        {
            new[] { 6, 4, 4, 4, 5, 6, 5, 5, 2, 3, 3, 3, 0, 0, 0, 3, 3, 7, 7, 1, 7, 1, 1, 1, 4 },
            new[] { 6, 3, 3, 3, 7, 7, 6, 7, 0, 0, 0, 0, 2, 4, 4, 4, 4, 1, 1, 1, 5, 1, 5, 5, 3 },
            new[] { 6, 3, 0, 0, 0, 2, 1, 1, 1, 6, 1, 7, 7, 4, 7, 4, 4, 4, 5, 5, 0, 5, 3, 3, 3 },
            new[] { 6, 1, 1, 1, 1, 6, 1, 4, 4, 4, 2, 5, 5, 3, 3, 3, 0, 3, 0, 0, 0, 4, 4, 7, 7 },
            new[] { 6, 4, 4, 3, 3, 3, 7, 7, 1, 2, 1, 1, 1, 2, 1, 4, 4, 4, 3, 6, 0, 0, 0, 5, 5 }
        };

        #region Public fields

        public static readonly int[,] WinForLinesAfricanTreasure =
        {
            {0, 5, 20, 50, 200},
            {0, 0, 20, 50, 200},
            {0, 0, 0, 0, 0},
            {0, 0, 20, 50, 200},
            {0, 0, 20, 50, 200},
            {0, 0, 50, 200, 500},
            {0, 0, 100, 1000, 5000},
            {0, 0, 50, 200, 500},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}
        };
        public static readonly int[] WinForScattersAfricanTreasure = { 0, 0, 2, 10, 50 };

        public static readonly int[,] GameLineAfricanTreasure =
        {
            {2, 2, 2, 2, 2},
            {1, 1, 1, 1, 1},
            {3, 3, 3, 3, 3},
            {1, 2, 3, 2, 1},
            {3, 2, 1, 2, 3}
        };

        #endregion

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber">Broj linije.</param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GameLineAfricanTreasure).CalculateLineWin(WinForLinesAfricanTreasure, null, -1, 1);
        }

        /// <summary>
        /// Konstruiše matricu na osnovu dvodimenzionalnog niza.
        /// </summary>
        /// <param name="matrix"></param>
        public void FromMatrixArrayAfricanTreasure(int[,] matrix)
        {
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    SetElement(i, j, matrix[i, j]);
                }
            }
        }

        public int GetWinningElementForLine(int lineNumber)
        {
            return GetLine(lineNumber, GameLineAfricanTreasure).GetElement(0);
        }

        public byte[] GetLinePositions(int lineNumber, int element)
        {
            var positionsArray = new byte[5];
            var i = 0;
            var line = GetLine(lineNumber, GameLineAfricanTreasure);
            while (i < 5 && (line.GetElement(i) == element))
            {
                positionsArray[i] = (byte)((GameLineAfricanTreasure[lineNumber - 1, i] - 1) * 5 + i);
                i++;
            }
            for (; i < 5; i++)
            {
                positionsArray[i] = 255;
            }
            return positionsArray;
        }

        /// <summary>
        /// Daje dobitak za sketere.
        /// </summary>
        /// <returns></returns>
        public new int GetScatterWin()
        {
            var count = 0;
            for (var i = 0; i < 5; i++)
            {
                for (var j = 1; j < 4; j++)
                {
                    if (GetElement(i, j) == 2)
                    {
                        count++;
                    }
                }
            }
            return count == 0 ? 0 : WinForScattersAfricanTreasure[count - 1];
        }

        /// <summary>
        /// Daje niz pozicija za sketere.
        /// </summary>
        /// <returns></returns>
        public byte[] GetScatterPositionsArray()
        {
            var positions = new byte[5];
            var index = 0;
            for (var i = 0; i < 5; i++)
            {
                for (var j = 1; j < 4; j++)
                {
                    if (GetElement(i, j) == 2)
                    {
                        positions[index++] = (byte)((j - 1) * 5 + i);
                    }
                }
            }
            for (; index < 5; index++)
            {
                positions[index] = 255;
            }
            return positions;
        }

        public static int[,] GetMatixArray()
        {
            var mat = new int[5, 5];
            for (var i = 0; i < 5; i++)
            {
                var l = _Reels[i].Length;
                var p = SoftwareRng.Next(l);
                for (var j = 0; j < 5; j++)
                {
                    mat[i, j] = _Reels[i][(p + j) % l];
                }
            }
            return mat;
        }
    }
}
