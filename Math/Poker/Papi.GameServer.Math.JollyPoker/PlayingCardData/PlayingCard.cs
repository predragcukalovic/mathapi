using System;

namespace Papi.GameServer.Math.JollyPoker.PlayingCardData
{
    public enum CardSign
    {
        Spades = 0,
        Diamonds = 3
    };

    public enum CardValue
    {
        ValueK = 11
    };

    public class PlayingCard : IComparable<PlayingCard>
    {
        #region Private fields

        private CardValue _Value;
        private CardSign _Sign;
        private int _Position;

        #endregion

        #region Private properties

        /// <summary>
        /// Vraća vrednost karte od 0 do 51.
        /// </summary>
        /// <returns></returns>
        public int GetCardNumericValue()
        {
            return (int)_Value * 4 + (int)_Sign;
        }

        private bool Equals(PlayingCard other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return Equals(other._Value, _Value) && Equals(other._Sign, _Sign) && other._Position == _Position;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Postavlja vrednosti karte na osnovu njene numericke vrednosti.
        /// </summary>
        /// <param name="value"></param>
        public void FromNumericValue(int value)
        {
            value %= 52;
            _Sign = (CardSign)(value % 4);
            _Value = (CardValue)(value / 4);
        }

        public PlayingCard(int numericValue, int position)
        {
            FromNumericValue(numericValue);
            _Position = position;
        }

        public PlayingCard(CardValue value, CardSign sign, int position)
        {
            _Value = value;
            _Sign = sign;
            _Position = position;
        }

        /// <summary>
        /// Postavlja poziciju karte na zadatu vrednost.
        /// </summary>
        /// <param name="position"></param>
        public void SetPosition(int position)
        {
            _Position = position;
        }

        /// <summary>
        /// Daje znak karte.
        /// </summary>
        /// <returns></returns>
        public CardSign GetSign()
        {
            return _Sign;
        }

        /// <summary>
        /// Daje vrednost karte.
        /// </summary>
        /// <returns></returns>
        public CardValue GetValue()
        {
            return _Value;
        }

        /// <summary>
        /// Daje poziciju karte.
        /// </summary>
        /// <returns></returns>
        public int GetPosition()
        {
            return _Position;
        }

        public static bool operator <(PlayingCard c1, PlayingCard c2)
        {
            return c1.GetCardNumericValue() < c2.GetCardNumericValue();
        }

        public static bool operator ==(PlayingCard c1, PlayingCard c2)
        {
            if (ReferenceEquals(c1, c2))
            {
                return true;
            }

            if ((object)c1 == null || (object)c2 == null)
            {
                return false;
            }
            return c1.GetCardNumericValue() == c2.GetCardNumericValue();
        }

        public static bool operator !=(PlayingCard c1, PlayingCard c2)
        {
            return !(c1 == c2);
        }

        public static bool operator >(PlayingCard c1, PlayingCard c2)
        {
            return !(c1 < c2) && c2 != c1;
        }

        public int CompareTo(PlayingCard other)
        {
            return GetCardNumericValue().CompareTo(other.GetCardNumericValue());
        }

        /// <summary>
        /// Za sotriranje po poziciji.
        /// </summary>
        public static readonly Comparison<PlayingCard> ComparisonPosition =
            (object1, object2) => object1._Position.CompareTo(object2._Position);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != typeof(PlayingCard))
            {
                return false;
            }
            return Equals((PlayingCard)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var result = _Value.GetHashCode();
                result = result * 397 ^ _Sign.GetHashCode();
                result = result * 397 ^ _Position;
                return result;
            }
        }

        /// <summary>
        /// Postavlja vrednost karte na zadatu vrednost.
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(CardValue value)
        {
            _Value = value;
        }

        /// <summary>
        /// Postavlja znak karte na zadatu vrednost.
        /// </summary>
        /// <param name="sign"></param>
        public void SetSign(CardSign sign)
        {
            _Sign = sign;
        }

        #endregion
    }
}
