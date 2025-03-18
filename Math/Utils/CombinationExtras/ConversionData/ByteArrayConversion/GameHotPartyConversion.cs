using Papi.GameServer.Utils.Converters;
using MathCombination.CombinationData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CombinationExtras.ConversionData.ByteArrayConversion
{
    class GameHotPartyConversion
    {
        public static byte[] ToByteArray(int numOfGratisGames, long newCreditMeter, bool isCurrentGameGratis,
            ICombination combination)
        {
            var winningLinesInBytes = new List<byte[]>();
            var size = 0;
            for (var i = 0; i < combination.NumberOfWinningLines; i++)
            {
                var winingLine = CommonByteArrayConversion.WinningLineToBytesArray(combination.LinesInformation[i]);
                winningLinesInBytes.Add(winingLine);
                size += winingLine.Length;
            }

            var data = new byte[55 + size];

            var matrixArray = Enumerable.Repeat((byte)255, 15).ToArray();

            var l = 0;
            for (var i = 0; i < combination.Matrix.GetLength(1); i++)
            {
                for (var j = 0; j < combination.Matrix.GetLength(0); j++)
                {
                    matrixArray[l++] = combination.Matrix[j, i];
                }
            }

            Array.Copy(matrixArray, 0, data, 0, matrixArray.Length);

            var k = matrixArray.Length;
            var emptyArray = Enumerable.Repeat((byte)0, 7).ToArray();
            Array.Copy(emptyArray, 0, data, k, 7);
            k += 7;

            emptyArray = Enumerable.Repeat((byte)255, 24).ToArray();
            Array.Copy(emptyArray, 0, data, k, 24);
            k += 24;

            DataConverters.UInt64ToBytes(ref data, (ulong)newCreditMeter, k);
            k += 8;

            data[k++] = combination.NumberOfWinningLines;

            for (var i = 0; i < winningLinesInBytes.Count; i++)
            {
                Array.Copy(winningLinesInBytes[i], 0, data, k, winningLinesInBytes[i].Length);
                k += winningLinesInBytes[i].Length;
            }

            return data;
        }
    }
}
