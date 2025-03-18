using CombinationExtras.ConversionData.V3Conversion;
using CombinationExtras.UnicornConversionData.V3Conversion;
using Papi.GameServer.Utils.Enums;
using MathBaseProject.StructuresV3;
using MathForUnicornGames.Game10JingleFruits;
using MathForUnicornGames.Game20HotStrike;
using MathForUnicornGames.Game20MegaFlames;
using MathForUnicornGames.Game20SuperFlames;
using MathForUnicornGames.Game40FruitReels;
using MathForUnicornGames.Game40MegaFlames;
using MathForUnicornGames.Game5HotStrike;
using MathForUnicornGames.GameBigHitSevens;
using MathForUnicornGames.GameBigSpinSevens;
using MathForUnicornGames.GameBikiniFruits;
using MathForUnicornGames.GameBuffaloSevens;
using MathForUnicornGames.GameChristmasPresents;
using MathForUnicornGames.GameCoyoteSevens;
using MathForUnicornGames.GameEpicMegaCash;
using MathForUnicornGames.GameFastFruits;
using MathForUnicornGames.GameFireStars;
using MathForUnicornGames.GameFrootClassic;
using MathForUnicornGames.GameFruitWildLines;
using MathForUnicornGames.GameGoldLine;
using MathForUnicornGames.GameGreatWhale;
using MathForUnicornGames.GameHavanaDice;
using MathForUnicornGames.GameHitLine;
using MathForUnicornGames.GameIslandRespins;
using MathForUnicornGames.GameMiniMegaCash;
using MathForUnicornGames.GameMoneyStandardWild;
using MathForUnicornGames.GamePumpkinHorror;
using MathForUnicornGames.GameReelDice;
using MathForUnicornGames.GameSimplySevens;
using MathForUnicornGames.GameStickyHot;
using MathForUnicornGames.GameSurfinHeat;
using MathForUnicornGames.GameTwentyFruits;
using MathForUnicornGames.GameVegasDice;
using MathForUnicornGames.GameWildParadice;
using MathForUnicornGames.GameWinterFruits;
using System;

namespace CombinationExtras.V3Data
{
    public static class UnicornInitGameDataV3
    {
        public static HelpConfigV3<object> GetGameHelpConfigV3(Games game)
        {
            switch (game)
            {
                case Games.UnicornVegasDice:
                case Games.UnicornFruitIsland:
                case Games.UnicornDaVincisFruits:
                case Games.UnicornDaVincisDice:
                case Games.UnicornFrootMachine:
                case Games.UnicornFruitIslandChristmas:
                case Games.UnicornDiceMachine:
                case Games.UnicornFruitIslandGems:
                case Games.UnicornSupremeSevens:
                    return MatrixVegasDice.GetHelpConfigV3();
                case Games.UnicornReelDice:
                case Games.UnicornRainbowSevens:
                case Games.UnicornThunderFruits:
                    return MatrixReelDice.GetHelpConfigV3();
                case Games.UnicornWildParadise:
                case Games.UnicornTheCrownFruit:
                case Games.UnicornFruitMagic:
                case Games.UnicornDynamiteRun:
                case Games.UnicornDynamiteRunPlatinum:
                    return MatrixWildParadice.GetHelpConfigV3();
                case Games.UnicornHavanaDice:
                case Games.UnicornCasinoFruits:
                case Games.UnicornReelyWildReels:
                    return MatrixHavanaDice.GetHelpConfigV3();
                case Games.UnicornTwentyFruits:
                case Games.UnicornTwentyDice:
                case Games.UnicornMoneyStandard:
                case Games.UnicornMoneyStandardDice:
                    return MatrixTwentyFruits.GetHelpConfigV3();
                case Games.UnicornIslandRespins:
                    return MatrixIslandRespins2.GetHelpConfigV3();
                case Games.UnicornDiceRespins:
                    return MatrixIslandRespins.GetHelpConfigV3();
                case Games.UnicornGreatWhale:
                    return MatrixGreatWhale.GetHelpConfigV3();
                case Games.Unicorn20MegaFlames:
                case Games.Unicorn20DiceFlames:
                    return Matrix20MegaFlames.GetHelpConfigV3();
                case Games.UnicornMiniMegaCash:
                case Games.UnicornDiceMegaCash:
                    return MatrixMiniMegaCash.GetHelpConfigV3();
                case Games.UnicornSurfinHeat:
                    return MatrixSurfinHeat.GetHelpConfigV3();
                case Games.Unicorn40MegaFlames:
                case Games.Unicorn40DiceFlames:
                    return Matrix40MegaFlames.GetHelpConfigV3();
                case Games.UnicornWinterFruits:
                case Games.Unicorn40HotStrike:
                case Games.Unicorn40HotStrikeDice:
                    return MatrixWinterFruits.GetHelpConfigV3();
                case Games.UnicornFastFruits:
                    return MatrixFastFruits.GetHelpConfigV3();
                case Games.UnicornMoneyStandardWild:
                    return MatrixMoneyStandardWild.GetHelpConfigV3();
                case Games.Unicorn20HotStrike:
                case Games.UnicornDoubleWildDice:
                case Games.Unicorn20HotStrikeDice:
                    return Matrix20HotStrike.GetHelpConfigV3();
                case Games.Unicorn20HotStrikeJackpot:
                case Games.Unicorn20HotStrikeDiceJackpot:
                    return Matrix20HotStrikeJackpot.GetHelpConfigV3();
                case Games.UnicornCoyoteSevens:
                case Games.UnicornCoyoteDice:
                case Games.UnicornSavanaFruits:
                    return MatrixCoyoteSevens.GetHelpConfigV3();
                case Games.UnicornFruitWildLines:
                case Games.UnicornDiceWildLines:
                    return MatrixFruitWildLines.GetHelpConfigV3();
                case Games.Unicorn40FruitReels:
                    return Matrix40FruitReels.GetHelpConfigV3();
                case Games.Unicorn5HotStrike:
                case Games.Unicorn5HotStrikeDice:
                    return Matrix5HotStrike.GetHelpConfigV3();
                case Games.UnicornBikiniFruits:
                case Games.UnicornBikiniDice:
                    return MatrixBikiniFruits.GetHelpConfigV3();
                case Games.UnicornQueenOfPyramids:
                    return MatrixQueenOfPyramids.GetHelpConfigV3();
                case Games.UnicornFireStars:
                case Games.UnicornStarDiceChristmas:
                    return MatrixFireStars.GetHelpConfigV3();
                case Games.UnicornSimplySevens:
                case Games.UnicornSimplySevensDice:
                case Games.UnicornLavaDiamonds:
                case Games.UnicornLavaDice:
                case Games.UnicornLavaDiamondsChristmas:
                case Games.UnicornSimplySevensDiceEaster:
                case Games.UnicornBunnyDice:
                    return MatrixSimplySevens.GetHelpConfigV3();
                case Games.Unicorn10JingleFruits:
                    return Matrix10JingleFruits.GetHelpConfigV3();
                case Games.UnicornGoldLine:
                case Games.UnicornGoldLineDice:
                    return MatrixGoldLine.GetHelpConfigV3();
                case Games.UnicornChristmasPresents:
                case Games.UnicornMisterLuck:
                    return MatrixChristmasPresents.GetHelpConfigV3();
                case Games.UnicornBigSpinSevens:
                    return MatrixBigSpinSevens.GetHelpConfigV3();
                case Games.UnicornBuffaloSevens:
                case Games.UnicornBuffaloDice:
                case Games.UnicornTripleStackedDiamonds:
                    return MatrixBuffaloSevens.GetHelpConfigV3();
                case Games.UnicornFrootClassic:
                    return MatrixFrootClassic.GetHelpConfigV3();
                case Games.UnicornStickyHot:
                    return MatrixStickyHot.GetHelpConfigV3();
                case Games.UnicornPumpkinHorror:
                case Games.UnicornWildHoppers:
                    return MatrixPumpkinHorror.GetHelpConfigV3();
                case Games.UnicornHitLine:
                case Games.UnicornHitLineDice:
                    return MatrixHitLine.GetHelpConfigV3();
                case Games.Unicorn20SuperFlames:
                    return Matrix20SuperFlames.GetHelpConfigV3();
                case Games.UnicornEpicMegaCash:
                    return MatrixEpicMegaCash.GetHelpConfigV3();
                case Games.UnicornBigHitSevens:
                    return MatrixBigHitSevens.GetHelpConfigV3();
                default:
                    return null;
            }
        }

        public static ReelsV3 GetGameReelsV3(Games game)
        {
            switch (game)
            {
                case Games.UnicornVegasDice:
                case Games.UnicornFruitIsland:
                case Games.UnicornDaVincisFruits:
                case Games.UnicornDaVincisDice:
                case Games.UnicornFrootMachine:
                case Games.UnicornFruitIslandChristmas:
                case Games.UnicornDiceMachine:
                case Games.UnicornFruitIslandGems:
                case Games.UnicornSupremeSevens:
                    return new ReelsV3 { regular = MatrixVegasDice.GetFakeReels() };
                case Games.UnicornReelDice:
                case Games.UnicornRainbowSevens:
                case Games.UnicornThunderFruits:
                    return new ReelsV3 { regular = MatrixReelDice.GetFakeReels() };
                case Games.UnicornWildParadise:
                case Games.UnicornTheCrownFruit:
                case Games.UnicornFruitMagic:
                case Games.UnicornDynamiteRun:
                case Games.UnicornDynamiteRunPlatinum:
                    return new ReelsV3 { regular = MatrixWildParadice.GetFakeReels() };
                case Games.UnicornHavanaDice:
                case Games.UnicornCasinoFruits:
                case Games.UnicornReelyWildReels:
                    return new ReelsV3 { regular = MatrixHavanaDice.GetFakeReels() };
                case Games.UnicornTwentyFruits:
                case Games.UnicornTwentyDice:
                case Games.UnicornMoneyStandard:
                case Games.UnicornMoneyStandardDice:
                    return new ReelsV3 { regular = MatrixTwentyFruits.GetFakeReels() };
                case Games.UnicornIslandRespins:
                case Games.UnicornDiceRespins:
                    return new ReelsV3 { regular = MatrixIslandRespins.GetFakeReels(), freeSpin = MatrixIslandRespins.GetGratisFakeReels() };
                case Games.UnicornGreatWhale:
                    return new ReelsV3 { regular = MatrixGreatWhale.GetFakeReels(), freeSpin = MatrixGreatWhale.GetGratisFakeReels() };
                case Games.Unicorn20MegaFlames:
                case Games.Unicorn20DiceFlames:
                    return new ReelsV3 { regular = Matrix20MegaFlames.GetFakeReels() };
                case Games.UnicornMiniMegaCash:
                case Games.UnicornDiceMegaCash:
                    return new ReelsV3 { regular = MatrixMiniMegaCash.GetFakeReels() };
                case Games.UnicornSurfinHeat:
                    return new ReelsV3 { regular = MatrixSurfinHeat.GetFakeReels(), freeSpin = MatrixSurfinHeat.GetFakeReels() };
                case Games.Unicorn40MegaFlames:
                case Games.Unicorn40DiceFlames:
                    return new ReelsV3 { regular = Matrix40MegaFlames.GetFakeReels() };
                case Games.UnicornWinterFruits:
                case Games.Unicorn40HotStrike:
                case Games.Unicorn40HotStrikeDice:
                    return new ReelsV3 { regular = MatrixWinterFruits.GetFakeReels() };
                case Games.UnicornFastFruits:
                    return new ReelsV3 { regular = MatrixFastFruits.GetFakeReels() };
                case Games.UnicornMoneyStandardWild:
                    return new ReelsV3 { regular = MatrixMoneyStandardWild.GetFakeReels() };
                case Games.Unicorn20HotStrike:
                case Games.UnicornDoubleWildDice:
                case Games.Unicorn20HotStrikeDice:
                    return new ReelsV3 { regular = Matrix20HotStrike.GetFakeReels() };
                case Games.Unicorn20HotStrikeJackpot:
                case Games.Unicorn20HotStrikeDiceJackpot:
                    return new ReelsV3 { regular = Matrix20HotStrikeJackpot.GetFakeReels() };
                case Games.UnicornCoyoteSevens:
                case Games.UnicornCoyoteDice:
                case Games.UnicornSavanaFruits:
                    return new ReelsV3 { regular = MatrixCoyoteSevens.GetFakeReels() };
                case Games.UnicornFruitWildLines:
                case Games.UnicornDiceWildLines:
                    return new ReelsV3 { regular = MatrixFruitWildLines.GetFakeReels() };
                case Games.Unicorn40FruitReels:
                    return new ReelsV3 { regular = Matrix40FruitReels.GetFakeReels() };
                case Games.Unicorn5HotStrike:
                case Games.Unicorn5HotStrikeDice:
                    return new ReelsV3 { regular = Matrix5HotStrike.GetFakeReels() };
                case Games.UnicornBikiniFruits:
                case Games.UnicornBikiniDice:
                    return new ReelsV3 { regular = MatrixBikiniFruits.GetFakeReels(), freeSpin = MatrixBikiniFruits.GetFakeReels() };
                case Games.UnicornQueenOfPyramids:
                    return new ReelsV3 { regular = MatrixQueenOfPyramids.GetFakeReels(), freeSpin = MatrixQueenOfPyramids.GetFakeReels() };
                case Games.UnicornFireStars:
                case Games.UnicornStarDiceChristmas:
                    return new ReelsV3 { regular = MatrixFireStars.GetFakeReels() };
                case Games.UnicornSimplySevens:
                case Games.UnicornSimplySevensDice:
                case Games.UnicornLavaDiamonds:
                case Games.UnicornLavaDice:
                case Games.UnicornLavaDiamondsChristmas:
                case Games.UnicornSimplySevensDiceEaster:
                case Games.UnicornBunnyDice:
                    return new ReelsV3 { regular = MatrixSimplySevens.GetFakeReels() };
                case Games.Unicorn10JingleFruits:
                    return new ReelsV3 { regular = Matrix10JingleFruits.GetFakeReels() };
                case Games.UnicornGoldLine:
                case Games.UnicornGoldLineDice:
                    return new ReelsV3 { regular = MatrixGoldLine.GetFakeReels() };
                case Games.UnicornChristmasPresents:
                case Games.UnicornMisterLuck:
                    return new ReelsV3 { regular = MatrixChristmasPresents.GetFakeReels() };
                case Games.UnicornBigSpinSevens:
                    return new ReelsV3 { regular = MatrixBigSpinSevens.GetFakeReels() };
                case Games.UnicornBuffaloSevens:
                case Games.UnicornBuffaloDice:
                case Games.UnicornTripleStackedDiamonds:
                    return new ReelsV3 { regular = MatrixBuffaloSevens.GetFakeReels() };
                case Games.UnicornFrootClassic:
                    return new ReelsV3 { regular = MatrixFrootClassic.GetFakeReels() };
                case Games.UnicornStickyHot:
                    return new ReelsV3 { regular = MatrixStickyHot.GetFakeReels() };
                case Games.UnicornPumpkinHorror:
                case Games.UnicornWildHoppers:
                    return new ReelsV3 { regular = MatrixPumpkinHorror.GetFakeReels() };
                case Games.UnicornHitLine:
                case Games.UnicornHitLineDice:
                    return new ReelsV3 { regular = MatrixHitLine.GetFakeReels() };
                case Games.Unicorn20SuperFlames:
                    return new ReelsV3 { regular = Matrix20SuperFlames.GetFakeReels() };
                case Games.UnicornEpicMegaCash:
                    return new ReelsV3 { regular = MatrixEpicMegaCash.GetFakeReels() };
                case Games.UnicornBigHitSevens:
                    return new ReelsV3 { regular = MatrixBigHitSevens.GetFakeReels() };
                default:
                    return null;
            }
        }

        public static int GetLinesByIndexV3(Games game, int index)
        {
            int[] playLines;
            switch (game)
            {
                case Games.UnicornVegasDice:
                case Games.UnicornFruitIsland:
                case Games.UnicornDaVincisFruits:
                case Games.UnicornDaVincisDice:
                case Games.UnicornFrootMachine:
                case Games.UnicornFruitIslandChristmas:
                case Games.UnicornDiceMachine:
                case Games.UnicornFruitIslandGems:
                case Games.UnicornSupremeSevens:
                    playLines = MatrixVegasDice.PlayLines;
                    break;
                case Games.UnicornReelDice:
                case Games.UnicornRainbowSevens:
                case Games.UnicornThunderFruits:
                    playLines = MatrixReelDice.PlayLines;
                    break;
                case Games.UnicornWildParadise:
                case Games.UnicornTheCrownFruit:
                case Games.UnicornFruitMagic:
                case Games.UnicornDynamiteRun:
                case Games.UnicornDynamiteRunPlatinum:
                    playLines = MatrixWildParadice.PlayLines;
                    break;
                case Games.UnicornHavanaDice:
                case Games.UnicornCasinoFruits:
                case Games.UnicornReelyWildReels:
                    playLines = MatrixHavanaDice.PlayLines;
                    break;
                case Games.UnicornTwentyFruits:
                case Games.UnicornTwentyDice:
                case Games.UnicornMoneyStandard:
                case Games.UnicornMoneyStandardDice:
                    playLines = MatrixTwentyFruits.PlayLines;
                    break;
                case Games.UnicornIslandRespins:
                    playLines = MatrixIslandRespins2.PlayLines;
                    break;
                case Games.UnicornDiceRespins:
                    playLines = MatrixIslandRespins.PlayLines;
                    break;
                case Games.UnicornGreatWhale:
                    playLines = MatrixGreatWhale.PlayLines;
                    break;
                case Games.Unicorn20MegaFlames:
                case Games.Unicorn20DiceFlames:
                    playLines = Matrix20MegaFlames.PlayLines;
                    break;
                case Games.UnicornMiniMegaCash:
                case Games.UnicornDiceMegaCash:
                    playLines = MatrixMiniMegaCash.PlayLines;
                    break;
                case Games.UnicornSurfinHeat:
                    playLines = MatrixSurfinHeat.PlayLines;
                    break;
                case Games.Unicorn40MegaFlames:
                case Games.Unicorn40DiceFlames:
                    playLines = Matrix40MegaFlames.PlayLines;
                    break;
                case Games.UnicornWinterFruits:
                case Games.Unicorn40HotStrike:
                case Games.Unicorn40HotStrikeDice:
                    playLines = MatrixWinterFruits.PlayLines;
                    break;
                case Games.UnicornFastFruits:
                    playLines = MatrixFastFruits.PlayLines;
                    break;
                case Games.UnicornMoneyStandardWild:
                    playLines = MatrixMoneyStandardWild.PlayLines;
                    break;
                case Games.Unicorn20HotStrike:
                case Games.UnicornDoubleWildDice:
                case Games.Unicorn20HotStrikeDice:
                    playLines = Matrix20HotStrike.PlayLines;
                    break;
                case Games.Unicorn20HotStrikeJackpot:
                case Games.Unicorn20HotStrikeDiceJackpot:
                    playLines = Matrix20HotStrikeJackpot.PlayLines;
                    break;
                case Games.UnicornCoyoteSevens:
                case Games.UnicornCoyoteDice:
                case Games.UnicornSavanaFruits:
                    playLines = MatrixCoyoteSevens.PlayLines;
                    break;
                case Games.UnicornFruitWildLines:
                case Games.UnicornDiceWildLines:
                    playLines = MatrixFruitWildLines.PlayLines;
                    break;
                case Games.Unicorn40FruitReels:
                    playLines = Matrix40FruitReels.PlayLines;
                    break;
                case Games.Unicorn5HotStrike:
                case Games.Unicorn5HotStrikeDice:
                    playLines = Matrix5HotStrike.PlayLines;
                    break;
                case Games.UnicornBikiniFruits:
                case Games.UnicornQueenOfPyramids:
                case Games.UnicornBikiniDice:
                    playLines = MatrixBikiniFruits.PlayLines;
                    break;
                case Games.UnicornFireStars:
                case Games.UnicornStarDiceChristmas:
                    playLines = MatrixFireStars.PlayLines;
                    break;
                case Games.UnicornSimplySevens:
                case Games.UnicornSimplySevensDice:
                case Games.UnicornLavaDiamonds:
                case Games.UnicornLavaDice:
                case Games.UnicornLavaDiamondsChristmas:
                case Games.UnicornSimplySevensDiceEaster:
                case Games.UnicornBunnyDice:
                    playLines = MatrixSimplySevens.PlayLines;
                    break;
                case Games.Unicorn10JingleFruits:
                    playLines = Matrix10JingleFruits.PlayLines;
                    break;
                case Games.UnicornGoldLine:
                case Games.UnicornGoldLineDice:
                    playLines = MatrixGoldLine.PlayLines;
                    break;
                case Games.UnicornChristmasPresents:
                case Games.UnicornMisterLuck:
                    playLines = MatrixChristmasPresents.PlayLines;
                    break;
                case Games.UnicornBigSpinSevens:
                    playLines = MatrixBigSpinSevens.PlayLines;
                    break;
                case Games.UnicornBuffaloSevens:
                case Games.UnicornBuffaloDice:
                case Games.UnicornTripleStackedDiamonds:
                    playLines = MatrixBuffaloSevens.PlayLines;
                    break;
                case Games.UnicornFrootClassic:
                    playLines = MatrixFrootClassic.PlayLines;
                    break;
                case Games.UnicornStickyHot:
                    playLines = MatrixStickyHot.PlayLines;
                    break;
                case Games.UnicornPumpkinHorror:
                case Games.UnicornWildHoppers:
                    playLines = MatrixPumpkinHorror.PlayLines;
                    break;
                case Games.UnicornHitLine:
                case Games.UnicornHitLineDice:
                    playLines = MatrixHitLine.PlayLines;
                    break;
                case Games.Unicorn20SuperFlames:
                    playLines = Matrix20SuperFlames.PlayLines;
                    break;
                case Games.UnicornEpicMegaCash:
                    playLines = MatrixEpicMegaCash.PlayLines;
                    break;
                case Games.UnicornBigHitSevens:
                    playLines = MatrixBigHitSevens.PlayLines;
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
                case Games.UnicornVegasDice:
                case Games.UnicornFruitIsland:
                case Games.UnicornDaVincisFruits:
                case Games.UnicornDaVincisDice:
                case Games.UnicornFrootMachine:
                case Games.UnicornFruitIslandChristmas:
                case Games.UnicornDiceMachine:
                case Games.UnicornFruitIslandGems:
                case Games.UnicornSupremeSevens:
                    return MatrixVegasDice.PlayLines;
                case Games.UnicornReelDice:
                case Games.UnicornRainbowSevens:
                case Games.UnicornThunderFruits:
                    return MatrixReelDice.PlayLines;
                case Games.UnicornWildParadise:
                case Games.UnicornTheCrownFruit:
                case Games.UnicornFruitMagic:
                case Games.UnicornDynamiteRun:
                case Games.UnicornDynamiteRunPlatinum:
                    return MatrixWildParadice.PlayLines;
                case Games.UnicornHavanaDice:
                case Games.UnicornCasinoFruits:
                case Games.UnicornReelyWildReels:
                    return MatrixHavanaDice.PlayLines;
                case Games.UnicornTwentyFruits:
                case Games.UnicornTwentyDice:
                case Games.UnicornMoneyStandard:
                case Games.UnicornMoneyStandardDice:
                    return MatrixTwentyFruits.PlayLines;
                case Games.UnicornIslandRespins:
                    return MatrixIslandRespins2.PlayLines;
                case Games.UnicornDiceRespins:
                    return MatrixIslandRespins.PlayLines;
                case Games.UnicornGreatWhale:
                    return MatrixGreatWhale.PlayLines;
                case Games.Unicorn20MegaFlames:
                case Games.Unicorn20DiceFlames:
                    return Matrix20MegaFlames.PlayLines;
                case Games.UnicornMiniMegaCash:
                case Games.UnicornDiceMegaCash:
                    return MatrixMiniMegaCash.PlayLines;
                case Games.UnicornSurfinHeat:
                    return MatrixSurfinHeat.PlayLines;
                case Games.Unicorn40MegaFlames:
                case Games.Unicorn40DiceFlames:
                    return Matrix40MegaFlames.PlayLines;
                case Games.UnicornWinterFruits:
                case Games.Unicorn40HotStrike:
                case Games.Unicorn40HotStrikeDice:
                    return MatrixWinterFruits.PlayLines;
                case Games.UnicornFastFruits:
                    return MatrixFastFruits.PlayLines;
                case Games.UnicornMoneyStandardWild:
                    return MatrixMoneyStandardWild.PlayLines;
                case Games.Unicorn20HotStrike:
                case Games.UnicornDoubleWildDice:
                case Games.Unicorn20HotStrikeDice:
                    return Matrix20HotStrike.PlayLines;
                case Games.Unicorn20HotStrikeJackpot:
                case Games.Unicorn20HotStrikeDiceJackpot:
                    return Matrix20HotStrikeJackpot.PlayLines;
                case Games.UnicornCoyoteSevens:
                case Games.UnicornCoyoteDice:
                case Games.UnicornSavanaFruits:
                    return MatrixCoyoteSevens.PlayLines;
                case Games.UnicornFruitWildLines:
                case Games.UnicornDiceWildLines:
                    return MatrixFruitWildLines.PlayLines;
                case Games.Unicorn40FruitReels:
                    return Matrix40FruitReels.PlayLines;
                case Games.Unicorn5HotStrike:
                case Games.Unicorn5HotStrikeDice:
                    return Matrix5HotStrike.PlayLines;
                case Games.UnicornBikiniFruits:
                case Games.UnicornQueenOfPyramids:
                case Games.UnicornBikiniDice:
                    return MatrixBikiniFruits.PlayLines;
                case Games.UnicornFireStars:
                case Games.UnicornStarDiceChristmas:
                    return MatrixFireStars.PlayLines;
                case Games.UnicornSimplySevens:
                case Games.UnicornSimplySevensDice:
                case Games.UnicornLavaDiamonds:
                case Games.UnicornLavaDice:
                case Games.UnicornLavaDiamondsChristmas:
                case Games.UnicornSimplySevensDiceEaster:
                case Games.UnicornBunnyDice:
                    return MatrixSimplySevens.PlayLines;
                case Games.Unicorn10JingleFruits:
                    return Matrix10JingleFruits.PlayLines;
                case Games.UnicornGoldLine:
                case Games.UnicornGoldLineDice:
                    return MatrixGoldLine.PlayLines;
                case Games.UnicornChristmasPresents:
                case Games.UnicornMisterLuck:
                    return MatrixChristmasPresents.PlayLines;
                case Games.UnicornBigSpinSevens:
                    return MatrixBigSpinSevens.PlayLines;
                case Games.UnicornBuffaloSevens:
                case Games.UnicornBuffaloDice:
                case Games.UnicornTripleStackedDiamonds:
                    return MatrixBuffaloSevens.PlayLines;
                case Games.UnicornFrootClassic:
                    return MatrixFrootClassic.PlayLines;
                case Games.UnicornStickyHot:
                    return MatrixStickyHot.PlayLines;
                case Games.UnicornPumpkinHorror:
                case Games.UnicornWildHoppers:
                    return MatrixPumpkinHorror.PlayLines;
                case Games.UnicornHitLine:
                case Games.UnicornHitLineDice:
                    return MatrixHitLine.PlayLines;
                case Games.Unicorn20SuperFlames:
                    return Matrix20SuperFlames.PlayLines;
                case Games.UnicornEpicMegaCash:
                    return MatrixEpicMegaCash.PlayLines;
                case Games.UnicornBigHitSevens:
                    return MatrixBigHitSevens.PlayLines;
                default:
                    return null;
            }
        }

        public static int GetIndexForLinesV3(Games game, int lines)
        {
            switch (game)
            {
                case Games.UnicornVegasDice:
                case Games.UnicornFruitIsland:
                case Games.UnicornDaVincisFruits:
                case Games.UnicornDaVincisDice:
                case Games.UnicornFrootMachine:
                case Games.UnicornFruitIslandChristmas:
                case Games.UnicornDiceMachine:
                case Games.UnicornFruitIslandGems:
                case Games.UnicornSupremeSevens:
                    return Array.IndexOf(MatrixVegasDice.PlayLines, lines);
                case Games.UnicornReelDice:
                case Games.UnicornRainbowSevens:
                case Games.UnicornThunderFruits:
                    return Array.IndexOf(MatrixReelDice.PlayLines, lines);
                case Games.UnicornWildParadise:
                case Games.UnicornTheCrownFruit:
                case Games.UnicornFruitMagic:
                case Games.UnicornDynamiteRun:
                case Games.UnicornDynamiteRunPlatinum:
                    return Array.IndexOf(MatrixWildParadice.PlayLines, lines);
                case Games.UnicornHavanaDice:
                case Games.UnicornCasinoFruits:
                case Games.UnicornReelyWildReels:
                    return Array.IndexOf(MatrixHavanaDice.PlayLines, lines);
                case Games.UnicornTwentyFruits:
                case Games.UnicornTwentyDice:
                case Games.UnicornMoneyStandard:
                case Games.UnicornMoneyStandardDice:
                    return Array.IndexOf(MatrixTwentyFruits.PlayLines, lines);
                case Games.UnicornIslandRespins:
                    return Array.IndexOf(MatrixIslandRespins2.PlayLines, lines);
                case Games.UnicornDiceRespins:
                    return Array.IndexOf(MatrixIslandRespins.PlayLines, lines);
                case Games.UnicornGreatWhale:
                    return Array.IndexOf(MatrixGreatWhale.PlayLines, lines);
                case Games.Unicorn20MegaFlames:
                case Games.Unicorn20DiceFlames:
                    return Array.IndexOf(Matrix20MegaFlames.PlayLines, lines);
                case Games.UnicornMiniMegaCash:
                case Games.UnicornDiceMegaCash:
                    return Array.IndexOf(MatrixMiniMegaCash.PlayLines, lines);
                case Games.UnicornSurfinHeat:
                    return Array.IndexOf(MatrixSurfinHeat.PlayLines, lines);
                case Games.Unicorn40MegaFlames:
                case Games.Unicorn40DiceFlames:
                    return Array.IndexOf(Matrix40MegaFlames.PlayLines, lines);
                case Games.UnicornWinterFruits:
                case Games.Unicorn40HotStrike:
                case Games.Unicorn40HotStrikeDice:
                    return Array.IndexOf(MatrixWinterFruits.PlayLines, lines);
                case Games.UnicornFastFruits:
                    return Array.IndexOf(MatrixFastFruits.PlayLines, lines);
                case Games.UnicornMoneyStandardWild:
                    return Array.IndexOf(MatrixMoneyStandardWild.PlayLines, lines);
                case Games.Unicorn20HotStrike:
                case Games.UnicornDoubleWildDice:
                case Games.Unicorn20HotStrikeDice:
                    return Array.IndexOf(Matrix20HotStrike.PlayLines, lines);
                case Games.Unicorn20HotStrikeJackpot:
                case Games.Unicorn20HotStrikeDiceJackpot:
                    return Array.IndexOf(Matrix20HotStrikeJackpot.PlayLines, lines);
                case Games.UnicornCoyoteSevens:
                case Games.UnicornCoyoteDice:
                case Games.UnicornSavanaFruits:
                    return Array.IndexOf(MatrixCoyoteSevens.PlayLines, lines);
                case Games.UnicornFruitWildLines:
                case Games.UnicornDiceWildLines:
                    return Array.IndexOf(MatrixFruitWildLines.PlayLines, lines);
                case Games.Unicorn40FruitReels:
                    return Array.IndexOf(Matrix40FruitReels.PlayLines, lines);
                case Games.Unicorn5HotStrike:
                case Games.Unicorn5HotStrikeDice:
                    return Array.IndexOf(Matrix5HotStrike.PlayLines, lines);
                case Games.UnicornBikiniFruits:
                case Games.UnicornQueenOfPyramids:
                case Games.UnicornBikiniDice:
                    return Array.IndexOf(MatrixBikiniFruits.PlayLines, lines);
                case Games.UnicornFireStars:
                case Games.UnicornStarDiceChristmas:
                    return Array.IndexOf(MatrixFireStars.PlayLines, lines);
                case Games.UnicornSimplySevens:
                case Games.UnicornSimplySevensDice:
                case Games.UnicornLavaDiamonds:
                case Games.UnicornLavaDice:
                case Games.UnicornLavaDiamondsChristmas:
                case Games.UnicornSimplySevensDiceEaster:
                case Games.UnicornBunnyDice:
                    return Array.IndexOf(MatrixSimplySevens.PlayLines, lines);
                case Games.Unicorn10JingleFruits:
                    return Array.IndexOf(Matrix10JingleFruits.PlayLines, lines);
                case Games.UnicornGoldLine:
                case Games.UnicornGoldLineDice:
                    return Array.IndexOf(MatrixGoldLine.PlayLines, lines);
                case Games.UnicornChristmasPresents:
                case Games.UnicornMisterLuck:
                    return Array.IndexOf(MatrixChristmasPresents.PlayLines, lines);
                case Games.UnicornBigSpinSevens:
                    return Array.IndexOf(MatrixBigSpinSevens.PlayLines, lines);
                case Games.UnicornBuffaloSevens:
                case Games.UnicornBuffaloDice:
                case Games.UnicornTripleStackedDiamonds:
                    return Array.IndexOf(MatrixBuffaloSevens.PlayLines, lines);
                case Games.UnicornFrootClassic:
                    return Array.IndexOf(MatrixFrootClassic.PlayLines, lines);
                case Games.UnicornStickyHot:
                    return Array.IndexOf(MatrixStickyHot.PlayLines, lines);
                case Games.UnicornPumpkinHorror:
                case Games.UnicornWildHoppers:
                    return Array.IndexOf(MatrixPumpkinHorror.PlayLines, lines);
                case Games.UnicornHitLine:
                case Games.UnicornHitLineDice:
                    return Array.IndexOf(MatrixHitLine.PlayLines, lines);
                case Games.Unicorn20SuperFlames:
                    return Array.IndexOf(Matrix20SuperFlames.PlayLines, lines);
                case Games.UnicornEpicMegaCash:
                    return Array.IndexOf(MatrixEpicMegaCash.PlayLines, lines);
                case Games.UnicornBigHitSevens:
                    return Array.IndexOf(MatrixBigHitSevens.PlayLines, lines);
                default:
                    return -1;
            }
        }

        public static SlotDataResV3 GetNonWinningCombination(Games game, int bet, int numberOfLines, int gratisGamesLeft)
        {
            switch (game)
            {
                case Games.UnicornVegasDice:
                case Games.UnicornFruitIsland:
                case Games.UnicornDaVincisFruits:
                case Games.UnicornDaVincisDice:
                case Games.UnicornFrootMachine:
                case Games.UnicornFruitIslandChristmas:
                case Games.UnicornDiceMachine:
                case Games.UnicornFruitIslandGems:
                case Games.UnicornSupremeSevens:
                    return GameVegasDiceConversion.ToSlotDataResV3(GameVegasDiceConversion.GetNonWinningCombination(bet, numberOfLines));
                case Games.UnicornReelDice:
                case Games.UnicornRainbowSevens:
                case Games.UnicornThunderFruits:
                    return GameReelDiceConversion.ToSlotDataResV3(GameReelDiceConversion.GetNonWinningCombination(bet, numberOfLines));
                case Games.UnicornWildParadise:
                case Games.UnicornTheCrownFruit:
                case Games.UnicornFruitMagic:
                case Games.UnicornDynamiteRun:
                case Games.UnicornDynamiteRunPlatinum:
                    return GameWildParadiceConversion.ToSlotDataResV3(GameWildParadiceConversion.GetNonWinningCombination(bet, numberOfLines));
                case Games.UnicornHavanaDice:
                case Games.UnicornCasinoFruits:
                case Games.UnicornReelyWildReels:
                    return GameHavanaDiceConversion.ToSlotDataResV3(GameHavanaDiceConversion.GetNonWinningCombination(bet, numberOfLines));
                case Games.UnicornTwentyFruits:
                case Games.UnicornTwentyDice:
                case Games.UnicornMoneyStandard:
                case Games.UnicornMoneyStandardDice:
                    return GameTwentyFruitsConversion.ToSlotDataResV3(GameTwentyFruitsConversion.GetNonWinningCombination(bet, numberOfLines));
                case Games.UnicornIslandRespins:
                    return GameIslandRespinsConversion.ToSlotDataResV3(GameIslandRespinsConversion.GetNonWinningCombination2(bet, numberOfLines, gratisGamesLeft));
                case Games.UnicornDiceRespins:
                    return GameIslandRespinsConversion.ToSlotDataResV3(GameIslandRespinsConversion.GetNonWinningCombination(bet, numberOfLines, gratisGamesLeft));
                case Games.UnicornGreatWhale:
                    return GameGreatWhaleConversion.ToSlotDataResV3(GameGreatWhaleConversion.GetNonWinningCombination(bet, numberOfLines, gratisGamesLeft));
                case Games.Unicorn20MegaFlames:
                case Games.Unicorn20DiceFlames:
                    return Game20MegaFlamesConversion.ToSlotDataResV3(Game20MegaFlamesConversion.GetNonWinningCombination(bet, numberOfLines));
                case Games.UnicornMiniMegaCash:
                case Games.UnicornDiceMegaCash:
                    return GameMiniMegaCashConversion.ToSlotDataResV3(GameMiniMegaCashConversion.GetNonWinningCombination(bet, numberOfLines));
                case Games.UnicornSurfinHeat:
                    return GameMiniMegaCashConversion.ToSlotDataResV3(GameSurfinHeatConversion.GetNonWinningCombination(bet, numberOfLines));
                case Games.Unicorn40MegaFlames:
                case Games.Unicorn40DiceFlames:
                    return Game40MegaFlamesConversion.ToSlotDataResV3(Game40MegaFlamesConversion.GetNonWinningCombination(bet, numberOfLines));
                case Games.UnicornWinterFruits:
                case Games.Unicorn40HotStrike:
                case Games.Unicorn40HotStrikeDice:
                    return GameWinterFruitsConversion.ToSlotDataResV3(GameWinterFruitsConversion.GetNonWinningCombination(bet, numberOfLines));
                case Games.UnicornFastFruits:
                    return GameFastFruitsConversion.ToSlotDataResV3(GameFastFruitsConversion.GetNonWinningCombination(bet, numberOfLines));
                case Games.UnicornMoneyStandardWild:
                    return GameMoneyStandardWildConversion.ToSlotDataResV3(GameMoneyStandardWildConversion.GetNonWinningCombination(bet, numberOfLines));
                case Games.Unicorn20HotStrike:
                case Games.UnicornDoubleWildDice:
                case Games.Unicorn20HotStrikeDice:
                    return Game20HotStrikeConversion.ToSlotDataResV3(Game20HotStrikeConversion.GetNonWinningCombination(bet, numberOfLines));
                case Games.Unicorn20HotStrikeJackpot:
                case Games.Unicorn20HotStrikeDiceJackpot:
                    return Game20HotStrikeConversion.ToSlotDataResV3(Game20HotStrikeConversion.GetNonWinningCombinationJackpot(bet, numberOfLines));
                case Games.UnicornCoyoteSevens:
                case Games.UnicornCoyoteDice:
                case Games.UnicornSavanaFruits:
                    return GameCoyoteSevensConversion.ToSlotDataResV3(GameCoyoteSevensConversion.GetNonWinningCombination(bet, numberOfLines));
                case Games.UnicornFruitWildLines:
                case Games.UnicornDiceWildLines:
                    return GameFruitWildLinesConversion.ToSlotDataResV3(GameFruitWildLinesConversion.GetNonWinningCombination(bet, numberOfLines));
                case Games.Unicorn40FruitReels:
                    return Game40FruitReelsConversion.ToSlotDataResV3(Game40FruitReelsConversion.GetNonWinningCombination(bet, numberOfLines));
                case Games.Unicorn5HotStrike:
                case Games.Unicorn5HotStrikeDice:
                    return Game5HotStrikeConversion.ToSlotDataResV3(Game5HotStrikeConversion.GetNonWinningCombination(bet, numberOfLines));
                case Games.UnicornBikiniFruits:
                case Games.UnicornQueenOfPyramids:
                case Games.UnicornBikiniDice:
                    return GameBikiniFruitsConversion.ToSlotDataResV3(GameBikiniFruitsConversion.GetNonWinningCombination(bet, numberOfLines));
                case Games.UnicornFireStars:
                case Games.UnicornStarDiceChristmas:
                    return GameFireStarsConversion.ToSlotDataResV3(GameFireStarsConversion.GetNonWinningCombination(bet, numberOfLines));
                case Games.UnicornSimplySevens:
                case Games.UnicornSimplySevensDice:
                case Games.UnicornLavaDiamonds:
                case Games.UnicornLavaDice:
                case Games.UnicornLavaDiamondsChristmas:
                case Games.UnicornSimplySevensDiceEaster:
                case Games.UnicornBunnyDice:
                    return GameSimplySevensConversion.ToSlotDataResV3(GameSimplySevensConversion.GetNonWinningCombination(bet, numberOfLines));
                case Games.Unicorn10JingleFruits:
                    return Game10JingleFruitsConversion.ToSlotDataResV3(Game10JingleFruitsConversion.GetNonWinningCombination(bet, numberOfLines));
                case Games.UnicornGoldLine:
                case Games.UnicornGoldLineDice:
                    return GameGoldLineConversion.ToSlotDataResV3(GameGoldLineConversion.GetNonWinningCombination(bet));
                case Games.UnicornChristmasPresents:
                case Games.UnicornMisterLuck:
                    return GameChristmasPresentsConversion.ToSlotDataResV3(GameChristmasPresentsConversion.GetNonWinningCombination(bet, numberOfLines));
                case Games.UnicornBigSpinSevens:
                    return GameBigSpinSevensConversion.ToSlotDataResV3(GameBigSpinSevensConversion.GetNonWinningCombination(bet, numberOfLines));
                case Games.UnicornBuffaloSevens:
                case Games.UnicornBuffaloDice:
                case Games.UnicornTripleStackedDiamonds:
                    return GameBuffaloSevensConversion.ToSlotDataResV3(GameBuffaloSevensConversion.GetNonWinningCombination(bet, numberOfLines));
                case Games.UnicornFrootClassic:
                    return GameFrootClassicConversion.ToSlotDataResV3(GameFrootClassicConversion.GetNonWinningCombination(bet, numberOfLines));
                case Games.UnicornStickyHot:
                    return GameStickyHotConversion.ToSlotDataResV3(GameStickyHotConversion.GetNonWinningCombination(bet, numberOfLines));
                case Games.UnicornPumpkinHorror:
                case Games.UnicornWildHoppers:
                    return GamePumpkinHorrorConversion.ToSlotDataResV3(GamePumpkinHorrorConversion.GetNonWinningCombination(bet, numberOfLines));
                case Games.UnicornHitLine:
                case Games.UnicornHitLineDice:
                    return GameHitLineConversion.ToSlotDataResV3(GameHitLineConversion.GetNonWinningCombination(bet, numberOfLines));
                case Games.Unicorn20SuperFlames:
                    return Game20SuperFlamesConversion.ToSlotDataResV3(Game20SuperFlamesConversion.GetNonWinningCombination(bet, numberOfLines));
                case Games.UnicornEpicMegaCash:
                    return GameEpicMegaCashConversion.ToSlotDataResV3(GameEpicMegaCashConversion.GetNonWinningCombination(bet, numberOfLines));
                case Games.UnicornBigHitSevens:
                    return GameBigHitSevensConversion.ToSlotDataResV3(GameBigHitSevensConversion.GetNonWinningCombination(bet, numberOfLines));
                default:
                    return null;
            }
        }
    }
}
