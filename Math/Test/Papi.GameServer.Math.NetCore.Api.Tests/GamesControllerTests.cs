using CombinationExtras;
using Papi.GameServer.Math.Contracts.Requests;
using Papi.GameServer.Math.Contracts.Responses;
using Papi.GameServer.Utils.Enums;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Xunit;

namespace Papi.GameServer.Math.NetCore.Api.Tests
{
    [Collection("MathCollection")]
    public class GamesControllerTests
    {
        #region  Fields and Constructor

        private readonly TestServer _server;

        public GamesControllerTests(TestServerFixture fixture)
        {
            _server = fixture.TestServer;
        }

        #endregion

        #region MyRegion

        public async Task<GenerateCombinationResult> GetCombinationApi(Games gameId, GenerateCombinationRequest request)
        {
            var response = await _server.CreateRequest($"/games/{gameId}/combinations")
                .And(x => x.Content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"))
                .PostAsync();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<GenerateCombinationResult>(
                    await response.Content.ReadAsStringAsync());
            }
            throw new Exception($"Combination not obtained. StatusCode: {response.StatusCode} , Message: {response.Content.ReadAsByteArrayAsync()}");
        }


        #endregion

        #region Tests

        [Theory]
        [InlineData(Games.NovomaticSizzlingHotDeluxe, 96, 5)]
        [InlineData(Games.NovomaticRoaringForties, 96, 40)]
        [InlineData(Games.TwinklingHot5, 95, 5)]
        [InlineData(Games.NovomaticBookOfRaDeluxe, 51, 10)] // base game

        public async Task HotGamesShouldHaveCorrectRtp(Games gameId, decimal predictedRtp, int numberOfLines)
        {
            decimal totalBet = 0, totalWin = 0;
            int i = 0;
            byte[] additionalArray  = new byte[5];
            byte gratisGameLeft = 0;

            for (; i < 10000000; i++)
            {
                var combination = SlotCombination.GetCombination(
                    gameId, 1, numberOfLines, gratisGameLeft,
                    ref additionalArray, 1, 0, 0, null);

                totalBet += numberOfLines;
                totalWin += combination.TotalWin;
            }

            decimal rtp = (totalWin / totalBet) * 100;

            rtp.Should().BeGreaterOrEqualTo((decimal)predictedRtp);
            rtp.Should().BeLessThan((decimal)(predictedRtp + 1));
        }


        [Theory]
        [InlineData(Games.BookOfSpells2, 96, 10)]
        [InlineData(Games.NovomaticLuckyLadysCharmDeluxe, 96, 10)]
        [InlineData(Games.NovomaticBookOfRaDeluxe, 96, 10)]
        [InlineData(Games.AlohaCharm, 96, 10)]

        public async Task BonusGamesShouldHaveCorrectRtp(Games gameId, decimal predictedRtp, int numberOfLines)
        {
            decimal totalBet = 0, totalWin = 0;
            int i = 0;
            byte[] additionalArray = new byte[5];
            byte additionalInformation = 0;
            int gratisGameLeft = 0;

            for (; i < 10000000; i++)
            {
                var isCurrentGameGratis = gratisGameLeft > 0;

                var combination = SlotCombination.GetCombination(
                    gameId, 1, numberOfLines, gratisGameLeft,
                    ref additionalArray, 1, additionalInformation, 0, null);

                additionalInformation = combination.AdditionalInformation;
                var cascadeWin = SlotCombination.GetCascadeWin(combination, gameId);

                long win = combination.TotalWin;
                if (cascadeWin > 0)
                {
                    win = cascadeWin;
                }

                if (combination.GratisGame)
                {
                    //ako su u toku igre, osvojene gratis igre, sabrati ih
                    gratisGameLeft += combination.NumberOfGratisGames;
                }
                if (isCurrentGameGratis)
                {
                    //ako je trenutna igra gratis, smanjiti broj za 1
                    gratisGameLeft--;
                }
                else
                {
                    totalBet += numberOfLines;
                }

                totalWin += win;
            }

            decimal rtp = (totalWin / totalBet) * 100;

            rtp.Should().BeGreaterOrEqualTo((decimal)predictedRtp-1);
            rtp.Should().BeLessThan((decimal)(predictedRtp + 1));
        }

        #endregion
    }
}