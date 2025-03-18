using MathBaseProject.StructuresV3;
using RNGUtils.RandomData;

namespace MathForGames.GameLuckyTwister
{
    public class MatrixLuckyTwister
    {
        #region Private fields

        private readonly int[,] _Matrix;

        public static readonly int[,] Win =
        {
            {50, 20, 20, 10, 10, 5, 5, 5, 5},
            {75, 40, 40, 15, 15, 10, 10, 10, 10},
            {150, 60, 60, 25, 25, 15, 15, 15, 15},
            {200, 80, 80, 40, 40, 20, 20, 20, 20},
            {250, 110, 110, 60, 60, 25, 25, 25, 25},
            {350, 160, 160, 80, 80, 30, 30, 30, 30},
            {500, 200, 200, 100, 100, 40, 40, 40, 40},
            {650, 250, 250, 130, 130, 60, 60, 60, 60},
            {900, 300, 300, 170, 170, 80, 80, 80, 80},
            {1200, 400, 400, 210, 210, 100, 100, 100, 100},
            {1500, 500, 500, 250, 250, 120, 120, 120, 120},
            {2000, 600, 600, 300, 300, 150, 150, 150, 150},
            {2500, 800, 800, 400, 400, 180, 180, 180, 180},
            {3000, 1100, 1100, 500, 500, 210, 210, 210, 210},
            {3500, 1400, 1400, 600, 600, 300, 300, 300, 300},
            {4000, 1700, 1700, 800, 800, 400, 400, 400, 400},
            {5000, 2100, 2100, 1000, 1000, 500, 500, 500, 500},
            {6000, 2500, 2500, 1200, 1200, 600, 600, 600, 600},
            {7000, 3000, 3000, 1500, 1500, 700, 700, 700, 700},
            {8000, 3500, 3500, 1800, 1800, 800, 800, 800, 800},
            {9000, 4000, 4000, 2000, 2000, 900, 900, 900, 900},
            {10000, 5000, 5000, 2500, 2500, 1000, 1000, 1000, 1000}
        };

        #endregion

        #region Public fields

        public int TwinStart = 0;
        public int TwinCount = 0;

        public static int[] PlayLines = { 10 };

        #endregion

        #region Constructors

        public MatrixLuckyTwister()
        {
            _Matrix = new int[6, 7];
        }

        #endregion

        /// <summary>
        /// Prvo još ne obiđeno polje u matrici.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="positionX"></param>
        /// <param name="positionY"></param>
        private static void GetNextFreePosition(ref int[,] matrix, out int positionX, out int positionY)
        {
            for (var i = 0; i < 6; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    if (matrix[i, j] == 0)
                    {
                        positionX = i;
                        positionY = j;
                        return;
                    }
                }
            }
            positionX = -1;
            positionY = -1;
        }

        public static byte[] GetPositionsFromClasterMatrix(int[,] clasterMatrix, int claserElement)
        {
            var b = new byte[30];
            var index = 0;
            for (var i = 0; i < 6; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    if (clasterMatrix[i, j] == claserElement)
                    {
                        b[index++] = (byte)(j * 6 + i);
                    }
                }
            }
            for (; index < 30; index++)
            {
                b[index] = 255;
            }

            return b;
        }

        /// <summary>
        /// Rekurzivna funkcija koja popunjava susedna polja odgovarajućim brojem.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="element"></param>
        /// <param name="value"></param>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        private void MarkField(ref int[,] matrix, int element, int value, int posX, int posY)
        {
            if (posX < 0 || posY < 0 || posX > 5 || posY > 4 || matrix[posX, posY] != 0 || _Matrix[posX, posY + 1] != element)
            {
                return;
            }
            matrix[posX, posY] = value;
            MarkField(ref matrix, element, value, posX - 1, posY);
            MarkField(ref matrix, element, value, posX, posY - 1);
            MarkField(ref matrix, element, value, posX + 1, posY);
            MarkField(ref matrix, element, value, posX, posY + 1);
        }

        /// <summary>
        /// Daje matricu 6×5 sa klasterima numerisani od 1 do najviše 30.
        /// </summary>
        /// <returns></returns>
        public int[,] GetClasterMatrix()
        {
            var clasters = new int[6, 5];
            var nextField = 1;
            while (true)
            {
                int indexX, indexY;
                GetNextFreePosition(ref clasters, out indexX, out indexY);
                if (indexX == -1 || indexY == -1)
                {
                    break;
                }
                MarkField(ref clasters, _Matrix[indexX, indexY + 1], nextField, indexX, indexY);
                nextField++;
            }
            return clasters;
        }

        #region Public methods

        public int GetElement(int i, int j)
        {
            return _Matrix[i, j];
        }

        public void FormMatrixFromArray(int[,] matrix)
        {
            TwinCount = 2;
            if (SoftwareRng.Next(20) == 0)
            {
                TwinCount++;
                if (SoftwareRng.Next(20) == 0)
                {
                    TwinCount++;
                    if (SoftwareRng.Next(20) == 0)
                    {
                        TwinCount++;
                        if (SoftwareRng.Next(20) == 0)
                        {
                            TwinCount++;
                        }
                    }
                }
            }
            TwinStart = (int)SoftwareRng.Next(7 - TwinCount);
            FormMatrixFromArray(matrix, TwinStart, TwinCount);
        }

        public void FromMatrixArray(int[,] matrix)
        {
            for (var i = 0; i < 6; i++)
            {
                for (var j = 0; j < 7; j++)
                {
                    _Matrix[i, j] = matrix[i, j];
                }
            }
        }

        public void FormMatrixFromArray(int[,] matrix, int twinStart, int twinCount)
        {
            for (var i = 0; i < 6; i++)
            {
                int ind;
                if (i < twinStart)
                {
                    ind = i;
                }
                else if (i >= twinStart && i < twinStart + twinCount)
                {
                    ind = twinStart;
                }
                else
                {
                    ind = i - twinCount + 1;
                }
                for (var j = 0; j < 7; j++)
                {
                    _Matrix[i, j] = matrix[ind, j];
                }
            }
            TwinStart = twinStart;
            TwinCount = twinCount;
        }

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[6][];
            fakeReels[0] = new[] { 7, 7, 7, 7, 7, 8, 8, 4, 4, 4, 4, 4, 6, 6, 5, 5, 5, 5, 5, 7, 7, 2, 2, 2, 2, 2, 3, 3, 8, 8, 8, 8, 8, 5, 5, 3, 3, 3, 3, 3, 7, 7, 6, 6, 6, 6, 6, 8, 8, 0, 0, 0, 0, 0, 7, 7, 1, 1, 1, 1, 1, 4, 4, 8, 8, 8, 8, 8, 5, 5, 6, 6, 6, 6, 6, 7, 7, 3, 3, 3, 3, 3, 6, 6, 4, 4, 4, 4, 4, 6, 6 };
            fakeReels[1] = new[] { 8, 8, 8, 8, 8, 1, 1, 7, 7, 7, 7, 7, 4, 4, 6, 6, 6, 6, 6, 3, 3, 5, 5, 5, 5, 5, 4, 4, 4, 4, 4, 6, 6, 3, 3, 3, 3, 3, 8, 8, 2, 2, 2, 2, 2, 7, 7, 1, 1, 1, 1, 1, 5, 5, 0, 0, 0, 0, 0, 5, 5, 7, 7, 7, 7, 7, 4, 4, 6, 6, 6, 6, 6, 1, 1, 8, 8, 8, 8, 8, 0, 0, 5, 5, 5, 5, 5, 7, 7, 6, 6, 6, 6, 6, 3, 3 };
            fakeReels[2] = new[] { 3, 3, 3, 3, 3, 7, 7, 8, 8, 8, 8, 8, 4, 4, 7, 7, 7, 7, 7, 3, 3, 6, 6, 6, 6, 6, 8, 8, 5, 5, 5, 5, 5, 4, 4, 7, 7, 7, 7, 7, 2, 2, 6, 6, 6, 6, 6, 0, 0, 5, 5, 5, 5, 5, 6, 6, 8, 8, 8, 8, 8, 3, 3, 8, 8, 6, 6, 3, 3, 8, 8, 8, 8, 8, 1, 1, 5, 5, 7, 7, 3, 3, 5, 5, 4, 4, 4, 4, 4, 6, 6, 5, 5, 7, 7, 6, 6 };
            fakeReels[3] = new[] { 8, 8, 5, 5, 5, 5, 5, 4, 4, 7, 7, 7, 7, 7, 2, 2, 6, 6, 6, 6, 6, 0, 0, 5, 5, 5, 5, 5, 6, 6, 8, 8, 8, 8, 8, 3, 3, 8, 8, 6, 6, 3, 3, 8, 8, 8, 8, 8, 1, 1, 5, 5, 7, 7, 3, 3, 5, 5, 4, 4, 4, 4, 4, 6, 6, 5, 5, 7, 7, 6, 6, 3, 3, 3, 3, 3, 7, 7, 8, 8, 8, 8, 8, 4, 4, 7, 7, 7, 7, 7, 3, 3, 6, 6, 6, 6, 6 };
            fakeReels[4] = new[] { 4, 4, 4, 4, 4, 5, 5, 8, 8, 8, 8, 8, 4, 4, 7, 7, 7, 7, 7, 2, 2, 6, 6, 6, 6, 6, 0, 0, 5, 5, 5, 5, 5, 3, 3, 6, 6, 6, 6, 6, 7, 7, 3, 3, 3, 3, 3, 6, 6, 5, 5, 5, 5, 5, 1, 1, 7, 7, 7, 7, 7, 2, 2, 8, 8, 8, 8, 8, 4, 4, 5, 5, 5, 5, 5, 4, 4, 6, 6, 6, 6, 6, 8, 8, 7, 7, 7, 7, 7, 5, 5, 3, 3, 3, 3, 3, 7, 7 };
            fakeReels[5] = new[] { 2, 2, 2, 2, 2, 6, 6, 1, 1, 1, 1, 1, 7, 7, 0, 0, 0, 0, 0, 6, 6, 4, 4, 4, 4, 4, 8, 8, 3, 3, 3, 3, 3, 5, 5, 8, 8, 8, 8, 8, 4, 4, 7, 7, 7, 7, 7, 2, 2, 6, 6, 6, 6, 6, 3, 3, 5, 5, 5, 5, 5, 0, 0, 8, 8, 8, 8, 8, 3, 3, 6, 6, 6, 6, 6, 8, 8, 5, 5, 5, 5, 5, 1, 1, 7, 7, 7, 7, 7, 8, 8, 6, 6, 6, 6, 6, 5, 5 };

            return fakeReels;
        }

        /// <summary>
        /// Vraća niz koeficijenata za id simbola.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int[] GetSymbolCoefficients(int id)
        {
            var coefficients = new int[30];
            for (var i = 8; i < 30; i++)
            {
                coefficients[i] = Win[i - 8, id];
            }
            return coefficients;
        }

        public static HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)96.249,
                symbols = GetHelpSymbolConfigV3(),
                lines = new HelpLineConfigV3[0]
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
                    features = new[] { HelpSymbolFeatureV3.Regular },
                    extra = new HelpSymbolExtraV3(),
                    coefficients = GetSymbolCoefficients(i)
                };
            }
            return symbols;
        }

        #endregion
    }
}
