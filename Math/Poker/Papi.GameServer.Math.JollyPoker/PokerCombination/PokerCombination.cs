using Papi.GameServer.Math.JollyPoker.PlayingCardData;
using RNGUtils.RandomData;
using System;
using System.Linq;

namespace Papi.GameServer.Math.JollyPoker.PokerCombination
{
    public class PokerCombination
    {
        #region Constructors

        public PokerCombination()
        {
            _CardHand = new CardHand();
            _CardValue = new byte[5];
            _CardSign = new byte[5];
            _HoldCards = new byte[5];
        }

        #endregion

        #region Private fields

        private readonly CardHand _CardHand;

        #endregion

        #region Private properties

        private readonly byte[] _CardValue;
        private readonly byte[] _CardSign;
        private byte[] _HoldCards;

        #endregion

        #region Public properties

        public int Win;
        public Win WinType;
        public bool IsFirstDeal;
        public byte[] CardSign
        {
            get { return _CardSign; }
        }
        public byte[] CardValue
        {
            get { return _CardValue; }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Daje nasumičnu kombinaciju.
        /// </summary>
        /// <param name="bet">Ulog</param>
        /// <param name="holdCards">Koje katre da zadrži</param>
        /// <param name="secondDeal">Da li je drugo deljenje, odnosno da li igrač izvlači nove karte ili menja</param>
        public void GetCombination(int bet, byte[] holdCards, bool secondDeal)
        {
            if (secondDeal && Array.Exists(holdCards, element => element != 255))
            {
                _CardHand.ReplaceCards(holdCards);
                WinType = _CardHand.GetWin();
                _HoldCards = WinType == PlayingCardData.Win.None ? Enumerable.Repeat((byte)0, 5).ToArray() : _CardHand.GetSuggestionBytes();
                IsFirstDeal = false;
            }
            else
            {
                _CardHand.GetRadnomHand();
                WinType = _CardHand.GetWin();
                _HoldCards = _CardHand.GetSuggestionBytes();
                if (WinType == PlayingCardData.Win.FourOfAKind)
                {
                    _HoldCards = Enumerable.Repeat((byte)1, 5).ToArray();
                }
                IsFirstDeal = true;
            }

            for (var i = 0; i < 5; i++)
            {
                CardValue[_CardHand.GetCard(i).GetPosition()] = (byte)_CardHand.GetCard(i).GetValue();
                CardSign[_CardHand.GetCard(i).GetPosition()] = (byte)_CardHand.GetCard(i).GetSign();
            }

            Win = _CardHand.GetIntegerWin() * bet;
        }

        /// <summary>
        /// Pretvara kombinaciju u niz bajtova.
        /// </summary>
        /// <returns></returns>
        public byte[] ToByteArray()
        {
            var array = new byte[20];

            for (var i = 0; i < 5; i++)
            {
                array[i * 2] = CardValue[i];
                array[i * 2 + 1] = CardSign[i];
                array[10 + i] = _HoldCards[i];
            }
            for (var i = 3; i >= 0; i--)
            {
                array[18 - i] = (byte)(Win >> 8 * i);
            }
            array[19] = (byte)WinType;


            return array;
        }

        /// <summary>
        /// Proverava da li izabrane karte daju dobitak.
        /// </summary>
        /// <param name="positions"></param>
        /// <returns></returns>
        public bool WinningCardsHolden(byte[] positions)
        {
            return _CardHand.CheckIsHoldenCardsAreWinning(positions);
        }

        /// <summary>
        /// Daje fleš rojal kombinaciju, za potrebe testiranja.
        /// </summary>
        /// <param name="bet">Ulog</param>
        /// <param name="holdCards">Koje katre da zadrži</param>
        /// <param name="secondDeal">Da li je drugo deljenje, odnosno da li igrač izvlači nove karte ili menja</param>
        public void GetFlushRoyalCombination(int bet, byte[] holdCards, bool secondDeal)
        {
            var card = 51 - (int)SoftwareRng.Next(4);
            for (var i = 0; i < 5; i++)
            {
                _CardHand.SetCard(card - 4 * i, i);
            }
            _CardHand.MixHand();
            if (secondDeal && Array.IndexOf(holdCards, (byte)1) > -1)
            {
                _HoldCards = WinType == PlayingCardData.Win.None ? Enumerable.Repeat((byte)0, 5).ToArray() : _CardHand.GetSuggestionBytes();
                IsFirstDeal = false;
            }
            else
            {
                _HoldCards = _CardHand.GetSuggestionBytes();
                IsFirstDeal = true;
            }

            for (var i = 0; i < 5; i++)
            {
                CardValue[_CardHand.GetCard(i).GetPosition()] = (byte)_CardHand.GetCard(i).GetValue();
                CardSign[_CardHand.GetCard(i).GetPosition()] = (byte)_CardHand.GetCard(i).GetSign();
            }

            WinType = _CardHand.GetWin();
            Win = _CardHand.GetIntegerWin() * bet;
        }

        public byte[] GetHoldCards()
        {
            return _HoldCards;
        }

        /// <summary>
        /// Postavlja trenutne karte iz niza bajtova.
        /// </summary>
        /// <param name="cardHandArray"></param>
        public void SetCardHand(byte[] cardHandArray)
        {
            _CardHand.FromByteArray(cardHandArray);
        }

        /// <summary>
        /// Daje trenutnu ruku kao niz bajtova.
        /// </summary>
        /// <returns></returns>
        public byte[] GetCardHand()
        {
            if (_CardHand != null)
            {
                return _CardHand.ToByteArray();
            }
            return null;
        }

        #endregion

    }
}