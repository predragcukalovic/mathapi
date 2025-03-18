using Papi.GameServer.Utils.Converters;
using MathCombination.CombinationData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CombinationExtras.ConversionData.ByteArrayConversion
{
    class GameCaptainSharkConversion
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
            var emptyArray = Enumerable.Repeat((byte)0, 4).ToArray();
            Array.Copy(emptyArray, 0, data, k, 4);
            k += 4;

            if (combination.GratisGame)
            {
                data[k++] = 1;
            }
            else
            {
                data[k++] = 0;
            }

            data[k++] = (byte)numOfGratisGames;

            if (isCurrentGameGratis)
            {
                data[k++] = 1;
            }
            else
            {
                data[k++] = 0;
            }

            emptyArray = Enumerable.Repeat((byte)255, 24).ToArray();
            Array.Copy(emptyArray, 0, data, k, 24);
            k += 24;

            DataConverters.UInt64ToBytes(ref data, (ulong)newCreditMeter, k);
            k += 8;

            data[k++] = combination.NumberOfWinningLines;

            foreach (byte[] winningLineBytes in winningLinesInBytes)
            {
                Array.Copy(winningLineBytes, 0, data, k, winningLineBytes.Length);
                k += winningLineBytes.Length;
            }

            return data;
        }
    }
}
