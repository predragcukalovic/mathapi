using System.Collections.Generic;
using RNGUtils.RandomData;

namespace MathCombination.ReelsData
{
    public class ReelsReader
    {
        /// <summary>
        /// Daje matricu n×6 na osnovu rilova.
        /// </summary>
        /// <param name="reels"></param>
        /// <returns></returns>
        public static int[,] ReadMatrixArrayFromReels(params List<byte>[] reels)
        {
            var matrixArray = new int[reels.Length, 7];
            for (var i = 0; i < reels.Length; i++)
            {
                var size = reels[i].Count;
                var random = (int)SoftwareRng.Next(size);
                for (var j = 0; j < 7; j++)
                {
                    matrixArray[i, j] = reels[i][(random + j) % size];
                }
            }
            return matrixArray;
        }
    }
}
