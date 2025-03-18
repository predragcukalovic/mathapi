using RNGUtils.RandomData;
using System;
using System.Collections.Generic;

namespace Papi.GameServer.Math.JollyPoker.PlayingCardData
{
    public enum Win
    {
        RoyalFlush = 8,
        StraightFlush = 7,
        FourOfAKind = 6,
        FullHouse = 5,
        Flush = 4,
        Straight = 3,
        ThreeOfAKind = 2,
        TwoPairs = 1,
        None = 0
    }

    public class CardHand
    {
        #region Constructor

        public CardHand()
        {
            _Cards = new PlayingCard[5];
            _HoldCards = new bool[5];
            _NextCards = new PlayingCard[4];
            for (var i = 0; i < 5; i++)
            {
                _Cards[i] = new PlayingCard((CardValue)i + 2, CardSign.Spades, i);
            }
            for (var i = 0; i < 4; i++)
            {
                _NextCards[i] = new PlayingCard((CardValue)i + 2, CardSign.Diamonds, i);
            }
        }

        #endregion

        #region Private fields

        private readonly PlayingCard[] _Cards;
        private readonly bool[] _HoldCards;
        private readonly PlayingCard[] _NextCards;
        private static readonly int[] _IntergerWins = { 0, 3, 5, 7, 9, 15, 50, 400, 1000 };

        #endregion

        #region Private properties

        /// <summary>
        /// Promeša pomoćni niz koji predstavlja špil karata
        /// </summary>
        /// <param name="deck">Niz od 44 elementa koji predstavlja špil</param>
        private static void CreateAndMixDeck(ref int[] deck)
        {
            for (var i = 0; i < 44; i++)
            {
                deck[i] = i + 8;
            }
            for (var i = 0; i < 43; i++)
            {
                var rnd = (int)SoftwareRng.Next(44 - i);
                var tmp = deck[43 - i];
                deck[43 - i] = deck[rnd];
                deck[rnd] = tmp;
            }
        }

        /// <summary>
        /// Popunjava niz _HoldCards sa true ili false.
        /// </summary>
        private void FillHoldArray(bool value)
        {
            for (var i = 0; i < 5; i++)
            {
                _HoldCards[i] = value;
            }
        }

        /// <summary>
        /// Provarava da li je dobitak Fleš Rojal u sotiranom nizu.
        /// </summary>
        /// <returns></returns>
        private bool CheckIsRoyalFlesh()
        {
            if (CheckIsStraightFlesh() && _Cards[3].GetValue() == CardValue.ValueK)
            {
                FillHoldArray(true);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Provarava da li je dobitak Kenta fleš u sotiranom nizu.
        /// </summary>
        /// <returns></returns>
        private bool CheckIsStraightFlesh()
        {
            if (CheckIsFlesh() && CheckIsStraight())
            {
                FillHoldArray(true);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Provarava da li je dobitak Četiri iste u sotiranom nizu.
        /// </summary>
        /// <returns></returns>
        private bool CheckIsFourOfAKind()
        {
            if (_Cards[4].GetValue() == _Cards[3].GetValue() &&
                _Cards[3].GetValue() == _Cards[2].GetValue() &&
                _Cards[2].GetValue() == _Cards[1].GetValue())
            {
                FillHoldArray(true);
                _HoldCards[_Cards[0].GetPosition()] = false;
                return true;
            }
            if (_Cards[3].GetValue() == _Cards[2].GetValue() &&
                _Cards[2].GetValue() == _Cards[1].GetValue() &&
                _Cards[1].GetValue() == _Cards[0].GetValue())
            {
                FillHoldArray(true);
                _HoldCards[_Cards[4].GetPosition()] = false;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Provarava da li je dobitak Ful u sotiranom nizu.
        /// </summary>
        /// <returns></returns>
        private bool CheckIsFullHouse()
        {
            if (_Cards[4].GetValue() == _Cards[3].GetValue() &&
                _Cards[3].GetValue() == _Cards[2].GetValue() &&
                _Cards[1].GetValue() == _Cards[0].GetValue())
            {
                FillHoldArray(true);
                return true;
            }
            if (_Cards[4].GetValue() == _Cards[3].GetValue() &&
                _Cards[2].GetValue() == _Cards[1].GetValue() &&
                _Cards[1].GetValue() == _Cards[0].GetValue())
            {
                FillHoldArray(true);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Provarava da li je dobitak Fleš u sotiranom nizu.
        /// </summary>
        /// <returns></returns>
        private bool CheckIsFlesh()
        {
            if (_Cards[0].GetSign() == _Cards[1].GetSign() &&
                _Cards[1].GetSign() == _Cards[2].GetSign() &&
                _Cards[2].GetSign() == _Cards[3].GetSign() &&
                _Cards[3].GetSign() == _Cards[4].GetSign())
            {
                FillHoldArray(true);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Provarava da li je dobitak Kenta u sotiranom nizu.
        /// </summary>
        /// <returns></returns>
        private bool CheckIsStraight()
        {
            if (_Cards[4].GetValue() == _Cards[3].GetValue() + 1 &&
                _Cards[3].GetValue() == _Cards[2].GetValue() + 1 &&
                _Cards[2].GetValue() == _Cards[1].GetValue() + 1 &&
                _Cards[1].GetValue() == _Cards[0].GetValue() + 1)
            {
                FillHoldArray(true);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Provarava da li je dobitak Tri iste u sotiranom nizu.
        /// </summary>
        /// <returns></returns>
        private bool CheckIsThreeOfAKind()
        {
            if (_Cards[4].GetValue() == _Cards[3].GetValue() &&
                _Cards[3].GetValue() == _Cards[2].GetValue())
            {
                FillHoldArray(true);
                _HoldCards[_Cards[1].GetPosition()] = false;
                _HoldCards[_Cards[0].GetPosition()] = false;
                return true;
            }
            if (_Cards[3].GetValue() == _Cards[2].GetValue() &&
                _Cards[2].GetValue() == _Cards[1].GetValue())
            {
                FillHoldArray(true);
                _HoldCards[_Cards[4].GetPosition()] = false;
                _HoldCards[_Cards[0].GetPosition()] = false;
                return true;
            }
            if (_Cards[2].GetValue() == _Cards[1].GetValue() &&
                _Cards[1].GetValue() == _Cards[0].GetValue())
            {
                FillHoldArray(true);
                _HoldCards[_Cards[4].GetPosition()] = false;
                _HoldCards[_Cards[3].GetPosition()] = false;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Provarava da li je dobitak Dva para u sotiranom nizu.
        /// </summary>
        /// <returns></returns>
        private bool CheckIsTwoPairs()
        {
            if (_Cards[4].GetValue() == _Cards[3].GetValue() &&
                _Cards[2].GetValue() == _Cards[1].GetValue())
            {
                FillHoldArray(true);
                _HoldCards[_Cards[0].GetPosition()] = false;
                return true;
            }
            if (_Cards[4].GetValue() == _Cards[3].GetValue() &&
                _Cards[1].GetValue() == _Cards[0].GetValue())
            {
                FillHoldArray(true);
                _HoldCards[_Cards[2].GetPosition()] = false;
                return true;
            }
            if (_Cards[3].GetValue() == _Cards[2].GetValue() &&
                _Cards[1].GetValue() == _Cards[0].GetValue())
            {
                FillHoldArray(true);
                _HoldCards[_Cards[4].GetPosition()] = false;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Daje predlog za kentu fleš.
        /// </summary>
        /// <returns></returns>
        private bool GetStraightFlushSuggestion()
        {
            for (var i = 0; i < 5; i++)
            {
                PlayingCard[] cardArray = { _Cards[(i + 1) % 5], _Cards[(i + 2) % 5], _Cards[(i + 3) % 5], _Cards[(i + 4) % 5] };
                Array.Sort(cardArray);
                if (CheckIsCanStraight(cardArray[0].GetValue(), cardArray[1].GetValue(), cardArray[2].GetValue(), cardArray[3].GetValue()))
                {
                    if (cardArray[0].GetSign() == cardArray[1].GetSign() &&
                    cardArray[1].GetSign() == cardArray[2].GetSign() &&
                    cardArray[2].GetSign() == cardArray[3].GetSign())
                    {
                        FillHoldArray(true);
                        _HoldCards[_Cards[i].GetPosition()] = false;
                        return true;
                    }
                }
            }

            for (var i = 0; i <= 2; i++)
            {
                for (var j = i + 1; j <= 3; j++)
                {
                    for (var k = j + 1; k <= 4; k++)
                    {
                        if (CheckIsCanStraight(_Cards[i].GetValue(), _Cards[j].GetValue(), _Cards[k].GetValue()))
                        {
                            if (_Cards[i].GetSign() == _Cards[j].GetSign() &&
                            _Cards[j].GetSign() == _Cards[k].GetSign())
                            {
                                FillHoldArray(false);
                                _HoldCards[_Cards[i].GetPosition()] = true;
                                _HoldCards[_Cards[j].GetPosition()] = true;
                                _HoldCards[_Cards[k].GetPosition()] = true;
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Daje predlog za 4 karte kojima fali jedna da bude Boja.
        /// </summary>
        /// <returns></returns>
        private bool GetFlushSuggestion()
        {
            for (var i = 0; i < 5; i++)
            {
                if (_Cards[(i + 1) % 5].GetSign() == _Cards[(i + 2) % 5].GetSign() &&
                    _Cards[(i + 2) % 5].GetSign() == _Cards[(i + 3) % 5].GetSign() &&
                    _Cards[(i + 3) % 5].GetSign() == _Cards[(i + 4) % 5].GetSign())
                {
                    FillHoldArray(true);
                    _HoldCards[_Cards[i].GetPosition()] = false;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Daje predlog za 4 karte kojima fali jedna da bude Kenta.
        /// </summary>
        /// <returns></returns>
        private bool GetStraightSuggestion()
        {
            for (var i = 0; i < 5; i++)
            {
                PlayingCard[] cardArray = { _Cards[(i + 1) % 5], _Cards[(i + 2) % 5], _Cards[(i + 3) % 5], _Cards[(i + 4) % 5] };
                Array.Sort(cardArray);
                if (CheckIsCanStraight(cardArray[0].GetValue(), cardArray[1].GetValue(), cardArray[2].GetValue(), cardArray[3].GetValue()))
                {
                    FillHoldArray(true);
                    _HoldCards[_Cards[i].GetPosition()] = false;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Daje predlog za dve iste karte.
        /// </summary>
        /// <returns></returns>
        private bool GetOnePairSuggestion()
        {
            for (var i = 4; i > 0; i--)
            {
                if (_Cards[i].GetValue() == _Cards[i - 1].GetValue())
                {
                    FillHoldArray(false);
                    _HoldCards[_Cards[i].GetPosition()] = true;
                    _HoldCards[_Cards[i - 1].GetPosition()] = true;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Proverava da li tri vrednosti mogu da budu kenta, pod predpostavkom da su sortirane.
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <param name="c3"></param>
        /// <returns></returns>
        private static bool CheckIsCanStraight(CardValue c1, CardValue c2, CardValue c3)
        {
            return c1 == c2 - 1 && c2 == c3 - 1 ||
                   c1 == c2 - 1 && c2 == c3 - 2 ||
                   c1 == c2 - 2 && c2 == c3 - 1 ||
                   c1 == c2 - 2 && c2 == c3 - 2 ||
                   c1 == c2 - 3 && c2 == c3 - 1 ||
                   c1 == c2 - 1 && c2 == c3 - 3;
        }

        /// <summary>
        /// Proverava da li četiri vrednosti mogu da budu kenta, pod predpostavkom da su sortirane.
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <param name="c3"></param>
        /// <param name="c4"> </param>
        /// <returns></returns>
        private static bool CheckIsCanStraight(CardValue c1, CardValue c2, CardValue c3, CardValue c4)
        {
            return c1 == c2 - 1 && c2 == c3 - 1 && c3 == c4 - 1 ||
                   c1 == c2 - 1 && c2 == c3 - 1 && c3 == c4 - 2 ||
                   c1 == c2 - 1 && c2 == c3 - 2 && c3 == c4 - 1 ||
                   c1 == c2 - 2 && c2 == c3 - 1 && c3 == c4 - 1;
        }

        /// <summary>
        /// Sortira karte.
        /// </summary>
        private void SortHand()
        {
            Array.Sort(_Cards);
        }

        /// <summary>
        /// Sortira karte po poziciji.
        /// </summary>
        private void UnsortHand()
        {
            Array.Sort(_Cards, PlayingCard.ComparisonPosition);
        }

        /// <summary>
        /// Daje predlog koje karte da se zadrže.
        /// </summary>
        /// <returns></returns>
        private bool[] GetSuggestion()
        {
            var win = GetWin();
            if (win > 0)
            {
                UnsortHand();
                return _HoldCards;
            }
            if (GetStraightFlushSuggestion())
            {
                UnsortHand();
                return _HoldCards;
            }
            if (GetStraightSuggestion())
            {
                UnsortHand();
                return _HoldCards;
            }
            if (GetFlushSuggestion())
            {
                UnsortHand();
                return _HoldCards;
            }
            if (GetOnePairSuggestion())
            {
                UnsortHand();
                return _HoldCards;
            }

            UnsortHand();
            return _HoldCards;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Daje nasumičnu ruku.
        /// </summary>
        public void GetRadnomHand()
        {
            var deck = new int[44];
            CreateAndMixDeck(ref deck);
            for (var i = 0; i < 5; i++)
            {
                _Cards[i] = new PlayingCard(deck[i], i);
            }
            for (var i = 5; i < 9; i++)
            {
                _NextCards[i - 5] = new PlayingCard(deck[i], i);
            }
        }

        /// <summary>
        /// Određuje dobitak.
        /// </summary>
        /// <returns></returns>
        public Win GetWin()
        {
            SortHand();
            FillHoldArray(false);
            if (CheckIsRoyalFlesh())
            {
                return Win.RoyalFlush;
            }
            if (CheckIsStraightFlesh())
            {
                return Win.StraightFlush;
            }
            if (CheckIsFourOfAKind())
            {
                return Win.FourOfAKind;
            }
            if (CheckIsFullHouse())
            {
                return Win.FullHouse;
            }
            if (CheckIsFlesh())
            {
                return Win.Flush;
            }
            if (CheckIsStraight())
            {
                return Win.Straight;
            }
            if (CheckIsThreeOfAKind())
            {
                return Win.ThreeOfAKind;
            }
            if (CheckIsTwoPairs())
            {
                return Win.TwoPairs;
            }
            return Win.None;
        }

        /// <summary>
        /// Određuje multiklikator dobitka.
        /// </summary>
        /// <returns></returns>
        public int GetIntegerWin()
        {
            return _IntergerWins[(int)GetWin()];
        }

        /// <summary>
        /// Menja karte koje nisu zadržane
        /// </summary>
        /// <param name="hold"></param>
        public void ReplaceCards(byte[] hold)
        {
            UnsortHand();
            var index = 0;
            for (var i = 0; i < 5; i++)
            {
                if (Array.IndexOf(hold, (byte)i) == -1)
                {
                    _Cards[i] = _NextCards[index++];
                    _Cards[i].SetPosition(i);
                }
            }
        }

        /// <summary>
        /// Daje predlog koje karte da se zadrže, kao niz bajtova.
        /// </summary>
        /// <returns></returns>
        public byte[] GetSuggestionBytes()
        {
            var boolArray = GetSuggestion();
            var array = new byte[5];

            for (var i = 0; i < 5; i++)
            {
                array[i] = (byte)(boolArray[i] ? 1 : 0);
            }

            return array;
        }

        /// <summary>
        /// Postavlja kartu na određenoj poziciji.
        /// </summary>
        /// <param name="cardNumericValue"></param>
        /// <param name="position"></param>
        public void SetCard(int cardNumericValue, int position)
        {
            for (int i = 0; i < 5; i++)
            {
                if (_Cards[i].GetPosition() == position)
                {
                    _Cards[i].FromNumericValue(cardNumericValue);
                    return;
                }
            }
        }

        /// <summary>
        /// Uzima kartu iz ruke na index poziciji.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public PlayingCard GetCard(int index)
        {
            return _Cards[index];
        }

        /// <summary>
        /// Proverava da li su zadržane karte dobitne.
        /// </summary>
        /// <param name="positionsOfHoldenCards">Pozicije zadržanih karata, niz od 4 elementa</param>
        /// <returns></returns>
        public bool CheckIsHoldenCardsAreWinning(byte[] positionsOfHoldenCards)
        {
            UnsortHand();
            if (Array.IndexOf(positionsOfHoldenCards, (byte)255) == -1)
            {
                if (_Cards[positionsOfHoldenCards[0]].GetValue() == _Cards[positionsOfHoldenCards[1]].GetValue())
                {
                    if (_Cards[positionsOfHoldenCards[0]].GetValue() == _Cards[positionsOfHoldenCards[2]].GetValue()
                        || _Cards[positionsOfHoldenCards[0]].GetValue() == _Cards[positionsOfHoldenCards[3]].GetValue())
                    {
                        return true;
                    }
                    if (_Cards[positionsOfHoldenCards[2]].GetValue() == _Cards[positionsOfHoldenCards[3]].GetValue())
                    {
                        return true;
                    }
                }
                if (_Cards[positionsOfHoldenCards[0]].GetValue() == _Cards[positionsOfHoldenCards[2]].GetValue())
                {
                    if (_Cards[positionsOfHoldenCards[0]].GetValue() == _Cards[positionsOfHoldenCards[3]].GetValue())
                    {
                        return true;
                    }
                    if (_Cards[positionsOfHoldenCards[1]].GetValue() == _Cards[positionsOfHoldenCards[3]].GetValue())
                    {
                        return true;
                    }
                }
                if (_Cards[positionsOfHoldenCards[0]].GetValue() == _Cards[positionsOfHoldenCards[3]].GetValue() &&
                    _Cards[positionsOfHoldenCards[1]].GetValue() == _Cards[positionsOfHoldenCards[2]].GetValue())
                {
                    return true;
                }
                if (_Cards[positionsOfHoldenCards[1]].GetValue() == _Cards[positionsOfHoldenCards[2]].GetValue()
                        && _Cards[positionsOfHoldenCards[2]].GetValue() == _Cards[positionsOfHoldenCards[3]].GetValue())
                {
                    return true;
                }
            }

            if (Array.IndexOf(positionsOfHoldenCards, (byte)255) == 3)
            {
                if (_Cards[positionsOfHoldenCards[0]].GetValue() == _Cards[positionsOfHoldenCards[1]].GetValue() &&
                    _Cards[positionsOfHoldenCards[1]].GetValue() == _Cards[positionsOfHoldenCards[2]].GetValue())
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Promeša pet karata.
        /// </summary>
        public void MixHand()
        {
            UnsortHand();
            for (int i = 0; i < 4; i++)
            {
                var position = SoftwareRng.Next(5 - i);
                var tmp = _Cards[position].GetPosition();
                _Cards[position].SetPosition(_Cards[4 - i].GetPosition());
                _Cards[4 - i].SetPosition(tmp);
            }
            UnsortHand();
        }

        /// <summary>
        /// Pretvara ruku u niz bajtova.
        /// </summary>
        /// <returns></returns>
        public byte[] ToByteArray()
        {
            var list = new List<byte>();
            for (var i = 0; i < 5; i++)
            {
                list.Add((byte)_Cards[i].GetCardNumericValue());
                list.Add((byte)_Cards[i].GetPosition());
                list.Add((byte)(_HoldCards[i] ? 1 : 0));
            }
            for (var i = 0; i < 4; i++)
            {
                list.Add((byte)_NextCards[i].GetCardNumericValue());
                list.Add((byte)_NextCards[i].GetPosition());
            }

            return list.ToArray();
        }

        /// <summary>
        /// Rekonstruiše ruku iz niza bajtova.
        /// </summary>
        /// <param name="array"></param>
        public void FromByteArray(byte[] array)
        {
            for (var i = 0; i < 5; i++)
            {
                _Cards[i].SetValue((CardValue)(array[i * 3] / 4));
                _Cards[i].SetSign((CardSign)(array[i * 3] % 4));
                _Cards[i].SetPosition(array[i * 3 + 1]);
                _HoldCards[i] = array[i * 3 + 2] != 0;
            }
            for (var i = 0; i < 4; i++)
            {
                _NextCards[i].SetValue((CardValue)(array[i * 2 + 15] / 4));
                _NextCards[i].SetSign((CardSign)(array[i * 2 + 15] % 4));
                _NextCards[i].SetPosition(array[i * 2 + 16]);
            }
        }

        #endregion
    }
}
