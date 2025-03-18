using RNGUtils.RandomData;
using System.Linq;

namespace MathForGames.GambleData
{
    public static class DoubleUp
    {
        #region Private methods

        /// <summary>
        /// Promeša pomoćni niz koji predstavlja špil karata
        /// </summary>
        /// <param name="deck">Niz od 52 elementa koji predstavlja špil</param>
        private static void CreateAndMixDeck(ref int[] deck)
        {
            for (int i = 0; i < 52; i++)
            {
                deck[i] = i;
            }
            for (int i = 0; i < 6; i++)
            {
                var rnd = (int)SoftwareRng.Next(52 - i);
                int tmp = deck[51 - i];
                deck[51 - i] = deck[rnd];
                deck[rnd] = tmp;
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Daje strukturu za igru DoubleUp. Struktura je niz od 18 bajtova.
        /// </summary>
        /// <param name="lastWin">Poslednji dobitak</param>
        /// <param name="alwaysWin">Da li treba uvek da daje dobitnu</param>
        /// <returns>Vraća niz od 18 bajtova</returns>
        public static byte[] GetDoubleUp(int lastWin, bool alwaysWin)
        {
            var arrayToReturn = new byte[18];
            var deck = new int[52];
            CreateAndMixDeck(ref deck);
            var fiveCards = deck.Skip(47).Take(5).ToArray();
            fiveCards = fiveCards.OrderByDescending(c => c).ToArray();
            int possibleWin, gamer, cpu;

            if (SoftwareRng.Next(2) != 0 && !alwaysWin)
            {
                gamer = (int)SoftwareRng.Next(2, 5);
                cpu = (int)SoftwareRng.Next(1, gamer);
                possibleWin = 0;
            }
            else
            {
                gamer = (int)SoftwareRng.Next(0, 4);
                cpu = (int)SoftwareRng.Next(gamer + 1, 5);
                possibleWin = lastWin * 2;
            }
            arrayToReturn[0] = (byte)(fiveCards[cpu] / 4);
            arrayToReturn[2] = (byte)(fiveCards[gamer] / 4);
            arrayToReturn[1] = (byte)(fiveCards[cpu] % 4);
            arrayToReturn[3] = (byte)(fiveCards[gamer] % 4);

            if (arrayToReturn[0] == arrayToReturn[2])
            {
                possibleWin = lastWin;
            }

            var j = 4;
            for (var i = 0; i < 5; i++)
            {
                if (deck[51 - i] != fiveCards[gamer] && deck[51 - i] != fiveCards[cpu])
                {
                    arrayToReturn[j++] = (byte)(deck[51 - i] / 4);
                    arrayToReturn[j++] = (byte)(deck[51 - i] % 4);
                }
            }

            for (var i = 3; i >= 0; i--)
            {
                arrayToReturn[13 - i] = (byte)(lastWin >> (8 * i));
                arrayToReturn[17 - i] = (byte)(possibleWin >> (8 * i));
            }

            return arrayToReturn;
        }

        #endregion
    }
}
