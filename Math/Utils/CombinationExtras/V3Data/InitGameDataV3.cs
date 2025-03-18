using CombinationExtras.ConversionData.V3Conversion;
using CombinationExtras.ConversionData.V3Conversion.V3ConversionTeam1;
using CombinationExtras.ConversionData.V3Conversion.V3ConversionTeam2;
using CombinationExtras.ConversionData.V3Conversion.V3ConversionTeam3;
using Papi.GameServer.Utils.Enums;
using Game20FireCash;
using Game40FireCash;
using Game5CloverBlast;
using GameAgeOfRome;
using GameAquaFlame;
using GameBlazingHeat;
using GameBlowFruits40;
using GameBonusEpicCrown;
using GameBookOfDouble;
using GameCashBells40;
using GameCrownOfSecret;
using GameElGrandeToro;
using GameExtraFlames10;
using GameFearOfDark;
using GameFruityForce;
using GameHeatClassic5;
using GameHeatDouble;
using GameHotHotStereoWin;
using GameHotRushFruitLines;
using GameJokerTripleDouble;
using GameKingOfThunder;
using GameLollasWorld;
using GameMegaHot;
using GameMysticJungle;
using GamePiratesPapi;
using GameSantasPresents;
using GameSimplyRunner;
using GameSpecialFruits;
using GameSuperLucky;
using GameTopHot5;
using GameToxicHaze;
using GameVeryHot40Extreme;
using GameVeryHot5Extreme;
using GameVintageFruits40;
using GameWild27;
using GameWild5;
using GameWild81;
using GameWildClover506;
using GameWildHeartBeat;
using GameWildHeat40;
using GameWildSunburst;
using GameWinningClover5Extreme;
using MathBaseProject.StructuresV3;
using MathForGames.GameBookOfMayanGold;
using MathForGames.GameBurstingHot5;
using MathForGames.GameCloverCash;
using MathForGames.GameGoldenClover;
using MathForGames.GameGoldenCrown;
using MathForGames.GameLuckyTwister;
using MathForGames.GamePokerSlot;
using MathForGames.GameWildClover40;
using System;
using MatrixBonusBells = GameBonusBells.MatrixBonusBells;

namespace CombinationExtras.V3Data
{
    public static class InitGameDataV3
    {
        public static HelpConfigV3<object> GetGameHelpConfigV3(Games game)
        {
            switch (game)
            {
                case Games.LuckyTwister:
                case Games.BuffaloFortune:
                    return MatrixLuckyTwister.GetHelpConfigV3();
                case Games.SpinCards:
                    return MatrixPokerSlot.GetHelpConfigV3();
                case Games.BookOfMayanGold:
                    return MatrixBookOfMayanGold.GetHelpConfigV3();
                case Games.GoldenClover:
                    return GameGoldenCloverConversion.GetHelpConfigV3();
                case Games.BurstingHot5:
                case Games.FireClover5:
                case Games.FireCloverDice5:
                case Games.VeryHot5:
                case Games.WildHeart5:
                case Games.AdmiralHot5:
                case Games.VeryHot5Christmas:
                case Games.CrownFire5:
                case Games.Money5:
                case Games.RedstoneChilliHot5:
                case Games.RedstoneJollyPresents:
                case Games.RedstoneHotRushCrownBurst:
                case Games.RedstoneLadyTripleFortune:
                case Games.FortuneClover5:
                case Games.PlayNetDiamondHeart5:
                case Games.RedstoneFunkyFruits:
                case Games.BetwoonHot5:
                    return MatrixBurstingHot5.GetHelpConfigV3();
                case Games.WinningClover5:
                    var help = MatrixBurstingHot5.GetHelpConfigV3();
                    help.rtp = (decimal?)96.447;
                    return help;
                case Games.WinningClover5Extreme:
                    return MatrixWinningClover5Extreme.GetHelpConfigV3();
                case Games.FireClover40:
                case Games.FireDice40:
                case Games.PlayNetFortuneClover40:
                case Games.PlayNetDiamondHeart40:
                    return MatrixBurstingHot5.GetHelpConfigV3(40);
                case Games.WildClover40:
                case Games.WildCloverDice40:
                case Games.RedstoneWildTrail:
                case Games.Redstone40JokersFreeGames:
                case Games.RedstoneMoonDog:
                case Games.RedstoneDivineStrike:
                    return MatrixWildClover40.GetHelpConfigV3();
                case Games.MayanCoins:
                case Games.CloverCoin:
                case Games.ShiningRush:
                    var helpMc = MatrixCloverCash.GetHelpConfigV3();
                    helpMc.rtp = (decimal?)94.89;
                    return helpMc;
                case Games.SuperLucky:
                case Games.RedstoneClover5LockAndCash:
                    return MatrixSuperLucky.GetHelpConfigV3();
                case Games.WildCrown10:
                case Games.WildDiamond10:
                case Games.WildDice10:
                case Games.WildPumpkin10:
                case Games.WildSanta10:
                case Games.RedstoneSlotRoyale:
                case Games.MajesticCrown10:
                case Games.RedstoneVolcanoCrown:
                    var helpWc = MatrixGoldenCrown.GetHelpConfigV3();
                    helpWc.rtp = (decimal?)95.96;
                    return helpWc;
                case Games.WildClover506:
                    return Matrix40WildClover6.GetHelpConfigV3();
                case Games.LollasWorld:
                case Games.ZabranjenoVoce:
                case Games.LollasWorldChristmas:
                case Games.KettysFashionWorld:
                case Games.MozzartsWorld:
                case Games.LollasSoccerWorld:
                case Games.PlayNetCrownStacks40:
                    return MatrixLilaWild.GetHelpConfigV3();
                case Games.LostBook:
                case Games.BookOfIbis:
                case Games.PlayNetBookOfAmun:
                    return GameSpellbookConversion.GetHelpConfigV3();
                case Games.BookOfScorpionsDouble:
                case Games.BookOfDread:
                case Games.RedstoneBookOfScorpio:
                    return MatrixBookOfDouble.GetHelpConfigV3();
                case Games.ElGrandeToro:
                    return MatrixElGrandeToro.GetHelpConfigV3();
                case Games.ActionHot40:
                case Games.TurboFire:
                case Games.CrystalHot40:
                    return GameTurboHot40Conversion.GetHelpConfigV3();
                case Games.FireCash40:
                case Games.Redstone40FruitFrenzy:
                    return Matrix40FireCash.GetHelpConfigV3();
                case Games.ActionHot20:
                    return GameFruitsAndStarsConversion.GetHelpConfigV3();
                case Games.FruitsAndStars20Deluxe:
                    var helpFsd = GameFruitsAndStarsConversion.GetHelpConfigV3();
                    helpFsd.rtp = (decimal?)96.5;
                    return helpFsd;
                case Games.JokerQueen:
                    return GameJokerQueenConversion.GetHelpConfigV3();
                case Games.BonusBells:
                    return MatrixBonusBells.GetHelpConfigV3();
                case Games.CashBells40:
                    return MatrixCashBells40.GetHelpConfigV3();
                case Games.SimplyRunner:
                    return MatrixSimplyRunner.GetHelpConfigV3();
                case Games.KingOfThunder:
                    return MatrixKingOfThunder.GetHelpConfigV3();
                case Games.ExtraFlames10:
                case Games.ShiningPresents:
                    return MatrixExtraFlames10.GetHelpConfigV3();
                case Games.HeatClassic5:
                    return MatrixHeatClassic5.GetHelpConfigV3();
                case Games.VeryHot5Extreme:
                case Games.RedstoneChilliDouble:
                case Games.RedstoneCapsAndCrowns:
                case Games.RedstoneDoubleFireClover:
                case Games.RedstoneDiceDouble:
                case Games.RedstoneChristmasDelight:
                case Games.PlayNetFortuneCloverX2:
                case Games.PlayNetDiamondHeartX2:
                    var helpVhe = MatrixVeryHot5Extreme.GetHelpConfigV3();
                    helpVhe.rtp = (decimal?)96.45;
                    return helpVhe;
                case Games.VeryHot40Extreme:
                    return MatrixVeryHot40Extreme.GetHelpConfigV3();
                case Games.FireCash20:
                case Games.Redstone20FruitFrenzy:
                case Games.RedstoneVolcanoHot:
                    return Matrix20FireCash.GetHelpConfigV3();
                case Games.Wild27:
                case Games.RedstoneApollo27Classic:
                case Games.PlayNet27WildStacks:
                case Games.PlayNet27WildStacksXmas:
                case Games.Redstone27DoubleFruit:
                case Games.BetAndreasWild:
                    return MatrixWild27.GetHelpConfigV3();
                case Games.Wild5:
                    return MatrixWild5.GetHelpConfigV3();
                case Games.PiratesPapi:
                    return MatrixPiratesPapi.GetHelpConfigV3();
                case Games.BlowFruits40:
                case Games.CoinSplash:
                    return MatrixBlowFruits40.GetHelpConfigV3();
                case Games.SpecialFruits:
                    return MatrixSpecialFruits.GetHelpConfigV3();
                case Games.JokerTripleDouble:
                case Games.ChilliRespin:
                    return MatrixJokerTripleDouble.GetHelpConfigV3();
                case Games.HeatDouble:
                    return MatrixHeatDouble.GetHelpConfigV3();
                case Games.BlazingHeat:
                case Games.RedstoneHotRushStarsDeluxe:
                    return MatrixBlazingHeat.GetHelpConfigV3();
                case Games.HotHotStereoWin:
                case Games.RedstoneHotRushBothWays:
                    return MatrixHotHotStereoWin.GetHelpConfigV3();
                case Games.CloverBlast5:
                case Games.Redstone5WildFire:
                    return Matrix5CloverBlast.GetHelpConfigV3();
                case Games.RedstoneFearOfDark:
                case Games.Redstone40HotJoker:
                    return MatrixFearOfDark.GetHelpConfigV3();
                case Games.SantasPresents:
                    return MatrixSantasPresents.GetHelpConfigV3();
                case Games.RedstoneWildHeartBeat:
                case Games.RedstoneSpinUp:
                case Games.RedstoneOnlyAnime:
                    return MatrixWildHeartBeat.GetHelpConfigV3();
                case Games.BonusEpicCrown:
                case Games.RedstoneSpookySpins:
                case Games.BonusCrown:
                    return MatrixBonusEpicCrown.GetHelpConfigV3();
                case Games.CrownOfSecret:
                    return MatrixCrownOfSecret.GetHelpConfigV3(10);
                case Games.Redstone20WildCrown:
                case Games.PlayNetMajesticCrown20:
                case Games.PlayNetMajesticCrown20Xmas:
                    return MatrixBurstingHot5.GetHelpConfigV3(20);
                case Games.Wild81:
                case Games.Redstone81VegasCrown:
                case Games.Redstone81DoubleMagic:
                    return MatrixWild81.GetHelpConfigV3();
                case Games.MysticJungle:
                    return MatrixMysticJungle.GetHelpConfigV3();
                case Games.VintageFruits40:
                case Games.PlayNetWildForties:
                case Games.PlayNetWildFortiesXmas:
                    return MatrixVintageFruits40.GetHelpConfigV3();
                case Games.RedstoneHotRushFruitLines:
                    return MatrixHotRushFruitLines.GetHelpConfigV3();
                case Games.ToxicHaze:
                    return MatrixToxicHaze.GetHelpConfigV3();
                case Games.WildSunburst:
                    return MatrixWildSunburst.GetHelpConfigV3();
                case Games.MegaHot10:
                    return MatrixMegaHot10.GetHelpConfigV3();
                case Games.WildHeat40:
                    return MatrixWildHeat40.GetHelpConfigV3();
                case Games.Redstone100CloverFire:
                case Games.PlayNetFortuneClover100:
                    return GameEpicClover40Conversion.GetHelpConfigV3();
                case Games.AgeOfRome:
                    return MatrixAgeOfRome.GetHelpConfigV3();
                case Games.PlayNetDashingHot5:
                    return GameTwinklingHotConversion.GetHelpConfigV3();
                case Games.FruityForce40:
                    return MatrixFruityForce.GetHelpConfigV3();
                case Games.AquaFlame:
                    return MatrixAquaFlame.GetHelpConfigV3();
                case Games.RedstoneJuicyHeat20:
                    return GameMegaHot20Conversion.GetHelpConfigV3();
                case Games.TopHot5:
                    return MatrixTopHot5.GetHelpConfigV3();
                case Games.PlayNetWildSpread40:
                case Games.PlayNetCrownSpread40:
                    return GameWildHotBlowConversion.GetHelpConfigV3();
                default:
                    return null;
            }
        }

        public static ReelsV3 GetGameReelsV3(Games game)
        {
            switch (game)
            {
                case Games.LuckyTwister:
                case Games.BuffaloFortune:
                    return new ReelsV3 { regular = MatrixLuckyTwister.GetFakeReels() };
                case Games.SpinCards:
                    return new ReelsV3 { regular = MatrixPokerSlot.GetFakeReels(), freeSpin = MatrixPokerSlot.GetGratisFakeReels() };
                case Games.BookOfMayanGold:
                    return new ReelsV3 { regular = MatrixBookOfMayanGold.GetFakeReels(), freeSpin = MatrixBookOfMayanGold.GetFakeReels() };
                case Games.GoldenClover:
                    return new ReelsV3 { regular = MatrixGoldenClover.GetFakeReels(), freeSpin = MatrixGoldenClover.GetGratisFakeReels() };
                case Games.BurstingHot5:
                case Games.FireClover5:
                case Games.FireCloverDice5:
                case Games.VeryHot5:
                case Games.FireClover40:
                case Games.FireDice40:
                case Games.WildHeart5:
                case Games.AdmiralHot5:
                case Games.VeryHot5Christmas:
                case Games.CrownFire5:
                case Games.Money5:
                case Games.WinningClover5:
                case Games.RedstoneChilliHot5:
                case Games.RedstoneJollyPresents:
                case Games.Redstone20WildCrown:
                case Games.RedstoneHotRushCrownBurst:
                case Games.RedstoneLadyTripleFortune:
                case Games.FortuneClover5:
                case Games.PlayNetFortuneClover40:
                case Games.PlayNetDiamondHeart5:
                case Games.PlayNetMajesticCrown20:
                case Games.PlayNetDiamondHeart40:
                case Games.PlayNetMajesticCrown20Xmas:
                case Games.RedstoneFunkyFruits:
                case Games.BetwoonHot5:
                    return new ReelsV3 { regular = MatrixBurstingHot5.GetFakeReels() };
                case Games.WinningClover5Extreme:
                    return new ReelsV3 { regular = MatrixWinningClover5Extreme.GetFakeReels() };
                case Games.WildClover40:
                case Games.WildCloverDice40:
                case Games.RedstoneWildTrail:
                case Games.Redstone40JokersFreeGames:
                case Games.RedstoneMoonDog:
                case Games.RedstoneDivineStrike:
                    return new ReelsV3 { regular = MatrixWildClover40.GetFakeReels(), freeSpin = MatrixWildClover40.GetGratisFakeReels() };
                case Games.MayanCoins:
                case Games.CloverCoin:
                case Games.ShiningRush:
                    return new ReelsV3 { regular = MatrixCloverCash.GetFakeReels(), freeSpin = MatrixCloverCash.GetGratisFakeReels() };
                case Games.SuperLucky:
                case Games.RedstoneClover5LockAndCash:
                    return new ReelsV3 { regular = MatrixSuperLucky.GetFakeReels(), freeSpin = MatrixSuperLucky.GetGratisFakeReels() };
                case Games.WildCrown10:
                case Games.WildDiamond10:
                case Games.WildDice10:
                case Games.WildPumpkin10:
                case Games.WildSanta10:
                case Games.RedstoneSlotRoyale:
                case Games.MajesticCrown10:
                case Games.RedstoneVolcanoCrown:
                    return new ReelsV3 { regular = MatrixGoldenCrown.GetFakeReels() };
                case Games.WildClover506:
                    return new ReelsV3 { regular = Matrix40WildClover6.GetFakeReels(), freeSpin = Matrix40WildClover6.GetGratisFakeReels() };
                case Games.LollasWorld:
                case Games.ZabranjenoVoce:
                case Games.LollasWorldChristmas:
                case Games.KettysFashionWorld:
                case Games.MozzartsWorld:
                case Games.LollasSoccerWorld:
                case Games.PlayNetCrownStacks40:
                    return new ReelsV3 { regular = MatrixLilaWild.GetFakeReels() };
                case Games.LostBook:
                case Games.BookOfIbis:
                case Games.PlayNetBookOfAmun:
                    return new ReelsV3 { regular = GameSpellbookConversion.GetFakeReels(), freeSpin = GameSpellbookConversion.GetFakeReels() };
                case Games.BookOfScorpionsDouble:
                case Games.BookOfDread:
                case Games.RedstoneBookOfScorpio:
                    return new ReelsV3 { regular = MatrixBookOfDouble.GetFakeReels(), freeSpin = MatrixBookOfDouble.GetFakeReels() };
                case Games.ElGrandeToro:
                    return new ReelsV3 { regular = MatrixElGrandeToro.GetFakeReels(), freeSpin = MatrixElGrandeToro.GetFakeReelsGratis() };
                case Games.ActionHot40:
                case Games.TurboFire:
                case Games.CrystalHot40:
                    return new ReelsV3 { regular = GameTurboHot40Conversion.GetFakeReels() };
                case Games.FireCash40:
                case Games.Redstone40FruitFrenzy:
                    return new ReelsV3 { regular = Matrix40FireCash.GetFakeReels() };
                case Games.ActionHot20:
                case Games.FruitsAndStars20Deluxe:
                    return new ReelsV3 { regular = GameFruitsAndStarsConversion.GetFakeReels() };
                case Games.JokerQueen:
                    return new ReelsV3 { regular = GameJokerQueenConversion.GetFakeReels() };
                case Games.BonusBells:
                    return new ReelsV3 { regular = MatrixBonusBells.GetFakeReels(), freeSpin = MatrixBonusBells.GetFakeReelsGratis() };
                case Games.CashBells40:
                    return new ReelsV3 { regular = MatrixCashBells40.GetFakeReels(), freeSpin = MatrixCashBells40.GetGratisFakeReels() };
                case Games.SimplyRunner:
                    return new ReelsV3 { regular = MatrixSimplyRunner.GetFakeReels() };
                case Games.KingOfThunder:
                    return new ReelsV3 { regular = MatrixKingOfThunder.GetFakeReels(), freeSpin = MatrixKingOfThunder.GetFakeReelsGratis() };
                case Games.ExtraFlames10:
                case Games.ShiningPresents:
                    return new ReelsV3 { regular = MatrixExtraFlames10.GetFakeReels() };
                case Games.HeatClassic5:
                    return new ReelsV3 { regular = MatrixHeatClassic5.GetFakeReels() };
                case Games.VeryHot5Extreme:
                case Games.VeryHot40Extreme:
                case Games.RedstoneChilliDouble:
                case Games.RedstoneCapsAndCrowns:
                case Games.RedstoneDoubleFireClover:
                case Games.RedstoneDiceDouble:
                case Games.RedstoneChristmasDelight:
                case Games.PlayNetFortuneCloverX2:
                case Games.PlayNetDiamondHeartX2:
                    return new ReelsV3 { regular = MatrixVeryHot5Extreme.GetFakeReels() };
                case Games.FireCash20:
                case Games.Redstone20FruitFrenzy:
                case Games.RedstoneVolcanoHot:
                    return new ReelsV3 { regular = Matrix20FireCash.GetFakeReels() };
                case Games.Wild27:
                case Games.RedstoneApollo27Classic:
                case Games.PlayNet27WildStacks:
                case Games.PlayNet27WildStacksXmas:
                case Games.Redstone27DoubleFruit:
                case Games.BetAndreasWild:
                case Games.Wild5:
                    return new ReelsV3 { regular = MatrixWild27.GetFakeReels() };
                case Games.PiratesPapi:
                    return new ReelsV3 { regular = MatrixPiratesPapi.GetFakeReels() };
                case Games.BlowFruits40:
                case Games.CoinSplash:
                    return new ReelsV3 { regular = MatrixBlowFruits40.GetFakeReels() };
                case Games.SpecialFruits:
                    return new ReelsV3 { regular = MatrixSpecialFruits.GetFakeReels() };
                case Games.JokerTripleDouble:
                case Games.ChilliRespin:
                    return new ReelsV3 { regular = MatrixJokerTripleDouble.GetFakeReels(), respin = MatrixJokerTripleDouble.GetFakeReelsRespin() };
                case Games.HeatDouble:
                    return new ReelsV3 { regular = MatrixHeatDouble.GetFakeReels() };
                case Games.BlazingHeat:
                case Games.RedstoneHotRushStarsDeluxe:
                    return new ReelsV3 { regular = MatrixBlazingHeat.GetFakeReels() };
                case Games.HotHotStereoWin:
                case Games.RedstoneHotRushBothWays:
                    return new ReelsV3 { regular = MatrixHotHotStereoWin.GetFakeReels() };
                case Games.CloverBlast5:
                case Games.Redstone5WildFire:
                    return new ReelsV3 { regular = Matrix5CloverBlast.GetFakeReels() };
                case Games.RedstoneFearOfDark:
                case Games.Redstone40HotJoker:
                    return new ReelsV3 { regular = MatrixFearOfDark.GetFakeReels() };
                case Games.SantasPresents:
                    return new ReelsV3 { regular = MatrixSantasPresents.GetFakeReels(), freeSpin = MatrixSantasPresents.GetFakeReelsGratis() };
                case Games.RedstoneWildHeartBeat:
                case Games.RedstoneSpinUp:
                case Games.RedstoneOnlyAnime:
                    return new ReelsV3 { regular = MatrixWildHeartBeat.GetFakeReels() };
                case Games.BonusEpicCrown:
                case Games.RedstoneSpookySpins:
                case Games.BonusCrown:
                    return new ReelsV3 { regular = MatrixBonusEpicCrown.GetFakeReels(), freeSpin = MatrixBonusEpicCrown.GetFakeReelsGratis() };
                case Games.CrownOfSecret:
                    return new ReelsV3 { regular = MatrixCrownOfSecret.GetFakeReels(), freeSpin = MatrixCrownOfSecret.GetFakeReelsGratisWithBonusSymbol(), respin = MatrixCrownOfSecret.GetFakeReelsGratisWithoutBonusSymbol() };
                case Games.Wild81:
                case Games.Redstone81VegasCrown:
                case Games.Redstone81DoubleMagic:
                    return new ReelsV3 { regular = MatrixWild81.GetFakeReels() };
                case Games.MysticJungle:
                    return new ReelsV3 { regular = MatrixMysticJungle.GetFakeReels() };
                case Games.VintageFruits40:
                case Games.PlayNetWildForties:
                case Games.PlayNetWildFortiesXmas:
                    return new ReelsV3 { regular = MatrixVintageFruits40.GetFakeReels() };
                case Games.RedstoneHotRushFruitLines:
                    return new ReelsV3 { regular = MatrixHotRushFruitLines.GetFakeReels() };
                case Games.ToxicHaze:
                    return new ReelsV3 { regular = MatrixToxicHaze.GetFakeReels(), freeSpin = MatrixToxicHaze.GetFakeReelsGratis() };
                case Games.WildSunburst:
                    return new ReelsV3 { regular = MatrixWildSunburst.GetFakeReels(), freeSpin = MatrixWildSunburst.GetFakeReelsGratis() };
                case Games.MegaHot10:
                    return new ReelsV3 { regular = MatrixMegaHot10.GetFakeReels() };
                case Games.WildHeat40:
                    return new ReelsV3 { regular = MatrixWildHeat40.GetFakeReels() };
                case Games.Redstone100CloverFire:
                case Games.PlayNetFortuneClover100:
                    return new ReelsV3 { regular = GameEpicClover40Conversion.GetFakeReels() };
                case Games.AgeOfRome:
                    return new ReelsV3 { regular = MatrixAgeOfRome.GetFakeReels(), freeSpin = MatrixAgeOfRome.GetFakeReels() };
                case Games.PlayNetDashingHot5:
                    return new ReelsV3 { regular = GameTwinklingHotConversion.GetFakeReels() };
                case Games.FruityForce40:
                    return new ReelsV3 { regular = MatrixFruityForce.GetFakeReels() };
                case Games.AquaFlame:
                    return new ReelsV3 { regular = MatrixAquaFlame.GetFakeReels() };
                case Games.RedstoneJuicyHeat20:
                    return new ReelsV3 { regular = GameMegaHot20Conversion.GetFakeReels() };
                case Games.TopHot5:
                    return new ReelsV3 { regular = MatrixTopHot5.GetFakeReels() };
                case Games.PlayNetWildSpread40:
                case Games.PlayNetCrownSpread40:
                    return new ReelsV3 { regular = GameWildHotBlowConversion.GetFakeReels() };
                default:
                    return null;
            }
        }

        public static int GetLinesByIndexV3(Games game, int index)
        {
            int[] playLines;
            switch (game)
            {
                case Games.LuckyTwister:
                case Games.BuffaloFortune:
                    playLines = MatrixLuckyTwister.PlayLines;
                    break;
                case Games.SpinCards:
                    playLines = MatrixPokerSlot.PlayLines;
                    break;
                case Games.BookOfMayanGold:
                    playLines = MatrixBookOfMayanGold.PlayLines;
                    break;
                case Games.GoldenClover:
                    playLines = MatrixGoldenClover.PlayLines;
                    break;
                case Games.BurstingHot5:
                case Games.FireClover5:
                case Games.FireCloverDice5:
                case Games.VeryHot5:
                case Games.WildHeart5:
                case Games.AdmiralHot5:
                case Games.VeryHot5Christmas:
                case Games.CrownFire5:
                case Games.Money5:
                case Games.WinningClover5:
                case Games.WinningClover5Extreme:
                case Games.RedstoneChilliHot5:
                case Games.RedstoneJollyPresents:
                case Games.RedstoneHotRushCrownBurst:
                case Games.RedstoneLadyTripleFortune:
                case Games.FortuneClover5:
                case Games.PlayNetDiamondHeart5:
                case Games.RedstoneFunkyFruits:
                case Games.BetwoonHot5:
                    playLines = MatrixBurstingHot5.PlayLines;
                    break;
                case Games.FireClover40:
                case Games.FireDice40:
                case Games.PlayNetFortuneClover40:
                case Games.PlayNetDiamondHeart40:
                    playLines = MatrixBurstingHot5.PlayLines40;
                    break;
                case Games.WildClover40:
                case Games.WildCloverDice40:
                case Games.RedstoneWildTrail:
                case Games.Redstone40JokersFreeGames:
                case Games.RedstoneMoonDog:
                case Games.RedstoneDivineStrike:
                    playLines = MatrixWildClover40.PlayLines;
                    break;
                case Games.MayanCoins:
                case Games.CloverCoin:
                case Games.ShiningRush:
                    playLines = MatrixCloverCash.PlayLines;
                    break;
                case Games.SuperLucky:
                case Games.RedstoneClover5LockAndCash:
                    playLines = MatrixSuperLucky.PlayLines;
                    break;
                case Games.WildCrown10:
                case Games.WildDiamond10:
                case Games.WildDice10:
                case Games.WildPumpkin10:
                case Games.WildSanta10:
                case Games.RedstoneSlotRoyale:
                case Games.MajesticCrown10:
                case Games.RedstoneVolcanoCrown:
                    playLines = MatrixGoldenCrown.PlayLines;
                    break;
                case Games.WildClover506:
                    playLines = Matrix40WildClover6.PlayLines;
                    break;
                case Games.LollasWorld:
                case Games.ZabranjenoVoce:
                case Games.LollasWorldChristmas:
                case Games.KettysFashionWorld:
                case Games.MozzartsWorld:
                case Games.LollasSoccerWorld:
                case Games.PlayNetCrownStacks40:
                    playLines = MatrixLilaWild.PlayLines;
                    break;
                case Games.LostBook:
                case Games.BookOfIbis:
                case Games.PlayNetBookOfAmun:
                    playLines = GameSpellbookConversion.PlayLines;
                    break;
                case Games.BookOfScorpionsDouble:
                case Games.BookOfDread:
                case Games.RedstoneBookOfScorpio:
                    playLines = MatrixBookOfDouble.PlayLines;
                    break;
                case Games.ElGrandeToro:
                    playLines = MatrixElGrandeToro.PlayLines;
                    break;
                case Games.ActionHot40:
                case Games.TurboFire:
                case Games.CrystalHot40:
                    playLines = GameTurboHot40Conversion.PlayLines;
                    break;
                case Games.FireCash40:
                    playLines = Matrix40FireCash.PlayLines;
                    break;
                case Games.Redstone40FruitFrenzy:
                    playLines = Matrix40FireCash.PlayLinesFrenzy;
                    break;
                case Games.ActionHot20:
                case Games.FruitsAndStars20Deluxe:
                    playLines = GameFruitsAndStarsConversion.PlayLines;
                    break;
                case Games.JokerQueen:
                    playLines = GameJokerQueenConversion.PlayLines;
                    break;
                case Games.BonusBells:
                    playLines = MatrixBonusBells.PlayLines;
                    break;
                case Games.CashBells40:
                    playLines = MatrixCashBells40.PlayLines;
                    break;
                case Games.SimplyRunner:
                    playLines = MatrixSimplyRunner.PlayLines;
                    break;
                case Games.KingOfThunder:
                    playLines = MatrixKingOfThunder.PlayLines;
                    break;
                case Games.ExtraFlames10:
                case Games.ShiningPresents:
                    playLines = MatrixExtraFlames10.PlayLines;
                    break;
                case Games.HeatClassic5:
                    playLines = MatrixHeatClassic5.PlayLines;
                    break;
                case Games.VeryHot5Extreme:
                case Games.RedstoneChilliDouble:
                case Games.RedstoneCapsAndCrowns:
                case Games.RedstoneDoubleFireClover:
                case Games.RedstoneDiceDouble:
                case Games.RedstoneChristmasDelight:
                case Games.PlayNetFortuneCloverX2:
                case Games.PlayNetDiamondHeartX2:
                    playLines = MatrixVeryHot5Extreme.PlayLines;
                    break;
                case Games.VeryHot40Extreme:
                    playLines = MatrixVeryHot40Extreme.PlayLines;
                    break;
                case Games.FireCash20:
                case Games.Redstone20WildCrown:
                case Games.RedstoneVolcanoHot:
                case Games.PlayNetMajesticCrown20:
                case Games.PlayNetMajesticCrown20Xmas:
                    playLines = Matrix20FireCash.PlayLines;
                    break;
                case Games.Redstone20FruitFrenzy:
                    playLines = Matrix20FireCash.PlayLinesFrenzy;
                    break;
                case Games.Wild27:
                case Games.RedstoneApollo27Classic:
                case Games.PlayNet27WildStacks:
                case Games.PlayNet27WildStacksXmas:
                case Games.Redstone27DoubleFruit:
                case Games.BetAndreasWild:
                case Games.Wild5:
                    playLines = MatrixWild27.PlayLines;
                    break;
                case Games.PiratesPapi:
                    playLines = MatrixPiratesPapi.PlayLines;
                    break;
                case Games.BlowFruits40:
                case Games.CoinSplash:
                    playLines = MatrixBlowFruits40.PlayLines;
                    break;
                case Games.SpecialFruits:
                    playLines = MatrixSpecialFruits.PlayLines;
                    break;
                case Games.JokerTripleDouble:
                case Games.ChilliRespin:
                    playLines = MatrixJokerTripleDouble.PlayLines;
                    break;
                case Games.HeatDouble:
                    playLines = MatrixHeatDouble.PlayLines;
                    break;
                case Games.BlazingHeat:
                case Games.RedstoneHotRushStarsDeluxe:
                    playLines = MatrixBlazingHeat.PlayLines;
                    break;
                case Games.HotHotStereoWin:
                case Games.RedstoneHotRushBothWays:
                    playLines = MatrixHotHotStereoWin.PlayLines;
                    break;
                case Games.CloverBlast5:
                case Games.Redstone5WildFire:
                    playLines = Matrix5CloverBlast.PlayLines;
                    break;
                case Games.RedstoneFearOfDark:
                case Games.Redstone40HotJoker:
                    playLines = MatrixFearOfDark.PlayLines;
                    break;
                case Games.SantasPresents:
                    playLines = MatrixSantasPresents.PlayLines;
                    break;
                case Games.RedstoneWildHeartBeat:
                case Games.RedstoneSpinUp:
                case Games.RedstoneOnlyAnime:
                    playLines = MatrixWildHeartBeat.PlayLines;
                    break;
                case Games.BonusEpicCrown:
                case Games.RedstoneSpookySpins:
                case Games.BonusCrown:
                case Games.CrownOfSecret:
                    playLines = MatrixBonusEpicCrown.PlayLines;
                    break;
                case Games.Wild81:
                case Games.Redstone81VegasCrown:
                case Games.Redstone81DoubleMagic:
                    playLines = MatrixWild81.PlayLines;
                    break;
                case Games.MysticJungle:
                    playLines = MatrixMysticJungle.PlayLines;
                    break;
                case Games.VintageFruits40:
                case Games.PlayNetWildForties:
                case Games.PlayNetWildFortiesXmas:
                    playLines = MatrixVintageFruits40.PlayLines;
                    break;
                case Games.RedstoneHotRushFruitLines:
                    playLines = MatrixHotRushFruitLines.PlayLines;
                    break;
                case Games.ToxicHaze:
                    playLines = MatrixToxicHaze.PlayLines;
                    break;
                case Games.WildSunburst:
                    playLines = MatrixWildSunburst.PlayLines;
                    break;
                case Games.MegaHot10:
                    playLines = MatrixMegaHot10.PlayLines;
                    break;
                case Games.WildHeat40:
                    playLines = MatrixWildHeat40.PlayLines;
                    break;
                case Games.Redstone100CloverFire:
                case Games.PlayNetFortuneClover100:
                    playLines = GameEpicClover40Conversion.PlayLines100;
                    break;
                case Games.AgeOfRome:
                    playLines = MatrixAgeOfRome.PlayLines;
                    break;
                case Games.PlayNetDashingHot5:
                    playLines = GameTwinklingHotConversion.PlayLines;
                    break;
                case Games.FruityForce40:
                    playLines = MatrixFruityForce.PlayLines;
                    break;
                case Games.AquaFlame:
                    playLines = MatrixAquaFlame.PlayLines;
                    break;
                case Games.RedstoneJuicyHeat20:
                    playLines = GameMegaHot20Conversion.PlayLines;
                    break;
                case Games.TopHot5:
                    playLines = MatrixTopHot5.PlayLines;
                    break;
                case Games.PlayNetWildSpread40:
                case Games.PlayNetCrownSpread40:
                    playLines = GameWildHotBlowConversion.PlayLines;
                    break;
                default:
                    return -1;
            }
            if (playLines.Length <= index)
            {
                return -1;
            }
            return playLines[index];
        }

        public static int[] GetPlayLines(Games game)
        {
            switch (game)
            {
                case Games.LuckyTwister:
                case Games.BuffaloFortune:
                    return MatrixLuckyTwister.PlayLines;
                case Games.SpinCards:
                    return MatrixPokerSlot.PlayLines;
                case Games.BookOfMayanGold:
                    return MatrixBookOfMayanGold.PlayLines;
                case Games.GoldenClover:
                    return MatrixGoldenClover.PlayLines;
                case Games.BurstingHot5:
                case Games.FireClover5:
                case Games.FireCloverDice5:
                case Games.VeryHot5:
                case Games.WildHeart5:
                case Games.AdmiralHot5:
                case Games.VeryHot5Christmas:
                case Games.CrownFire5:
                case Games.Money5:
                case Games.WinningClover5:
                case Games.WinningClover5Extreme:
                case Games.RedstoneChilliHot5:
                case Games.RedstoneJollyPresents:
                case Games.RedstoneHotRushCrownBurst:
                case Games.RedstoneLadyTripleFortune:
                case Games.FortuneClover5:
                case Games.PlayNetDiamondHeart5:
                case Games.RedstoneFunkyFruits:
                case Games.BetwoonHot5:
                    return MatrixBurstingHot5.PlayLines;
                case Games.FireClover40:
                case Games.FireDice40:
                case Games.PlayNetFortuneClover40:
                case Games.PlayNetDiamondHeart40:
                    return MatrixBurstingHot5.PlayLines40;
                case Games.WildClover40:
                case Games.WildCloverDice40:
                case Games.RedstoneWildTrail:
                case Games.Redstone40JokersFreeGames:
                case Games.RedstoneMoonDog:
                case Games.RedstoneDivineStrike:
                    return MatrixWildClover40.PlayLines;
                case Games.MayanCoins:
                case Games.CloverCoin:
                case Games.ShiningRush:
                    return MatrixCloverCash.PlayLines;
                case Games.SuperLucky:
                case Games.RedstoneClover5LockAndCash:
                    return MatrixSuperLucky.PlayLines;
                case Games.WildCrown10:
                case Games.WildDiamond10:
                case Games.WildDice10:
                case Games.WildPumpkin10:
                case Games.WildSanta10:
                case Games.RedstoneSlotRoyale:
                case Games.MajesticCrown10:
                case Games.RedstoneVolcanoCrown:
                    return MatrixGoldenCrown.PlayLines;
                case Games.WildClover506:
                    return Matrix40WildClover6.PlayLines;
                case Games.LollasWorld:
                case Games.ZabranjenoVoce:
                case Games.LollasWorldChristmas:
                case Games.KettysFashionWorld:
                case Games.MozzartsWorld:
                case Games.LollasSoccerWorld:
                case Games.PlayNetCrownStacks40:
                    return MatrixLilaWild.PlayLines;
                case Games.LostBook:
                case Games.BookOfIbis:
                case Games.PlayNetBookOfAmun:
                    return GameSpellbookConversion.PlayLines;
                case Games.BookOfScorpionsDouble:
                case Games.BookOfDread:
                case Games.RedstoneBookOfScorpio:
                    return MatrixBookOfDouble.PlayLines;
                case Games.ElGrandeToro:
                    return MatrixElGrandeToro.PlayLines;
                case Games.ActionHot40:
                case Games.TurboFire:
                case Games.CrystalHot40:
                    return GameTurboHot40Conversion.PlayLines;
                case Games.FireCash40:
                    return Matrix40FireCash.PlayLines;
                case Games.Redstone40FruitFrenzy:
                    return Matrix40FireCash.PlayLinesFrenzy;
                case Games.ActionHot20:
                case Games.FruitsAndStars20Deluxe:
                    return GameFruitsAndStarsConversion.PlayLines;
                case Games.JokerQueen:
                    return GameJokerQueenConversion.PlayLines;
                case Games.BonusBells:
                    return MatrixBonusBells.PlayLines;
                case Games.CashBells40:
                    return MatrixCashBells40.PlayLines;
                case Games.SimplyRunner:
                    return MatrixSimplyRunner.PlayLines;
                case Games.KingOfThunder:
                    return MatrixKingOfThunder.PlayLines;
                case Games.ExtraFlames10:
                case Games.ShiningPresents:
                    return MatrixExtraFlames10.PlayLines;
                case Games.HeatClassic5:
                    return MatrixHeatClassic5.PlayLines;
                case Games.VeryHot5Extreme:
                case Games.RedstoneChilliDouble:
                case Games.RedstoneCapsAndCrowns:
                case Games.RedstoneDoubleFireClover:
                case Games.RedstoneDiceDouble:
                case Games.RedstoneChristmasDelight:
                case Games.PlayNetFortuneCloverX2:
                case Games.PlayNetDiamondHeartX2:
                    return MatrixVeryHot5Extreme.PlayLines;
                case Games.VeryHot40Extreme:
                    return MatrixVeryHot40Extreme.PlayLines;
                case Games.FireCash20:
                case Games.Redstone20WildCrown:
                case Games.RedstoneVolcanoHot:
                case Games.PlayNetMajesticCrown20:
                case Games.PlayNetMajesticCrown20Xmas:
                    return Matrix20FireCash.PlayLines;
                case Games.Redstone20FruitFrenzy:
                    return Matrix20FireCash.PlayLinesFrenzy;
                case Games.Wild27:
                case Games.RedstoneApollo27Classic:
                case Games.PlayNet27WildStacks:
                case Games.PlayNet27WildStacksXmas:
                case Games.Redstone27DoubleFruit:
                case Games.BetAndreasWild:
                case Games.Wild5:
                    return MatrixWild27.PlayLines;
                case Games.PiratesPapi:
                    return MatrixPiratesPapi.PlayLines;
                case Games.BlowFruits40:
                case Games.CoinSplash:
                    return MatrixBlowFruits40.PlayLines;
                case Games.SpecialFruits:
                    return MatrixSpecialFruits.PlayLines;
                case Games.JokerTripleDouble:
                case Games.ChilliRespin:
                    return MatrixJokerTripleDouble.PlayLines;
                case Games.HeatDouble:
                    return MatrixHeatDouble.PlayLines;
                case Games.BlazingHeat:
                case Games.RedstoneHotRushStarsDeluxe:
                    return MatrixBlazingHeat.PlayLines;
                case Games.HotHotStereoWin:
                case Games.RedstoneHotRushBothWays:
                    return MatrixHotHotStereoWin.PlayLines;
                case Games.CloverBlast5:
                case Games.Redstone5WildFire:
                    return Matrix5CloverBlast.PlayLines;
                case Games.RedstoneFearOfDark:
                case Games.Redstone40HotJoker:
                    return MatrixFearOfDark.PlayLines;
                case Games.SantasPresents:
                    return MatrixSantasPresents.PlayLines;
                case Games.RedstoneWildHeartBeat:
                case Games.RedstoneSpinUp:
                case Games.RedstoneOnlyAnime:
                    return MatrixWildHeartBeat.PlayLines;
                case Games.BonusEpicCrown:
                case Games.RedstoneSpookySpins:
                case Games.BonusCrown:
                case Games.CrownOfSecret:
                    return MatrixBonusEpicCrown.PlayLines;
                case Games.Wild81:
                case Games.Redstone81VegasCrown:
                case Games.Redstone81DoubleMagic:
                    return MatrixWild81.PlayLines;
                case Games.MysticJungle:
                    return MatrixMysticJungle.PlayLines;
                case Games.VintageFruits40:
                case Games.PlayNetWildForties:
                case Games.PlayNetWildFortiesXmas:
                    return MatrixVintageFruits40.PlayLines;
                case Games.RedstoneHotRushFruitLines:
                    return MatrixHotRushFruitLines.PlayLines;
                case Games.ToxicHaze:
                    return MatrixToxicHaze.PlayLines;
                case Games.WildSunburst:
                    return MatrixWildSunburst.PlayLines;
                case Games.MegaHot10:
                    return MatrixMegaHot10.PlayLines;
                case Games.WildHeat40:
                    return MatrixWildHeat40.PlayLines;
                case Games.Redstone100CloverFire:
                case Games.PlayNetFortuneClover100:
                    return GameEpicClover40Conversion.PlayLines100;
                case Games.AgeOfRome:
                    return MatrixAgeOfRome.PlayLines;
                case Games.PlayNetDashingHot5:
                    return GameTwinklingHotConversion.PlayLines;
                case Games.FruityForce40:
                    return MatrixFruityForce.PlayLines;
                case Games.AquaFlame:
                    return MatrixAquaFlame.PlayLines;
                case Games.RedstoneJuicyHeat20:
                    return GameMegaHot20Conversion.PlayLines;
                case Games.TopHot5:
                    return MatrixTopHot5.PlayLines;
                case Games.PlayNetWildSpread40:
                case Games.PlayNetCrownSpread40:
                    return GameWildHotBlowConversion.PlayLines;
                default:
                    return null;
            }
        }

        public static int GetIndexForLinesV3(Games game, int lines)
        {
            switch (game)
            {
                case Games.LuckyTwister:
                case Games.BuffaloFortune:
                    return Array.IndexOf(MatrixLuckyTwister.PlayLines, lines);
                case Games.SpinCards:
                    return Array.IndexOf(MatrixPokerSlot.PlayLines, lines);
                case Games.BookOfMayanGold:
                    return Array.IndexOf(MatrixBookOfMayanGold.PlayLines, lines);
                case Games.GoldenClover:
                    return Array.IndexOf(MatrixGoldenClover.PlayLines, lines);
                case Games.BurstingHot5:
                case Games.FireClover5:
                case Games.FireCloverDice5:
                case Games.VeryHot5:
                case Games.FireClover40:
                case Games.FireDice40:
                case Games.WildHeart5:
                case Games.AdmiralHot5:
                case Games.VeryHot5Christmas:
                case Games.CrownFire5:
                case Games.Money5:
                case Games.WinningClover5:
                case Games.WinningClover5Extreme:
                case Games.RedstoneChilliHot5:
                case Games.RedstoneJollyPresents:
                case Games.RedstoneHotRushCrownBurst:
                case Games.RedstoneLadyTripleFortune:
                case Games.FortuneClover5:
                case Games.PlayNetFortuneClover40:
                case Games.PlayNetDiamondHeart5:
                case Games.PlayNetDiamondHeart40:
                case Games.RedstoneFunkyFruits:
                case Games.BetwoonHot5:
                    return Array.IndexOf(MatrixBurstingHot5.PlayLines, lines);
                case Games.WildClover40:
                case Games.WildCloverDice40:
                case Games.RedstoneWildTrail:
                case Games.Redstone40JokersFreeGames:
                case Games.RedstoneMoonDog:
                case Games.RedstoneDivineStrike:
                    return Array.IndexOf(MatrixWildClover40.PlayLines, lines);
                case Games.MayanCoins:
                case Games.CloverCoin:
                case Games.ShiningRush:
                    return Array.IndexOf(MatrixCloverCash.PlayLines, lines);
                case Games.SuperLucky:
                case Games.RedstoneClover5LockAndCash:
                    return Array.IndexOf(MatrixSuperLucky.PlayLines, lines);
                case Games.WildCrown10:
                case Games.WildDiamond10:
                case Games.WildDice10:
                case Games.WildPumpkin10:
                case Games.WildSanta10:
                case Games.RedstoneSlotRoyale:
                case Games.MajesticCrown10:
                case Games.RedstoneVolcanoCrown:
                    return Array.IndexOf(MatrixGoldenCrown.PlayLines, lines);
                case Games.WildClover506:
                    return Array.IndexOf(Matrix40WildClover6.PlayLines, lines);
                case Games.LollasWorld:
                case Games.ZabranjenoVoce:
                case Games.LollasWorldChristmas:
                case Games.KettysFashionWorld:
                case Games.MozzartsWorld:
                case Games.LollasSoccerWorld:
                case Games.PlayNetCrownStacks40:
                    return Array.IndexOf(MatrixLilaWild.PlayLines, lines);
                case Games.LostBook:
                case Games.BookOfIbis:
                case Games.PlayNetBookOfAmun:
                    return Array.IndexOf(GameSpellbookConversion.PlayLines, lines);
                case Games.BookOfScorpionsDouble:
                case Games.BookOfDread:
                case Games.RedstoneBookOfScorpio:
                    return Array.IndexOf(MatrixBookOfDouble.PlayLines, lines);
                case Games.ElGrandeToro:
                    return Array.IndexOf(MatrixElGrandeToro.PlayLines, lines);
                case Games.ActionHot40:
                case Games.TurboFire:
                case Games.CrystalHot40:
                    return Array.IndexOf(GameTurboHot40Conversion.PlayLines, lines);
                case Games.FireCash40:
                    return Array.IndexOf(Matrix40FireCash.PlayLines, lines);
                case Games.Redstone40FruitFrenzy:
                    return Array.IndexOf(Matrix40FireCash.PlayLinesFrenzy, lines);
                case Games.ActionHot20:
                case Games.FruitsAndStars20Deluxe:
                    return Array.IndexOf(GameFruitsAndStarsConversion.PlayLines, lines);
                case Games.JokerQueen:
                    return Array.IndexOf(GameJokerQueenConversion.PlayLines, lines);
                case Games.BonusBells:
                    return Array.IndexOf(MatrixBonusBells.PlayLines, lines);
                case Games.CashBells40:
                    return Array.IndexOf(MatrixCashBells40.PlayLines, lines);
                case Games.SimplyRunner:
                    return Array.IndexOf(MatrixSimplyRunner.PlayLines, lines);
                case Games.KingOfThunder:
                    return Array.IndexOf(MatrixKingOfThunder.PlayLines, lines);
                case Games.ExtraFlames10:
                case Games.ShiningPresents:
                    return Array.IndexOf(MatrixExtraFlames10.PlayLines, lines);
                case Games.HeatClassic5:
                    return Array.IndexOf(MatrixHeatClassic5.PlayLines, lines);
                case Games.VeryHot5Extreme:
                case Games.RedstoneChilliDouble:
                case Games.RedstoneCapsAndCrowns:
                case Games.RedstoneDoubleFireClover:
                case Games.RedstoneDiceDouble:
                case Games.RedstoneChristmasDelight:
                case Games.PlayNetFortuneCloverX2:
                case Games.PlayNetDiamondHeartX2:
                    return Array.IndexOf(MatrixVeryHot5Extreme.PlayLines, lines);
                case Games.VeryHot40Extreme:
                    return Array.IndexOf(MatrixVeryHot40Extreme.PlayLines, lines);
                case Games.FireCash20:
                case Games.Redstone20WildCrown:
                case Games.RedstoneVolcanoHot:
                case Games.PlayNetMajesticCrown20:
                case Games.PlayNetMajesticCrown20Xmas:
                    return Array.IndexOf(Matrix20FireCash.PlayLines, lines);
                case Games.Redstone20FruitFrenzy:
                    return Array.IndexOf(Matrix20FireCash.PlayLinesFrenzy, lines);
                case Games.Wild27:
                case Games.RedstoneApollo27Classic:
                case Games.PlayNet27WildStacks:
                case Games.PlayNet27WildStacksXmas:
                case Games.Redstone27DoubleFruit:
                case Games.BetAndreasWild:
                case Games.Wild5:
                    return Array.IndexOf(MatrixWild27.PlayLines, lines);
                case Games.PiratesPapi:
                    return Array.IndexOf(MatrixPiratesPapi.PlayLines, lines);
                case Games.BlowFruits40:
                case Games.CoinSplash:
                    return Array.IndexOf(MatrixBlowFruits40.PlayLines, lines);
                case Games.SpecialFruits:
                    return Array.IndexOf(MatrixSpecialFruits.PlayLines, lines);
                case Games.JokerTripleDouble:
                case Games.ChilliRespin:
                    return Array.IndexOf(MatrixJokerTripleDouble.PlayLines, lines);
                case Games.HeatDouble:
                    return Array.IndexOf(MatrixHeatDouble.PlayLines, lines);
                case Games.BlazingHeat:
                case Games.RedstoneHotRushStarsDeluxe:
                    return Array.IndexOf(MatrixBlazingHeat.PlayLines, lines);
                case Games.HotHotStereoWin:
                case Games.RedstoneHotRushBothWays:
                    return Array.IndexOf(MatrixHotHotStereoWin.PlayLines, lines);
                case Games.CloverBlast5:
                case Games.Redstone5WildFire:
                    return Array.IndexOf(Matrix5CloverBlast.PlayLines, lines);
                case Games.RedstoneFearOfDark:
                case Games.Redstone40HotJoker:
                    return Array.IndexOf(MatrixFearOfDark.PlayLines, lines);
                case Games.SantasPresents:
                    return Array.IndexOf(MatrixSantasPresents.PlayLines, lines);
                case Games.RedstoneWildHeartBeat:
                case Games.RedstoneSpinUp:
                case Games.RedstoneOnlyAnime:
                    return Array.IndexOf(MatrixWildHeartBeat.PlayLines, lines);
                case Games.BonusEpicCrown:
                case Games.RedstoneSpookySpins:
                case Games.BonusCrown:
                case Games.CrownOfSecret:
                    return Array.IndexOf(MatrixBonusEpicCrown.PlayLines, lines);
                case Games.Wild81:
                case Games.Redstone81VegasCrown:
                case Games.Redstone81DoubleMagic:
                    return Array.IndexOf(MatrixWild81.PlayLines, lines);
                case Games.MysticJungle:
                    return Array.IndexOf(MatrixMysticJungle.PlayLines, lines);
                case Games.VintageFruits40:
                case Games.PlayNetWildForties:
                case Games.PlayNetWildFortiesXmas:
                    return Array.IndexOf(MatrixVintageFruits40.PlayLines, lines);
                case Games.RedstoneHotRushFruitLines:
                    return Array.IndexOf(MatrixHotRushFruitLines.PlayLines, lines);
                case Games.ToxicHaze:
                    return Array.IndexOf(MatrixToxicHaze.PlayLines, lines);
                case Games.WildSunburst:
                    return Array.IndexOf(MatrixWildSunburst.PlayLines, lines);
                case Games.MegaHot10:
                    return Array.IndexOf(MatrixMegaHot10.PlayLines, lines);
                case Games.WildHeat40:
                    return Array.IndexOf(MatrixWildHeat40.PlayLines, lines);
                case Games.Redstone100CloverFire:
                case Games.PlayNetFortuneClover100:
                    return Array.IndexOf(GameEpicClover40Conversion.PlayLines100, lines);
                case Games.AgeOfRome:
                    return Array.IndexOf(MatrixAgeOfRome.PlayLines, lines);
                case Games.PlayNetDashingHot5:
                    return Array.IndexOf(GameTwinklingHotConversion.PlayLines, lines);
                case Games.FruityForce40:
                    return Array.IndexOf(MatrixFruityForce.PlayLines, lines);
                case Games.AquaFlame:
                    return Array.IndexOf(MatrixAquaFlame.PlayLines, lines);
                case Games.RedstoneJuicyHeat20:
                    return Array.IndexOf(GameMegaHot20Conversion.PlayLines, lines);
                case Games.TopHot5:
                    return Array.IndexOf(MatrixTopHot5.PlayLines, lines);
                case Games.PlayNetWildSpread40:
                case Games.PlayNetCrownSpread40:
                    return Array.IndexOf(GameWildHotBlowConversion.PlayLines, lines);
                default:
                    return -1;
            }
        }

        public static SlotDataResV3 GetNonWinningCombination(Games game, int bet, int numberOfLines, int gratisGamesLeft)
        {
            switch (game)
            {
                case Games.LuckyTwister:
                case Games.BuffaloFortune:
                    return GameLuckyTwisterConversion.ToSlotDataResV3(GameLuckyTwisterConversion.GetNonWinningCombination(bet));
                case Games.SpinCards:
                    return GameSpinCardsConversion.ToSlotDataResV3(GameSpinCardsConversion.GetNonWinningCombination(bet, numberOfLines, gratisGamesLeft));
                case Games.BookOfMayanGold:
                    return GameBookOfMayanGoldConversion.ToSlotDataResV3(GameBookOfMayanGoldConversion.GetNonWinningCombination(bet, numberOfLines, gratisGamesLeft));
                case Games.GoldenClover:
                    return GameGoldenCloverConversion.ToSlotDataResV3(GameGoldenCloverConversion.GetNonWinningCombination(bet, numberOfLines, gratisGamesLeft), numberOfLines, gratisGamesLeft > 0);
                case Games.BurstingHot5:
                case Games.FireClover5:
                case Games.FireCloverDice5:
                case Games.VeryHot5:
                case Games.FireClover40:
                case Games.FireDice40:
                case Games.WildHeart5:
                case Games.AdmiralHot5:
                case Games.VeryHot5Christmas:
                case Games.CrownFire5:
                case Games.Money5:
                case Games.WinningClover5:
                case Games.RedstoneChilliHot5:
                case Games.RedstoneJollyPresents:
                case Games.Redstone20WildCrown:
                case Games.RedstoneHotRushCrownBurst:
                case Games.RedstoneLadyTripleFortune:
                case Games.FortuneClover5:
                case Games.PlayNetFortuneClover40:
                case Games.PlayNetDiamondHeart5:
                case Games.PlayNetMajesticCrown20:
                case Games.PlayNetDiamondHeart40:
                case Games.PlayNetMajesticCrown20Xmas:
                case Games.RedstoneFunkyFruits:
                case Games.BetwoonHot5:
                    return GameBurstingHot5Conversion.ToSlotDataResV3(GameBurstingHot5Conversion.GetNonWinningCombination(bet, numberOfLines, gratisGamesLeft));
                case Games.WinningClover5Extreme:
                    return GameBurstingHot5Conversion.ToSlotDataResV3(CombinationWinningClover5Extreme.GetNonWinningCombination(bet, numberOfLines));
                case Games.WildClover40:
                case Games.WildCloverDice40:
                case Games.RedstoneWildTrail:
                case Games.Redstone40JokersFreeGames:
                case Games.RedstoneMoonDog:
                case Games.RedstoneDivineStrike:
                    return GameWildClover40Conversion.ToSlotDataResV3(GameWildClover40Conversion.GetNonWinningCombination(bet, numberOfLines, gratisGamesLeft));
                case Games.MayanCoins:
                case Games.CloverCoin:
                case Games.ShiningRush:
                    return GameCloverCashConversion.ToSlotDataResV3(GameCloverCashConversion.GetNonWinningCombination(bet, numberOfLines, gratisGamesLeft), gratisGamesLeft > 0);
                case Games.SuperLucky:
                case Games.RedstoneClover5LockAndCash:
                    return GameCloverCashConversion.ToSlotDataResV3SuperLucky(GameCloverCashConversion.GetNonWinningCombination(bet, numberOfLines, gratisGamesLeft), gratisGamesLeft > 0);
                case Games.WildCrown10:
                case Games.WildDiamond10:
                case Games.WildDice10:
                case Games.WildPumpkin10:
                case Games.WildSanta10:
                case Games.RedstoneSlotRoyale:
                case Games.MajesticCrown10:
                case Games.RedstoneVolcanoCrown:
                    return GameGoldenCrownConversion.ToSlotDataResV3(GameGoldenCrownConversion.GetNonWinningCombination(bet, numberOfLines, gratisGamesLeft));
                case Games.WildClover506:
                    return GameWildClover506Conversion.ToSlotDataResV3(GameWildClover506Conversion.GetNonWinningCombination(bet, numberOfLines, gratisGamesLeft));
                case Games.LollasWorld:
                case Games.ZabranjenoVoce:
                case Games.LollasWorldChristmas:
                case Games.KettysFashionWorld:
                case Games.MozzartsWorld:
                case Games.LollasSoccerWorld:
                case Games.PlayNetCrownStacks40:
                    return GameLollasWorldCoversion.ToSlotDataResV3(GameLollasWorldCoversion.GetNonWinningCombination(bet, numberOfLines, gratisGamesLeft));
                case Games.LostBook:
                case Games.BookOfIbis:
                case Games.PlayNetBookOfAmun:
                    return GameSpellbookConversion.ToSlotDataResV3(GameSpellbookConversion.GetNonWinningCombination(bet, numberOfLines, gratisGamesLeft), gratisGamesLeft > 0);
                case Games.BookOfScorpionsDouble:
                case Games.BookOfDread:
                case Games.RedstoneBookOfScorpio:
                    return GameBookOfDoubleConversion.ToSlotDataResV3(GameBookOfDoubleConversion.GetNonWinningCombination(bet, numberOfLines, gratisGamesLeft));
                case Games.ElGrandeToro:
                    return GameElGrandeToroConversion.ToSlotDataResV3(GameElGrandeToroConversion.GetNonWinningCombination(bet, numberOfLines, gratisGamesLeft));
                case Games.ActionHot40:
                case Games.TurboFire:
                case Games.CrystalHot40:
                    return GameTurboHot40Conversion.ToSlotDataResV3(GameTurboHot40Conversion.GetNonWinningCombination(bet, numberOfLines, gratisGamesLeft));
                case Games.FireCash40:
                    return Game40FireCashConversion.ToSlotDataResV3(Game40FireCashConversion.GetNonWinningCombination(bet, numberOfLines));
                case Games.Redstone40FruitFrenzy:
                    return Game40FireCashConversion.ToSlotDataResV3(Game40FireCashConversion.GetNonWinningCombinationFrenzy(bet));
                case Games.ActionHot20:
                case Games.FruitsAndStars20Deluxe:
                    return GameFruitsAndStarsConversion.ToSlotDataResV3(GameFruitsAndStarsConversion.GetNonWinningCombination(bet, numberOfLines, gratisGamesLeft));
                case Games.JokerQueen:
                    return GameJokerQueenConversion.ToSlotDataResV3(GameJokerQueenConversion.GetNonWinningCombination(bet, numberOfLines, gratisGamesLeft));
                case Games.BonusBells:
                    return GameBonusBellsConversion.ToSlotDataResV3(GameBonusBellsConversion.GetNonWinningCombination(bet, numberOfLines, gratisGamesLeft));
                case Games.CashBells40:
                    return GameCashBells40Conversion.ToSlotDataResV3(GameCashBells40Conversion.GetNonWinningCombination(bet, numberOfLines, gratisGamesLeft));
                case Games.SimplyRunner:
                    return GameSimplyRunnerConversion.ToSlotDataResV3(GameSimplyRunnerConversion.GetNonWinningCombination(bet, numberOfLines));
                case Games.KingOfThunder:
                    return GameKingOfThunderConversion.ToSlotDataResV3(GameKingOfThunderConversion.GetNonWinningCombination(bet, numberOfLines, gratisGamesLeft));
                case Games.ExtraFlames10:
                case Games.ShiningPresents:
                    return GameExtraFlames10Conversion.ToSlotDataResV3(GameExtraFlames10Conversion.GetNonWinningCombination(bet, numberOfLines, gratisGamesLeft));
                case Games.HeatClassic5:
                    return GameHeatClassicConversion.ToSlotDataResV3(GameHeatClassicConversion.GetNonWinningCombination(bet, numberOfLines, gratisGamesLeft));
                case Games.VeryHot5Extreme:
                case Games.RedstoneChilliDouble:
                case Games.RedstoneCapsAndCrowns:
                case Games.RedstoneDoubleFireClover:
                case Games.RedstoneDiceDouble:
                case Games.RedstoneChristmasDelight:
                case Games.PlayNetFortuneCloverX2:
                case Games.PlayNetDiamondHeartX2:
                    return GameVeryHotExtremeConversion.ToSlotDataResV3(GameVeryHotExtremeConversion.GetNonWinningCombination(bet, numberOfLines));
                case Games.VeryHot40Extreme:
                    return GameVeryHot40ExtremeConversion.ToSlotDataResV3(GameVeryHot40ExtremeConversion.GetNonWinningCombination(bet));
                case Games.FireCash20:
                case Games.RedstoneVolcanoHot:
                    return Game20FireCashConversion.ToSlotDataResV3(Game20FireCashConversion.GetNonWinningCombination(bet, numberOfLines));
                case Games.Redstone20FruitFrenzy:
                    return Game20FireCashConversion.ToSlotDataResV3(Game20FireCashConversion.GetNonWinningCombinationFrenzy(bet));
                case Games.Wild27:
                case Games.RedstoneApollo27Classic:
                case Games.PlayNet27WildStacks:
                case Games.PlayNet27WildStacksXmas:
                case Games.Redstone27DoubleFruit:
                case Games.BetAndreasWild:
                    return GameWild27Conversion.ToSlotDataResV3(GameWild27Conversion.GetNonWinningCombination(bet));
                case Games.Wild5:
                    return GameWild27Conversion.ToSlotDataResV3(GameWild5Conversion.GetNonWinningCombination(bet));
                case Games.PiratesPapi:
                    return GamePiratesPapiConversion.ToSlotDataResV3(GamePiratesPapiConversion.GetNonWinningCombination(bet, numberOfLines));
                case Games.BlowFruits40:
                case Games.CoinSplash:
                    return GameBlowFruits40Conversion.ToSlotDataResV3(GameBlowFruits40Conversion.GetNonWinningCombination(bet, numberOfLines));
                case Games.SpecialFruits:
                    return GameSpecialFruitsConversion.ToSlotDataResV3(GameSpecialFruitsConversion.GetNonWinningCombination(bet, numberOfLines));
                case Games.JokerTripleDouble:
                case Games.ChilliRespin:
                    return GameJokerTripleDoubleConversion.ToSlotDataResV3(GameJokerTripleDoubleConversion.GetNonWinningCombination(bet, gratisGamesLeft));
                case Games.HeatDouble:
                    return GameHeatDoubleConversion.ToSlotDataResV3(GameHeatDoubleConversion.GetNonWinningCombination(bet));
                case Games.BlazingHeat:
                case Games.RedstoneHotRushStarsDeluxe:
                    return GameBlazingHeatConversion.ToSlotDataResV3(GameBlazingHeatConversion.GetNonWinningCombination(bet));
                case Games.HotHotStereoWin:
                case Games.RedstoneHotRushBothWays:
                    return GameHotHotStereoWinConversion.ToSlotDataResV3(GameHotHotStereoWinConversion.GetNonWinningCombination(bet));
                case Games.CloverBlast5:
                case Games.Redstone5WildFire:
                    return Game5CloverBlastConversion.ToSlotDataResV3(Game5CloverBlastConversion.GetNonWinningCombination(bet));
                case Games.RedstoneFearOfDark:
                case Games.Redstone40HotJoker:
                    return GameFearOfDarkConversion.ToSlotDataResV3(GameFearOfDarkConversion.GetNonWinningCombination(bet));
                case Games.SantasPresents:
                    return GameSantasPresentsConversion.ToSlotDataResV3(GameSantasPresentsConversion.GetNonWinningCombination(bet, numberOfLines, gratisGamesLeft > 0));
                case Games.RedstoneWildHeartBeat:
                case Games.RedstoneSpinUp:
                case Games.RedstoneOnlyAnime:
                    return GameWildHeartBeatConversion.ToSlotDataResV3(GameWildHeartBeatConversion.GetNonWinningCombination(bet));
                case Games.BonusEpicCrown:
                case Games.RedstoneSpookySpins:
                    return GameBonusEpicCrownConversion.ToSlotDataResV3(GameBonusEpicCrownConversion.GetNonWinningCombination(bet, gratisGamesLeft > 0, 10, 40));
                case Games.CrownOfSecret:
                    return GameCrownOfSecretConversion.ToSlotDataResV3(GameCrownOfSecretConversion.GetNonWinningCombination(bet, numberOfLines, gratisGamesLeft > 0));
                case Games.BonusCrown:
                    return GameBonusEpicCrownConversion.ToSlotDataResV3(GameBonusEpicCrownConversion.GetNonWinningCombination(bet, gratisGamesLeft > 0, 10, 20));
                case Games.Wild81:
                case Games.Redstone81VegasCrown:
                case Games.Redstone81DoubleMagic:
                    return GameWild81Conversion.ToSlotDataResV3(GameWild81Conversion.GetNonWinningCombination(bet));
                case Games.MysticJungle:
                    return GameMysticJungleConversion.ToSlotDataResV3(GameMysticJungleConversion.GetNonWinningCombination(numberOfLines, bet));
                case Games.VintageFruits40:
                case Games.PlayNetWildForties:
                case Games.PlayNetWildFortiesXmas:
                    return GameVintageFruits40Conversion.ToSlotDataResV3(GameVintageFruits40Conversion.GetNonWinningCombination(bet, numberOfLines));
                case Games.RedstoneHotRushFruitLines:
                    return GameHotRushFruitLinesConversion.ToSlotDataResV3(GameHotRushFruitLinesConversion.GetNonWinningCombination(bet));
                case Games.ToxicHaze:
                    return GameToxicHazeConversion.ToSlotDataResV3(GameToxicHazeConversion.GetNonWinningCombination(bet, numberOfLines, gratisGamesLeft), gratisGamesLeft, gratisGamesLeft > 0);
                case Games.WildSunburst:
                    return GameWildSunburstConversion.ToSlotDataResV3(GameWildSunburstConversion.GetNonWinningCombination(bet, numberOfLines, gratisGamesLeft), gratisGamesLeft > 0);
                case Games.MegaHot10:
                    return GameMegaHot10Conversion.ToSlotDataResV3(GameMegaHot10Conversion.GetNonWinningCombination(bet, numberOfLines));
                case Games.WildHeat40:
                    return GameWildHeat40Conversion.ToSlotDataResV3(GameWildHeat40Conversion.GetNonWinningCombination(bet, numberOfLines));
                case Games.Redstone100CloverFire:
                case Games.PlayNetFortuneClover100:
                    return GameEpicClover40Conversion.ToSlotDataResV3(GameEpicClover40Conversion.GetNonWinningCombination(bet, numberOfLines));
                case Games.AgeOfRome:
                    return GameAgeOfRomeConversion.ToSlotDataResV3(GameAgeOfRomeConversion.GetNonWinningCombination(bet, numberOfLines, gratisGamesLeft), gratisGamesLeft > 0);
                case Games.PlayNetDashingHot5:
                    return GameTwinklingHotConversion.ToSlotDataResV3(GameTwinklingHotConversion.GetNonWinningCombination(bet, numberOfLines));
                case Games.FruityForce40:
                    return GameFruityForce40Conversion.ToSlotDataResV3(GameFruityForce40Conversion.GetNonWinningCombination(bet, numberOfLines));
                case Games.AquaFlame:
                    return GameAquaFlameConversion.ToSlotDataResV3(GameAquaFlameConversion.GetNonWinningCombination(bet, numberOfLines));
                case Games.RedstoneJuicyHeat20:
                    return GameMegaHot20Conversion.ToSlotDataResV3(GameMegaHot20Conversion.GetNonWinningCombination(bet, numberOfLines));
                case Games.TopHot5:
                    return GameWild27Conversion.ToSlotDataResV3(GameTopHot5Conversion.GetNonWinningCombination(bet));
                case Games.PlayNetWildSpread40:
                case Games.PlayNetCrownSpread40:
                    return GameWildHotBlowConversion.ToSlotDataResV3(GameWildHotBlowConversion.GetNonWinningCombination(bet, numberOfLines));
                default:
                    return null;
            }
        }
    }
}
