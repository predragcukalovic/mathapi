using CombinationExtras;
using CombinationExtras.GameDataConversion;
using CombinationExtras.Logging;
using Papi.GameServer.Math.Api.Helpers;
using Papi.GameServer.Math.Contracts.Requests;
using Papi.GameServer.Math.Contracts.Responses;
using Papi.GameServer.Math.MathCheatTool;
using Papi.GameServer.Utils.Enums;
using Papi.GameServer.Utils.Helper;
using Papi.GameServer.Utils.Logging;
using MathCombination.CombinationData;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using V4Converter;
using Formatting = System.Xml.Formatting;

namespace Papi.GameServer.Math.NetCore.Api.Controllers
{
    public class TestController : Controller
    {
        [HttpPost]
        [Route("test/{gameId}/combinations")]
        public async Task<IActionResult> GenerateCombination(Games gameId, [FromBody] GenerateTestCombinationRequest model)
        {
            try
            {
                Logger.LogInfo(string.Join("\n", model.GetType(), JsonConvert.SerializeObject(model, (Newtonsoft.Json.Formatting)Formatting.Indented)));

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

                var cheatToolObject = model.CheatTool;
                if (GameHelper.IsUnicornProviderGame((int)gameId) && model.TestCombinations == 0)
                {
                    combination = UnicornSlotCombinationCheatTool.GetCombination(ref cheatToolObject, gameId, ToV4Converter.getBet(gameId, model.Bet), model.NumberOfLines, model.GratisGamesLeft,
                        model.AdditionalInformation, ref additionalArray);
                }
                else
                {
                    if (model.TestCombinations == 0 && cheatToolObject != null)
                    {
                        combination = SlotCombinationCheatTool.GetCombination(ref cheatToolObject, gameId, ToV4Converter.getBet(gameId, model.Bet), model.NumberOfLines, model.GratisGamesLeft,
                            ref additionalArray, model.Bet, model.AdditionalInformation, model.PlayerChoice, model.GameDataobj);
                    }
                    else
                    {
                        combination = SlotCombinationCheatTool.GetCombination(gameId, ToV4Converter.getBet(gameId, model.Bet), model.NumberOfLines, model.GratisGamesLeft,
                            ref additionalArray, model.Bet, model.AdditionalInformation, model.PlayerChoice, model.GameDataobj, model.TestCombinations);
                    }
                }

                combination.LogCombination();

                var isGratisGame = model.GratisGamesLeft > 0;
                var isBonusGame = combination.IsBonus(gameId, isGratisGame, model.AdditionalInformation, additionalArray);
                var cascadeWin = SlotCombination.GetCascadeWin(combination, gameId);

                long win = combination.TotalWin;
                if (cascadeWin > 0)
                {
                    win = cascadeWin;
                }

                var numberOfGratisGames = model.GratisGamesLeft;//broj gratis igara bez smanjivanja ako je trenutna gratis
                if (combination.GratisGame)
                {
                    Logger.LogInfo("Current credit: " + model.Credits
                        + ". Old number of gratis games: " + numberOfGratisGames
                        + ", new number of gratis games: " + numberOfGratisGames + combination.NumberOfGratisGames);

                    //ako su u toku igre, osvojene gratis igre, sabrati ih
                    numberOfGratisGames += combination.NumberOfGratisGames;
                }

                var updatedNumberOfGratisGames = numberOfGratisGames;//broj gratis igara sa smanjivanjem ako je trenutna gratis
                if (model.IsCurrentGameGratis)
                {
                    //ako je trenutna igra gratis, smanjiti broj za 1
                    updatedNumberOfGratisGames--;
                }

                var frontendData = CombinationToGameData.ToGameData(gameId, updatedNumberOfGratisGames, model.Credits,
                    model.IsCurrentGameGratis, combination, model.ReturnJson);

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

                Logger.LogInfo(string.Join("\n", response.GetType(), JsonConvert.SerializeObject(response, (Newtonsoft.Json.Formatting)Formatting.Indented)));
                return Ok(response);
            }
            catch (Exception exception)
            {
                Logger.LogError("GenerateCombination error" + exception.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, exception);
            }
        }
    }
}