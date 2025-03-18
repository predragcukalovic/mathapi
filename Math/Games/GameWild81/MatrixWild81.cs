using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using RNGUtils.RandomData;
using System.Collections.Generic;

namespace GameWild81
{
    public class MatrixWild81
    {
        #region Private fields

        public static int[][][] _Reels =
{
    new[]{
        new[]{6,6,6,1,5,5,5,5,9,9,9,9,9,7,10,8,5,5,5,1,3,3,3,3,9,6,2,2,5,8,6,4,3,10,5,2,8,8,8,8,9,4,4,4,5,2,3,6,10,3,4,6,8,2,6,6,6,6,6,1,1,5,8,8,8,8,2,7,9,4,10,7,7,7,7,7,2,5,5,6,9,4,1,3,5,6,6,6,6,5,1,7,7,7,7,9,9,9,9,9,2,10,3,3,3,5,6,5,3,2,4,4,9,9,9,9,9,5,10,8,8,8,8,8,5 },
        new[]{5,3,4,9,4,10,6,8,5,2,6,9,9,9,9,9,1,10,3,3,3,2,5,5,5,5,5,0,7,7,7,7,1,2,5,10,3,5,4,0,6,3,2,6,6,6,6,6,5,5,5,5,5,8,8,8,8,8,0,5,5,10,2,4,7,6,4,0,10,9,9,9,9,6,5,8,2,5,7,7,7,2,9,9,9,9,7,10,6,5,3,3,3,3,2,10,1,1,2,6,0,5,3,8,6,9,5,10,8,8,8,8,4,4,4,4,4,5,7},
        new[]{5,9,6,2,5,8,9,9,9,9,9,1,5,8,10,0,8,5,5,5,5,5,6,6,6,6,6,7,7,7,7,7,5,4,4,4,4,4,2,8,8,8,8,8,6,4,9,4,3,10,5,5,5,0,3,5,9,9,9,9,7,7,7,10,3,4,6,6,6,6,2,9,0,6,4,3,5,1,3,3,3,3,3,5,6,2,5,7,8,9,10,8,8,8,8,8,3,3,3,9,9,9,9,10,5,2,9,9,9,9},
        new[]{3,7,7,7,5,2,9,8,3,5,5,5,5,10,8,5,2,5,10,3,5,5,5,7,7,7,10,6,6,6,5,3,3,3,5,4,6,9,1,2,7,7,7,9,9,9,9,9,5,3,4,9,4,6,10,8,8,8,8,8,5,6,4,3,4,4,4,1,2,8,5,4,4,4,4,8,8,8,8,9,9,9,9,7,3,2,9,1,6,5,5,5,5,2,6,9,2,3,6,6,6,6,6,8,8,8,8,2,3,10,2,7,7,7,9,9}
    },
    new[]{
        new[]{3,2,10,8,8,8,8,4,4,4,6,8,2,10,2,4,7,7,8,7,2,10,7,7,7,2,8,8,8,4,4,4,7,5,8,4,7,7,7,8,8,2,4,7,7,2,4,8,8},
        new[]{5,9,6,2,5,8,9,9,9,9,9,1,10,5,8,8,5,5,5,5,5,6,6,6,6,6,7,7,7,7,7,5,4,4,4,4,4,2,8,8,8,8,8,6,4,9,4,10,3,5,5,5,0,3,5,9,9,9,9,7,7,7,3,4,6,6,6,6,2,9,0,6,4,3,5,1,3,3,3,3,3,5,6,2,5,7,8,8,8,3,3,3,9,9,9,9,5,2,9,9,9,9},
        new[]{1,3,3,3,5,9,9,9,9,9,5,6,5,1,6,6,6,1,5,5,5,5,1,1,3,3,3,9,6,2,6,6,6,4,3,3,5,6,6,6,1,5,5,5,9,9,9},
        new[]{3,7,7,7,5,2,9,8,3,5,5,5,5,10,8,5,2,5,10,3,5,5,5,7,7,7,10,6,6,6,5,3,3,3,5,4,6,9,1,2,7,7,7,9,9,9,9,9,5,3,4,9,4,6,10,8,8,8,8,8,5,6,4,3,4,4,4,1,2,8,5,4,4,4,4,8,8,8,8,9,9,9,9,7,3,2,9,1,6,5,5,5,5,2,6,9,2,3,6,6,6,6,6,8,8,8,8,2,3,10,2,7,7,7}
    },
    new[]{
        new[]{6,6,6,1,5,9,9,9,9,9,7,10,8,5,1,3,3,3,3,9,6,2,2,5,8,6,4,3,10,5,2,8,8,9,4,10,3,4,6,8,2,6,6,6,6,6,1,1,5,8,2,7,9,7,10,7,7,7,7,7,2,5,5,6,9,4,1,3,5,6,6,6,6,5,1,7,7,7,7,9,9,9,9,9,2,10,3,3,3,5,6,5,3,2,4,4,9,9,9,9,9,5,10,8,8},
        new[]{5,5,5,3,4,9,4,10,6,8,5,2,6,9,5,1,2,5,5,5,5,5,7,7,1,2,5,5,5,4,6,3,2,5,10,5,5,5,5,8,8,8,8,8,5,5,5,5,8,8,8,4,7,6,4,9,4,6,5,8,2,5,5,5,2,9,9,7,7,6,5,3,3,3,3,2,5,3,8,6,5,5,5,8,8,8,8,4,4,4,4,4,5,7},
        new[]{5,9,6,2,5,8,9,9,9,9,9,1,5,8,10,8,8,5,5,5,5,6,6,6,6,6,7,7,7,7,7,5,4,9,4,9,4,4,4,2,8,8,5,8,8,8,6,4,9,4,3,3,3,5,9,9,9,9,7,10,7,7,4,4,6,6,6,6,2,6,6,4,3,5,1,3,3,3,3,3,5,6,2,5,7,8,8,8,8,8,3,3,3,9,9,9,9},
        new[]{3,7,7,7,5,2,9,8,3,5,5,5,5,10,8,5,2,5,10,3,5,5,5,7,7,7,10,6,6,6,5,3,3,3,5,4,6,9,1,2,7,7,7,9,9,9,9,9,5,3,4,9,4,6,10,8,8,8,8,8,5,6,4,3,4,4,4,1,2,8,5,4,4,4,4,8,8,8,8,9,9,9,9,7,3,2,9,1,6,5,5,5,5,2,6,9,2,3,6,6,6,6,6,8,8,8,8,2,3,10,2,7,7,7}
    },
    new[]{
        new[]{6,1,5,5,5,3,3,3,9,6,6,6,1,1,3,5,9,9,9,5,6,9,1,3,3,5,1,3,6,5,1,3,5,6,6,6,5,5,5,3,3,9,9,9,5,6,5,1,3,3},
        new[]{8,8,8,8,2,2,8,8,4,10,8,8,2,7,7,7,2,8,4,4,4,10,7,7,4,8,2,2,7,8,7,7,2,4,4,8,2,7,8,4},
        new[]{5,9,6,2,5,8,9,9,9,9,9,1,5,8,6,0,8,5,5,5,5,5,6,6,6,6,6,7,7,7,7,7,5,4,4,4,4,4,2,8,8,8,8,8,6,4,9,4,3,5,5,5,0,3,5,9,9,9,9,7,7,7,3,4,6,6,6,6,2,9,0,6,4,3,5,1,3,3,3,3,3,5,6,2,5,7,8,9,8,8,8,8,8,3,3,3,9,9,9,9,5,2,9,9,9,9},
        new[]{3,7,7,7,5,2,9,8,3,5,5,5,5,10,8,5,2,5,5,5,5,7,7,7,10,6,6,6,5,3,3,3,5,4,6,9,1,2,7,7,7,9,9,9,9,9,5,3,4,9,4,6,10,8,8,8,8,8,5,6,4,3,4,4,4,1,2,8,5,4,4,4,4,8,8,8,8,9,9,9,9,7,3,2,9,1,6,5,5,5,5,2,6,9,2,3,6,6,6,6,6,8,8,8,8,2,3,7,7,7}
    },
    new[]{
        new[]{8,8,8,2,6,6,6,4,8,2,7,7,7,2,4,4,4,6,6,2,2,4,4,6,8,8,8,2,7,7,8,7,4,4,2,6,6,6,4,4},
        new[]{5,3,4,9,4,6,8,5,2,6,9,9,9,9,9,1,10,3,3,3,2,5,5,5,5,5,0,7,7,7,7,1,2,5,10,3,5,4,0,6,3,2,6,6,6,6,6,5,5,5,5,5,8,8,8,8,8,0,5,5,2,4,7,6,4,0,10,9,9,9,9,6,5,8,2,5,7,7,7,2,9,9,9,9,7,10,6,5,3,3,3,3,2,10,1,1,2,6,0,5,3,8,6,9,5,8,8,8,8,4,4,4,4,4,5,7},
        new[]{1,3,3,3,5,9,9,9,9,5,10,3,3,9,5,5,5,9,9,9,3,10,5,1,3,5,5,3,3,9,9,1,1,5,5,9,9,5,1,3,5,5,9},
        new[]{3,7,7,7,5,2,9,8,3,5,5,5,5,8,8,5,4,4,9,4,3,5,5,5,7,7,7,5,6,6,6,5,3,3,3,5,4,6,9,1,2,7,7,7,9,9,9,9,9,5,3,4,9,4,6,8,8,8,8,8,5,6,4,3,4,4,4,1,2,8,5,4,4,4,4,8,8,8,8,9,9,9,9,7,3,2,9,1,6,5,5,5,5,2,6,9,2,3,6,6,6,6,6,8,8,8,8,2,3,7,7,7}
    }
};
        private static readonly double[] _ReelProbs = { 0.05, 0.2, 0.251, 0.2495, 0.2495 };

        protected readonly int[,] _Matrix;

        #endregion

        #region Constructors

        public MatrixWild81()
        {
            _Matrix = new int[4, 5];
        }

        #endregion

        #region Public fields

        public static readonly int[,] WinForLinesWild81 =
        {
            {0, 0},
            {200, 2000},
            {50, 250},
            {30, 125},
            {20, 75},
            {15, 50},
            {10, 40},
            {10, 40},
            {5, 20},
            {5, 20},
            {0, 0}
        };
        public static readonly int[] Mystery3Win = { 5, 10, 10, 20, 20, 30, 30, 30, 40, 40, 40, 50, 50, 50, 50, 60, 60, 60, 60, 70, 70, 70, 70, 80, 80, 80, 80, 100, 100, 100, 100, 120, 120, 120, 140, 140, 140, 150, 150, 150, 160, 160, 160, 170, 170, 170, 180, 180, 180, 200 };
        public static readonly int[] Mystery4Win = { 20, 50, 100, 150, 200, 220, 250, 280, 300, 320, 350, 380, 400, 420, 450, 480, 500, 540, 550, 600, 630, 650, 700, 750, 770, 800, 850, 900, 950, 1000, 1050, 1100, 1140, 1200, 1260, 1300, 1330, 1400, 1480, 1500, 1550, 1600, 1660, 1700, 1790, 1800, 1810, 1900, 1950, 2000 };
        public static int[] PlayLines = { 5 };

        #endregion

        #region Private methods

        /// <summary>
        /// Daje broj elemenata u rilu.
        /// </summary>
        /// <param name="reel"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        protected int GetNumberOfElementsInReel(int reel, int element)
        {
            int number = 0;
            for (var i = 1; i < 4; i++)
            {
                if (_Matrix[reel, i] == element)
                {
                    number++;
                }
            }

            return number;
        }

        /// <summary>
        /// Daje koliko prvih rila sadrže simbol ili wild
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        protected int SymbolLength(int symbol)
        {
            var n = 0;
            for (var i = 0; i < 4; i++)
            {
                if (GetNumberOfElementsInReel(i, symbol) > 0 || GetNumberOfElementsInReel(i, 0) > 0)
                {
                    n++;
                }
                else
                {
                    return n;
                }
            }

            return n;
        }

        /// <summary>
        /// Daje pozicije simbola po rilovima
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        protected List<int>[] SymbolPositions(int symbol)
        {
            var l = new List<int>[4];
            for (var i = 0; i < 4; i++)
            {
                l[i] = new List<int>();
                for (var j = 1; j < 4; j++)
                {
                    if (_Matrix[i, j] == symbol || _Matrix[i, j] == 0)
                    {
                        l[i].Add(j - 1);
                    }
                }
            }
            return l;
        }

        protected LineInfo GetMystery(int bet)
        {
            var mystPos = new List<byte>();
            for (var i = 0; i < 4; i++)
            {
                for (var j = 1; j < 4; j++)
                {
                    if (_Matrix[i, j] == 10)
                    {
                        mystPos.Add((byte)((j - 1) * 4 + i));
                    }
                }
            }
            if (mystPos.Count < 3)
            {
                return null;
            }
            var win = (mystPos.Count == 4 ? Mystery4Win[SoftwareRng.Next(Mystery4Win.Length)] : Mystery3Win[SoftwareRng.Next(Mystery3Win.Length)]) * bet;
            if (mystPos.Count == 3)
            {
                mystPos.Add(255);
            }
            var li = new LineInfo
            {
                Id = 254,
                WinningElement = 10,
                WinningPosition = mystPos.ToArray(),
                Win = win
            };
            return li;
        }

        #endregion

        #region Public methods

        public List<LineInfo> GetAllWinningLines(int bet)
        {
            var winCount = new int[10];
            var lineInfos = new List<LineInfo>();
            for (var i = 1; i < 10; i++)
            {
                winCount[i] = SymbolLength(i);
            }
            for (var i = 1; i < 10; i++)
            {
                if (winCount[i] < 3)
                {
                    continue;
                }
                var pos = SymbolPositions(i);
                for (var r1 = 0; r1 < pos[0].Count; r1++)
                {
                    for (var r2 = 0; r2 < pos[1].Count; r2++)
                    {
                        for (var r3 = 0; r3 < pos[2].Count; r3++)
                        {
                            var mult = (_Matrix[1, pos[1][r2] + 1] == 0 || _Matrix[2, pos[2][r3] + 1] == 0) ? 2 : 1;
                            if (winCount[i] == 3)
                            {
                                var li = new LineInfo
                                {
                                    Id = (byte)(pos[0][r1] * 27 + pos[1][r2] * 9 + pos[2][r3] * 3),
                                    WinningElement = (byte)i,
                                    Win = WinForLinesWild81[i, 0] * bet * mult,
                                    WinningPosition = new byte[] { (byte)(pos[0][r1] * 4), (byte)(pos[1][r2] * 4 + 1), (byte)(pos[2][r3] * 4 + 2), 255 }
                                };
                                lineInfos.Add(li);
                            }
                            if (winCount[i] == 4)
                            {
                                for (var r4 = 0; r4 < pos[3].Count; r4++)
                                {
                                    var li = new LineInfo
                                    {
                                        Id = (byte)(pos[0][r1] * 27 + pos[1][r2] * 9 + pos[2][r3] * 3 + pos[3][r4]),
                                        WinningElement = (byte)i,
                                        Win = WinForLinesWild81[i, 1] * bet * mult,
                                        WinningPosition = new byte[] { (byte)(pos[0][r1] * 4), (byte)(pos[1][r2] * 4 + 1), (byte)(pos[2][r3] * 4 + 2), (byte)(pos[3][r4] * 4 + 3) }
                                    };
                                    lineInfos.Add(li);
                                }
                            }
                        }
                    }
                }
            }
            var mystLi = GetMystery(bet);
            if (mystLi != null)
            {
                lineInfos.Add(mystLi);
            }
            return lineInfos;
        }

        public int GetElement(int reel, int row)
        {
            return _Matrix[reel, row];
        }

        /// <summary>
        /// Konstruiše matricu na osnovu dvodimenzionalnog niza.
        /// </summary>
        /// <param name="matrix"></param>
        public void FromMatrixArrayWild81(int[,] matrix)
        {
            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    _Matrix[i, j] = matrix[i, j];
                }
            }
        }

        public static int[,] GetMatixArray()
        {
            var reelsSet = -1;
            var sum = 0.0;
            var rnd = SoftwareRng.Next();
            var n = _ReelProbs.Length;
            for (var i = 0; i < n; i++)
            {
                sum += _ReelProbs[i];
                if (rnd < sum)
                {
                    reelsSet = i;
                    break;
                }
            }
            var mat = new int[4, 5];
            for (var i = 0; i < 4; i++)
            {
                var l = _Reels[reelsSet][i].Length;
                var p = SoftwareRng.Next(l);
                for (var j = 0; j < 5; j++)
                {
                    mat[i, j] = _Reels[reelsSet][i][(p + j) % l];
                }
            }
            return mat;
        }

        #endregion

        #region V3 structs

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[4][];
            fakeReels[0] = new[] { 4, 8, 2, 6, 9, 7, 7, 7, 2, 5, 10, 8, 5, 5, 5, 9, 9, 9, 1, 7, 7, 9, 4, 10, 8, 8, 8, 2, 6, 6, 5, 5, 3, 3, 3, 9, 9, 6, 2, 2, 4, 4, 4, 5, 1, 7, 7, 7, 2, 5, 8, 6, 10, 3, 4, 6, 6, 6, 1, 5, 5, 6, 9, 9, 9, 5, 10, 8, 8, 8, 5, 1, 3, 3, 5, 6, 6, 9, 4, 1, 3, 5, 5, 6, 2, 2, 10, 3, 6, 5, 1, 4, 6, 8, 2, 5, 5, 2, 8, 8, 8, 6, 4, 4, 3, 10, 3, 4 };
            fakeReels[1] = new[] { 3, 2, 10, 1, 2, 5, 10, 3, 3, 3, 5, 4, 6, 9, 9, 9, 1, 10, 3, 5, 8, 2, 4, 7, 6, 6, 6, 5, 5, 3, 2, 5, 9, 7, 10, 6, 4, 0, 6, 4, 8, 8, 8, 4, 4, 6, 8, 5, 2, 6, 6, 0, 5, 5, 7, 7, 7, 2, 10, 7, 7, 1, 3, 2, 6, 8, 5, 5, 0, 7, 9, 4, 6, 8, 9, 0, 10, 9, 9, 9, 5, 3, 4, 9, 9, 2, 6, 6, 3, 3, 2, 5, 7, 8, 9, 7 };
            fakeReels[2] = new[] { 9, 9, 9, 7, 7, 7, 2, 9, 0, 6, 2, 5, 5, 5, 1, 3, 5, 5, 1, 3, 3, 3, 5, 1, 8, 8, 8, 9, 9, 4, 3, 10, 5, 5, 6, 6, 6, 2, 9, 6, 2, 4, 4, 4, 2, 8, 10, 0, 5, 5, 4, 4, 9, 4, 6, 6, 7, 7, 5, 7, 8, 9, 10, 8, 8, 6, 4, 3, 5, 5, 0, 3, 5, 9, 1, 5, 8, 9, 0, 8, 5, 6, 2, 3, 3, 4, 6, 2, 8, 8, 8, 5, 5, 9, 9, 9 };
            fakeReels[3] = new[] { 5, 2, 6, 6, 6, 10, 8, 8, 5, 4, 6, 9, 1, 6, 6, 8, 8, 8, 9, 9, 9, 7, 3, 5, 4, 4, 1, 2, 7, 7, 10, 5, 5, 5, 7, 7, 9, 9, 5, 6, 4, 3, 6, 6, 5, 5, 10, 8, 5, 3, 3, 5, 5, 7, 7, 2, 8, 8, 9, 1, 2, 5, 10, 2, 5, 3, 4, 9, 4, 6, 10, 6, 6, 5, 3, 6, 7, 5, 6, 6, 5, 5, 10, 3, 5, 9, 2, 3, 6, 9, 1, 2, 8, 5, 2, 9, 8, 3, 7, 7, 3, 2, 5, 1, 4 };
            return fakeReels;
        }

        /// <summary>
        /// Vraća niz koeficijenata za id simbola.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int[] GetSymbolCoefficients(int id)
        {
            if (id == 10)
            {
                return new[] { Mystery3Win[0] / 5, Mystery3Win[Mystery3Win.Length - 1] / 5, Mystery4Win[0] / 5, Mystery4Win[Mystery4Win.Length - 1] / 5 };
            }
            return new[] { 0, 0, WinForLinesWild81[id, 0], WinForLinesWild81[id, 1] };
        }

        public static HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)96.55,
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
            var lines = new HelpLineConfigV3[81];
            for (var i = 0; i < 81; i++)
            {
                var pos = new int[4] { i / 27, (i / 9) % 3, (i / 3) % 3, i % 3 };
                lines[i] = new HelpLineConfigV3 { id = i, positions = pos };
            }

            return lines;
        }

        #endregion
    }
}
