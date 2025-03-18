using Papi.GameServer.Utils.Enums;
using MathBaseProject.StructuresV3;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using V4Converter.DTOs;
using V4Converter.Mappers;
using V4Converter.Readers;

namespace V4Converter
{
    public static class ByteArrayToV4Converter
    {
        private static GameConfig GetGameConfig(Games gameId)
        {
            string gameName = Enum.GetName(typeof(Games), gameId);
            return GameConfigReader.GetGameConfig(gameName);
        }
        public static string ConvertByteArrayToV3(string frontendDataString, Games gameId)
        {
            byte[] frontendData = Convert.FromBase64String(frontendDataString);
            GameConfig gameConfig = GetGameConfig(gameId);
            int[,] matrix = NormalizeSymbolArray(GetSymbols(frontendData), gameConfig);
            ByteV3MapperParams byteV3MapperParams = new ByteV3MapperParams(frontendData, gameConfig, matrix, gameId);
            return JsonConvert.SerializeObject(new SlotDataResV3
            {
                symbols = matrix,
                extra = ExtraDataObjectBuilder(byteV3MapperParams),
                wins = GetWinLines(ConvertWinStructToWins(byteV3MapperParams), gameConfig),
                win = GetTotalWin(GetWinLinesByte(frontendData)),
                gratisGame = Convert.ToBoolean(frontendData[19])
            });
        }

        public static string ConvertBlackOrRedByte(string frontendDataString)
        {
            byte[] blackOrRedArray = Convert.FromBase64String(frontendDataString);
            return JsonConvert.SerializeObject(new
            {
                Card = (int)blackOrRedArray[1],
                Win = (long)getBlackOrRedWin(blackOrRedArray),
                GamesLeft = (int)blackOrRedArray[22],
                History = getGambleHistory(blackOrRedArray)
            });
        }

        private static int getBlackOrRedWin(byte[] blackOrRedArray)
        {
            return ((blackOrRedArray[10] << 24) | (blackOrRedArray[11] << 16) | (blackOrRedArray[12] << 8) | (blackOrRedArray[13]));
        }

        private static int[] getGambleHistory(byte[] blackOrRedArray)
        {
            List<int> history = new List<int>();
            int i = 3;
            while (blackOrRedArray[i] < 255 && i < 10)
            {
                history.Add(blackOrRedArray[i]);
                i += 2;
            }
            return history.ToArray();
        }

        private static int[,] NormalizeSymbolArray(byte[] symbols, GameConfig gameConfig)
        {
            int numberOfReels = gameConfig.NumberOfReels;
            int numberOfRows = gameConfig.NumberOfRows;
            int[,] normalizedSymbols = new int[numberOfReels, numberOfRows];
            for (int i = 0; i < symbols.Length; i++)
            {
                normalizedSymbols[(i % numberOfReels), (i / numberOfReels)] = (int)symbols[i];
            }
            return normalizedSymbols;
        }

        private static byte[] GetSymbols(byte[] frontendData)
        {
            byte[] symbols = new byte[15];
            Array.Copy(frontendData, symbols, 15);
            return symbols;
        }

        private static List<byte[]> GetWinLinesByte(byte[] frontendData)
        {
            int numberOfWinningLines = (int)frontendData[54];
            List<byte[]> lines = new List<byte[]>();
            for (int i = 0; i < numberOfWinningLines; i++)
            {
                byte[] line = new byte[8];
                Array.Copy(frontendData, 55 + i * 8, line, 0, 8);
                lines.Add(line);
            }
            return lines;
        }

        private static int GetTotalWin(List<byte[]> winLines)
        {
            return winLines.Sum(winLine => Convert.ToInt32(winLine[4]));
        }
        private static WinSymbolV3[] ConvertWinPositionsToWinSymbols(byte[] lineInfo, GameConfig gameConfig)
        {
            string bits = string.Concat(lineInfo.Select(l => Convert.ToString(l, 2).PadLeft(8, '0')).ToArray());
            int reelsNo = gameConfig.NumberOfReels;
            List<WinSymbolV3> symbols = new List<WinSymbolV3>();
            for (int i = 44; i < 64; i += 4)
            {
                int position = Convert.ToInt32(bits.Substring(i, 4), 2);
                if (position < reelsNo * gameConfig.NumberOfRows)
                {
                    symbols.Add(new WinSymbolV3
                    {
                        reel = position % reelsNo,
                        row = position / reelsNo,
                        id = Convert.ToInt32(bits.Substring(40, 4), 2),
                        value = 0
                    });
                }
            }
            return symbols.ToArray();
        }
        private static WinLineV3[] ConvertWinStructToWins(ByteV3MapperParams byteV3MapperParams)
        {
            List<byte[]> lines = GetWinLinesByte(byteV3MapperParams.FrontendData);
            List<WinLineV3> wins = new List<WinLineV3>();
            foreach (byte[] line in lines)
            {
                wins.Add(new WinLineV3
                {
                    lineId = line[0],
                    soundId = line[5] >> 4,
                    win = line[4],
                    symbols = ConvertWinPositionsToWinSymbols(line, byteV3MapperParams.GameConfig)
                });
            }
            return wins.ToArray();
        }

        private static int GetLineDelimiterIndex(WinLineV3[] wins)
        {
            return Array.IndexOf(wins, wins.FirstOrDefault(w => w.lineId == 254));
        }
        private static WinLineV3[] getWinLinesSpellbook(WinLineV3[] wins)
        {
            int delimiterIndex = GetLineDelimiterIndex(wins);
            return delimiterIndex > -1 ? wins.Skip(0).Take(GetLineDelimiterIndex(wins)).ToArray() : wins;
        }
        private static WinLineV3[] GetWinLines(WinLineV3[] wins, GameConfig gameConfig)
        {
            switch (gameConfig.WinLineType)
            {
                case "Book":
                    return getWinLinesSpellbook(wins);
                default:
                    return wins;
            }
        }
        private static object ExtraDataObjectBuilder(ByteV3MapperParams byteV3MapperParams)
        {
            var extraDataObject = new ExpandoObject() as IDictionary<string, Object>;
            foreach (string field in byteV3MapperParams.GameConfig.ExtraFields.Except(new string[] { "upperRow", "bottomRow", "recall" }))
            {
                extraDataObject.Add(field, Type.GetType("V4Converter.ByteArrayToV4Converter").GetMethod($"{field}Get").Invoke(null, new Object[] { byteV3MapperParams }));
            }
            if (byteV3MapperParams.GameConfig.ExtraFields.Contains("recall"))
            {
                extraDataObject.Add("recall", RecallMapper.MapExpandSymbolsToRecall(byteV3MapperParams.Matrix, extraDataObject, byteV3MapperParams.GameConfig));
            }
            return extraDataObject;
        }


        public static WinLineV3[] getWinLinesExtraSpellbook(WinLineV3[] wins)
        {
            return wins.Skip(GetLineDelimiterIndex(wins)).Take(wins.Length).ToArray();
        }
        private static int ToInt32(byte[] data, int index)
        {
            return (data[index++] << 24) | (data[index++] << 16) | (data[index++] << 8) | data[index++];
        }
        public static object bonusSymbolGet(ByteV3MapperParams byteV3MapperParams)
        {
            switch (byteV3MapperParams.GameConfig.BonusSymbolStrategy)
            {
                case "Book":
                    int addInfo = ToInt32(byteV3MapperParams.FrontendData, 15);
                    if (addInfo == 0)
                    {
                        return null;
                    }
                    else
                    {
                        return addInfo;
                    }
                default:
                    return null;
            }
        }

        public static int mysteryGet(ByteV3MapperParams byteV3MapperParams)
        {
            return ToInt32(byteV3MapperParams.FrontendData, 15);
        }
        public static int multiplierGet(ByteV3MapperParams byteV3MapperParams)
        {
            int mult = 0;
            byte[] frontendData = byteV3MapperParams.FrontendData;
            int[,] matrix = byteV3MapperParams.Matrix;
            if (byteV3MapperParams.GameConfig.GameName == "GoldenExplosion")
            {
                mult = ToInt32(byteV3MapperParams.FrontendData, 15);
            }

            for (var i = 0; i < 5; i++)
            {
                if (frontendData[22 + i] < 15)
                {
                    int reel = frontendData[22 + i] % 5;
                    int row = frontendData[22 + i] / 5;
                    if (matrix[reel, row] > 9)
                    {
                        mult += matrix[reel, row] - 9;
                    }
                }
            }
            return mult;
        }
        public static int[] transformReelsGet(ByteV3MapperParams byteV3MapperParams)
        {
            byte[] positionFor2 = new byte[5];
            Array.Copy(byteV3MapperParams.FrontendData, 22, positionFor2, 0, 5);
            return ToInt32(byteV3MapperParams.FrontendData, 15) > 0 ? positionFor2.Select(x => (int)x).ToArray() : new[] { 0, 0, 0, 0, 0 };
        }

        public static WinLineV3[] winsExtraGet(ByteV3MapperParams byteV3MapperParams)
        {
            WinLineV3[] winLines = ConvertWinStructToWins(byteV3MapperParams);
            return getWinLinesExtraSpellbook(winLines);
        }

        public static long winExtraGet(ByteV3MapperParams byteV3MapperParams)
        {
            return winsExtraGet(byteV3MapperParams).Sum(we => we.win);
        }

        public static object bonusSymbol1Get(ByteV3MapperParams byteV3MapperParams)
        {
            int bonus1 = ToInt32(byteV3MapperParams.FrontendData, 17);
            if (bonus1 == 0)
            {
                return null;
            }
            else
            {
                return bonus1;
            }
        }

        public static object bonusSymbol2Get(ByteV3MapperParams byteV3MapperParams)
        {
            int bonus2 = ToInt32(byteV3MapperParams.FrontendData, 18);
            if (bonus2 == 0)
            {
                return null;
            }
            else
            {
                return bonus2;
            }
        }
        public static object transformReels1Get(ByteV3MapperParams byteV3MapperParams)
        {
            int[] transform1 = new int[5];
            for (int i = 0; i < 5; i++)
            {
                transform1[i] = byteV3MapperParams.FrontendData[22 + i] == 255 ? 0 : (byteV3MapperParams.FrontendData[22 + i] > 1 ? 1 : 0);
            }
            return transform1;
        }

        public static object transformReels2Get(ByteV3MapperParams byteV3MapperParams)
        {
            int[] transform2 = new int[5];
            for (int i = 0; i < 5; i++)
            {
                transform2[i] = byteV3MapperParams.FrontendData[22 + i] == 255 ? 0 : (byteV3MapperParams.FrontendData[22 + i] % 2);
            }
            return transform2;
        }

        public static object wildExpandGet(ByteV3MapperParams byteV3MapperParams)
        {
            byte[] positionFor2 = new byte[5];
            Array.Copy(byteV3MapperParams.FrontendData, 22, positionFor2, 0, 5);
            return WildExpandMapper.getWildExpand(new WildExpandParams(byteV3MapperParams.GameId, byteV3MapperParams.Matrix, byteV3MapperParams.GameConfig, positionFor2));
        }

        public static string wildExpandSourceGet(ByteV3MapperParams byteV3MapperParams)
        {
            return byteV3MapperParams.GameConfig.WildExpandSource;
        }

    }
}
