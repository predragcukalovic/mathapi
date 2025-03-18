using Papi.GameServer.Utils.Converters;
using MathCombination.CombinationData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CombinationExtras.ConversionData.ByteArrayConversion
{
    class GameVegasHotConversion
    {
        private static byte[] WinningLineToBytesArray(LineInfo lineInfo)
        {
            var k = 0;

            for (var i = 0; i < lineInfo.WinningPosition.Length; i++)
            {
                if (lineInfo.WinningPosition[i] == 255)
                {
                    lineInfo.WinningPosition[i] = 15;
                }
            }

            var data = new byte[8];

            data[k++] = lineInfo.Id;

            DataConverters.UInt32ToBytes(ref data, (uint)lineInfo.Win, k);
            k += 4;

            data[k++] = DataConverters.SetLowerAndHigherBytePortion(lineInfo.WinningElement, lineInfo.WinningPosition[0]);
            data[k++] = DataConverters.SetLowerAndHigherBytePortion(lineInfo.WinningPosition[1], lineInfo.WinningPosition[2]);
            data[k] = DataConverters.SetLowerAndHigherBytePortion(lineInfo.WinningPosition[3], lineInfo.WinningPosition[4]);

            return data;
        }

        public static byte[] ToByteArray(int numOfGratisGames, long newCreditMeter, bool isCurrentGameGratis,
            ICombination combination)
        {
            var winningLinesInBytes = new List<byte[]>();
            var size = 0;
            for (var i = 0; i < combination.NumberOfWinningLines; i++)
            {
                var winingLine = WinningLineToBytesArray(combination.LinesInformation[i]);
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

            var emptyArray = Enumerable.Repeat((byte)255, 19).ToArray();
            Array.Copy(emptyArray, 0, data, k, 19);
            k += 19;

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
