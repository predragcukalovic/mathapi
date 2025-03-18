using Papi.GameServer.Math.ApiCore.Extensions;
using Papi.GameServer.Math.Contracts.Requests;
using Papi.GameServer.Math.JollyPoker.PlayingCardData;
using Papi.GameServer.Math.JollyPoker.PokerReader;
using Papi.GameServer.Utils.Logging;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace Papi.GameServer.Math.ApiCore.Controllers
{
    public class PokerController : ControllerBase
    {
        private readonly JollyPokerReader _JollyPokerReader;

        public PokerController(JollyPokerReader jollyPokerReader)
        {
            _JollyPokerReader = jollyPokerReader;
        }

        [HttpPost]
        [Route("poker/next-deal")]
        public IActionResult GetNextDeal([FromBody] NextDealRequest model)
        {
            try
            {
                Logger.LogInfo("GetNextDeal request: {@GetNextDealRequest}", model);

                byte[] cardHand = null;
                if (model.CardHand != null)
                {
                    cardHand = Convert.FromBase64String(model.CardHand);
                }

                byte[] cardsToHold = null;
                if (model.CardsToHold != null)
                {
                    cardsToHold = Convert.FromBase64String(model.CardsToHold);
                }

                Logger.LogInfo("GetNextDeal request: {@GetNextDealBytes}", new
                {
                    CardHand = cardHand,
                    CardsToHold = cardsToHold,
                    Bet = model.Bet,
                    IsHoldingAllowd = model.IsHoldingAllowed
                });

                var deal = _JollyPokerReader.GetNextDeal(cardHand, model.Bet, cardsToHold, model.IsHoldingAllowed);
                var dealResponse = deal.ToPokerCombinationModel();

                Logger.LogInfo("GetNextDeal response: {@GetNextDealResponse}", dealResponse);
                return Ok(dealResponse);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "GetNextDeal Exception {@GetNextDealRequest}", model);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("poker/next-deal-test")]
        public IActionResult GetNextDealTest([FromBody] NextDealTestRequest model)
        {
            try
            {
                Logger.LogInfo("GetNextDealTest request: {@GetNextDealTestRequest}", model);

                byte[] cardsToHold = null;
                if (model.CardsToHold != null)
                {
                    cardsToHold = Convert.FromBase64String(model.CardsToHold);
                }

                Logger.LogInfo("GetNextDeal request: {@GetNextDealTestBytes}", new
                {
                    TestCombination = model.TestCombination,
                    CardsToHold = cardsToHold,
                    Bet = model.Bet,
                    IsHoldingAllowd = model.IsHoldingAllowed
                });

                var deal = _JollyPokerReader.GetNextDealTest(model.Bet,
                    cardsToHold, model.IsHoldingAllowed, (Win)model.TestCombination);

                var dealResponse = deal.ToPokerCombinationModel();

                Logger.LogInfo("GetNextDealTest response: {@GetNextDealTestResponse}", dealResponse);
                return Ok(dealResponse);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "GetNetxDealTest Exception {@GetNextDealTestRequest}", model);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}