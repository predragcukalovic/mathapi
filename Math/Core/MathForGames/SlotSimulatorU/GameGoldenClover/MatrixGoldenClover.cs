using MathBaseProject.BaseMathData;
using MathBaseProject.StructuresV3;
using MathForGames.BasicGameData;
using RNGUtils.RandomData;
using System;

namespace MathForGames.GameGoldenClover
{
    public class MatrixGoldenClover : Matrix
    {
        #region Data structure

        public class GoldenCloverConfig
        {
            public bool DrawingAllowed { get; set; }
            public int MiniFixedValue { get; set; }
            public int MinorFixedValue { get; set; }
            public long MajorBaseValue { get; set; }
            public long MajorMinValue { get; set; }
            public long MajorMaxValue { get; set; }
            public long MajorCurrentValue { get; set; }
            public long MajorAvgValue { get; set; }
            public long GrandBaseValue { get; set; }
            public long GrandMinValue { get; set; }
            public long GrandMaxValue { get; set; }
            public long GrandCurrentValue { get; set; }
            public long GrandAvgValue { get; set; }
            public int MajorJpId { get; set; }
            public int GrandJpId { get; set; }
            public decimal GrandContribution { get; set; }
            public decimal MajorContribution { get; set; }
        }

        public class ExtraGoldenCloverData
        {
            public int[] globalJackpotData { get; set; }
            public int[] scatterWins { get; set; }
            public int[] scatterPosition { get; set; }
            public long scatterWin { get; set; }
            public byte freeSpinsLeft { get; set; }
            public bool doubleWin { get; set; }
            //public int scatterId { get; set; }
        }

        #endregion Data structure

        #region Private properties

        private const int PROB_EXPAND = 60;
        private const int PROB_MINI = 60;
        private const int PROB_MINOR = 88;
        private const int PROB_JACKPOT_MAX = 100;
        private static readonly double[] _ProbGrand = { -1.0, -1.0, -1.0, -1.0, -1.0 };
        private static readonly double[] _ProbMajor = { -1.0, -1.0, -1.0, -1.0, -1.0 };

        #endregion

        #region Public properties

        public static readonly byte[] ScatterValues = { 25, 50, 75 };
        public static readonly int[] GoldJackpotWins = { 1, 2, 3, 4, 5, 10, 15, 20, 50, 100 };

        public static int[] PlayLines = { 50 };

        #endregion

        #region Private methods

        private bool IsReelHaveSpecial(int reel)
        {
            for (var i = 0; i < 3; i++)
            {
                if (GetElement(reel, i) >= 9)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber">Broj linije.</param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLineExtra).CalculateLineWin(LineWinsForGames.WinForLinesGoldenClover, LineWinsForGames.WinForWildGoldenClover, 0, 1);
        }

        public void SetAllData(bool gratis, ref byte[] addArray)
        {
            if (gratis)
            {
                return;
            }
            addArray[16] = 0;
            addArray[17] = 0;
            for (var i = 1; i < 4; i++)
            {
                if (!IsReelHaveSpecial(i) && SoftwareRng.Next(PROB_EXPAND) == 0)
                {
                    SetElement(i, (int)SoftwareRng.Next(3), 0);
                }
            }
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    SetJackpotField(i, j, ref addArray[16], ref addArray[17]);
                }
            }
        }

        public bool IsGratis()
        {
            var count = 0;
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (GetElement(i, j) >= 11)
                    {
                        count++;
                    }
                }
            }
            return count >= 5;
        }

        private void SetJackpotField(int reel, int row, ref byte major, ref byte grand)
        {
            if (GetElement(reel, row) != 11)
            {
                return;
            }
            var c = GetJackpotCoefficiet(reel, ref major, ref grand) % 10;
            SetElement(reel, row, 11 + c);
        }

        public static int GetJackpotCoefficiet(int reel, ref byte major, ref byte grand)
        {
            if (_ProbGrand[reel] > 0 && SoftwareRng.Next() < _ProbGrand[reel] && reel % 2 == 0 && (grand & (1 << (reel / 2))) == 0)
            {
                grand += (byte)(1 << (reel / 2));
                return 5 + 10 * GetRandomGoldJackpotIndex();
            }
            if (_ProbMajor[reel] > 0 && SoftwareRng.Next() < _ProbMajor[reel] && major == 0)
            {
                major = 1;
                return 4;
            }
            var rndJackpot = SoftwareRng.Next(PROB_JACKPOT_MAX);
            if (rndJackpot >= PROB_MINOR)
            {
                return 3;
            }
            if (rndJackpot >= PROB_MINI)
            {
                return 2;
            }
            return 1 + 10 * GetRandomGoldJackpotIndex();
        }

        public static int GetRandomGoldJackpotIndex()
        {
            var goldJackpotProbs = new[] { 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 5, 5, 5, 6, 6, 7, 7, 8, 9 };
            return goldJackpotProbs[SoftwareRng.Next(goldJackpotProbs.Length)];
        }

        public static void SetJackpotLimits(GoldenCloverConfig config, int bet)
        {
            var grandProb = new[] { -1.0, -1.0, -1.0, -1.0, -1.0 };
            var majorProb = new[] { -1.0, -1.0, -1.0, -1.0, -1.0 };
            if (config.MiniFixedValue <= 0 || config.MinorFixedValue <= 0)
            {
                throw new Exception("Bad Mini or Minor Jackpot Fixed Values!");
            }
            if (!config.DrawingAllowed)
            {
                return;
            }
            var reelProb = new[] { 5.0 / 50.0, 4.0 / 45.0, 5.0 / 51.0, 4.0 / 52.0, 5.0 / 48.0 };
            const double gratisProb = 0.01228;
            const double respinAvg = 8.5; //TODO:provera
            if (config.GrandCurrentValue >= 0.95 * config.GrandMaxValue)
            {
                grandProb = new[] { 1.0, -1.0, 1.0, -1.0, 1.0 };
            }
            else if (config.GrandCurrentValue > 0.5 * (config.GrandMaxValue + config.GrandAvgValue))
            {
                grandProb = new[] { 1.2, -1.0, 1.2, -1.0, 1.2 };
            }
            else if (config.GrandMinValue <= config.GrandAvgValue)
            {
                var ppp = reelProb[0] * reelProb[2] * reelProb[4];
                var range = (config.GrandAvgValue - config.GrandBaseValue) * ppp / (bet * (double)config.GrandContribution * 0.01); //TODO: Potvrda
                for (var i = 0; i < 5; i++)
                {
                    grandProb[i] = Math.Pow(range, 1.0 / 3.0);
                    grandProb[i] *= 1.0 - gratisProb + gratisProb * respinAvg;
                }
            }
            if (config.MajorCurrentValue >= 0.95 * config.MajorMaxValue)
            {
                majorProb = new[] { 1.0, 1.0, 1.0, 1.0, 1.0 };
            }
            else if (config.MajorCurrentValue > 0.5 * (config.MajorMaxValue + config.MajorAvgValue))
            {
                majorProb = new[] { 1.2, 1.2, 1.2, 1.2, 1.2 };
            }
            else if (config.MajorMinValue <= config.MajorAvgValue)
            {
                var range = (config.MajorAvgValue - config.MajorBaseValue) / (bet * (double)config.MajorContribution * 0.01); //TODO: Potvrda
                for (var i = 0; i < 5; i++)
                {
                    majorProb[i] = range * 8.4;
                    majorProb[i] = (majorProb[i] * reelProb[i]);
                    majorProb[i] *= 1.0 - gratisProb + gratisProb * respinAvg;
                }
            }
            _ProbGrand[0] = 1.0 / (grandProb[0] * 4.5);
            _ProbGrand[2] = 1.0 / (grandProb[2] * 4.5);
            _ProbGrand[4] = 1.0 / (grandProb[4] * 4.5);
            _ProbGrand[1] = -1.0;
            _ProbGrand[3] = -1.0;
            for (var i = 0; i < 5; i++)
            {
                if (_ProbGrand[i] >= 0)
                {
                    _ProbMajor[i] = (1.0 / majorProb[i]) / (1 - _ProbGrand[i]);
                }
                else
                {
                    _ProbMajor[i] = (1.0 / majorProb[i]);
                }
            }
        }

        #endregion

        #region V3Structs

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 12, 12, 12, 8, 8, 1, 6, 6, 10, 6, 2, 2, 9, 7, 7, 7, 3, 3, 10, 7, 7, 3, 3, 5, 5, 5, 9, 4, 4, 7, 4, 4, 7, 7, 1, 8, 8, 1, 8, 6, 6, 10, 5, 6, 5, 5, 2, 2, 6, 6 };
            fakeReels[1] = new[] { 12, 13, 8, 8, 1, 8, 5, 5, 9, 5, 6, 6, 2, 6, 2, 2, 5, 2, 5, 5, 4, 4, 7, 7, 4, 4, 9, 6, 6, 4, 3, 3, 8, 3, 3, 8, 7, 1, 8, 7, 7, 1, 7, 6, 6 };
            fakeReels[2] = new[] { 13, 12, 12, 8, 8, 4, 4, 8, 7, 7, 10, 7, 5, 5, 2, 2, 9, 5, 5, 1, 3, 3, 6, 1, 6, 6, 3, 3, 1, 7, 7, 2, 7, 2, 8, 8, 9, 8, 5, 5, 4, 3, 3, 4, 4, 2, 2, 10, 6, 6, 6 };
            fakeReels[3] = new[] { 12, 12, 5, 5, 9, 5, 4, 4, 8, 8, 2, 8, 2, 2, 9, 8, 8, 7, 7, 8, 7, 7, 7, 1, 3, 3, 5, 5, 3, 6, 5, 6, 6, 1, 4, 4, 7, 7, 1, 5, 5, 7, 7, 4, 4, 6, 4, 6, 8, 6, 8, 8 };
            fakeReels[4] = new[] { 14, 12, 12, 2, 2, 5, 5, 1, 7, 7, 4, 4, 7, 7, 9, 6, 6, 3, 3, 8, 3, 8, 8, 1, 6, 6, 9, 7, 7, 5, 5, 7, 10, 5, 5, 4, 4, 10, 4, 8, 8, 1, 5, 5, 10, 6, 6, 2 };

            return fakeReels;
        }

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetGratisFakeReels()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 16, 21, 13, 24, 24, 24, 14, 26, 26, 12, 22, 22, 22, 13, 27, 27, 27, 27, 27, 14, 26, 26, 12, 24, 24, 12, 25, 25, 21, 21, 21, 12, 26, 26, 26, 26, 13, 23, 23, 23, 25, 25, 25, 25, 12, 24, 24, 28, 28, 12, 28, 28, 28, 13, 22, 22, 12, 25, 25, 25, 27, 27, 27, 27, 27, 14, 26, 26, 26, 12, 28, 28, 28, 21, 21, 24, 15, 23, 23, 23, 28, 28, 12, 28, 13, 22, 22, 22, 24, 24, 24, 25, 25, 12, 28, 28, 28, 26, 26, 26, 25, 25 };
            fakeReels[1] = new[] { 13, 21, 21, 15, 22, 22, 22, 12, 25, 25, 25, 25, 14, 12, 26, 26, 26, 21, 21, 21, 23, 23, 12, 24, 24, 28, 28, 13, 22, 22, 14, 27, 27, 27, 27, 12, 27, 12, 21, 21, 13, 24, 24, 24, 12, 25, 25, 25, 14, 28, 28, 13, 28, 28, 23, 23, 23, 12, 27, 27, 27, 25, 25, 25, 21, 21, 12, 24, 24, 24, 28, 28, 28, 27, 27, 28, 28, 12, 22, 22, 13, 26, 26, 26, 28, 28, 28, 26, 26, 24, 24, 12, 25, 25, 25, 27, 27, 21, 21, 23, 23, 23 };
            fakeReels[2] = new[] { 14, 21, 12, 27, 27, 27, 13, 28, 28, 12, 22, 14, 22, 22, 16, 28, 28, 24, 24, 24, 12, 23, 23, 23, 21, 21, 21, 12, 13, 25, 25, 25, 24, 24, 12, 26, 26, 26, 26, 28, 28, 28, 13, 27, 27, 27, 25, 25, 13, 24, 24, 28, 28, 28, 13, 25, 25, 25, 25, 12, 22, 22, 15, 26, 26, 26, 26, 26, 24, 24, 12, 23, 23, 23, 27, 27, 14, 21, 21, 21, 28, 28, 12, 28, 28, 26, 26, 26, 27, 27, 27, 12, 23, 23, 23, 26, 26, 26, 26, 12, 25, 25 };
            fakeReels[3] = new[] { 12, 21, 21, 14, 25, 25, 25, 13, 27, 12, 24, 24, 28, 28, 14, 27, 27, 27, 27, 13, 23, 23, 23, 12, 28, 28, 28, 28, 21, 21, 22, 22, 12, 24, 24, 28, 28, 14, 28, 28, 28, 12, 26, 26, 26, 26, 13, 22, 12, 24, 24, 25, 25, 28, 28, 12, 23, 23, 23, 26, 26, 28, 28, 12, 28, 28, 28, 28, 13, 24, 24, 25, 12, 25, 25, 27, 27, 27, 13, 28, 28, 28, 26, 26, 26, 27, 27, 27, 26, 26, 26, 21, 21, 12, 23, 23, 25, 25, 25, 22, 22, 22 };
            fakeReels[4] = new[] { 21, 21, 13, 24, 24, 12, 28, 28, 14, 22, 22, 12, 13, 28, 14, 28, 28, 23, 23, 23, 27, 27, 27, 13, 28, 26, 26, 26, 12, 24, 24, 28, 28, 23, 23, 23, 12, 26, 26, 26, 26, 21, 21, 25, 25, 28, 28, 12, 24, 24, 25, 25, 25, 12, 24, 24, 24, 13, 28, 28, 26, 26, 26, 26, 28, 28, 13, 21, 21, 21, 12, 25, 25, 25, 16, 28, 28, 28, 28, 15, 26, 26, 26, 26, 14, 22, 12, 22, 22, 22, 24, 24, 27, 27, 27, 12, 25, 25, 12, 23, 23, 23 };

            return fakeReels;
        }

        /// <summary>
        /// Vraća niz koeficijenata za id simbola.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int[] GetSymbolCoefficients(int id)
        {
            if (id == 9)
            {
                return LineWinsForGames.WinForScattersGoldenClover;
            }
            if (id == 10)
            {
                return new[] { (int)ScatterValues[0], ScatterValues[1], ScatterValues[2] };
            }
            if (id == 12)
            {
                return GoldJackpotWins;
            }
            var coefficients = new int[5];
            for (var i = 0; i < 5; i++)
            {
                coefficients[i] = LineWinsForGames.WinForLinesGoldenClover[id, i];
            }
            return coefficients;
        }

        public static HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)90.0,
                symbols = GetHelpSymbolConfigV3(),
                lines = GetHelpLineConfigV3()
            };

            return helpV3;
        }

        private static HelpSymbolConfigV3<object>[] GetHelpSymbolConfigV3()
        {
            var symbols = new HelpSymbolConfigV3<object>[17];
            for (var i = 0; i < 17; i++)
            {
                symbols[i] = new HelpSymbolConfigV3<object>
                {
                    id = i,
                    extra = new HelpSymbolExtraV3(),
                    coefficients = GetSymbolCoefficients(i)
                };
                if (i == 9 || i == 10)
                {
                    symbols[i].features = new[] { HelpSymbolFeatureV3.Scatter };
                }
                else if (i > 10)
                {
                    symbols[i].features = new[] { HelpSymbolFeatureV3.Bonus };
                }
                else
                {
                    symbols[i].features = new[] { HelpSymbolFeatureV3.Regular };
                }
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
