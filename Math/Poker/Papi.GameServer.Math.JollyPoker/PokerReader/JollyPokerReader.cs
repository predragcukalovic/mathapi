using Papi.GameServer.Math.JollyPoker.PlayingCardData;
using RNGUtils.RandomData;
using System;

namespace Papi.GameServer.Math.JollyPoker.PokerReader
{
    public class JollyPokerReader
    {
        #region Constructors

        public JollyPokerReader()
        {
            _PokerCombination = new PokerCombination.PokerCombination();
        }

        #endregion

        #region Event Handler

        private static void ValidatorWarning()
        {
            _ValidGenerator = false;
        }

        #endregion

        #region Private fields

        private PokerCombination.PokerCombination _PokerCombination;
        private static bool _ValidGenerator = true;

        #endregion

        #region Public methods

        public static void InitHandler()
        {
            RngValidator.OnValidatorWarning += ValidatorWarning;
        }

        /// <summary>
        /// Daje sledeću ruku.
        /// </summary>
        /// <param name="bet">Ulog</param>
        /// <param name="hold">Karte koje igrač želi da zadrži</param>
        /// <param name="secondDeal">Da li je u pitanju zamena karata?</param>
        /// <returns></returns>
        public PokerCombination.PokerCombination GetNextDeal(byte[] cardHand, int bet, byte[] hold, bool secondDeal)
        {
            if (_PokerCombination == null)
            {
                if (secondDeal)
                {
                    ///TODO: LOGIRANJE
                    return null;
                }
                _PokerCombination = new PokerCombination.PokerCombination();
            }
            if (!_ValidGenerator)
            {
                throw new Exception("Random Number Generator Failed!");
            }

            if (cardHand != null && cardHand.Length != 0)
            {
                _PokerCombination.SetCardHand(cardHand);
            }

            _PokerCombination.GetCombination(bet, hold, secondDeal);
            return _PokerCombination;
        }

        /// <summary>
        /// Daje sledeću ruku koja sigurno ima dobitak winType.
        /// </summary>
        /// <param name="bet"></param>
        /// <param name="hold"></param>
        /// <param name="secondDeal"></param>
        /// <param name="winType"></param>
        /// <returns></returns>
        public PokerCombination.PokerCombination GetNextDealTest(int bet, byte[] hold, bool secondDeal, Win winType)
        {
            if (secondDeal)
            {
                _PokerCombination.GetCombination(bet, hold, true);
                return _PokerCombination;
            }
            if (winType == Win.RoyalFlush)
            {
                _PokerCombination.GetFlushRoyalCombination(bet, hold, false);
                return _PokerCombination;
            }
            do
            {
                _PokerCombination.GetCombination(bet, hold, false);
            } while (_PokerCombination.WinType != winType);
            return _PokerCombination;
        }

        /// <summary>
        /// Proverava da li zadržane karte daju dobitak.
        /// </summary>
        /// <returns></returns>
        public bool IsHoldenCardsWinning(byte[] positionsHolden)
        {
            return _PokerCombination.WinningCardsHolden(positionsHolden);
        }

        /// <summary>
        /// Rekonstruiše ruku iz niza bajtova.
        /// </summary>
        /// <param name="array"></param>
        public void ReconstructCardHand(byte[] array)
        {
            try
            {
                _PokerCombination.SetCardHand(array);
            }
            catch (Exception)
            {
                _PokerCombination = null;
            }
        }

        /// <summary>
        /// Daje trenutnu ruku kao niz bajtova.
        /// </summary>
        /// <returns></returns>
        public byte[] GetCurrentCardHand()
        {
            try
            {
                return _PokerCombination.GetCardHand();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        #endregion
    }
}