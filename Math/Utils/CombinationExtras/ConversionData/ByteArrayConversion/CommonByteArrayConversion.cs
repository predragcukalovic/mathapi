using Papi.GameServer.Utils.Converters;
using MathCombination.CombinationData;

namespace CombinationExtras.ConversionData.ByteArrayConversion
{
    class CommonByteArrayConversion
    {
        /// <summary>
        /// Kovertuje dobitne linije u niz bajtova
        /// </summary>
        /// <param name="lineInfo"></param>
        /// <returns></returns>
        public static byte[] WinningLineToBytesArray(LineInfo lineInfo)
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
    }
}
