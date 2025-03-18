using Papi.GameServer.Math.Contracts.Requests;
using Papi.GameServer.Math.History;
using Papi.GameServer.Utils.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Text;
using V4Converter.Mappers;

namespace V4Converter
{
    public static class RecallDelegator
    {
        public static readonly string JSON_SPIN_PREFIX = "{\"symbols";
        public static readonly string JSON_GAMBLE_PREFIX = "{\"limit";
        public static readonly string V3_STANDARD_PREFIX = "[{\"type";
        public static readonly string V3_TOTAL_WIN = "{\"totalWin";
        public static List<string> ConvertBulkRecallObject(Games gameId, BulkRecallConversionRequest bulkRecallConversionRequest)
        {
            List<string> convertedData = new List<string>();
            foreach (RecallConversionRequest request in bulkRecallConversionRequest.RecallConversionRequests)
            {
                convertedData.Add(ConvertRecallObject(request.data, gameId, request.type));
            }
            return convertedData;
        }

        public static string ConvertRecallObject(string recallObject, Games gameId, int gameType)
        {
            if (recallObject.StartsWith(JSON_SPIN_PREFIX))
            {
                return V2JsonToV4Converter.ConvertJsonToV3(JObject.Parse(recallObject), gameId);
            }
            else if (recallObject.StartsWith(JSON_GAMBLE_PREFIX))
            {
                return V2JsonToV4Converter.ConvertBlackOrRedJson(JObject.Parse(recallObject));
            }
            else if (!recallObject.StartsWith((V3_STANDARD_PREFIX)) && !recallObject.StartsWith((V3_TOTAL_WIN)))
            {
                if (gameType != (int)GameTypeEnum.DoubleUp)
                {
                    return ByteArrayToV4Converter.ConvertByteArrayToV3(recallObject, gameId);
                }
                else
                {
                    return ByteArrayToV4Converter.ConvertBlackOrRedByte(recallObject);
                }
            }
            return recallObject;
        }

        public static bool RecallIsV4(byte[] recallObject)
        {
            return Encoding.UTF8.GetString(recallObject).StartsWith((V3_STANDARD_PREFIX));
        }

        public static List<RecallConversionRequest> GetGameV3Object(string recallObject, Games gameId, int gameType)
        {
            List<RecallConversionRequest> gamesList = new List<RecallConversionRequest>();
            if (!recallObject.StartsWith((V3_STANDARD_PREFIX)))
            {
                if (gameType == (int)GameTypeEnum.FreeSpin || gameType == (int)GameTypeEnum.DoubleUp)
                {
                    RecallConversionRequest defaultGame = new RecallConversionRequest
                    {
                        type = (int)GameTypeEnumV3.Slot,
                        data = JsonConvert.SerializeObject(ToV4Converter.getNonWinningCombination(gameId, 0))
                    };
                    gamesList.Add(defaultGame);
                }
                RecallConversionRequest game = new RecallConversionRequest
                {
                    type = GameTypeEnumV3Mapper.GetV3GameType(gameType),
                    data = ConvertRecallObject(recallObject, gameId, gameType)
                };
                gamesList.Add(game);
            }
            else
            {
                RecallConversionRequest game = new RecallConversionRequest
                {
                    type = gameType,
                    data = recallObject
                };
                gamesList.Add(game);
            }
            return gamesList;
        }
    }
}