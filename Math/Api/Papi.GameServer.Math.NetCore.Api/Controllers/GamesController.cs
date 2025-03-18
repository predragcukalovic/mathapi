using CombinationExtras;
using CombinationExtras.GameDataConversion;
using CombinationExtras.Logging;
using CombinationExtras.V3Data;
using Papi.GameServer.Math.Api.Extensions;
using Papi.GameServer.Math.Api.Helpers;
using Papi.GameServer.Math.Contracts.Requests;
using Papi.GameServer.Math.Contracts.Responses;
using Papi.GameServer.Utils.Enums;
using Papi.GameServer.Utils.Helper;
using Papi.GameServer.Utils.Logging;
using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using MathForGames.GambleData;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Papi.GameServer.Math.NetCore.Api.Controllers
{
    public class GamesController : Controller
    {
        [HttpPost]
        [Route("games/{gameId}/combinations")]
        public async Task<IActionResult> GenerateCombination(Games gameId, [FromBody] GenerateCombinationRequest model)
        {
            try
            {
                Log.Information("GenerateCombination request: {@GenerateCombinationRequest}", new { gameId, model });

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
                    combination = UnicornSlotCombination.GetCombination(gameId, 1, model.NumberOfLines, model.GratisGamesLeft,
                        model.AdditionalInformation, ref additionalArray);
                }
                else
                {
                    combination = SlotCombination.GetCombination(gameId, 1, model.NumberOfLines, model.GratisGamesLeft,
                        ref additionalArray, model.Bet, model.AdditionalInformation, model.PlayerChoice, model.GameDataobj);
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
                    Log.Information("Current credit: " + model.Credits +
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

                Logger.LogInfo("GenerateCombination response: {@GenerateCombinationResponse}", new { gameId, response });
                return Ok(response);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, "GenerateCombination Exception {@GenerateCombinationRequest}", new { gameId, model });

                return StatusCode(StatusCodes.Status500InternalServerError, exception);
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
                return StatusCode(StatusCodes.Status500InternalServerError);
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

            var blackOrRed = BlackOrRed.GetBlackOrRedData(model.Win, model.AlwaysWin, model.ReturnJson);
            if (blackOrRed == null)
            {
                Logger.LogError("GenerateBlackOrRed result is null");
                return StatusCode(StatusCodes.Status500InternalServerError);
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
            if (GameHelper.IsUnicornProviderGame((int)gameId))
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

        [HttpGet]
        [Route("games/{gameId}/combinations/init")]
        public async Task<IActionResult> GetNonWinningCombination(Games gameId, [FromQuery] NonWinningRequest model)
        {
            SlotDataResV3 combination;
            if (GameHelper.IsUnicornProviderGame((int)gameId))
            {
                combination = UnicornInitGameDataV3.GetNonWinningCombination(gameId, model.Bet, model.NumberOfLines, model.GratisGamesLeft);
            }
            else
            {
                combination = InitGameDataV3.GetNonWinningCombination(gameId, model.Bet, model.NumberOfLines, model.GratisGamesLeft);
            }

            return Ok(combination);
        }

        [HttpGet]
        [Route("games/{gameId}/lines")]
        public async Task<IActionResult> GetLines(Games gameId)
        {
            if (GameHelper.IsUnicornProviderGame((int)gameId))
            {
                return Ok(UnicornInitGameDataV3.GetPlayLines(gameId));
            }
            else
            {
                return Ok(InitGameDataV3.GetPlayLines(gameId));
            }
        }

        [HttpGet]
        [Route("games/{gameId}/lines/{index}")]
        public async Task<IActionResult> GetLine(Games gameId, int index)
        {
            if (GameHelper.IsUnicornProviderGame((int)gameId))
            {
                return Ok(UnicornInitGameDataV3.GetLinesByIndexV3(gameId, index));
            }
            else
            {
                return Ok(InitGameDataV3.GetLinesByIndexV3(gameId, index));
            }
        }

        [HttpGet]
        [Route("games/{gameId}/lines/{line}/index")]
        public async Task<IActionResult> GetIndexOfLine(Games gameId, int line)
        {
            if (GameHelper.IsUnicornProviderGame((int)gameId))
            {
                return Ok(UnicornInitGameDataV3.GetIndexForLinesV3(gameId, line));
            }
            else
            {
                return Ok(InitGameDataV3.GetIndexForLinesV3(gameId, line));
            }
        }

        [HttpPost]
        [Route("games/{gameId}/extract-bonus-details")]
        public async Task<IActionResult> ExtractBonusDetails(Games gameId, [FromBody] byte[] additionalDataArray)
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
    }
}