using CombinationExtras;
using CombinationExtras.GameDataConversion;
using CombinationExtras.Logging;
using CombinationExtras.ReaderData;
using CombinationExtras.V3Data;
using Papi.GameServer.Math.ApiCore.Extensions;
using Papi.GameServer.Math.ApiCore.Helpers;
using Papi.GameServer.Math.Contracts.Requests;
using Papi.GameServer.Math.Contracts.Responses;
using Papi.GameServer.Utils.Enums;
using Papi.GameServer.Utils.Helper;
using Papi.GameServer.Utils.JsonObjectConverter;
using Papi.GameServer.Utils.Logging;
using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using MathForGames.GambleData;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using V4Converter;
using V4Converter.DTOs;

namespace Papi.GameServer.Math.ApiCore.Controllers
{
    [ApiController]
    public class GamesController : ControllerBase
    {
        [HttpPost]
        [Route("games/{gameId}/combinations")]
        public async Task<IActionResult> GenerateCombination([FromQuery] Games gameId, [FromBody] GenerateCombinationRequest model)
        {
            try
            {
                Logger.LogDebug("GenerateCombination request: {@GenerateCombinationRequest}", new { gameId, model });

                ICombination combination = null;
                if (gameId == Games.BookOfSpells)
                {
                    gameId = Games.Spellbook;
                }

                byte[] additionalArray = null;
                if (model.AdditionalArray != null)
                {
                    additionalArray = Convert.FromBase64String(model.AdditionalArray);
                }

                if (GameHelper.IsUnicornProviderGame((int)gameId))
                {
                    combination = UnicornSlotCombination.GetCombination(gameId, ToV4Converter.getBet(gameId, model.Bet), model.NumberOfLines, model.GratisGamesLeft,
                        model.AdditionalInformation, ref additionalArray);
                }
                else
                {
                    combination = SlotCombination.GetCombination(gameId, ToV4Converter.getBet(gameId, model.Bet), model.NumberOfLines, model.GratisGamesLeft,
                        ref additionalArray, model.Bet, model.AdditionalInformation, model.PlayerChoice, model.GameDataobj);
                }

                combination.LogCombination();

                var isGratisGame = model.GratisGamesLeft > 0;
                var isBonusGame = combination.IsBonus(gameId, isGratisGame, model.AdditionalInformation, additionalArray);
                var cascadeWin = SlotCombination.GetCascadeWin(combination, gameId);

                long win = ToV4Converter.multiplyWinIfNeeded(combination.TotalWin, model.Bet, gameId);

                if (cascadeWin > 0)
                {
                    win = cascadeWin;
                }

                var numberOfGratisGames = model.GratisGamesLeft;//broj gratis igara bez smanjivanja ako je trenutna gratis
                if (combination.GratisGame)
                {
                    Logger.LogInfo("Current credit: " + model.Credits +
                            ". Old number of gratis games: " + numberOfGratisGames +
                            ", new number of gratis games: " + numberOfGratisGames + combination.NumberOfGratisGames);

                    //ako su u toku igre, osvojene gratis igre, sabrati ih
                    numberOfGratisGames += combination.NumberOfGratisGames;
                }

                var updatedNumberOfGratisGames = numberOfGratisGames;//broj gratis igara sa smanjivanjem ako je trenutna gratis
                if (model.IsCurrentGameGratis)
                {
                    //ako je trenutna igra gratis, smanjiti broj za 1
                    updatedNumberOfGratisGames--;
                }

                object frontendData;
                if (ToV4Converter.gameIsConverted(gameId))
                {
                    frontendData = ToV4Converter.toSlotDataResV3(combination, gameId, model.Bet, updatedNumberOfGratisGames, model.IsCurrentGameGratis);
                }
                else
                {
                    frontendData = CombinationToGameData.ToGameData(gameId, updatedNumberOfGratisGames, model.Credits,
                    model.IsCurrentGameGratis, combination, model.ReturnJson);
                }

                //id in free spins
                var smBetId = isGratisGame ? 1 : 0;
                var smBetResult = SmBetHelper.GenearateSmBetResult(combination, smBetId, (int)gameId, isBonusGame,
                    model.NumberOfLines, model.ShouldGenerateSmBetResult, model.SmBetResultInBonus, model.Bet);

                //da li je additionalArray isti kao i combination.AdditionalArray??
                var response = new GenerateCombinationResult
                {
                    AdditionalArray = additionalArray,
                    AdditionalInformation = combination.AdditionalInformation,
                    IsBonusGame = isBonusGame,
                    FrontendData = frontendData,
                    NextNumberOfGratisGames = combination.GratisGame ? combination.NumberOfGratisGames : 0,
                    NumberOfGratisGames = numberOfGratisGames,
                    SmBetResult = smBetResult,
                    HasWinFor2 = combination.WinFor2 > 0,
                    Win = win,
                    IsGratisGame = combination.GratisGame,
                    NumberOfWinningLines = combination.NumberOfWinningLines
                };

                var responseLog = new GenerateCombinationResultLog(response);
                Logger.LogDebug("GenerateCombination response: {@GenerateCombinationResponse}", new { gameId, responseLog });
                return Ok(response);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, "GenerateCombination Exception {@GenerateCombinationRequest}", new { gameId, model });
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("games/{gameId}/buy-bonus-combinations")]
        public async Task<IActionResult> GenerateCombinationWithBonus(Games gameId, [FromBody] GenerateCombinationWithBonusRequest model)
        {
            try
            {
                Logger.LogInfo(string.Join("\n", model.GetType(), JsonConvert.SerializeObject(model, Formatting.Indented)));

                ICombination combination = null;
                if (gameId == Games.BookOfSpells)
                {
                    gameId = Games.Spellbook;
                }

                byte[] additionalArray = null;
                if (model.AdditionalArray != null)
                {
                    additionalArray = Convert.FromBase64String(model.AdditionalArray);
                }

                combination = BuyBonusCombination.BuyBonusCombination.GetBuyBonusCombination(gameId, ToV4Converter.getBet(gameId, model.Bet), model.NumberOfLines, model.GratisGamesLeft,
                            model.BonusType, ref additionalArray, model.Bet, model.AdditionalInformation, model.PlayerChoice, model.GameDataobj);

                combination.LogCombination();

                long win = ToV4Converter.multiplyWinIfNeeded(combination.TotalWin, model.Bet, gameId);
                var isBonusGame = combination.IsBonus(gameId, true, model.AdditionalInformation, additionalArray);

                object frontendData;
                if (ToV4Converter.gameIsConverted(gameId))
                {
                    frontendData = ToV4Converter.toSlotDataResV3(combination, gameId, model.Bet, combination.NumberOfGratisGames, model.IsCurrentGameGratis);
                }
                else
                {
                    frontendData = CombinationToGameData.ToGameData(gameId, combination.NumberOfGratisGames, model.Credits,
                    model.IsCurrentGameGratis, combination, model.ReturnJson);
                }


                //id in free spins
                var smBetId = 1;
                var smBetResult = SmBetHelper.GenearateSmBetResult(combination, smBetId, (int)gameId, isBonusGame,
                    model.NumberOfLines, model.ShouldGenerateSmBetResult, model.SmBetResultInBonus, model.Bet);

                //da li je additionalArray isti kao i combination.AdditionalArray??
                var response = new GenerateCombinationResult
                {
                    AdditionalArray = additionalArray,
                    AdditionalInformation = combination.AdditionalInformation,
                    IsBonusGame = isBonusGame,
                    FrontendData = frontendData,
                    NextNumberOfGratisGames = combination.NumberOfGratisGames,
                    NumberOfGratisGames = combination.NumberOfGratisGames,
                    SmBetResult = smBetResult,
                    HasWinFor2 = combination.WinFor2 > 0,
                    Win = win,
                    IsGratisGame = true,
                    NumberOfWinningLines = combination.NumberOfWinningLines
                };

                Logger.LogInfo(string.Join("\n", response.GetType(), JsonObjectConverter.SerializeObject(response)));
                return Ok(response);
            }
            catch (Exception exception)
            {
                Logger.LogError("GenerateCombination error" + exception.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("games/double-up")]
        public async Task<IActionResult> GenerateDoubleUp([FromBody] DoubleUpRequest model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            if (model.Win <= 0)
            {
                return BadRequest("Win is zero or negative");
            }

            var doubleUpResult = DoubleUp.GetDoubleUp(model.Win, model.AlwaysWin);
            if (doubleUpResult == null || !doubleUpResult.Any())
            {
                Logger.LogError("Double up result is null or empty");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

            return Ok(doubleUpResult);
        }

        [HttpPost]
        [Route("games/black-or-red")]
        public async Task<IActionResult> GenerateBlackOrRed([FromBody] BlackOrRedRequest model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            if (model.Win <= 0)
            {
                return BadRequest("Win is zero or negative");
            }

            if (!(ToV4Converter.gameIsConverted((Games)model.GameId)) && (model.Win > Int32.MaxValue / 2 + 1))
            {
                Logger.LogError("BlackOrRed bet exceeds max value for non-converted game");
                return BadRequest("BlackOrRed bet exceeds max value for non-converted game");
            }

            var blackOrRed = BlackOrRed.GetBlackOrRedData(model.Win, model.AlwaysWin, model.ReturnJson);
            if (blackOrRed == null)
            {
                Logger.LogError("GenerateBlackOrRed result is null");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

            if (!model.ReturnJson)
            {
                return Ok((byte[])blackOrRed);
            }

            var blackOrRedData = (BlackOrRed.BlackOrRedData)blackOrRed;
            return Ok(new BlackOrRedResponse
            {
                CardSign = blackOrRedData.CardSign,
                CardValue = blackOrRedData.CardValue,
                CurrentWin = blackOrRedData.CurrentWin,
                NextWin = blackOrRedData.NextWin
            });
        }

        [HttpGet]
        [Route("games/{gameId}/init")]
        public async Task<IActionResult> GetInitData(Games gameId)
        {
            if (ToV4Converter.gameIsConverted(gameId))
            {
                GameConfig gameConfig = ToV4Converter.GetGameConfig(gameId);
                return Ok(new InitResponse
                {
                    HelpConfig = ToV4Converter.getGameHelpConfigV3(gameConfig).Map(),
                    Reels = ToV4Converter.getGameReelsV3(gameConfig).Map(),
                });
            }
            else
            {
                if (gameId.ToString().Contains("Unicorn"))
                {
                    return Ok(new InitResponse
                    {
                        HelpConfig = UnicornInitGameDataV3.GetGameHelpConfigV3(gameId).Map(),
                        Reels = UnicornInitGameDataV3.GetGameReelsV3(gameId).Map(),
                    });
                }
                else
                {
                    return Ok(new InitResponse
                    {
                        HelpConfig = InitGameDataV3.GetGameHelpConfigV3(gameId).Map(),
                        Reels = InitGameDataV3.GetGameReelsV3(gameId).Map(),
                    });
                }
            }
        }

        [HttpGet]
        [Route("games/{gameId}/buyBonusTypes")]
        public async Task<IActionResult> GetBuyBonusTypes(Games gameId)
        {
            var buyBonusParams = MathBuyBonusFilesReader.GetBuyBonusParametersForGame(gameId.ToString());

            var response = new BuyBonusTypesResponse()
            {
                BuyBonusTypes = buyBonusParams?.Select(x => new BuyBonusTypeConfig()
                {
                    Type = x.Type,
                    PriceMultiplier = x.Multiplier,
                    Lines = x.Lines,
                    Rtp = x.Rtp
                }).ToArray() ?? Array.Empty<BuyBonusTypeConfig>()
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("games/{gameId}/combinations/init")]
        public async Task<IActionResult> GetNonWinningCombination(Games gameId, [FromQuery] NonWinningRequest model)
        {
            SlotDataResV3 combination;
            if (ToV4Converter.gameIsConverted(gameId))
            {
                combination = ToV4Converter.getNonWinningCombination(gameId, model.GratisGamesLeft);
            }
            else
            {
                {
                    if (gameId.ToString().Contains("Unicorn"))
                    {
                        combination = UnicornInitGameDataV3.GetNonWinningCombination(gameId, model.Bet, model.NumberOfLines, model.GratisGamesLeft);
                    }
                    else
                    {
                        combination = InitGameDataV3.GetNonWinningCombination(gameId, model.Bet, model.NumberOfLines, model.GratisGamesLeft);
                    }
                }
            }
            return Ok(combination);
        }

        [HttpGet]
        [Route("games/{gameId}/lines")]
        public async Task<IActionResult> GetLines(Games gameId)
        {
            if (ToV4Converter.gameIsConverted(gameId))
            {
                return Ok(ToV4Converter.getPlayLines(gameId));
            }
            else
            {
                if (gameId.ToString().Contains("Unicorn"))
                {
                    return Ok(UnicornInitGameDataV3.GetPlayLines(gameId));
                }
                else
                {
                    return Ok(InitGameDataV3.GetPlayLines(gameId));
                }
            }
        }

        [HttpGet]
        [Route("games/{gameId}/lines/{index}")]
        public async Task<IActionResult> GetLine(Games gameId, int index)
        {
            if (ToV4Converter.gameIsConverted(gameId))
            {
                return Ok(ToV4Converter.GetLinesByIndexV3(gameId, index));
            }
            else
            {
                if (gameId.ToString().Contains("Unicorn"))
                {
                    return Ok(UnicornInitGameDataV3.GetLinesByIndexV3(gameId, index));
                }
                else
                {
                    return Ok(InitGameDataV3.GetLinesByIndexV3(gameId, index));
                }
            }
        }

        [HttpGet]
        [Route("games/{gameId}/lines/{line}/index")]
        public async Task<IActionResult> GetIndexOfLine(Games gameId, int line)
        {
            if (ToV4Converter.gameIsConverted(gameId))
            {
                return Ok(ToV4Converter.GetIndexForLinesV3(gameId, line));
            }
            else
            {
                if (gameId.ToString().Contains("Unicorn"))
                {
                    return Ok(UnicornInitGameDataV3.GetIndexForLinesV3(gameId, line));
                }
                else
                {
                    return Ok(InitGameDataV3.GetIndexForLinesV3(gameId, line));
                }
            }
        }

        [HttpPost]
        [Route("games/{gameId}/extract-bonus-details")]
        public async Task<IActionResult> ExtractBonusDetails([FromQuery] Games gameId, [FromBody] byte[] additionalDataArray)
        {
            if (gameId != Games.CrystalsOfMagic)
            {
                return NotFound();
            }
            if (additionalDataArray == null || !additionalDataArray.Any())
            {
                return BadRequest();
            }

            var details = SlotCombination.GetBonusGameData(gameId, additionalDataArray);
            return Ok(details);
        }

        [HttpPost]
        [Route("games/{gameId}/convert-recall")]
        public async Task<IActionResult> ConvertRecallData([FromQuery] Games gameId, [FromBody] RecallConversionRequest recallConversionRequest)
        {
            try
            {
                List<RecallConversionRequest> recallInformation = RecallDelegator.GetGameV3Object(recallConversionRequest.data, gameId, recallConversionRequest.type);
                return Ok(recallInformation);
            }
            catch (Exception ex)
            {
                Logger.LogError("Recall conversion error" + ex.ToString());
                Logger.LogError("Error converting history for " + gameId + ", RecallObject:" + JsonObjectConverter.SerializeObject(recallConversionRequest));
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("games/{gameId}/convert-recall-bulk")]
        public async Task<IActionResult> ConvertBulkRecallData([FromQuery] Games gameId, [FromBody] BulkRecallConversionRequest bulkRecallConversionRequest)
        {
            try
            {
                List<string> recallInformation = RecallDelegator.ConvertBulkRecallObject(gameId, bulkRecallConversionRequest);
                return Ok(recallInformation);
            }
            catch (Exception ex)
            {
                Logger.LogError("Bulk recall conversion error" + ex.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}