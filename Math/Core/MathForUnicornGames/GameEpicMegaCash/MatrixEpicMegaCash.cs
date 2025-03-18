using MathBaseProject.BaseMathData;
using MathBaseProject.StructuresV3;
using MathForUnicornGames.BasicUnicornData;
using RNGUtils.RandomData;
using System.Collections.Generic;
using System.Linq;


namespace MathForUnicornGames.GameEpicMegaCash
{
    public class MatrixEpicMegaCash : Matrix
    {
        #region Constructor

        public MatrixEpicMegaCash()
            : base(5)
        {

        }

        #endregion

        #region Public properties

        public static int[] PlayLines = { 10 };

        public static readonly int[,] WinForLinesEpicMegaCash =
        {
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 50, 200, 1000 },
            { 0, 0, 20, 100, 400 },
            { 0, 0, 20, 100, 400 },
            { 0, 0, 10, 50, 100 },
            { 0, 0, 10, 50, 100 },
            { 0, 0, 5, 20, 80 },
            { 0, 0, 5, 20, 80 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 }
        };

        public static readonly double[,] MultiplierProbs =
        {
            { 0.3, 0.55, 0.8 },
            { 0.35, 0.65, 0.93 },
            { 0.4, 0.75, 0.95 }
        };

        public static readonly int[] _ScatterMultipliers = { 2, 5, 10, 40 };

        public static readonly double[] _ReelProbs = { 0.150236, 0.176364, 0.176364, 0.061364, 0.207036, 0.072036, 0.072036, 0.084564 };

        public static readonly int[][] _Reels = new[]
        {
            new[]{ 3, 9, 6, 9, 4, 3, 8, 6, 6, 6, 3, 8, 4, 4, 7, 3, 5, 9, 7, 8, 3, 6, 9, 4, 4, 7, 8, 7, 8, 5, 5, 6, 6, 8, 6, 6, 5, 9, 9, 8, 8, 6, 8, 9, 9, 6, 8, 6, 7, 9, 8, 8, 7, 7, 6, 8, 8, 6, 8, 7, 4, 9, 8, 9, 8, 6, 4, 7, 7, 6, 8, 4, 9, 4, 9, 6, 6, 8, 7, 7, 3, 4, 6, 6, 9, 6, 9, 8, 7, 9, 9, 7, 7, 9, 9, 6, 6, 7, 9, 9 },
            new[]{ 3, 9, 6, 9, 4, 3, 4, 6, 6, 6, 3, 9, 4, 4, 9, 3, 5, 5, 9, 5, 3, 7, 6, 6, 8, 9, 6, 9, 5, 6, 3, 8, 8, 9, 6, 3, 5, 5, 5, 6, 3, 9, 8, 5, 9, 8, 9, 7, 7, 7, 9, 9, 9, 5, 5, 7, 7, 8, 8, 7, 4, 7, 6, 6, 6, 7, 7, 9, 8, 7, 6, 9, 7, 7, 6, 6, 7, 7, 5, 6, 9, 7, 5, 7, 9, 6, 7, 5, 5, 8, 8, 7, 7, 5, 8, 5, 8, 8, 7, 8},
            new[]{ 3, 9, 4, 9, 4, 3, 4, 6, 6, 6, 3, 9, 4, 4, 6, 3, 5, 9, 7, 5, 3, 7, 4, 4, 4, 3, 6, 9, 6, 7, 3, 5, 7, 6, 7, 3, 5, 5, 5, 6, 3, 9, 8, 4, 4, 3, 7, 8, 9, 9, 3, 9, 8, 5, 5, 3, 8, 8, 9, 7, 4, 9, 8, 7, 7, 8, 9, 6, 9, 7, 7, 3, 4, 9, 9, 6, 6, 8, 8, 5, 9, 9, 8, 5, 8, 9, 8, 5, 5, 8, 8, 8, 6, 5, 9, 5, 8, 8, 7, 8},
            new[]{ 9, 5, 5, 5, 4, 9, 4, 6, 6, 6, 3, 9, 4, 4, 7, 4, 5, 9, 9, 7, 3, 9, 4, 4, 4, 3, 6, 9, 5, 6, 3, 8, 8, 6, 8, 3, 5, 5, 5, 9, 9, 9, 8, 4, 4, 7, 7, 5, 5, 5, 7, 7, 7, 5, 5, 8, 8, 9, 9, 7, 4, 9, 8, 5, 7, 4, 4, 4, 9, 8, 7, 4, 7, 4, 8, 8, 5, 7, 6, 6, 5, 9, 8, 8, 5, 7, 7, 5, 5, 9, 9, 7, 8, 5, 8, 5, 7, 7, 9, 7},
            new[]{ 3, 5, 5, 5, 4, 5, 4, 7, 9, 9, 3, 8, 4, 4, 7, 5, 5, 9, 7, 4, 4, 4, 5, 9, 9, 4, 7, 3, 9, 9, 6, 5, 9, 9, 9, 6, 5, 9, 5, 8, 8, 8, 9, 4, 4, 9, 8, 8, 9, 9, 8, 7, 7, 5, 5, 8, 6, 6, 8, 8, 4, 8, 8, 8, 7, 4, 4, 4, 9, 9, 7, 4, 4, 8, 6, 6, 7, 4, 9, 6, 6, 5, 8, 4, 7, 5, 6, 5, 5, 8, 8, 6, 6, 5, 9, 5, 7, 7, 7, 9}
        };

        public static readonly int[][][] _ReelsSpecial = new[]
        {
            new[]{new[]{-1,-1,-1,-1,-1}, new[] { -1, -1, -1, -1, -1 }, new[] { -1, -1, -1, -1, -1 }, new[] { -1, -1, -1, -1, -1 }, new[] { -1, -1, -1, -1, -1 } },
            new[]{new[]{-1,-1,-1,-1,-1}, new[] { -1, -1, -1, -1, 10 }, new[] { -1, -1, -1, -1, -1 }, new[] { -1, -1, -1, -1, -1 }, new[] { -1, -1, -1, -1, -1 } },
            new[]{new[]{-1,-1,-1,-1,-1}, new[] { -1, -1, -1, -1, -1 }, new[] { -1, -1, -1, -1, 10 }, new[] { -1, -1, -1, -1, -1 }, new[] { -1, -1, -1, -1, -1 } },
            new[]{new[]{-1,-1,-1,-1,-1}, new[] { -1, -1, -1, -1, -1 }, new[] { -1, -1, -1, -1, -1 }, new[] { -1, -1, -1, -1, 10 }, new[] { -1, -1, -1, -1, -1 } },
            new[]{new[]{-1,-1,-1,-1,-1}, new[] { -1, -1, -1, -1, 10 }, new[] { -1, -1, -1, -1, 10 }, new[] { -1, -1, -1, -1, -1 }, new[] { -1, -1, -1, -1, -1 } },
            new[]{new[]{-1,-1,-1,-1,-1}, new[] { -1, -1, -1, -1, 10 }, new[] { -1, -1, -1, -1, -1 }, new[] { -1, -1, -1, -1, 10 }, new[] { -1, -1, -1, -1, -1 } },
            new[]{new[]{-1,-1,-1,-1,-1}, new[] { -1, -1, -1, -1, -1 }, new[] { -1, -1, -1, -1, 10 }, new[] { -1, -1, -1, -1, 10 }, new[] { -1, -1, -1, -1, -1 } },
            new[]{new[]{-1,-1,-1,-1,-1}, new[] { -1, -1, -1, -1, 10 }, new[] { -1, -1, -1, -1, 10 }, new[] { -1, -1, -1, -1, 10 }, new[] { -1, -1, -1, -1, -1 } },
        };

        #endregion

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, UnicornGlobalData.GameLineShifted).CalculateLineWin(WinForLinesEpicMegaCash, null, -1, 1);
        }

        /// <summary>
        /// Konstruiše matricu na osnovu dvodimenzionalnog niza.
        /// </summary>
        /// <param name="matrix"></param>
        public void FromMatrixArrayEpicMegaCash(int[,] matrix)
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
        /// Za koliko se uveća ID simbola sketera od početne vrednosti.
        /// </summary>
        /// <returns></returns>
        private static int GetIdMultTypeOffset(int scattersSet, int[] scatterTypes)
        {
            var p = SoftwareRng.Next();
            if (scattersSet == 0)
            {
                if (p < 0.2)
                {
                    return 0;
                }
                if (p < 0.52)
                {
                    return 1;
                }
                return 2;
            }
            else if (scattersSet == 1)
            {
                List<int> scatters = new List<int>() { 0, 1, 2 };
                if (p < 0.45)
                {
                    return scatterTypes[0];
                }
                else
                {
                    if (SoftwareRng.Next(2) == 0)
                    {
                        return scatters.Where(x => x != scatterTypes[0]).First();
                    }
                    else
                    {
                        return scatters.Where(x => x != scatterTypes[0]).Last();
                    }
                }
            }
            else
            {
                List<int> scatters = new List<int>() { 0, 1, 2 };
                if (p < 0.45)
                {
                    return scatterTypes[1];
                }
                else
                {
                    if (SoftwareRng.Next(2) == 0)
                    {
                        return scatters.Where(x => x != scatterTypes[1]).First();
                    }
                    else
                    {
                        return scatters.Where(x => x != scatterTypes[1]).Last();
                    }
                }
            }
        }

        private static int GetIdMultiplierOffset(int multProbsIndex)
        {
            var p = SoftwareRng.Next();
            if (p < MultiplierProbs[multProbsIndex, 0])
            {
                return 0;
            }
            if (p < MultiplierProbs[multProbsIndex, 1])
            {
                return 1;
            }
            if (p < MultiplierProbs[multProbsIndex, 2])
            {
                return 2;
            }
            return 3;
        }

        private void SetMultipliers(int multiplierOffsetIndex, int[] scatterPositionsArray)
        {
            for (var i = 0; i < scatterPositionsArray.Length; i++)
            {
                SetElement(scatterPositionsArray[i] % 5, scatterPositionsArray[i] / 5, GetElement(scatterPositionsArray[i] % 5, scatterPositionsArray[i] / 5) + GetIdMultiplierOffset(multiplierOffsetIndex));
            }
        }
        public void SetScatters()
        {
            var epicCount = 0;
            var maxCount = 0;
            var miniCount = 0;
            var scatterPositions = new List<int>();
            var scatterCount = GetScatterCount();
            int[] scatterTypes = new int[scatterCount];
            var scattersSet = 0;
            for (var i = 1; i < 4; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    var elem = GetElement(i, j);
                    if (elem == 10)
                    {
                        var scatterType = GetIdMultTypeOffset(scattersSet, scatterTypes);
                        SetElement(i, j, 10 + 4 * scatterType);
                        scatterTypes[scattersSet] = scatterType;
                        scattersSet++;
                        scatterPositions.Add(j * 5 + i);
                        if (GetElement(i, j) < 14)
                        {
                            epicCount++;
                        }
                        else if (GetElement(i, j) < 18)
                        {
                            maxCount++;
                        }
                        else
                        {
                            miniCount++;
                        }
                    }
                }
            }
            var scatterPositionsArray = scatterPositions.ToArray();
            if (scatterPositionsArray.Length < 3)
            {
                SetMultipliers(0, scatterPositionsArray);
            }
            else if (epicCount == 3 || maxCount == 3)
            {
                SetMultipliers(2, scatterPositionsArray);
            }
            else
            {
                SetMultipliers(1, scatterPositionsArray);
            }
        }

        public int GetScatterMultiplier()
        {
            var mult = new[] { 1, 2, 3, 5 };
            var epicCount = 0;
            var maxCount = 0;
            var miniCount = 0;
            var epicMult = 1;
            var maxMult = 1;
            var miniMult = 1;
            for (var i = 1; i < 4; i++)
            {
                for (var j = 1; j < 4; j++)
                {
                    var elem = GetElement(i, j);
                    if (elem >= 10 && elem <= 13)
                    {
                        epicCount++;
                        epicMult *= mult[elem - 10];
                    }
                    if (elem >= 14 && elem <= 17)
                    {
                        maxCount++;
                        maxMult *= mult[elem - 14];
                    }
                    if (elem >= 18 && elem <= 21)
                    {
                        miniCount++;
                        miniMult *= mult[elem - 18];
                    }
                }
            }
            if (epicCount == 3)
            {
                return epicMult * _ScatterMultipliers[3];
            }
            if (maxCount == 3)
            {
                return maxMult * _ScatterMultipliers[2];
            }
            if (miniCount == 3)
            {
                return miniMult * _ScatterMultipliers[1];
            }
            if (epicCount + maxCount + miniCount == 3)
            {
                return epicMult * maxMult * miniMult * _ScatterMultipliers[0];
            }
            return 0;
        }

        public byte[] GetScatterPositionArray()
        {
            var position = new byte[] { 255, 255, 255, 255, 255 };
            var index = 0;
            for (var i = 1; i < 4; i++)
            {
                for (var j = 1; j < 4; j++)
                {
                    if (GetElement(i, j) > 9)
                    {
                        position[index++] = (byte)(j * 5 + i);
                    }
                }
            }
            return position;
        }

        public int GetScatterCount()
        {
            var count = 0;
            for (var i = 1; i < 4; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    if (GetElement(i, j) == 10)
                    {
                        count++;
                    }
                }
            }
            return count;
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
            var mat = new int[5, 5];
            for (var i = 0; i < 5; i++)
            {
                var l = _Reels[i].Length;
                var p = SoftwareRng.Next(l);
                var lspec = _ReelsSpecial[reelsSet][i].Length;
                var pspec = SoftwareRng.Next(lspec);
                for (var j = 0; j < 5; j++)
                {
                    mat[i, j] = _Reels[i][(p + j) % l];
                    var spec = _ReelsSpecial[reelsSet][i][(pspec + j) % lspec];
                    if (spec == 10)
                    {
                        mat[i, j] = spec;
                    }
                }
            }
            return mat;
        }

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 3, 9, 6, 9, 4, 3, 8, 6, 6, 6, 3, 8, 4, 4, 7, 3, 5, 9, 7, 8, 3, 6, 9, 4, 4, 7, 8, 5, 5, 6, 6, 5, 9, 9, 8, 6, 8, 9, 9, 6, 8, 6, 7, 9, 8, 7, 7, 8, 6, 8, 7, 4, 9, 8, 6, 4, 7, 7, 6, 8, 4, 9, 4, 9, 6, 6, 8, 7, 7, 3, 4, 6, 6, 9, 8, 7, 9, 9, 6, 6, 7, 9, 9 };
            fakeReels[1] = new[] { 3, 9, 6, 9, 4, 3, 4, 6, 6, 9, 4, 4, 9, 3, 5, 5, 9, 5, 3, 7, 6, 6, 8, 9, 6, 9, 5, 6, 3, 8, 8, 9, 6, 3, 5, 5, 6, 3, 9, 8, 5, 9, 8, 9, 7, 7, 7, 9, 9, 9, 5, 5, 7, 7, 8, 8, 7, 4, 7, 9, 8, 7, 6, 9, 7, 7, 6, 7, 5, 6, 9, 7, 5, 7, 9, 6, 7, 5, 5, 8, 7, 5, 8, 5, 8, 8, 7, 8 };
            fakeReels[2] = new[] { 3, 9, 6, 9, 4, 3, 8, 6, 6, 6, 3, 8, 4, 4, 7, 3, 5, 9, 7, 8, 3, 6, 9, 4, 4, 7, 8, 5, 5, 6, 6, 5, 9, 9, 8, 6, 8, 9, 9, 6, 8, 6, 7, 9, 8, 7, 7, 8, 6, 8, 7, 4, 9, 8, 6, 4, 7, 7, 6, 8, 4, 9, 4, 9, 6, 6, 8, 7, 7, 3, 4, 6, 6, 9, 8, 7, 9, 9, 6, 6, 7, 9, 9 };
            fakeReels[3] = new[] { 9, 5, 5, 5, 4, 9, 4, 4, 7, 4, 5, 9, 9, 7, 3, 9, 4, 4, 4, 3, 6, 9, 5, 8, 8, 6, 8, 3, 5, 5, 5, 9, 9, 9, 8, 4, 4, 5, 5, 7, 7, 7, 5, 5, 8, 8, 9, 9, 7, 4, 9, 8, 5, 7, 4, 4, 4, 9, 8, 7, 4, 7, 4, 8, 8, 5, 7, 6, 6, 5, 9, 8, 8, 5, 9, 9, 7, 8, 5, 8, 5, 7, 7, 9, 7 };
            fakeReels[4] = new[] { 3, 5, 5, 5, 4, 7, 9, 9, 3, 8, 4, 4, 7, 5, 5, 9, 7, 4, 4, 4, 5, 9, 9, 4, 7, 3, 9, 9, 9, 6, 5, 9, 5, 8, 8, 8, 9, 4, 4, 9, 8, 9, 8, 7, 7, 5, 5, 8, 6, 6, 8, 8, 8, 4, 4, 9, 9, 7, 4, 4, 8, 6, 6, 7, 4, 9, 6, 6, 5, 8, 4, 7, 5, 8, 8, 6, 6, 5, 9, 5, 7, 7, 7, 9 };
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
                coefficients[i] = WinForLinesEpicMegaCash[id, i];
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
            var symbols = new HelpSymbolConfigV3<object>[10];

            for (var i = 0; i < 7; i++)
            {
                symbols[i] = new HelpSymbolConfigV3<object>
                {
                    id = i + 3,
                    features = new[] { HelpSymbolFeatureV3.Regular },
                    extra = new HelpSymbolExtraV3(),
                    coefficients = GetSymbolCoefficients(i)
                };
            }
            for (var i = 7; i < 10; i++)
            {
                symbols[i] = new HelpSymbolConfigV3<object>
                {
                    id = 10 + 4 * (i - 7),
                    features = new[] { HelpSymbolFeatureV3.Scatter },
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
    }

}
