using CombinationExtras.ConversionData.V3Conversion;
using Papi.GameServer.Utils.Enums;
using GameFruityForce.Config;
using MathCombination.CombinationData;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Services
{
    public class AdditionalGameDataService
    {
        private readonly List<Games> GAME_USES_ADDITIONAL_GAME_DATA = new List<Games>
        {
            Games.FruityForce40
        };
        public object GetAdditionalGameData(Games game, ICombination combination, long betPerLine)
        {
            switch (game)
            {
                case Games.FruityForce40:
                    return GameFruityForce40Conversion.GetFruityForceConfigObject(combination, betPerLine);
                default:
                    return null;
            }
        }

        public object ConvertAdditionalGameDataToFront(Games game, object additionalGameData)
        {
            if (GAME_USES_ADDITIONAL_GAME_DATA.Contains(game))
            {
                switch (game)
                {
                    case Games.FruityForce40:
                        FruityForceLevelDataRequestParams request = JsonConvert.DeserializeObject<FruityForceLevelDataRequestParams>(additionalGameData.ToString());
                        return GameFruityForce40Conversion.GenerateLevelFields(request);
                    default:
                        return null;
                }
            }
            return null;
        }
    }
}
