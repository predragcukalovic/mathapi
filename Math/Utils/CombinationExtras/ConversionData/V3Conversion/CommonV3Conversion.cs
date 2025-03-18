using MathCombination.CombinationData;
using System;
using System.Linq;

namespace CombinationExtras.ConversionData.V3Conversion
{
    class CommonV3Conversion
    {
        /// <summary>
        /// Pretvara LineInfo u JSON strukturu.
        /// </summary>
        /// <param name="lineInfo"></param>
        /// <returns></returns>
        public static LineInfoJson[] ToLineInfoJson(LineInfo[] lineInfo)
        {
            if (lineInfo.Length > 0)
            {
                var lineInfoJson = new LineInfoJson[lineInfo.Length];
                for (var i = 0; i < lineInfo.Length; i++)
                {
                    var lineJson = new LineInfoJson
                    {
                        lineId = lineInfo[i].Id,
                        totalWin = lineInfo[i].Win,
                        winningElement = lineInfo[i].WinningElement,
                        symbolPositions = (Array.ConvertAll(lineInfo[i].WinningPosition, c => (int)c)).Where(val => val != 255).ToArray()
                    };

                    lineInfoJson[i] = lineJson;
                }
                return lineInfoJson;
            }
            return new LineInfoJson[0];
        }
    }
}
