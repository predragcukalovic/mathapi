using Papi.GameServer.Utils.Enums;
using System.Data;

namespace Papi.GameServer.Math.Api.Test.Unicorn
{
    [TestClass]
    public sealed class UnicornTwentyFruitsTest : BaseTestClass
    {
      

        [TestMethod]
        [DataRow(Games.UnicornTwentyFruits, 20)]
        [DataRow(Games.UnicornTwentyDice, 20)]
        [DataRow(Games.UnicornMoneyStandard, 20)]
        [DataRow(Games.UnicornMoneyStandardDice, 20)]
        public async Task Game_ShouldHave_NumberOfLines(Games game, int exepectedLines)
        {
            //Act
            var response = await GetLinesForGameTestMethod(game);

            //Assert     
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Length == 1);
            Assert.IsTrue(response.Any(x => x == exepectedLines));
        }

        [TestMethod]
        [DataRow(Games.UnicornTwentyFruits, 20, 20, 96.0, 20000, 2000000)]
        [DataRow(Games.UnicornTwentyDice, 20, 20, 96.0, 20000, 2000000)]
        [DataRow(Games.UnicornMoneyStandard, 20, 20, 96.0, 20000, 2000000)]
        [DataRow(Games.UnicornMoneyStandardDice, 20, 20, 96.0, 20000, 2000000)]
        public async Task Game_ShouldHave_Rtp(Games game, int lines, int bet, double expectedRtp, int iterationsMin, int iterationMax)
        {
            //Arrange
            double totalBet = 0, totalWin = 0;
            var iterationCount = 0;
            double rtp = 0;
            var condition = false;

            //Act
            while (iterationCount < iterationMax && !condition)
            {
                var rtpCalculation = await CalculateRtpForRegularGame(game, lines, bet, iterationsMin, totalBet, totalWin);
                iterationCount += iterationsMin;
                rtp = rtpCalculation.Rtp;
                totalBet = rtpCalculation.TotalBet;
                totalWin = rtpCalculation.TotalWin;

                condition = rtpCalculation.Rtp > (expectedRtp - 1) && rtpCalculation.Rtp < (expectedRtp + 1);
            }
            //Assert
            Assert.IsTrue(rtp > expectedRtp - 1);
            Assert.IsTrue(rtp < expectedRtp + 1);
        }
    }
}
