using Papi.GameServer.Utils.Enums;
using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using V4Converter.DTOs;
using V4Converter.Mappers;
using V4Converter.Readers;

namespace V4Converter
{
    public static class V2JsonToV4Converter
    {
        private const string CARDS_HISTORY = "cardsHistory";
        private static GameConfig GetGameConfig(Games gameId)
        {
            string gameName = Enum.GetName(typeof(Games), gameId);
            return GameConfigReader.GetGameConfig(gameName);
        }
        public static string ConvertJsonToV3(JObject frontendData, Games gameId)
        {
            GameConfig gameConfig = GetGameConfig(gameId);
            var symbols = frontendData["symbols"].ToObject<int[]>();
            int[,] matrix = normalizeSymbolArray(symbols, gameConfig);
            var winStruct = frontendData["winStruct"].ToObject<LineInfoJson[]>();
            V2JsonV3MapperParams v2JsonV3MapperParams = new V2JsonV3MapperParams(frontendData, gameConfig, matrix, gameId);

            return JsonConvert.SerializeObject(new SlotDataResV3
            {
                symbols = matrix,
                extra = ExtraDataObjectBuilder(gameConfig, v2JsonV3MapperParams),
                wins = convertWinStructToWins(winStruct, gameConfig),
                win = frontendData["totalSum"].ToObject<int>(),
                gratisGame = frontendData["isGratis"].ToObject<bool>()
            });
        }

        public static string ConvertBlackOrRedJson(JObject blackOrRedJson)
        {
            var blackOrRedCardsArray = blackOrRedJson[CARDS_HISTORY].ToObject<BlackOrRedCards[]>();
            var blackOrRedHistory = new List<int>();
            if (blackOrRedCardsArray != null)
            {
                for (var i = 0; i < blackOrRedCardsArray.Length; i++)
                {
                    blackOrRedHistory.Add(ConvertSignToBlackOrRed(blackOrRedCardsArray[i].Sign));
                }
            }

            return JsonConvert.SerializeObject(new
            {
                Card = (int)blackOrRedJson["pickedCardSign"],
                Win = (long)blackOrRedJson["currentWin"],
                GamesLeft = (int)blackOrRedJson["numOfAttempts"],
                History = blackOrRedHistory
            });
        }
        private static int ConvertSignToBlackOrRed(int sign)
        {
            return sign % 2 == 0 ? 2 : 1;
        }
        private static int[,] normalizeSymbolArray(int[] symbols, GameConfig gameConfig)
        {
            int numberOfReels = gameConfig.NumberOfReels;
            int numberOfRows = gameConfig.NumberOfRows;
            int[,] normalizedSymbols = new int[numberOfReels, numberOfRows];
            for (int i = 0; i < symbols.Length; i++)
            {
                normalizedSymbols[(i % numberOfReels), (i / numberOfReels)] = symbols[i];
            }
            return normalizedSymbols;
        }

        private static WinLineV3[] convertWinStructToWins(LineInfoJson[] winStruct, GameConfig gameConfig)
        {
            WinLineV3[] wins = winStruct.Select(win =>
                new WinLineV3
                {
                    lineId = win.lineId,
                    soundId = win.winningElement,
                    win = win.totalWin,
                    symbols = convertWinPositionsToWinSymbols(win, gameConfig)
                })
                .ToArray();
            return wins;
        }

        private static WinSymbolV3[] convertWinPositionsToWinSymbols(LineInfoJson win, GameConfig gameConfig)
        {
            int reelsNo = gameConfig.NumberOfReels;
            WinSymbolV3[] symbols = win.symbolPositions.Select(position =>
                new WinSymbolV3
                {
                    reel = position % reelsNo,
                    row = (position / reelsNo),
                    id = win.winningElement,
                    value = 0
                }).ToArray();
            return symbols;
        }
        private static object ExtraDataObjectBuilder(GameConfig gameConfig, V2JsonV3MapperParams v2JsonV3MapperParams)
        {
            var extraDataObject = new ExpandoObject() as IDictionary<string, Object>;
            foreach (string field in gameConfig.ExtraFields.Except(new string[] { "recall" }))
            {
                extraDataObject.Add(field, Type.GetType("V4Converter.V2JsonToV4Converter").GetMethod($"{field}Get").Invoke(null, new Object[] { v2JsonV3MapperParams }));
            }
            if (gameConfig.ExtraFields.Contains("recall"))
            {
                extraDataObject.Add("recall", RecallMapper.MapExpandSymbolsToRecall(v2JsonV3MapperParams.Matrix, extraDataObject, v2JsonV3MapperParams.GameConfig));
            }
            return extraDataObject;
        }

        private static int GetIntParam(V2JsonV3MapperParams v2JsonV3MapperParams, string field)
        {
            return v2JsonV3MapperParams.FrontendData[$"{field}"].ToObject<int>();
        }

        private static int[] GetIntArrayParam(V2JsonV3MapperParams v2JsonV3MapperParams, string field)
        {
            return v2JsonV3MapperParams.FrontendData[$"{field}"].ToObject<int[]>();
        }

        public static int[] upperRowGet(V2JsonV3MapperParams v2JsonV3MapperParams)
        {
            return GetIntArrayParam(v2JsonV3MapperParams, "upperRow");
        }
        public static int[] bottomRowGet(V2JsonV3MapperParams v2JsonV3MapperParams)
        {
            return GetIntArrayParam(v2JsonV3MapperParams, "bottomRow");
        }

        public static int numOfBonusGet(V2JsonV3MapperParams v2JsonV3MapperParams)
        {
            return GetIntParam(v2JsonV3MapperParams, "numOfBonus");
        }

        public static int bonusGet(V2JsonV3MapperParams v2JsonV3MapperParams)
        {
            return GetIntParam(v2JsonV3MapperParams, "bonus");
        }
        public static int numberOfFreeSpinsGet(V2JsonV3MapperParams v2JsonV3MapperParams)
        {
            return GetIntParam(v2JsonV3MapperParams, "numberOfFreeSpins");
        }
        public static int noWinLinesGet(V2JsonV3MapperParams v2JsonV3MapperParams)
        {
            return GetIntParam(v2JsonV3MapperParams, "noWinLines");
        }
        public static int wildSymbolGet(V2JsonV3MapperParams v2JsonV3MapperParams)
        {
            return GetIntParam(v2JsonV3MapperParams, "wildSymbol");
        }

        public static object wildExpandGet(V2JsonV3MapperParams v2JsonV3MapperParams)
        {
            JObject frontendData = v2JsonV3MapperParams.FrontendData;
            byte[] positionFor2 = frontendData["expandWild"].ToObject<byte[]>();
            int[,] matrix = normalizeSymbolArray(frontendData["symbols"].ToObject<int[]>(), v2JsonV3MapperParams.GameConfig);
            return WildExpandMapper.getWildExpand(new WildExpandParams(v2JsonV3MapperParams.GameId, matrix, v2JsonV3MapperParams.GameConfig, positionFor2));
        }

        public static string wildExpandSourceGet(V2JsonV3MapperParams v2JsonV3MapperParams)
        {
            return v2JsonV3MapperParams.GameConfig.WildExpandSource;
        }
    }
}
