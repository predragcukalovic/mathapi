using MathForGames.BasicGameData;
using System;
using System.Linq;

namespace MathForGames.GamePokerSlot
{
    public enum PokerSlotWin
    {
        RoyalFlush = 8,
        FiveOfAKind = 9,
        StraightFlush = 7,
        FourOfAKind = 6,
        FullHouse = 5,
        Flush = 4,
        Straight = 3,
        ThreeOfAKind = 2,
        TwoPairs = 1,
        NoWin = 0
    }

    public class LinePokerSlot
    {
        public int[] Hand;
        private int[] _ValueCount;
        private int[] _SignCount;

        public LinePokerSlot()
        {
            Hand = new int[5];
            _ValueCount = new int[13];
            _SignCount = new int[4];
        }

        public LinePokerSlot(int c1, int c2, int c3, int c4, int c5)
        {
            Hand = new int[5];
            Hand[0] = c1;
            Hand[1] = c2;
            Hand[2] = c3;
            Hand[3] = c4;
            Hand[4] = c5;
            DoCount();
        }

        private void DoCount()
        {
            _ValueCount = new int[13];
            _SignCount = new int[4];
            for (var i = 0; i < 5; i++)
            {
                if (Hand[i] < 52)
                {
                    _ValueCount[Hand[i] / 4]++;
                    _SignCount[Hand[i] % 4]++;
                }
            }
        }

        private int JokerCount(bool gratis)
        {
            if (!gratis)
            {
                return 0;
            }
            return Hand.Count(x => x == 52);
        }

        private bool IsRoyalFlush(bool straigth, bool flush)
        {
            if (!flush || !straigth)
            {
                return false;
            }
            if (_ValueCount[12] > 0 && _ValueCount[0] == 0 && _ValueCount[1] == 0 && _ValueCount[2] == 0 && _ValueCount[3] == 0)
            {
                return true;
            }
            var l = 0;
            while (_ValueCount[l] == 0 && l < 12)
            {
                l++;
            }
            if (l >= 8)
            {
                return true;
            }
            return false;
        }

        private bool IsFlush(int jokerCount)
        {
            return _SignCount.Any(x => x == 5 - jokerCount);
        }

        private bool IsStraigth(int jokerCount)
        {
            var cc = Hand.Where(x => x < 52).ToArray();
            if (jokerCount >= 4)
            {
                return true;
            }
            if (cc.Length + jokerCount < 5)
            {
                return false;
            }
            Array.Sort(cc);
            Array.Reverse(cc);
            if (_ValueCount[0] < 2 && _ValueCount[1] < 2 && _ValueCount[2] < 2 && _ValueCount[3] < 2 && _ValueCount[12] < 2
                && _ValueCount[0] + _ValueCount[1] + _ValueCount[2] + _ValueCount[3] + _ValueCount[12] + jokerCount == 5)
            {
                return true;
            }
            var diff = new int[cc.Length - 1];
            for (var i = 0; i < diff.Length; i++)
            {
                diff[i] = (cc[i] / 4) - (cc[i + 1] / 4);
            }
            for (var i = 0; i < diff.Length; i++)
            {
                while (diff[i] > 1 && jokerCount > 0)
                {
                    diff[i]--;
                    jokerCount--;
                }
                if (diff[i] != 1)
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsOfKind(int n, int jokerCount)
        {
            return _ValueCount.Any(x => x == n - jokerCount);
        }

        private bool IsTwoPairs(int jokerCount)
        {
            var s = 0;
            for (var i = 0; i < 8; i++)
            {
                s += _ValueCount[i];
            }
            if (s > 1)
            {
                return false;
            }
            if (_ValueCount.Count(x => x == 2) == 2)
            {
                return true;
            }
            if (jokerCount == 1)
            {
                if (_ValueCount.Any(x => x == 2) && _ValueCount.Any(x => x == 1))
                {
                    return true;
                }
            }
            if (jokerCount > 1)
            {
                return true;
            }
            return false;
        }

        private bool IsFullHouse(int jokerCount)
        {
            switch (jokerCount)
            {
                case 0:
                    if (_ValueCount.Any(x => x == 3) && _ValueCount.Any(x => x == 2))
                    {
                        return true;
                    }
                    break;
                case 1:
                    if ((_ValueCount.Count(x => x == 2) == 2) || (_ValueCount.Any(x => x == 3) && _ValueCount.Any(x => x == 1)))
                    {
                        return true;
                    }
                    break;
                case 2:
                    if (_ValueCount.Any(x => x == 2) || _ValueCount.Any(x => x == 3))
                    {
                        return true;
                    }
                    break;
                default:
                    if (jokerCount > 3)
                    {
                        return true;
                    }
                    break;
            }

            return false;
        }

        public PokerSlotWin GetWin(bool gratis)
        {
            var jk = JokerCount(gratis);
            if (jk == 5)
            {
                return PokerSlotWin.RoyalFlush;
            }
            var straigth = IsStraigth(jk);
            var flush = IsFlush(jk);
            if (IsRoyalFlush(straigth, flush))
            {
                return PokerSlotWin.RoyalFlush;
            }
            if (IsOfKind(5, jk))
            {
                return PokerSlotWin.FiveOfAKind;
            }
            if (straigth && flush)
            {
                return PokerSlotWin.StraightFlush;
            }
            if (IsOfKind(4, jk))
            {
                return PokerSlotWin.FourOfAKind;
            }
            if (IsFullHouse(jk))
            {
                return PokerSlotWin.FullHouse;
            }
            if (flush)
            {
                return PokerSlotWin.Flush;
            }
            if (straigth)
            {
                return PokerSlotWin.Straight;
            }
            if (IsOfKind(3, jk))
            {
                return PokerSlotWin.ThreeOfAKind;
            }
            if (IsTwoPairs(jk))
            {
                return PokerSlotWin.TwoPairs;
            }

            return PokerSlotWin.NoWin;
        }

        public int CalculateLineWin(bool gratis)
        {
            return LineWinsForGames.WinForLinesPokerSlot[(int)GetWin(gratis)];
        }

        /// <summary>
        /// Daje pozicije dobitnih elemenata.
        /// </summary>
        /// <param name="lineNumber">Broj linije.</param>
        /// <param name="win">Win.</param>
        /// <param name="gratis"></param>
        /// <returns></returns>
        public byte[] GetLinesPositions(int lineNumber, PokerSlotWin win, bool gratis)
        {
            var positionsArray = new byte[5];
            for (var i = 0; i < 5; i++)
            {
                positionsArray[i] = (byte)(GlobalData.GameLineExtra[lineNumber - 1, i] * 5 + i);
            }
            if (win == PokerSlotWin.TwoPairs || win == PokerSlotWin.FourOfAKind)
            {
                var maxI = -1;
                var maxSum = -1;
                for (var i = 0; i < 5; i++)
                {
                    var hnd = (int[])Hand.Clone();
                    var cardSum = hnd.Sum();
                    var replacedCard = hnd[i];
                    hnd[i] = 53;
                    var l = new LinePokerSlot(hnd[0], hnd[1], hnd[2], hnd[3], hnd[4]);
                    if (l.GetWin(gratis) == win)
                    {
                        if (maxSum < cardSum - replacedCard)
                        {
                            maxSum = cardSum - replacedCard;
                            maxI = i;
                        }
                    }
                }
                positionsArray[maxI] = 255;
                return positionsArray;
            }
            if (win == PokerSlotWin.ThreeOfAKind) //tri daju dobitak
            {
                var maxI = -1;
                var maxJ = -1;
                var maxSum = -1;
                for (var i = 0; i < 4; i++)
                {
                    for (var j = i + 1; j < 5; j++)
                    {
                        var hnd = (int[])Hand.Clone();
                        var cardSum = hnd.Sum();
                        var replacedCard1 = hnd[i];
                        var replacedCard2 = hnd[j];
                        hnd[i] = 53;
                        hnd[j] = 54;
                        var l = new LinePokerSlot(hnd[0], hnd[1], hnd[2], hnd[3], hnd[4]);
                        if (l.GetWin(gratis) == win)
                        {
                            if (maxSum < cardSum - replacedCard1 - replacedCard2)
                            {
                                maxSum = cardSum - replacedCard1 - replacedCard2;
                                maxI = i;
                                maxJ = j;
                            }
                        }
                    }
                }
                positionsArray[maxI] = 255;
                positionsArray[maxJ] = 255;
                return positionsArray;
            }
            return positionsArray;
        }
    }
}
