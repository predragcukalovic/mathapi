using CombinationExtras.ReaderData;
using Papi.GameServer.Math.Api.Controllers;
using Papi.GameServer.Math.Api.Test.Model;
using Papi.GameServer.Math.Contracts.Responses;
using Papi.GameServer.Utils.Enums;
using Papi.GameServer.Utils.Logging;
using Newtonsoft.Json;
using Serilog.Events;
using System.Web.Http.Results;
using Services;

namespace Papi.GameServer.Math.Api.Test
{
    [TestClass]
    public class BaseTestClass
    {
        protected GamesController _controller;

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext testContext)
        {
            var minimumLevel = (LogEventLevel)Enum.Parse(
                typeof(LogEventLevel), "Information", true);

            MathSlotFilesReader.ReadAllFiles($"{TestSettings.ServerPath}\\Data", new Games(), "Regular");
            UnicornFileReader.ReadAllFiles($"{TestSettings.ServerPath}\\DataExt", new Games());
            GamesConfigReader.ReadGamesConfigData($"{TestSettings.ServerPath}\\GameConfigData\\GamesConfig.json");
            Logger.Init("C:\\Logs", "MathAPI", false, minimumLevel);
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
        }

        [TestInitialize]
        public void TestInitialize()
        {
            var additionalGameDataService = new AdditionalGameDataService();
            _controller = new GamesController(additionalGameDataService);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // This method is called after each test method.
        }

        public async Task<int[]> GetLinesForGameTestMethod(Games game)
        {
            var response = await _controller.GetLines(game);

            //Assert
            var okObjectResult = response as OkNegotiatedContentResult<int[]>;
            Assert.IsNotNull(okObjectResult);

            return okObjectResult.Content;
        }


        public async Task<double> CalculateRtpForRegularGame(Games game, int lines, int bet, int iterations)
        {
            double totalBet = 0, totalWin = 0;
            int betPerLine = bet / lines;

            //Act
            for (int i = 0; i < iterations; i++)
            {
                var response = await _controller.GenerateCombination(game, new Contracts.Requests.GenerateCombinationRequest
                {
                    Bet = betPerLine,
                    NumberOfLines = lines,
                    GratisGamesLeft = 0,
                    AdditionalInformation = 0,
                    AdditionalArray = null,
                    GameDataobj = null,
                    PlayerChoice = 0,
                    ReturnJson = false,
                    Credits = 1000,
                    IsCurrentGameGratis = false,
                    ShouldGenerateSmBetResult = false,
                    SmBetResultInBonus = string.Empty
                });

                var okObjectResult = response as OkNegotiatedContentResult<GenerateCombinationResult>;
                Assert.IsNotNull(okObjectResult);

                var model = okObjectResult.Content;
                Assert.IsNotNull(model);

                totalBet += bet;
                totalWin += model.Win;
            }

            double rtp = totalWin / totalBet * 100;

            return rtp;
        }


        public async Task<RtpCalculationDto> CalculateRtpForRegularGame(Games game, int lines, int bet, int iterations, double totalBet, double totalWin)
        {
            int betPerLine = bet / lines;

            //Act
            for (int i = 0; i < iterations; i++)
            {
                var response = await _controller.GenerateCombination(game, new Contracts.Requests.GenerateCombinationRequest
                {
                    Bet = betPerLine,
                    NumberOfLines = lines,
                    GratisGamesLeft = 0,
                    AdditionalInformation = 0,
                    AdditionalArray = null,
                    GameDataobj = null,
                    PlayerChoice = 0,
                    ReturnJson = false,
                    Credits = 1000,
                    IsCurrentGameGratis = false,
                    ShouldGenerateSmBetResult = false,
                    SmBetResultInBonus = string.Empty
                });

                var v = JsonConvert.SerializeObject(new Contracts.Requests.GenerateCombinationRequest
                {
                    Bet = betPerLine,
                    NumberOfLines = lines,
                    GratisGamesLeft = 0,
                    AdditionalInformation = 0,
                    AdditionalArray = null,
                    GameDataobj = null,
                    PlayerChoice = 0,
                    ReturnJson = false,
                    Credits = 1000,
                    IsCurrentGameGratis = false,
                    ShouldGenerateSmBetResult = false,
                    SmBetResultInBonus = string.Empty
                });


                var okObjectResult = response as OkNegotiatedContentResult<GenerateCombinationResult>;
                Assert.IsNotNull(okObjectResult);

                var model = okObjectResult.Content;
                Assert.IsNotNull(model);

                totalBet += bet;
                totalWin += model.Win;
            }

            double rtp = totalWin / totalBet * 100;

            return new RtpCalculationDto
            {
                Rtp = rtp,
                TotalBet = totalBet,
                TotalWin = totalWin
            };
        }



        public async Task<double> CalculateRtpForGamesWithFreeSpinsFeature(Games game, int lines, int bet, int iterations)
        {
            double totalBet = 0, totalWin = 0;
            string additionalArray = null;
            int betPerLine = bet / lines;

            byte additionalInformation = 0;
            int gratisGameLeft = 0;
            bool isCurrentGameGratis = false;

            //Act
            for (int i = 0; i < iterations; i++)
            {
                isCurrentGameGratis = gratisGameLeft > 0;

                var response = await _controller.GenerateCombination(game, new Contracts.Requests.GenerateCombinationRequest
                {
                    Bet = betPerLine,
                    NumberOfLines = lines,
                    GratisGamesLeft = gratisGameLeft,
                    AdditionalInformation = additionalInformation,
                    AdditionalArray = additionalArray,
                    GameDataobj = null,
                    PlayerChoice = 0,
                    ReturnJson = false,
                    Credits = 1000,
                    IsCurrentGameGratis = isCurrentGameGratis,
                    ShouldGenerateSmBetResult = false,
                    SmBetResultInBonus = string.Empty
                });

                var okObjectResult = response as OkNegotiatedContentResult<GenerateCombinationResult>;
                Assert.IsNotNull(okObjectResult);

                var model = okObjectResult.Content;
                Assert.IsNotNull(model);

                if (model.IsGratisGame)
                {
                    gratisGameLeft += model.NumberOfGratisGames;
                }


                if (isCurrentGameGratis)
                {
                    //ako je trenutna igra gratis, smanjiti broj za 1
                    gratisGameLeft--;
                }
                else
                {
                    totalBet += bet;
                }

                additionalInformation = model.AdditionalInformation;

                if (model.AdditionalArray != null && model.AdditionalArray.Any(x => x != 0))
                {
                    additionalArray = Convert.ToBase64String(model.AdditionalArray);
                }
                else
                {
                    additionalArray = null;
                }

                totalWin += model.Win;
            }

            double rtp = totalWin / totalBet * 100;

            return rtp;
        }


        public async Task<RtpCalculationDto> CalculateRtpForGamesWithFreeSpinsFeature(Games game, int lines, int bet, int iterations, double totalBet, double totalWin)
        {
            string additionalArray = null;
            int betPerLine = bet / lines;

            byte additionalInformation = 0;
            int gratisGameLeft = 0;
            bool isCurrentGameGratis = false;

            //Act
            for (int i = 0; i < iterations; i++)
            {
                isCurrentGameGratis = gratisGameLeft > 0;

                var response = await _controller.GenerateCombination(game, new Contracts.Requests.GenerateCombinationRequest
                {
                    Bet = betPerLine,
                    NumberOfLines = lines,
                    GratisGamesLeft = gratisGameLeft,
                    AdditionalInformation = additionalInformation,
                    AdditionalArray = additionalArray,
                    GameDataobj = null,
                    PlayerChoice = 0,
                    ReturnJson = false,
                    Credits = 1000,
                    IsCurrentGameGratis = isCurrentGameGratis,
                    ShouldGenerateSmBetResult = false,
                    SmBetResultInBonus = string.Empty
                });

                var okObjectResult = response as OkNegotiatedContentResult<GenerateCombinationResult>;
                Assert.IsNotNull(okObjectResult);

                var model = okObjectResult.Content;
                Assert.IsNotNull(model);

                if (model.IsGratisGame)
                {
                    gratisGameLeft += model.NumberOfGratisGames;
                }


                if (isCurrentGameGratis)
                {
                    //ako je trenutna igra gratis, smanjiti broj za 1
                    gratisGameLeft--;
                }
                else
                {
                    totalBet += bet;
                }

                additionalInformation = model.AdditionalInformation;

                if (model.AdditionalArray != null && model.AdditionalArray.Any(x => x != 0))
                {
                    additionalArray = Convert.ToBase64String(model.AdditionalArray);
                }
                else
                {
                    additionalArray = null;
                }

                totalWin += model.Win;
            }

            double rtp = totalWin / totalBet * 100;

            return new RtpCalculationDto
            {
                Rtp = rtp,
                TotalBet = totalBet,
                TotalWin = totalWin
            };
        }



    }
}
