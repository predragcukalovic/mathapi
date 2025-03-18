using RNGUtils.RandomData;
using System.Collections.Generic;
using System.Linq;

namespace LibraryBuyBonus
{
    public class BonusMatrixLibrary
    {
        public static List<int> GetShuffledArray(int countOfReels, params bool[] symbolInReel)
        {
            return GetShuffledArray(countOfReels, countOfReels, symbolInReel);
        }

        public static List<int> GetShuffledArray(int countOfReels, int arrayLength, params bool[] symbolInReel)
        {
            var tmp = new List<int>();
            for (var i = 0; i < countOfReels; i++)
            {
                if (symbolInReel[i])
                {
                    tmp.Add(i);
                }
            }
            var l = new List<int>();
            for (var i = arrayLength - 1; i >= 0; i--)
            {
                var r = (int)SoftwareRng.Next(tmp.Count);
                l.Add(tmp[r]);
                tmp.RemoveAt(r);
            }
            return l;
        }

        /// <summary>
        /// Postavlja bonus simbole u matrici.
        /// </summary>
        /// <param name="matrixArray">Matrica</param>
        /// <param name="bonusSymbol">Bonus simbol</param>
        /// <param name="countOfBonusSymbols">Koliko bonus simbola treba</param>
        /// <param name="reels">Koliko rilova ima</param>
        /// <param name="rows">Koliko redova</param>
        /// <param name="offset">Ako postoje gornji redovi, koliko njih</param>
        /// <param name="symbolInReel">Da li u odgovarajucem rilu sme da se nadje bonus simbol?</param>
        public static void SetBonusSymbols(ref int[,] matrixArray, int bonusSymbol, int countOfBonusSymbols, int reels, int rows, int offset = 0, params bool[] symbolInReel)
        {
            var reelsForSymbol = GetShuffledArray(reels, countOfBonusSymbols, symbolInReel);
            foreach (var reel in reelsForSymbol)
            {
                matrixArray[reel, SoftwareRng.Next(rows) + offset] = bonusSymbol;
            }
        }

        public static int GetRandomDistributionNumber(params int[] probs)
        {
            var sum = probs.Sum();
            var n = probs.Length;
            int s = 0;
            var r = SoftwareRng.Next(sum);
            for (var i = 0; i < n; i++)
            {
                s += probs[i];
                if (r < s)
                {
                    return i;
                }
            }
            return -1;
        }

        private static int GetRandomReelPosition(List<byte> reel, int bonusSymbol, bool shouldHaveBonusSymbol, int rows, int offset)
        {
            var positions = new List<int>();
            var n = reel.Count();
            for (var i = 0; i < n; i++)
            {
                var haveBonus = false;
                for (var j = 0; j < rows; j++)
                {
                    if (reel[(i + j + offset) % n] == bonusSymbol)
                    {
                        haveBonus = true;
                        break;
                    }
                }
                if (haveBonus == shouldHaveBonusSymbol)
                {
                    positions.Add(i);
                }
            }
            return positions[(int)SoftwareRng.Next(positions.Count)];
        }

        public static int[,] ReadDirectedMatrixArrayFromReels(int bonusSymbol, int countOfBonusSymbols, int rows, int rowsSize, bool[] symbolInReel, int offset, params List<byte>[] reels)
        {
            var matrixArray = new int[reels.Length, rowsSize];
            var reelsForSymbol = GetShuffledArray(reels.Length, countOfBonusSymbols, symbolInReel);
            for (var i = 0; i < reels.Length; i++)
            {
                var size = reels[i].Count;
                var random = GetRandomReelPosition(reels[i], bonusSymbol, reelsForSymbol.Contains(i), rows, offset);
                for (var j = 0; j < rowsSize; j++)
                {
                    matrixArray[i, j] = reels[i][(random + j) % size];
                }
            }
            return matrixArray;
        }

        public static int[,] ReadDirectedMatrixArrayFromReelsWithPredefinedPositions(int bonusSymbol, int countOfBonusSymbols, int rows, int rowsSize, bool[] symbolInReel, int offset,
            int[] positions, params List<byte>[] reels)
        {
            var matrixArray = new int[reels.Length, rowsSize];
            var reelsForSymbol = GetShuffledArray(reels.Length, countOfBonusSymbols, symbolInReel);
            for (var i = 0; i < reels.Length; i++)
            {
                var size = reels[i].Count;
                if (reelsForSymbol.Contains(i))
                {
                    var random = GetRandomDistributionNumber(Enumerable.Repeat(1, positions.Length).ToArray());
                    for (var j = 0; j < rowsSize; j++)
                    {
                        matrixArray[i, j] = reels[i][(positions[random] - 1 + j + size) % size];
                    }
                    //Add dynamic wild
                    var wildPosition = SoftwareRng.Next(1, rowsSize - 1);
                    matrixArray[i, wildPosition] = 0;
                }
                else
                {
                    var random = GetRandomReelPosition(reels[i], bonusSymbol, false, rows, offset);
                    for (var j = 0; j < rowsSize; j++)
                    {
                        matrixArray[i, j] = reels[i][(random + j) % size];
                    }
                }
            }

            return matrixArray;
        }
    }
}
