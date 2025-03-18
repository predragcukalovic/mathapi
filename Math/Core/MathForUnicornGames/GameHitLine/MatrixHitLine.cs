using MathBaseProject.BaseMathData;
using MathBaseProject.StructuresV3;
using MathForUnicornGames.BasicUnicornData;
using RNGUtils.RandomData;

namespace MathForUnicornGames.GameHitLine
{
    public class MatrixHitLine : Matrix
    {
        public MatrixHitLine() : base(5) { }

        private static double[] _ScenarioTable = new[] { 0.958184999, 0.01165, 0.01165, 0.01165, 0.002271667, 0.002271667, 0.002271667, 0.00005 };
        private static double[] _Reel2Table = new[] { 0.086, 0.172, 0.215, 0.172, 0.086, 0.086, 0.0688, 0.02293, 0.01529, 0.01529, 0.01529, 0.0454 };
        private static double[] _Table1 = new[] { 0.065, 0.065, 0.08125, 0.065, 0.0975, 0.0975, 0.0975, 0.0975, 0.0975, 0.0975, 0.0975, 0.04125 };
        private static double[] _Table2 = new[] { 0.09, 0.135, 0.135, 0.135, 0.135, 0.135, 0.108, 0.027, 0.018, 0.018, 0.018, 0.046 };
        private static double[] _Table3 = new[] { 0.1, 0.15, 0.15, 0.15, 0.15, 0.1, 0.08, 0.0267, 0.0178, 0.0178, 0.0178, 0.0399 };

        #region Public properties

        public static int[] PlayLines = { 10 };
        public static readonly int[,] WinForLinesHitLine =
        {
            {0, 0, 50, 200, 1000 },
            {0, 0, 20, 80, 400 },
            {0, 0, 20, 80, 400 },
            {0, 0, 10, 40, 200 },
            {0, 0, 10, 40, 200 },
            {0, 0, 5, 20, 100 },
            {0, 0, 5, 20, 100 }
        };
        public static int[] MultiplierReward = new[] { 2, 3, 4, 5, 6, 8, 10, 12, 14, 16, 18, 20 };

        #endregion

        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, UnicornGlobalData.GameLineShifted).CalculateLineWin(WinForLinesHitLine, null, -1, 1);
        }

        private int GetNumberFromTable(double[] table)
        {
            var n = table.Length;
            var sum = 0.0;
            var rnd = SoftwareRng.Next();
            for (var i = 0; i < n; i++)
            {
                sum += table[i];
                if (rnd < sum)
                {
                    return i;
                }
            }
            return -1;
        }

        public void SetRewards()
        {
            var scenario = GetNumberFromTable(_ScenarioTable);
            switch (scenario)
            {
                case 0:
                    SetNonMiddleReward(1);
                    SetNonMiddleReward(2);
                    SetNonMiddleReward(3);
                    break;
                case 1:
                    SetElement(1, 2, 7);
                    SetNonMiddleReward(2);
                    SetNonMiddleReward(3);
                    break;
                case 2:
                    SetNonMiddleReward(1);
                    SetElement(2, 2, 7);
                    SetNonMiddleReward(3);
                    break;
                case 3:
                    SetNonMiddleReward(1);
                    SetNonMiddleReward(2);
                    SetElement(3, 2, 7);
                    break;
                case 4:
                    SetNonMiddleReward(1);
                    SetElement(2, 2, 7);
                    SetElement(3, 2, 7);
                    break;
                case 5:
                    SetElement(1, 2, 7);
                    SetNonMiddleReward(2);
                    SetElement(3, 2, 7);
                    break;
                case 6:
                    SetElement(1, 2, 7);
                    SetElement(2, 2, 7);
                    SetNonMiddleReward(3);
                    break;
                case 7:
                    SetElement(1, 2, 7);
                    SetElement(2, 2, 7);
                    SetElement(3, 2, 7);
                    break;
            }
            SetRewardId();
        }

        private void SetNonMiddleReward(int reel)
        {
            var rnd = SoftwareRng.Next(100);
            if (rnd < 5)
            {
                SetElement(reel, 0, 7);
            }
            if (rnd >= 5 && rnd < 15)
            {
                SetElement(reel, 1, 7);
            }
            if (rnd >= 85 && rnd < 95)
            {
                SetElement(reel, 3, 7);
            }
            if (rnd >= 95)
            {
                SetElement(reel, 4, 7);
            }
        }

        private void SetRewardId()
        {
            var subs = new int[3];
            subs[0] = GetNumberFromTable(_Reel2Table) + 10;
            if (subs[0] <= 13)
            {
                subs[1] = GetNumberFromTable(_Table1) + 10;
            }
            else if (subs[0] >= 17)
            {
                subs[1] = GetNumberFromTable(_Table3) + 10;
            }
            else
            {
                subs[1] = GetNumberFromTable(_Table2) + 10;
            }
            if (subs[0] <= 15 && subs[1] <= 15)
            {
                subs[2] = GetNumberFromTable(_Table1) + 10;
            }
            else if ((subs[0] == 16 && subs[1] < 17) || (subs[1] == 16 && subs[0] < 17))
            {
                subs[2] = GetNumberFromTable(_Table2) + 10;
            }
            else
            {
                subs[2] = GetNumberFromTable(_Table3) + 10;
            }
            var subind = 0;
            for (var i = 1; i < 4; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    if (GetElement(i, j) == 7)
                    {
                        SetElement(i, j, subs[subind++]);
                    }
                }
            }
        }

        /// <summary>
        /// Konstruiše matricu na osnovu dvodimenzionalnog niza za igru HitLine.
        /// </summary>
        /// <param name="matrix"></param>
        public void FromMatrixArrayHitLine(int[,] matrix)
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
            fakeReels[0] = new[] { 0, 0, 0, 3, 3, 3, 4, 4, 4, 10, 0, 0, 0, 5, 5, 5, 3, 3, 3, 0, 0, 6, 6, 6, 6, 10, 0, 0, 5, 5, 5, 0, 0, 0, 0, 3, 3, 3, 4, 4, 4, 4, 10, 1, 1, 1, 1, 5, 5, 5, 6, 6, 6, 1, 1, 1, 1, 6, 6, 6, 6, 10, 5, 5, 5, 5, 2, 2, 2, 4, 4, 4, 10, 2, 2, 2, 4, 4, 3, 3, 5, 5, 5, 5, 2, 2, 2, 6, 6, 6, 2, 2, 2, 2, 5, 5, 5, 3, 3, 3, 5, 5 };
            fakeReels[1] = new[] { 0, 0, 0, 3, 3, 3, 4, 4, 4, 10, 0, 0, 0, 5, 5, 5, 3, 3, 3, 0, 0, 6, 6, 6, 6, 10, 0, 0, 5, 5, 5, 0, 0, 0, 0, 3, 3, 3, 4, 4, 4, 4, 10, 1, 1, 1, 1, 5, 5, 5, 6, 6, 6, 1, 1, 1, 1, 6, 6, 6, 6, 10, 5, 5, 5, 5, 2, 2, 2, 4, 4, 4, 10, 2, 2, 2, 4, 4, 3, 3, 3, 5, 5, 5, 5, 2, 2, 2, 2, 6, 6, 6, 2, 2, 2, 5, 5, 5, 3, 3, 3, 5, 5 };
            fakeReels[2] = new[] { 0, 0, 0, 3, 3, 3, 4, 4, 4, 10, 0, 0, 0, 5, 5, 5, 3, 3, 3, 0, 0, 6, 6, 6, 6, 10, 0, 0, 5, 5, 5, 0, 0, 0, 0, 3, 3, 3, 4, 4, 4, 4, 10, 1, 1, 1, 1, 5, 5, 5, 6, 6, 6, 1, 1, 1, 1, 6, 6, 6, 6, 10, 5, 5, 5, 5, 2, 2, 2, 4, 4, 4, 10, 2, 2, 2, 4, 4, 3, 3, 5, 5, 5, 5, 2, 2, 2, 6, 6, 6, 2, 2, 2, 5, 5, 5, 3, 3, 3, 5, 5 };
            fakeReels[3] = new[] { 0, 0, 0, 3, 3, 3, 4, 4, 4, 10, 0, 0, 0, 5, 5, 5, 3, 3, 3, 0, 0, 6, 6, 6, 6, 10, 0, 0, 5, 5, 5, 0, 0, 0, 0, 3, 3, 3, 4, 4, 4, 4, 10, 1, 1, 1, 1, 5, 5, 5, 6, 6, 6, 1, 1, 1, 1, 6, 6, 6, 6, 10, 5, 5, 5, 5, 2, 2, 2, 4, 4, 4, 10, 2, 2, 2, 4, 4, 3, 3, 5, 5, 5, 5, 2, 2, 2, 6, 6, 6, 2, 2, 2, 2, 2, 2, 5, 5, 5, 3, 3, 3, 5, 5 };
            fakeReels[4] = new[] { 0, 0, 0, 3, 3, 3, 4, 4, 4, 10, 0, 0, 0, 5, 5, 5, 3, 3, 3, 0, 0, 6, 6, 6, 10, 0, 0, 5, 5, 5, 0, 0, 0, 0, 3, 3, 3, 4, 4, 4, 4, 10, 1, 1, 1, 1, 5, 5, 5, 6, 6, 6, 1, 1, 1, 6, 6, 6, 6, 10, 5, 5, 5, 2, 2, 2, 4, 4, 4, 10, 2, 2, 2, 4, 4, 3, 3, 5, 5, 5, 5, 2, 2, 2, 2, 6, 6, 6, 2, 2, 2, 2, 5, 5, 5, 3, 3, 3, 5, 5 };

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
                coefficients[i] = WinForLinesHitLine[id, i];
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
