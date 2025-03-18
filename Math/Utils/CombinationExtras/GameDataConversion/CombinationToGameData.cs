using CombinationExtras.ConversionData.ByteArrayConversion;
using CombinationExtras.ConversionData.V3Conversion;
using CombinationExtras.ConversionData.V3Conversion.V3ConversionTeam1;
using CombinationExtras.ConversionData.V3Conversion.V3ConversionTeam2;
using CombinationExtras.UnicornConversionData.V3Conversion;
using Papi.GameServer.Utils.Enums;
using Papi.GameServer.Utils.Logging;
using MathCombination.CombinationData;

namespace CombinationExtras.GameDataConversion
{
    public static class CombinationToGameData
    {
        public static object ToGameData(Games game, int numOfGratisGames, long newCreditMeter, bool isCurrentGameGratis,
            ICombination combination, bool json)
        {
            if (json)
            {
                return ToJson(game, numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
            }
            return ToByteArray(game, numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
        }

        public static object ToJson(Games game, int numOfGratisGames, long newCreditMeter, bool isCurrentGameGratis, ICombination combination)
        {
            switch (game)
            {
                //Combination
                case Games.TurboHot40:
                case Games.CrystalHot40:
                case Games.TurboDice40:
                case Games.CrystalHot40Gd:
                case Games.CrystalHot80:
                case Games.CrystalHot40Deluxe:
                case Games.CrystalHot40Gd2:
                case Games.WildHot40:
                case Games.FruityWin40:
                case Games.CrystalHot401X:
                case Games.CrystalJokerHot:
                case Games.CrystalHotAdmiral:
                case Games.CrystalHot40Soccer:
                case Games.WildHot40Meridian:
                case Games.CrystalHot40Pw:
                case Games.CrystalHot100:
                case Games.TurboHot80:
                case Games.TurboHot100:
                case Games.TurboPinn40:
                case Games.OlimpHot:
                case Games.KingOfMyCastle:
                case Games.GigaHot40:
                case Games.FruityWin20:
                case Games.VolcanoHot40:
                case Games.ArenaHot40:
                case Games.PskHot40:
                case Games.PariHot40:
                case Games.WildHot40Halloween:
                case Games.CrystalHot40Halloween:
                case Games.CrystalHot40Christmas:
                case Games.TurboHot40Christmas:
                case Games.WildHot40Christmas:
                case Games.PixelHot40:
                case Games.EpicFire40:
                case Games.RetroFire:
                case Games.WildHot40Dice:
                case Games.MozzartWild40:
                case Games.RizkHot40:
                case Games.CloversAndStars:
                case Games.BetOleHot40:
                case Games.NovomaticRoaringForties:
                case Games.TotoWildHot40:
                case Games.TotoFruityWin40:
                case Games.HeartsAndStars:
                    return GameTurboHot40Conversion.ToJsonObject(combination);
                case Games.ActionHot40:
                case Games.TurboFire:
                    return GameTurboHot40Conversion.ToSlotDataResV3(combination);
                case Games.FireCash40:
                case Games.Redstone40FruitFrenzy:
                    return Game40FireCashConversion.ToSlotDataResV3(combination);
                case Games.WildClover40:
                case Games.WildCloverDice40:
                case Games.RedstoneWildTrail:
                case Games.Redstone40JokersFreeGames:
                case Games.RedstoneMoonDog:
                case Games.RedstoneDivineStrike:
                    return GameWildClover40Conversion.ToSlotDataResV3(combination);
                case Games.CrystalHot40Free:
                case Games.WildHot40FreeSpins:
                case Games.AmazingJoker:
                case Games.CrystalsOfMozzart:
                case Games.SoccerHot40FreeSpins:
                    return GameWildClover40Conversion.ToJsonObject(combination, numOfGratisGames, isCurrentGameGratis);
                case Games.VikingGold:
                case Games.KingOfMyCastleDice:
                    return GameVikingGoldConversion.ToJsonObject(combination, numOfGratisGames, isCurrentGameGratis);
                case Games.PowerOfTheGreat:
                    return GameVikingGoldConversion.ToJsonObjectPowerOfTheGreat(combination, numOfGratisGames, isCurrentGameGratis);
                case Games.SpinCards:
                    return GameSpinCardsConversion.ToSlotDataResV3(combination);
                case Games.LuckyTwister:
                case Games.BuffaloFortune:
                    return GameLuckyTwisterConversion.ToSlotDataResV3(combination);
                case Games.BookOfMayanGold:
                    return GameBookOfMayanGoldConversion.ToSlotDataResV3(combination);
                case Games.FireClover5:
                case Games.FireCloverDice5:
                case Games.FireClover40:
                case Games.FireDice40:
                case Games.WildHeart5:
                case Games.CrownFire5:
                case Games.RedstoneChilliHot5:
                case Games.RedstoneJollyPresents:
                case Games.RedstoneHotRushCrownBurst:
                case Games.RedstoneLadyTripleFortune:
                case Games.FortuneClover5:
                case Games.PlayNetFortuneClover40:
                case Games.PlayNetDiamondHeart5:
                case Games.PlayNetDiamondHeart40:
                case Games.RedstoneFunkyFruits:
                    return GameBurstingHot5Conversion.ToSlotDataResV3(combination);
                case Games.WinningClover5:
                case Games.WinningClover5Extreme:
                case Games.Redstone20WildCrown:
                case Games.PlayNetMajesticCrown20:
                case Games.PlayNetMajesticCrown20Xmas:
                    return GameBurstingHot5Conversion.ToSlotDataResV3WinningClover(combination);
                case Games.MayanCoins:
                case Games.CloverCoin:
                case Games.ShiningRush:
                    return GameCloverCashConversion.ToSlotDataResV3(combination, isCurrentGameGratis);
                case Games.SuperLucky:
                case Games.RedstoneClover5LockAndCash:
                    return GameCloverCashConversion.ToSlotDataResV3SuperLucky(combination, isCurrentGameGratis);
                case Games.WildCrown10:
                case Games.WildDiamond10:
                case Games.WildDice10:
                case Games.WildPumpkin10:
                case Games.WildSanta10:
                case Games.RedstoneSlotRoyale:
                case Games.MajesticCrown10:
                case Games.RedstoneVolcanoCrown:
                    return GameGoldenCrownConversion.ToSlotDataResV3(combination);
                case Games.WildClover506:
                    return GameWildClover506Conversion.ToSlotDataResV3(combination);
                case Games.LollasWorld:
                case Games.ZabranjenoVoce:
                case Games.LollasWorldChristmas:
                case Games.KettysFashionWorld:
                case Games.MozzartsWorld:
                case Games.LollasSoccerWorld:
                    return GameLollasWorldCoversion.ToJsonObject(combination);
                case Games.PlayNetCrownStacks40:
                    return GameLollasWorldCoversion.ToSlotDataResV3(combination);
                case Games.LostBook:
                case Games.BookOfIbis:
                case Games.PlayNetBookOfAmun:
                    return GameSpellbookConversion.ToSlotDataResV3(combination, isCurrentGameGratis);
                case Games.BookOfScorpionsDouble:
                case Games.BookOfDread:
                case Games.RedstoneBookOfScorpio:
                    return GameBookOfDoubleConversion.ToSlotDataResV3(combination);
                case Games.ElGrandeToro:
                    return GameElGrandeToroConversion.ToJsonObject(combination, numOfGratisGames, isCurrentGameGratis);
                case Games.ActionHot20:
                case Games.FruitsAndStars20Deluxe:
                    return GameFruitsAndStarsConversion.ToSlotDataResV3(combination);
                case Games.BonusBells:
                    return GameBonusBellsConversion.ToSlotDataResV3(combination);
                case Games.CashBells40:
                    return GameCashBells40Conversion.ToSlotDataResV3(combination);
                case Games.KingOfThunder:
                    return GameKingOfThunderConversion.ToSlotDataResV3(combination);
                case Games.CrystalHot40Max:
                case Games.WildHot40Blow:
                case Games.WildHot40BlowDice:
                    return GameCrystalHot40MaxConversion.ToJsonObject(combination);
                case Games.PlayNetWildSpread40:
                case Games.PlayNetCrownSpread40:
                    return GameWildHotBlowConversion.ToSlotDataResV3(combination);
                case Games.HeatingFruits:
                    return GameHeatingFruitsConversion.ToJsonObject(combination);
                case Games.ExtraFlames10:
                case Games.ShiningPresents:
                    return GameExtraFlames10Conversion.ToSlotDataResV3(combination);
                case Games.WildLuckyClover:
                case Games.DeadNight:
                case Games.MrFirstCryptonium:
                case Games.LuckyMozzart:
                case Games.WildLuckyDice:
                case Games.SummerRush:
                case Games.SoccersClover:
                case Games.WildLuckyBetebet:
                    return GameWildLuckyCloverConversion.ToJsonObject(combination, numOfGratisGames, isCurrentGameGratis);
                case Games.AfricanTreasure:
                    return GameAfricanTreasureConversion.ToJsonObject(combination);
                case Games.VeryHot5Extreme:
                case Games.RedstoneChilliDouble:
                case Games.RedstoneCapsAndCrowns:
                case Games.RedstoneDoubleFireClover:
                case Games.RedstoneDiceDouble:
                case Games.RedstoneChristmasDelight:
                case Games.PlayNetFortuneCloverX2:
                case Games.PlayNetDiamondHeartX2:
                    return GameVeryHotExtremeConversion.ToSlotDataResV3(combination);
                case Games.VeryHot40Extreme:
                    return GameVeryHot40ExtremeConversion.ToSlotDataResV3(combination);
                case Games.FireCash20:
                case Games.Redstone20FruitFrenzy:
                case Games.RedstoneVolcanoHot:
                    return Game20FireCashConversion.ToSlotDataResV3(combination);
                case Games.PiratesPapi:
                    return GamePiratesPapiConversion.ToSlotDataResV3(combination);
                case Games.BlowFruits40:
                case Games.CoinSplash:
                    return GameBlowFruits40Conversion.ToSlotDataResV3(combination);
                case Games.SpecialFruits:
                    return GameSpecialFruitsConversion.ToSlotDataResV3(combination);
                case Games.WildLuckyClover2:
                    return GameWildLuckyCloverConversion.ToJsonObject2(combination, numOfGratisGames, isCurrentGameGratis);
                case Games.EpicClover40:
                case Games.EpicClover100:
                case Games.EpicDice100:
                case Games.GoldenCrownMax:
                case Games.BrilliantHeart:
                    return GameEpicClover40Conversion.ToJsonObject(combination);
                case Games.Redstone100CloverFire:
                case Games.PlayNetFortuneClover100:
                    return GameEpicClover40Conversion.ToSlotDataResV3(combination);
                case Games.BlazingHeat:
                case Games.RedstoneHotRushStarsDeluxe:
                    return GameBlazingHeatConversion.ToSlotDataResV3(combination);
                case Games.HotHotStereoWin:
                case Games.RedstoneHotRushBothWays:
                    return GameHotHotStereoWinConversion.ToSlotDataResV3(combination);
                case Games.CloverBlast5:
                case Games.Redstone5WildFire:
                    return Game5CloverBlastConversion.ToSlotDataResV3(combination);
                case Games.MayansBattle:
                case Games.FootballVictory:
                    return GameMayansBattleConversion.ToJsonObject(combination, numOfGratisGames, isCurrentGameGratis);
                case Games.RedstoneFearOfDark:
                case Games.Redstone40HotJoker:
                    return GameFearOfDarkConversion.ToSlotDataResV3(combination);
                case Games.SantasPresents:
                    return GameSantasPresentsConversion.ToSlotDataResV3(combination);
                case Games.RedstoneWildHeartBeat:
                case Games.RedstoneSpinUp:
                case Games.RedstoneOnlyAnime:
                    return GameWildHeartBeatConversion.ToSlotDataResV3(combination);
                case Games.BonusEpicCrown:
                case Games.RedstoneSpookySpins:
                case Games.BonusCrown:
                    return GameBonusEpicCrownConversion.ToSlotDataResV3(combination);
                case Games.CrownOfSecret:
                    return GameCrownOfSecretConversion.ToSlotDataResV3(combination);
                case Games.MysticJungle:
                    return GameMysticJungleConversion.ToSlotDataResV3(combination);
                case Games.VintageFruits40:
                case Games.PlayNetWildForties:
                case Games.PlayNetWildFortiesXmas:
                    return GameVintageFruits40Conversion.ToSlotDataResV3(combination);
                case Games.RedstoneHotRushFruitLines:
                    return GameHotRushFruitLinesConversion.ToSlotDataResV3(combination);
                case Games.ToxicHaze:
                    return GameToxicHazeConversion.ToSlotDataResV3(combination, numOfGratisGames, isCurrentGameGratis);
                case Games.MegaHot10:
                    return GameMegaHot10Conversion.ToSlotDataResV3(combination);
                case Games.WildSunburst:
                    return GameWildSunburstConversion.ToSlotDataResV3(combination, isCurrentGameGratis);
                case Games.WildHeat40:
                    return GameWildHeat40Conversion.ToSlotDataResV3(combination);
                case Games.AgeOfRome:
                    return GameAgeOfRomeConversion.ToSlotDataResV3(combination, isCurrentGameGratis);
                case Games.PlayNetDashingHot5:
                    return GameTwinklingHotConversion.ToSlotDataResV3(combination);
                case Games.FruityForce40:
                    return GameFruityForce40Conversion.ToSlotDataResV3(combination);
                case Games.AquaFlame:
                    return GameAquaFlameConversion.ToSlotDataResV3(combination);
                //Combination3
                case Games.JokerQueen:
                    return GameJokerQueenConversion.ToSlotDataResV3(combination);
                case Games.SimplyRunner:
                    return GameSimplyRunnerConversion.ToSlotDataResV3(combination);
                case Games.HeatClassic5:
                    return GameHeatClassicConversion.ToSlotDataResV3(combination);
                case Games.Wild27:
                case Games.RedstoneApollo27Classic:
                case Games.PlayNet27WildStacks:
                case Games.PlayNet27WildStacksXmas:
                case Games.Redstone27DoubleFruit:
                case Games.BetAndreasWild:
                case Games.TopHot5:
                case Games.Wild5:
                    return GameWild27Conversion.ToSlotDataResV3(combination);
                case Games.JokerTripleDouble:
                case Games.ChilliRespin:
                    return GameJokerTripleDoubleConversion.ToSlotDataResV3(combination);
                case Games.HeatDouble:
                    return GameHeatDoubleConversion.ToSlotDataResV3(combination);
                case Games.Wild81:
                case Games.Redstone81VegasCrown:
                case Games.Redstone81DoubleMagic:
                    return GameWild81Conversion.ToSlotDataResV3(combination);
                //CombinationGame
                case Games.CrystalsOfMagic:
                    return GameCrystalsOfMagicConversion.ToJsonObject(combination, numOfGratisGames, isCurrentGameGratis);
                case Games.TemplarsQuest:
                    return GameTemplarsQuestConversion.ToJsonObject(combination, numOfGratisGames, isCurrentGameGratis);
                case Games.GoldenClover:
                    return GameGoldenCloverConversion.ToSlotDataResV3(combination, numOfGratisGames, isCurrentGameGratis);
                //CombinationUnicorn
                case Games.UnicornVegasDice:
                case Games.UnicornFruitIsland:
                case Games.UnicornDaVincisFruits:
                case Games.UnicornDaVincisDice:
                case Games.UnicornFrootMachine:
                case Games.UnicornFruitIslandChristmas:
                case Games.UnicornDiceMachine:
                case Games.UnicornFruitIslandGems:
                case Games.UnicornSupremeSevens:
                    return GameVegasDiceConversion.ToSlotDataResV3(combination);
                case Games.UnicornReelDice:
                case Games.UnicornRainbowSevens:
                case Games.UnicornThunderFruits:
                    return GameReelDiceConversion.ToSlotDataResV3(combination);
                case Games.UnicornWildParadise:
                case Games.UnicornTheCrownFruit:
                case Games.UnicornFruitMagic:
                case Games.UnicornDynamiteRun:
                case Games.UnicornDynamiteRunPlatinum:
                    return GameWildParadiceConversion.ToSlotDataResV3(combination);
                case Games.UnicornHavanaDice:
                case Games.UnicornCasinoFruits:
                case Games.UnicornReelyWildReels:
                    return GameHavanaDiceConversion.ToSlotDataResV3(combination);
                case Games.UnicornTwentyFruits:
                case Games.UnicornTwentyDice:
                case Games.UnicornMoneyStandard:
                case Games.UnicornMoneyStandardDice:
                    return GameTwentyFruitsConversion.ToSlotDataResV3(combination);
                case Games.UnicornIslandRespins:
                    return GameIslandRespinsConversion.ToSlotDataResV3_2(combination, isCurrentGameGratis);
                case Games.UnicornDiceRespins:
                    return GameIslandRespinsConversion.ToSlotDataResV3(combination);
                case Games.UnicornGreatWhale:
                    return GameGreatWhaleConversion.ToSlotDataResV3(combination);
                case Games.Unicorn20MegaFlames:
                case Games.Unicorn20DiceFlames:
                    return Game20MegaFlamesConversion.ToSlotDataResV3(combination);
                case Games.UnicornMiniMegaCash:
                case Games.UnicornDiceMegaCash:
                    return GameMiniMegaCashConversion.ToSlotDataResV3(combination);
                case Games.UnicornSurfinHeat:
                    return GameSurfinHeatConversion.ToSlotDataResV3(combination, isCurrentGameGratis);
                case Games.Unicorn40MegaFlames:
                case Games.Unicorn40DiceFlames:
                    return Game40MegaFlamesConversion.ToSlotDataResV3(combination);
                case Games.UnicornWinterFruits:
                case Games.Unicorn40HotStrike:
                case Games.Unicorn40HotStrikeDice:
                    return GameWinterFruitsConversion.ToSlotDataResV3(combination);
                case Games.UnicornFastFruits:
                    return GameFastFruitsConversion.ToSlotDataResV3(combination);
                case Games.UnicornMoneyStandardWild:
                    return GameMoneyStandardWildConversion.ToSlotDataResV3(combination);
                case Games.Unicorn20HotStrike:
                case Games.UnicornDoubleWildDice:
                case Games.Unicorn20HotStrikeDice:
                case Games.Unicorn20HotStrikeJackpot:
                case Games.Unicorn20HotStrikeDiceJackpot:
                    return Game20HotStrikeConversion.ToSlotDataResV3(combination);
                case Games.UnicornCoyoteSevens:
                case Games.UnicornCoyoteDice:
                case Games.UnicornSavanaFruits:
                    return GameCoyoteSevensConversion.ToSlotDataResV3(combination);
                case Games.UnicornFruitWildLines:
                case Games.UnicornDiceWildLines:
                    return GameFruitWildLinesConversion.ToSlotDataResV3(combination);
                case Games.Unicorn40FruitReels:
                    return Game40FruitReelsConversion.ToSlotDataResV3(combination);
                case Games.Unicorn5HotStrike:
                case Games.Unicorn5HotStrikeDice:
                    return Game5HotStrikeConversion.ToSlotDataResV3(combination);
                case Games.UnicornBikiniFruits:
                case Games.UnicornQueenOfPyramids:
                case Games.UnicornBikiniDice:
                    return GameBikiniFruitsConversion.ToSlotDataResV3(combination);
                case Games.UnicornFireStars:
                case Games.UnicornStarDiceChristmas:
                    return GameFireStarsConversion.ToSlotDataResV3(combination);
                case Games.UnicornSimplySevens:
                case Games.UnicornSimplySevensDice:
                case Games.UnicornLavaDiamonds:
                case Games.UnicornLavaDice:
                case Games.UnicornLavaDiamondsChristmas:
                case Games.UnicornSimplySevensDiceEaster:
                case Games.UnicornBunnyDice:
                    return GameSimplySevensConversion.ToSlotDataResV3(combination);
                case Games.Unicorn10JingleFruits:
                    return Game10JingleFruitsConversion.ToSlotDataResV3(combination);
                case Games.UnicornGoldLine:
                case Games.UnicornGoldLineDice:
                    return GameGoldLineConversion.ToSlotDataResV3(combination);
                case Games.UnicornChristmasPresents:
                case Games.UnicornMisterLuck:
                    return GameChristmasPresentsConversion.ToSlotDataResV3(combination);
                case Games.UnicornBigSpinSevens:
                    return GameBigSpinSevensConversion.ToSlotDataResV3(combination);
                case Games.UnicornBuffaloSevens:
                case Games.UnicornBuffaloDice:
                case Games.UnicornTripleStackedDiamonds:
                    return GameBuffaloSevensConversion.ToSlotDataResV3(combination);
                case Games.UnicornFrootClassic:
                    return GameFrootClassicConversion.ToSlotDataResV3(combination);
                case Games.UnicornStickyHot:
                    return GameStickyHotConversion.ToSlotDataResV3(combination);
                case Games.UnicornPumpkinHorror:
                case Games.UnicornWildHoppers:
                    return GamePumpkinHorrorConversion.ToSlotDataResV3(combination);
                case Games.UnicornHitLine:
                case Games.UnicornHitLineDice:
                    return GameHitLineConversion.ToSlotDataResV3(combination);
                case Games.Unicorn20SuperFlames:
                    return Game20SuperFlamesConversion.ToSlotDataResV3(combination);
                case Games.RedstoneJuicyHeat20:
                    return GameMegaHot20Conversion.ToSlotDataResV3(combination);
                case Games.UnicornEpicMegaCash:
                    return GameEpicMegaCashConversion.ToSlotDataResV3(combination);
                case Games.UnicornBigHitSevens:
                    return GameBigHitSevensConversion.ToSlotDataResV3(combination);
                default:
                    Logger.LogError("Error ToJSON doesnt exist for: " + game);
                    return null;
            }
        }

        public static byte[] ToByteArray(Games game, int numOfGratisGames, long newCreditMeter, bool isCurrentGameGratis, ICombination combination)
        {
            switch (game)
            {
                //Combination
                case Games.Postman:
                case Games.AlohaCharm:
                case Games.DolphinsShine:
                case Games.SevenClassicHot:
                case Games.WildCraps:
                case Games.NovomaticLuckyLadysCharmDeluxe:
                    return GamePostmanConversion.ToByteArray(numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
                case Games.DeepJungle:
                case Games.BurstingHot5:
                case Games.BurstingHot40:
                case Games.GoldenCrown:
                case Games.VeryHot5:
                case Games.VeryHot40:
                case Games.VeryHot40Dice:
                case Games.BurstingHot40Mozzart:
                case Games.GoldenCrownMaxbet:
                case Games.BurstingHot5Admiral:
                case Games.GoldenCrown20BalkanBet:
                case Games.BigBuddha:
                case Games.VeryHot20:
                case Games.WildJokerHot:
                case Games.AdmiralHot5:
                case Games.GermaniaHot40:
                case Games.WinXtip:
                case Games.SuperCasinoCrown:
                case Games.TopHot20:
                case Games.VeryHot40Respin:
                case Games.GoldenCrownChristmas:
                case Games.VeryHot5Christmas:
                case Games.VeryHot40Christmas:
                case Games.GoldenExplosion:
                case Games.GoldenCrown40:
                case Games.EpicCrown10:
                case Games.Money5:
                case Games.EpicCrown5:
                case Games.HotLine1X:
                case Games.FashionNight:
                case Games.GoldenVegas:
                case Games.GrandpashabetCrown:
                case Games.MozzartHot5:
                case Games.MozzartHot40:
                case Games.CrownOfMozzart:
                case Games.FortuneParrot:
                case Games.QuickWinCrown10:
                case Games.BetwoonHot5:
                    return GameJungleConversion.ToByteArray(numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
                case Games.Wizard:
                case Games.KatanasOfTime:
                case Games.EggspandingRush:
                    return GameWizardConversion.ToByteArray(numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
                case Games.FruitsAndStars:
                case Games.FruitsAndStars40:
                case Games.ForestFruits:
                case Games.CubesAndStars:
                case Games.FruityHot:
                case Games.StarsOfOktagon:
                case Games.FruityHot5:
                case Games.FruitsAndStarsChristmas:
                case Games.FruitsAndStars40Christmas:
                case Games.MozzartFruits:
                    return GameFruitsConversion.ToByteArray(numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
                case Games.HotParty:
                case Games.NeonHot5:
                case Games.NeonDice5:
                case Games.FluoDice5:
                case Games.FluoHot5:
                case Games.MegaCubesDeluxe:
                case Games.FruityFace:
                    return GameHotPartyConversion.ToByteArray(numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
                case Games.CaptainShark:
                    return GameCaptainSharkConversion.ToByteArray(numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
                case Games.MagicTarget:
                    return GameMagicTargetConversion.ToByteArray(numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
                case Games.MagicOfTheRing:
                    return GameMagicOfTheRingConversion.ToByteArray(numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
                case Games.Spellbook:
                case Games.BookOfSpells:
                case Games.BookOfBruno:
                case Games.BookOfSpellsV2:
                case Games.DiceOfSpells:
                case Games.BookOfSpellsDeluxe:
                case Games.BookOfLuxorDouble:
                case Games.BookOfSpells2:
                case Games.BookOfMozzart:
                case Games.NovomaticLordOfTheOcean:
                case Games.NovomaticBookOfRaDeluxe:
                    return GameMagicOfTheRingConversion.ToByteArray(numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
                case Games.WildWest:
                case Games.Monsters:
                    return GameWildWestConversion.ToByteArray(numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
                case Games.Pyramid:
                case Games.WildKingdom:
                case Games.EyeOfTut:
                    return GamePyramidConversion.ToByteArray(numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
                case Games.Alpinist:
                    return GameAlpinistConversion.ToByteArray(numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
                case Games.Diamonds:
                case Games.JewelsBeat:
                    return GameDiamondsConversion.ToByteArray(numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
                case Games.HotStars:
                case Games.StarGems:
                case Games.SpaceGuardians:
                case Games.Starlight:
                case Games.StarRunner:
                case Games.CrystalWin:
                case Games.CrystalJewels:
                case Games.WinningStars:
                case Games.HotStarsChristmas:
                case Games.TurboStars10:
                case Games.TurboStars40:
                case Games.TurboStars20:
                case Games.CrownsAndStars:
                    return GameHotStarsConversion.ToByteArray(numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
                case Games.JuicyHot:
                case Games.TwinklingHot5:
                case Games.TwinklingHot40:
                case Games.JazzyFruits:
                case Games.FruityJokerHot:
                case Games.TwinklingHot80:
                case Games.LuckyBrilliants:
                case Games.TwinklingHot40Christmas:
                case Games.NovomaticSizzlingHotDeluxe:
                    return GameHotPartyConversion.ToByteArray(numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
                case Games.MegaHot:
                case Games.MegaHot20:
                    return GameMegaHotConversion.ToByteArray(numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
                case Games.TropicalHot:
                    return GameHotPartyConversion.ToByteArray(numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
                case Games.Pirates:
                    return GamePyramidConversion.ToByteArray(numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
                //Combination3
                case Games.VegasHot:
                case Games.FenixPlay:
                case Games.Hot777:
                case Games.MagicFruits:
                case Games.BurningIce:
                case Games.TripleHot:
                case Games.BurningIceDeluxe:
                case Games.TripleDice:
                case Games.FlashingDice:
                case Games.Retro7Hot:
                case Games.BurningIceGd:
                case Games.MysteryJokerHot:
                case Games.HeatingIce:
                case Games.HeatingIceDeluxe:
                case Games.HeatingDice:
                case Games.ClassicLuckySpin:
                case Games.Retro7HotChristmas:
                case Games.TripleFieldsOfLuck:
                case Games.MozzartHeat:
                    return GameVegasHotConversion.ToByteArray(numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);
                //Combination4
                case Games.RollingDices81:
                    return GameCrissCrossConversion.ToByteArray(numOfGratisGames, newCreditMeter, isCurrentGameGratis, combination);

                default:
                    Logger.LogError("Error ToByteArray doesnt exist for: " + game);
                    return null;
            }
        }
    }
}

