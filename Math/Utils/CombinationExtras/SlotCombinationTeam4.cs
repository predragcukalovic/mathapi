using Papi.GameServer.Utils.Enums;
using MathCombination.CombinationData;
using MathCombination.ReelsData;

namespace CombinationExtras
{
    public static partial class SlotCombination
    {
        #region Private methods

        #endregion

        #region Public methods

        public static ICombination GetCombinationTeam4(Games game, int bet, int numberOfLines, int gratisGamesLeft, ref byte[] additionalArray, byte additionalInformation = 0, int selectedField = 0, object gameDataObj = null)
        {
            switch (game)
            {
                case Games.HeartsAndStars:
                    ValidateLines(game, numberOfLines, 40);
                    return GetCombinationCloversAndStars(bet, numberOfLines);
            }

            var reels = ReadReelsFromSlotFile(game, gratisGamesLeft > 0, additionalInformation);
            var matrixArray = ReelsReader.ReadMatrixArrayFromReels(reels);

            switch (game)
            {
                case Games.BetOleHot40:
                case Games.TotoWildHot40:
                case Games.TotoFruityWin40:
                    ValidateLines(game, numberOfLines, 40);
                    return GetCombinationTurboHot40(matrixArray, bet, 40);
                case Games.LollasSoccerWorld:
                    ValidateLines(game, numberOfLines, 40);
                    return GetCombinationLilaWild(matrixArray, bet, 40);
                case Games.SoccersClover:
                case Games.WildLuckyBetebet:
                    ValidateLines(game, numberOfLines, 40);
                    return GetCombinationWildLuckyClover(matrixArray, numberOfLines, bet, gratisGamesLeft > 0, additionalInformation);
                case Games.SoccerHot40FreeSpins:
                    ValidateLines(game, numberOfLines, 40);
                    return GetCombinationCrystalHot40Free(matrixArray, bet, numberOfLines, gratisGamesLeft > 0);
                case Games.QuickWinCrown10:
                    ValidateLines(game, numberOfLines, 10);
                    return GetCombinationGoldenCrown(matrixArray, numberOfLines, bet);
                case Games.BetAndreasWild:
                    return GetCombinationWild27(matrixArray, bet);
                case Games.BetwoonHot5:
                    ValidateLines(game, numberOfLines, 5);
                    return GetCombinationBurstingHot5(matrixArray, numberOfLines, bet);
                default:
                    return null;
            }
        }

        #endregion
    }
}
