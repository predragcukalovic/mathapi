using MathBaseProject.StructuresV3;
using MathForGames.BasicGameData;
using RNGUtils.RandomData;

namespace MathForGames.GamePokerSlot
{
    public class MatrixPokerSlot
    {
        #region Public properties

        public static int[] NumberOfGratis = { 0, 0, 0, 7, 12, 20 };

        public static int[] PlayLines = { 1, 3, 5, 7, 9 };

        #endregion

        private readonly int[,] _Matrix;

        public MatrixPokerSlot()
        {
            _Matrix = new int[5, 5];
        }

        public LinePokerSlot GetLine(int line)
        {
            var l = new LinePokerSlot(_Matrix[0, GlobalData.GameLineExtra[line - 1, 0]],
                _Matrix[1, GlobalData.GameLineExtra[line - 1, 1]],
                _Matrix[2, GlobalData.GameLineExtra[line - 1, 2]],
                _Matrix[3, GlobalData.GameLineExtra[line - 1, 3]],
                _Matrix[4, GlobalData.GameLineExtra[line - 1, 4]]);
            return l;
        }

        public int CalculateLineWin(int line, bool gratis)
        {
            return GetLine(line).CalculateLineWin(gratis);
        }

        public PokerSlotWin GetLineWin(int line, bool gratis)
        {
            return GetLine(line).GetWin(gratis);
        }

        public static int WinToInt(PokerSlotWin win)
        {
            return LineWinsForGames.WinForLinesPokerSlot[(int)win];
        }

        public int GetNumberOfJokers()
        {
            var n = 0;
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (_Matrix[i, j] == 52)
                    {
                        n++;
                    }
                }
            }
            return n;
        }

        /// <summary>
        /// Konstruiše matricu na osnovu dvodimenzionalnog niza.
        /// </summary>
        /// <param name="matrix"></param>
        public void FromMatrixArray(int[,] matrix)
        {
            for (var i = 0; i < 5; i++)
            {
                for (var j = 1; j < 5; j++)
                {
                    _Matrix[i, j - 1] = matrix[i, j];
                }
                _Matrix[i, 4] = matrix[i, 0];
            }
        }

        public void SetRandomMatrix(bool gratis)
        {
            var cards = new int[52];
            for (var i = 0; i < 52; i++)
            {
                cards[i] = i;
            }
            for (var i = 0; i < 51; i++)
            {
                var rnd = SoftwareRng.Next(52 - i);
                var tmp = cards[51 - i];
                cards[51 - i] = cards[rnd];
                cards[rnd] = tmp;
            }
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    _Matrix[i, j] = cards[j * 5 + i];
                }
            }
            if (gratis)
            {
                var probsJoker = new[] { 15, 12, 12, 12, 18 };
                var prJokerFrom = new[] { 57, 56, 56, 56, 58 };
                for (var i = 0; i < 5; i++)
                {
                    if (SoftwareRng.Next(prJokerFrom[i]) < probsJoker[i])
                    {
                        _Matrix[i, SoftwareRng.Next(3)] = 52;
                    }
                }
            }
        }

        public int GetElement(int i, int j)
        {
            return _Matrix[i, j];
        }

        public void SetElement(int i, int j, int elem)
        {
            _Matrix[i, j] = elem;
        }

        /// <summary>
        /// Daje niz pozicija za simbol.
        /// </summary>
        /// <returns></returns>
        public static byte[] GetJokerPositionsArray(byte[,] matrix)
        {
            var positions = new byte[5];
            var index = 0;
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (matrix[i, j] > 51)
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

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 43, 22, 51, 49, 33, 35, 18, 0, 128, 8, 46, 42, 19, 38, 50, 24, 4, 2, 5, 37, 44, 29, 12, 6, 20, 27, 3, 13, 15, 36, 1, 40, 31, 30, 10, 140, 34, 11, 48, 21, 7, 45, 23, 41, 25, 16, 47, 39, 14, 9, 32, 28, 17, 26, 43, 22, 51, 49, 33, 35, 18, 0, 138, 8, 46, 42, 19, 38, 50, 24, 4, 2, 5, 37, 44, 29, 12, 6, 20, 27, 3, 13, 15, 36, 1, 40, 31, 30, 10, 149, 34, 11, 48, 21, 7, 45, 23, 41, 25, 16, 47, 39, 14, 9, 32, 28, 17, 26, 43, 22, 51, 49, 33, 35, 18, 0, 118, 8, 46, 42, 19, 38, 50, 24, 4, 2, 5, 37, 44, 29, 12, 6, 20, 27, 3, 13, 15, 36, 1, 40, 31, 30, 10, 106, 34, 11, 48, 21, 7, 45, 23, 41, 25, 16, 47, 39, 14, 9, 32, 28, 17, 26, 43, 22, 51, 49, 33, 35, 18, 0, 113, 8, 46, 42, 19, 38, 50, 24, 4, 2, 5, 37, 44, 29, 12, 6, 20, 27, 3, 13, 15, 36, 1, 40, 31, 30, 10, 146, 34, 11, 48, 21, 7, 45, 23, 41, 25, 16, 47, 39, 14, 9, 32, 28, 17, 26, 43, 22, 51, 49, 33, 35, 18, 0, 108, 8, 46, 42, 19, 38, 50, 24, 4, 2, 5, 37, 44, 29, 12, 6, 20, 27, 3, 13, 15, 36, 1, 40, 31, 30, 10, 115, 34, 11, 48, 21, 7, 45, 23, 41, 25, 16, 47, 39, 14, 9, 32, 28, 17, 26 };
            fakeReels[1] = new[] { 24, 14, 15, 11, 33, 35, 21, 3, 30, 37, 46, 45, 31, 42, 49, 5, 127, 29, 23, 38, 36, 17, 22, 8, 39, 25, 144, 13, 0, 48, 47, 34, 7, 26, 40, 27, 1, 9, 28, 51, 19, 4, 50, 12, 18, 2, 32, 10, 20, 44, 41, 16, 43, 6, 24, 14, 15, 11, 33, 35, 21, 3, 30, 37, 46, 45, 31, 42, 49, 5, 102, 29, 23, 38, 36, 17, 22, 8, 39, 25, 147, 13, 0, 48, 47, 34, 7, 26, 40, 27, 1, 9, 28, 51, 19, 4, 50, 12, 18, 2, 32, 10, 20, 44, 41, 16, 43, 6, 24, 14, 15, 11, 33, 35, 21, 3, 30, 37, 46, 45, 31, 42, 49, 5, 136, 29, 23, 38, 36, 17, 22, 8, 39, 25, 125, 13, 0, 48, 47, 34, 7, 26, 40, 27, 1, 9, 28, 51, 19, 4, 50, 12, 18, 2, 32, 10, 20, 44, 41, 16, 43, 6, 24, 14, 15, 11, 33, 35, 21, 3, 30, 37, 46, 45, 31, 42, 49, 5, 111, 29, 23, 38, 36, 17, 22, 8, 39, 25, 134, 13, 0, 48, 47, 34, 7, 26, 40, 27, 1, 9, 28, 51, 19, 4, 50, 12, 18, 2, 32, 10, 20, 44, 41, 16, 43, 6, 24, 14, 15, 11, 33, 35, 21, 3, 30, 37, 46, 45, 31, 42, 49, 5, 141, 29, 23, 38, 36, 17, 22, 8, 39, 25, 131, 13, 0, 48, 47, 34, 7, 26, 40, 27, 1, 9, 28, 51, 19, 4, 50, 12, 18, 2, 32, 10, 20, 44, 41, 16, 43, 6 };
            fakeReels[2] = new[] { 36, 1, 2, 28, 32, 130, 11, 26, 44, 7, 39, 20, 42, 27, 51, 0, 43, 31, 23, 12, 22, 30, 17, 33, 19, 15, 46, 18, 49, 16, 48, 40, 114, 35, 4, 34, 13, 6, 10, 47, 38, 24, 41, 29, 5, 37, 21, 50, 25, 14, 45, 8, 3, 9, 36, 1, 2, 28, 32, 103, 11, 26, 44, 7, 39, 20, 42, 27, 51, 0, 43, 31, 23, 12, 22, 30, 17, 33, 19, 15, 46, 18, 49, 16, 48, 40, 139, 35, 4, 34, 13, 6, 10, 47, 38, 24, 41, 29, 5, 37, 21, 50, 25, 14, 45, 8, 3, 9, 36, 1, 2, 28, 32, 151, 11, 26, 44, 7, 39, 20, 42, 27, 51, 0, 43, 31, 23, 12, 22, 30, 17, 33, 19, 15, 46, 18, 49, 16, 48, 40, 145, 35, 4, 34, 13, 6, 10, 47, 38, 24, 41, 29, 5, 37, 21, 50, 25, 14, 45, 8, 3, 9, 36, 1, 2, 28, 32, 123, 11, 26, 44, 7, 39, 20, 42, 27, 51, 0, 43, 31, 23, 12, 22, 30, 17, 33, 19, 15, 46, 18, 49, 16, 48, 40, 100, 35, 4, 34, 13, 6, 10, 47, 38, 24, 41, 29, 5, 37, 21, 50, 25, 14, 45, 8, 3, 9, 36, 1, 2, 28, 32, 105, 11, 26, 44, 7, 39, 20, 42, 27, 51, 0, 43, 31, 23, 12, 22, 30, 17, 33, 19, 15, 46, 18, 49, 16, 48, 40, 121, 35, 4, 34, 13, 6, 10, 47, 38, 24, 41, 29, 5, 37, 21, 50, 25, 14, 45, 8, 3, 9 };
            fakeReels[3] = new[] { 18, 20, 19, 27, 28, 14, 42, 6, 26, 50, 32, 5, 12, 34, 33, 0, 30, 9, 1, 48, 16, 4, 107, 44, 49, 24, 41, 38, 25, 51, 45, 40, 35, 13, 22, 117, 3, 17, 11, 47, 21, 29, 46, 23, 37, 2, 7, 10, 31, 36, 15, 43, 8, 39, 18, 20, 19, 27, 28, 14, 42, 6, 26, 50, 32, 5, 12, 34, 33, 0, 30, 9, 1, 48, 16, 4, 150, 44, 49, 24, 41, 38, 25, 51, 45, 40, 35, 13, 22, 143, 3, 17, 11, 47, 21, 29, 46, 23, 37, 2, 7, 10, 31, 36, 15, 43, 8, 39, 18, 20, 19, 27, 28, 14, 42, 6, 26, 50, 32, 5, 12, 34, 33, 0, 30, 9, 1, 48, 16, 4, 132, 44, 49, 24, 41, 38, 25, 51, 45, 40, 35, 13, 22, 135, 3, 17, 11, 47, 21, 29, 46, 23, 37, 2, 7, 10, 31, 36, 15, 43, 8, 39, 18, 20, 19, 27, 28, 14, 42, 6, 26, 50, 32, 5, 12, 34, 33, 0, 30, 9, 1, 48, 16, 4, 142, 44, 49, 24, 41, 38, 25, 51, 45, 40, 35, 13, 22, 120, 3, 17, 11, 47, 21, 29, 46, 23, 37, 2, 7, 10, 31, 36, 15, 43, 8, 39, 18, 20, 19, 27, 28, 14, 42, 6, 26, 50, 32, 5, 12, 34, 33, 0, 30, 9, 1, 48, 16, 4, 137, 44, 49, 24, 41, 38, 25, 51, 45, 40, 35, 13, 22, 109, 3, 17, 11, 47, 21, 29, 46, 23, 37, 2, 7, 10, 31, 36, 15, 43, 8, 39 };
            fakeReels[4] = new[] { 50, 24, 16, 44, 42, 148, 32, 12, 22, 6, 27, 36, 46, 35, 33, 15, 19, 43, 47, 31, 25, 1, 14, 23, 18, 2, 0, 8, 28, 7, 112, 34, 5, 38, 49, 39, 45, 4, 3, 26, 20, 13, 30, 37, 40, 10, 48, 9, 29, 21, 17, 11, 41, 51, 50, 24, 16, 44, 42, 119, 32, 12, 22, 6, 27, 36, 46, 35, 33, 15, 19, 43, 47, 31, 25, 1, 14, 23, 18, 2, 0, 8, 28, 7, 133, 34, 5, 38, 49, 39, 45, 4, 3, 26, 20, 13, 30, 37, 40, 10, 48, 9, 29, 21, 17, 11, 41, 51, 50, 24, 16, 44, 42, 110, 32, 12, 22, 6, 27, 36, 46, 35, 33, 15, 19, 43, 47, 31, 25, 1, 14, 23, 18, 2, 0, 8, 28, 7, 124, 34, 5, 38, 49, 39, 45, 4, 3, 26, 20, 13, 30, 37, 40, 10, 48, 9, 29, 21, 17, 11, 41, 51, 50, 24, 16, 44, 42, 126, 32, 12, 22, 6, 27, 36, 46, 35, 33, 15, 19, 43, 47, 31, 25, 1, 14, 23, 18, 2, 0, 8, 28, 7, 101, 34, 5, 38, 49, 39, 45, 4, 3, 26, 20, 13, 30, 37, 40, 10, 48, 9, 29, 21, 17, 11, 41, 51, 50, 24, 16, 44, 42, 122, 32, 12, 22, 6, 27, 36, 46, 35, 33, 15, 19, 43, 47, 31, 25, 1, 14, 23, 18, 2, 0, 8, 28, 7, 129, 34, 5, 38, 49, 39, 45, 4, 3, 26, 20, 13, 30, 37, 40, 10, 48, 9, 29, 21, 17, 11, 41, 51, 50, 24, 16, 44, 42, 104, 32, 12, 22, 6, 27, 36, 46, 35, 33, 15, 19, 43, 47, 31, 25, 1, 14, 23, 18, 2, 0, 8, 28, 7, 116, 34, 5, 38, 49, 39, 45, 4, 3, 26, 20, 13, 30, 37, 40, 10, 48, 9, 29, 21, 17, 11, 41, 51 };

            return fakeReels;
        }

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetGratisFakeReels()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 43, 22, 51, 49, 33, 35, 18, 0, 53, 8, 46, 42, 19, 38, 50, 24, 4, 2, 5, 37, 44, 29, 12, 6, 20, 27, 3, 13, 15, 36, 1, 40, 31, 30, 10, 53, 34, 11, 48, 21, 7, 45, 23, 41, 25, 16, 47, 39, 14, 9, 32, 28, 17, 26 };
            fakeReels[1] = new[] { 24, 14, 15, 11, 33, 35, 21, 3, 30, 37, 46, 45, 31, 42, 49, 5, 53, 29, 23, 38, 36, 17, 22, 8, 39, 25, 53, 13, 0, 48, 47, 34, 7, 26, 40, 27, 1, 9, 28, 51, 19, 4, 50, 12, 18, 2, 32, 10, 20, 44, 41, 16, 43, 6 };
            fakeReels[2] = new[] { 36, 1, 2, 28, 32, 53, 11, 26, 44, 7, 39, 20, 42, 27, 51, 0, 43, 31, 23, 12, 22, 30, 17, 33, 19, 15, 46, 18, 49, 16, 48, 40, 53, 35, 4, 34, 13, 6, 10, 47, 38, 24, 41, 29, 5, 37, 21, 50, 25, 14, 45, 8, 3, 9 };
            fakeReels[3] = new[] { 18, 20, 19, 27, 28, 14, 42, 6, 26, 50, 32, 5, 12, 34, 33, 0, 30, 9, 1, 48, 16, 4, 53, 44, 49, 24, 41, 38, 25, 51, 45, 40, 35, 13, 22, 53, 3, 17, 11, 47, 21, 29, 46, 23, 37, 2, 7, 10, 31, 36, 15, 43, 8, 39 };
            fakeReels[4] = new[] { 50, 24, 16, 44, 42, 53, 32, 12, 22, 6, 27, 36, 46, 35, 33, 15, 19, 43, 47, 31, 25, 1, 14, 23, 18, 2, 0, 8, 28, 7, 53, 34, 5, 38, 49, 39, 45, 4, 3, 26, 20, 13, 30, 37, 40, 10, 48, 9, 29, 21, 17, 11, 41, 51 };

            return fakeReels;
        }

        public static HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)96.254,
                symbols = GetHelpSymbolConfigV3(),
                lines = GetHelpLineConfigV3()
            };

            return helpV3;
        }

        private static HelpSymbolConfigV3<object>[] GetHelpSymbolConfigV3()
        {
            var symbols = new HelpSymbolConfigV3<object>[12];
            for (var i = 0; i < 10; i++)
            {
                symbols[i] = new HelpSymbolConfigV3<object>
                {
                    id = i,
                    coefficients = new[] { LineWinsForGames.WinForLinesPokerSlot[i] },
                    features = new[] { HelpSymbolFeatureV3.Regular },
                    extra = new HelpSymbolExtraV3()
                };
            }
            symbols[10] = new HelpSymbolConfigV3<object>
            {
                id = 52,
                coefficients = new int[] { },
                features = new[] { HelpSymbolFeatureV3.FreeSpin },
                extra = new HelpSymbolExtraV3
                {
                    freeSpins = new[] { 0, 0, 7, 10, 15 }
                }
            };
            symbols[11] = new HelpSymbolConfigV3<object>
            {
                id = 53,
                coefficients = new int[] { },
                features = new[] { HelpSymbolFeatureV3.Regular },
                extra = new HelpSymbolExtraV3()
            };
            return symbols;
        }

        private static HelpLineConfigV3[] GetHelpLineConfigV3()
        {
            var lines = new HelpLineConfigV3[9];
            for (var i = 0; i < 9; i++)
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

        /// <summary>
        /// Daje minimalnu i maksimalnu poziciju od kad do kad treba da se pale simboli da bi se stvorio bolji utisak u igri
        /// </summary>
        /// <param name="lineNumber">Broj linije</param>
        /// <param name="gratis">Da li je gratis igra</param>
        /// <param name="minPos">Nakon što se padne taj ril krenu da se pale simboli</param>
        /// <param name="maxPos">Simboli se gase nakon tog rila; ako je vrednost 5 znači da je linija dobitna</param>
        private void GetMinMaxPotentionalWin(int lineNumber, bool gratis, out int minPos, out int maxPos)
        {
            minPos = 0;
            maxPos = 0;
            PokerSlotWin[] limit = { PokerSlotWin.ThreeOfAKind, PokerSlotWin.ThreeOfAKind, PokerSlotWin.ThreeOfAKind, PokerSlotWin.ThreeOfAKind, PokerSlotWin.NoWin };
            var line = GetLine(lineNumber);
            for (var i = 3; i < 6; i++)
            {
                var hnd = (int[])line.Hand.Clone();
                if (!gratis)
                {
                    for (var j = 0; j < i; j++)
                    {
                        if (hnd[j] == 52)
                        {
                            hnd[j] = 53;
                        }
                    }
                }
                for (var j = i; j < 5; j++)
                {
                    hnd[j] = 52;
                }
                var tmpLine = new LinePokerSlot(hnd[0] % 100, hnd[1] % 100, hnd[2] % 100, hnd[3] % 100, hnd[4] % 100);
                var tmpWin = tmpLine.GetWin(true);
                if (i == 3 && tmpWin > PokerSlotWin.ThreeOfAKind && tmpWin != PokerSlotWin.FullHouse && tmpWin != PokerSlotWin.FourOfAKind)
                {
                    if (minPos == 0)
                    {
                        minPos = 2;
                    }
                    maxPos = 3;
                }
                else if (tmpWin > limit[i - 1])
                {
                    if (minPos == 0)
                    {
                        minPos = i - 1;
                    }
                    maxPos = i;
                }
            }
        }

        public int[][,] GetPotentionalWinObject(int numberOfLines, bool gratis)
        {
            var matrices = new int[5][,];
            for (var i = 0; i < 5; i++)
            {
                matrices[i] = new int[5, 3];
            }
            for (var i = 0; i < numberOfLines; i++)
            {
                int min, max;
                GetMinMaxPotentionalWin(i + 1, gratis, out min, out max);
                if (min > 0)
                {
                    for (var j = 0; j < 5; j++)
                    {
                        if (j >= min && j < max)
                        {
                            for (var k = 0; k <= j; k++)
                            {
                                matrices[j][k, GlobalData.GameLineExtra[i, k]] = 1;
                            }
                        }
                    }
                }
            }
            return matrices;
        }
    }
}
