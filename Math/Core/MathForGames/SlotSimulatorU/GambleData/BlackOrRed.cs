using RNGUtils.RandomData;

namespace MathForGames.GambleData
{
    public static class BlackOrRed
    {
        public struct BlackOrRedData
        {
            public byte CardValue;
            public byte CardSign;
            public long CurrentWin;
            public long NextWin;
        }

        #region Private methods

        /// <summary>
        /// Daje strukturu za igru BlackOrRed.
        /// </summary>
        /// <param name="lastWin">Poslednji dobitak</param>
        /// <param name="alwaysWin"> </param>
        /// <returns>Vraća niz od 18 bajtova</returns>
        private static BlackOrRedData GetBlackOrRedDataStructure(long lastWin, bool alwaysWin)
        {
            var toReturn = new BlackOrRedData { CardValue = 12, CurrentWin = lastWin };
            var sign = (byte)(SoftwareRng.Next(2) * 2);

            if (SoftwareRng.Next(2) == 0 && !alwaysWin)
            {
                sign++;
                toReturn.NextWin = 0;
            }
            else
            {
                toReturn.NextWin = lastWin * 2;
            }

            toReturn.CardSign = sign;

            return toReturn;
        }

        /// <summary>
        /// Daje strukturu za igru BlackOrRed. Struktura je niz od 18 bajtova.
        /// </summary>
        /// <param name="lastWin">Poslednji dobitak</param>
        /// <param name="alwaysWin"> </param>
        /// <returns>Vraća niz od 18 bajtova</returns>
        private static byte[] GetBlackOrRed(long lastWin, bool alwaysWin)
        {
            var arrayToReturn = new byte[18];
            long possibleWin;
            var sign = (byte)(0 + SoftwareRng.Next(2) * 2);

            if (SoftwareRng.Next(2) == 0 && !alwaysWin)
            {
                sign++;
                possibleWin = 0;
            }
            else
            {
                possibleWin = lastWin * 2;
            }

            arrayToReturn[0] = 12;
            arrayToReturn[1] = sign;

            for (int i = 2; i < 10; i++)
            {
                arrayToReturn[i] = 255;
            }

            for (int i = 3; i >= 0; i--)
            {
                arrayToReturn[13 - i] = (byte)(lastWin >> (8 * i));
                arrayToReturn[17 - i] = (byte)(possibleWin >> (8 * i));
            }

            return arrayToReturn;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Daje strukturu za igru BlackOrRed. Struktura je niz od 18 bajtova.
        /// </summary>
        /// <param name="lastWin">Poslednji dobitak</param>
        /// <param name="alwaysWin">Da li uvek da daje dobitak (za testiranje)</param>
        /// <param name="returnJson">Da li vraća JSON ili niz</param>
        /// <returns>Vraća ili niz od 18 bajtova ili strukturu</returns>
        public static object GetBlackOrRedData(long lastWin, bool alwaysWin, bool returnJson)
        {
            if (returnJson)
            {
                return GetBlackOrRedDataStructure(lastWin, alwaysWin);
            }

            return GetBlackOrRed(lastWin, alwaysWin);
        }

        #endregion
    }
}
