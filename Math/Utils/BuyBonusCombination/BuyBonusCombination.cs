using BuyBonusAgeOfRome;
using BuyBonusBonusBells;
using BuyBonusBonusEpicCrown;
using BuyBonusBookOfLuxorDouble;
using BuyBonusBookOfSpells;
using BuyBonusCrownOfSecret;
using BuyBonusCrystalHot40Free;
using BuyBonusElGrandeToro;
using BuyBonusFruityForce40;
using BuyBonusKingOfThunder;
using BuyBonusWildHot40FreeSpins;
using BuyBonusWildLuckyClover;
using BuyWildEpicClover40;
using BuyWildGoldenCrown;
using BuyWildGoldenCrownMax;
using BuyWildVeryHot5;
using Papi.GameServer.Utils.Enums;
using MathCombination.CombinationData;
using System;

namespace BuyBonusCombination
{
    public class BuyBonusCombination
    {
        #region Public methods

        /// <summary>
        /// Daje kombinaciju za slot fajl.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="bet"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="gratisGamesLeft"></param>
        /// <param name="buyBonusType"></param>
        /// <param name="additionalInformation"></param>
        /// <param name="additionalArray"></param>
        /// <param name="selectedField"></param>
        /// <param name="gameDataObj"></param>
        /// <returns></returns>
        public static ICombination GetBuyBonusCombination(Games game, int bet, int numberOfLines, int gratisGamesLeft, int buyBonusType, ref byte[] additionalArray, long betModifier, byte additionalInformation = 0, int selectedField = 0, object gameDataObj = null)
        {
            switch (game)
            {
                case Games.Spellbook:
                case Games.BookOfSpells:
                case Games.BookOfSpells2:
                    return BuyBookOfSpells.GetCombinationBookOfSpells(game.ToString(), buyBonusType, numberOfLines, bet, gratisGamesLeft > 0, additionalInformation);
                case Games.ElGrandeToro:
                    return BuyElGrandeToro.GetCombinationElGrandeToro(game.ToString(), buyBonusType, numberOfLines, bet, gratisGamesLeft > 0, ref additionalArray);
                case Games.KingOfThunder:
                    return BuyKingOfThunder.GetCombinationKingOfThunder(game.ToString(), buyBonusType, numberOfLines, bet, gratisGamesLeft);
                case Games.BookOfLuxorDouble:
                    return BuyBookOfLuxorDouble.GetCombinationBookOfLuxor(game.ToString(), buyBonusType, numberOfLines, bet, gratisGamesLeft > 0, additionalInformation);
                case Games.BonusBells:
                    return BuyBonusBells.GetCombinationBonusBells(game.ToString(), buyBonusType, numberOfLines, bet, gratisGamesLeft > 0);
                case Games.WildLuckyClover:
                    return BuyWildLuckyClover.GetCombinationWildLuckyClover(game.ToString(), buyBonusType, numberOfLines, bet, gratisGamesLeft > 0, additionalInformation);
                case Games.WildLuckyClover2:
                    return BuyWildLuckyClover.GetCombinationWildLuckyClover2(game.ToString(), buyBonusType, numberOfLines, bet, gratisGamesLeft > 0, additionalInformation);
                case Games.CrystalHot40Free:
                    return BuyCrystalHot40Free.GetCombinationCrystalHot40Free(game.ToString(), buyBonusType, numberOfLines, bet, gratisGamesLeft > 0);
                case Games.WildHot40FreeSpins:
                    return BuyWildHot40FreeSpins.GetCombinationWildHot40FreeSpins(game.ToString(), buyBonusType, numberOfLines, bet, gratisGamesLeft > 0);
                case Games.BonusEpicCrown:
                    return BuyBonusEpicCrown.GetCombinationBonusEpicCrown(game.ToString(), buyBonusType, numberOfLines, bet, gratisGamesLeft > 0, ref additionalArray);
                case Games.CrownOfSecret:
                    return BuyCrownOfSecret.GetCombinationCrownOfSecret(game.ToString(), buyBonusType, numberOfLines, bet, gratisGamesLeft > 0, ref additionalArray);
                case Games.AgeOfRome:
                    return BuyAgeOfRome.GetCombinationAgeOfRome(game.ToString(), buyBonusType, numberOfLines, bet, gratisGamesLeft > 0, ref additionalArray);
                case Games.FruityForce40:
                    return BuyFruityForce40.GetCombinationFruityForce(game.ToString(), buyBonusType, bet, 40, Convert.ToInt32(gameDataObj));
                case Games.BrilliantHeart:
                    return BuyGoldenCrownMax.GetCombinationGoldenCrownMax(game.ToString(), buyBonusType, numberOfLines, bet);
            }
            return null;
        }

        #endregion
    }
}
