using Papi.GameServer.Utils.Converters;
using MathCombination.CombinationData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CombinationExtras.ConversionData.ByteArrayConversion
{
    class GameWizardConversion
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
            DataConverters.UInt32ToBytes(ref data, (uint)combination.WinFor2, k);
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

            Array.Copy(combination.PositionFor2, 0, data, k, combination.PositionFor2.Length);
            k += 5;

            Array.Copy(combination.MultiplyFor2, 0, data, k, combination.MultiplyFor2.Length);
            k += 4;

            var emptyArray = Enumerable.Repeat((byte)255, 15).ToArray();
            Array.Copy(emptyArray, 0, data, k, 15);
            k += 15;

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
