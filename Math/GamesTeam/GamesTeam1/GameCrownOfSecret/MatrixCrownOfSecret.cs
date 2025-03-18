using GameBonusEpicCrown;
using MathBaseProject.StructuresV3;
using MathForGames.BasicGameData;
using RNGUtils.RandomData;
using System;

namespace GameCrownOfSecret
{
    public class MatrixCrownOfSecret : MatrixBonusEpicCrown
    {
        /// <summary>
        /// In the bonus game, the bonus symbol is added dynamically, with the probability of 29/100 for
        /// 3 unlocked reels and 201/1000 for 4 unlocked reels
        /// The bonus symbol can not appear if all 5 reels have been unlocked.
        /// </summary>
        public static readonly double[] BonusSymbolInBonusProbabilities = { (double)29 / 100, (double)201 / 1000, 0 };

        private static readonly int[] _BonusReel = new int[]
        {
            8, 8, 5, 6, 6, 2, 2, 5, 5, 1, 4, 3, 3, 6, 8, 2, 7, 7, 5, 2, 6, 6, 4, 7, 3, 3, 8,
            8, 6, 6, 2, 5, 8, 7, 7, 5, 5, 6, 4, 8, 8, 5, 5, 8, 6, 6, 4, 4, 7, 7, 4, 6, 5, 8,
            7, 7, 6, 6, 8, 8, 4, 4, 7, 6, 6, 4, 5, 7, 7, 4, 8, 8, 2, 6, 4, 4, 7, 7, 6, 1, 7,
            8, 8, 6, 5, 5, 3, 7, 7, 5, 5, 8, 8, 7, 5, 5, 4, 4, 3, 5
        };

        public int CalculateWinLineForBonusGame(int lineNumber, int numOfActiveReels, int landedSymbol)
        {
            return GetLineForBonus(landedSymbol)
                .CalculateLineWinForBonus(WinForLinesBonusEpicCrown, numOfActiveReels, landedSymbol);
        }

        private LineCrownOfSecret GetLineForBonus(int landedSymbol)
        {
            try
            {
                var line = new LineCrownOfSecret();
                for (var i = 0; i < 5; i++)
                {
                    line.SetElement(i, landedSymbol);
                }

                return line;
            }
            catch (Exception)
            {
                return null;
            }
        }


        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public new static int[][] GetFakeReels()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[]
            {
                5, 5, 5, 3, 3, 3, 1, 5, 5, 5, 6, 6, 6, 7, 7, 7, 1, 6, 6, 6, 5, 5, 5, 7, 7, 9, 8, 8, 5, 5, 5, 5, 7, 7, 7,
                6, 6, 6, 3, 3, 5, 5, 5, 2, 4, 4, 4, 8, 8, 8, 2, 2, 3, 3, 3, 1, 2, 2, 9, 6, 6, 6, 1, 5, 5, 5, 1, 4, 4, 4,
                9, 7, 7, 7, 7, 4, 4, 4, 2, 8, 8, 9, 5, 5, 5, 2, 8, 8, 8, 9, 4, 4, 4
            };
            fakeReels[1] = new[]
            {
                8, 8, 1, 2, 2, 5, 5, 5, 9, 6, 6, 6, 0, 4, 4, 4, 7, 7, 7, 1, 5, 5, 5, 9, 6, 6, 6, 4, 4, 8, 8, 8, 3, 3, 3,
                6, 6, 6, 8, 8, 9, 7, 7, 1, 3, 3, 2, 0, 3, 2, 4, 4, 4, 9, 2, 6, 6, 6, 1, 7, 7, 7, 6, 6, 6, 4, 4, 0, 3, 3,
                3, 8, 8, 8, 6, 6, 1, 2, 2, 3, 3, 3, 5, 5, 2,
            };
            fakeReels[2] = new[]
            {
                1, 4, 4, 4, 9, 1, 6, 6, 6, 4, 4, 4, 9, 3, 3, 3, 1, 0, 3, 1, 6, 6, 6, 1, 7, 7, 7, 1, 6, 6, 6, 5, 5, 5, 1,
                8, 8, 8, 4, 4, 4, 5, 5, 7, 7, 7, 9, 2, 4, 4, 4, 3, 3, 3, 1, 8, 8, 8, 1, 7, 7, 7, 2, 2, 1, 4, 4, 4, 3, 3,
                0, 6, 6, 6, 9, 7, 7, 7, 1, 4, 4, 4, 2, 0
            };
            fakeReels[3] = new[]
            {
                4, 4, 4, 5, 0, 4, 5, 5, 5, 8, 8, 8, 6, 6, 6, 1, 2, 2, 5, 5, 5, 9, 7, 7, 1, 0, 5, 5, 5, 9, 4, 4, 8, 8, 8,
                3, 3, 3, 2, 2, 9, 5, 5, 5, 3, 3, 2, 9, 8, 8, 9, 6, 6, 8, 0, 6, 8, 8, 8, 2, 6, 6, 6, 7, 7, 1, 4, 4, 4, 8,
                8, 8, 7, 7, 3, 3, 5, 5, 2, 4, 4, 4, 5, 5, 5, 1, 0
            };
            fakeReels[4] = new[]
            {
                9, 5, 5, 5, 3, 3, 3, 1, 5, 5, 5, 6, 6, 6, 7, 7, 7, 1, 6, 6, 6, 7, 7, 7, 9, 8, 8, 8, 6, 6, 6, 1, 5, 5, 5,
                8, 8, 9, 3, 3, 3, 2, 2, 0, 1, 4, 4, 4, 9, 3, 3, 1, 4, 4, 9, 6, 6, 5, 5, 5, 2, 8, 8, 8, 5, 5, 5, 9, 7, 7,
                7, 2, 2,
            };
            return fakeReels;
        }

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja, za gratis igre sa bonus simbolom
        /// ovo je slucaj kada su otkljucan 3 i 4 rila, ne kad je i peti jer tada bonus simbol ne moze da se padne.
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReelsGratisWithBonusSymbol()
        {
            var fakeReels = new int[1][];
            fakeReels[0] = new[]
            {
                5, 5, 3, 3, 1, 6, 6, 7, 7, 1, 5, 7, 7, 9, 8, 8, 5, 3, 3, 2, 4, 4, 8, 8, 2, 2, 3, 3, 1, 2, 2, 9, 6, 6, 1,
                1, 5, 5, 2, 2, 1, 4, 4, 9, 7, 7, 4, 4, 2, 8, 8, 9, 3, 5, 5, 2, 8, 8, 4, 4
            };
            return fakeReels;
        }

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja, za gratis igre kada su sva 5 rila otkljucana
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReelsGratisWithoutBonusSymbol()
        {
            var fakeReels = new int[1][];
            fakeReels[0] = new[]
            {
                5, 5, 3, 3, 1, 6, 6, 7, 7, 1, 5, 7, 7, 8, 8, 5, 3, 3, 2, 4, 4, 8, 8, 2, 2, 3, 3, 1, 2, 2, 6, 6, 1, 1, 5,
                5, 2, 2, 1, 4, 4, 7, 7, 4, 4, 2, 8, 8, 3, 5, 5, 2, 8, 8, 4, 4
            };
            return fakeReels;
        }

        public static int[,] GetBonusMatrixArray(ref byte[] addArray)
        {
            var mat = new int[5, 3];
            var reelLength = _BonusReel.Length;
            var randomNumber = SoftwareRng.Next(reelLength);

            var bonusProb = addArray[3] == 3
                ? BonusSymbolInBonusProbabilities[0]
                : addArray[3] == 4
                    ? BonusSymbolInBonusProbabilities[1]
                    : 0;

            var didBonusLand = SoftwareRng.Next() < bonusProb;
            var pickedSymbol = didBonusLand ? 9 : _BonusReel[randomNumber];

            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    mat[i, j] = pickedSymbol;
                }
            }

            return mat;
        }

        public static void InitializeAddArrayIfNeeded(ref byte[] addArray)
        {
            if (addArray == null)
            {
                addArray = new byte[5];
            }
        }

        public new static HelpConfigV3<object> GetHelpConfigV3(int numberOfLines)
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)96.74,
                symbols = GetHelpSymbolConfigV3(),
                lines = GetHelpLineConfigV3(numberOfLines)
            };

            return helpV3;
        }

        private static HelpSymbolConfigV3<object>[] GetHelpSymbolConfigV3()
        {
            var symbols = new HelpSymbolConfigV3<object>[10];
            for (var i = 0; i < 10; i++)
            {
                symbols[i] = new HelpSymbolConfigV3<object>
                {
                    id = i,
                    extra = new HelpSymbolExtraV3(),
                    coefficients = GetSymbolCoefficients(i),
                    features = new[] { i == 9 ? HelpSymbolFeatureV3.FreeSpin : HelpSymbolFeatureV3.Regular }
                };
            }

            return symbols;
        }

        private static HelpLineConfigV3[] GetHelpLineConfigV3(int numberOfLines)
        {
            var lines = new HelpLineConfigV3[numberOfLines];
            for (var i = 0; i < numberOfLines; i++)
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
    }
}
