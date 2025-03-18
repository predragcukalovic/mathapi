using Papi.GameServer.Utils.Enums;
using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using V4Converter.DTOs;
using V4Converter.Readers;

namespace V4Converter
{
    public static class ToV4Converter
    {
        public static GameConfig GetGameConfig(Games gameId)
        {
            string gameName = Enum.GetName(typeof(Games), gameId);
            return GameConfigReader.GetGameConfig(gameName);
        }

        private static List<Games> CONVERTED_GAMES = new List<Games> {
            Games.Wild27, Games.WildHot40Halloween, Games.CrystalHot40, Games.GoldenCrown,
            Games.VeryHot5, Games.WildHot40Blow,Games.KingOfThunder,
            Games.LollasWorld, Games.BookOfSpells2, Games.WildHot40FreeSpins,
            Games.EpicClover40, Games.BonusEpicCrown, Games.WildLuckyClover,
            Games.VeryHot5Extreme, Games.WinningClover5, Games.WinningClover5Extreme,
            Games.MegaHot10, Games.BetwoonHot5, Games.Wild5, Games.FruityForce40,
            Games.CoinSplash
            /*, Games.JokerTripleDouble, Games.MysticJungle, Games.BlowFruits40,
            Games.VintageFruits40, Games.BonusBells,Games.KingOfThunder,
            Games.ChilliRespin, Games.TurboFire, Games.SantasPresents, 
            Games.PiratesPapi, Games.FruitsAndStars20Deluxe, Games.WildHeat40,
            Games.WildSunburst, Games.ToxicHaze*/
        };

        public static List<Games> getConvertedGames()
        {
            return CONVERTED_GAMES;
        }

        public static long multiplyWinIfNeeded(int win, long bet, Games gameId)
        {
            if (CONVERTED_GAMES.Contains(gameId))
            {
                return win * bet;
            }
            else return win;
        }

        public static int getBet(Games gameId, long bet)
        {
            if (CONVERTED_GAMES.Contains(gameId))
            {
                return 1;
            }
            else if (bet > Int32.MaxValue)
            {
                throw new Exception("Bet higher than max int value for non-converted game");
            }
            else return (int)bet;
        }

        public static bool gameIsConverted(Games gameId)
        {
            return CONVERTED_GAMES.Contains(gameId);
        }

        public static SlotDataResV3 toSlotDataResV3(ICombination combination, Games gameId, long bet, int numOfGratisGames, bool isCurrentGameGratis)
        {
            GameConfig gameConfig = GetGameConfig(gameId);
            int[,] matrix = MatrixMapper.GetMatrix(gameConfig, combination, isCurrentGameGratis);
            V3MapperParams v3MapperParams = new V3MapperParams(combination, gameId, matrix, bet, isCurrentGameGratis, gameConfig, numOfGratisGames);
            object extraData = ExtraDataMapper.getExtraData(v3MapperParams);
            if (gameConfig.UseRecallForWins && ((dynamic)extraData).recall != null)
            {
                v3MapperParams.setMatrix(((dynamic)extraData).recall);
            }
            return new SlotDataResV3
            {
                win = combination.TotalWin * bet,
                symbols = MatrixMapper.ModifyMatrixIfNeeded(gameConfig, matrix),
                extra = extraData,
                wins = WinLinesMapper.getWinLines(v3MapperParams),
                gratisGame = combination.GratisGame
            };
        }

        public static HelpConfigV3<object> getGameHelpConfigV3(GameConfig gameConfig)
        {
            return new HelpConfigV3<object>
            {
                rtp = gameConfig.Rtp,
                symbols = HelpConfigMapper.getHelpSymbolConfigV3(gameConfig),
                lines = HelpConfigMapper.getHelpLineConfigV3(gameConfig)
            };
        }

        public static ReelsV3 getGameReelsV3(GameConfig gameConfig)
        {
            return new ReelsV3
            {
                regular = gameConfig.FakeReels,
                freeSpin = gameConfig.BonusFakeReels,
                respin = gameConfig.RespinFakeReels
            };
        }

        public static int[] getPlayLines(Games gameId)
        {
            GameConfig gameConfig = GetGameConfig(gameId);
            return gameConfig.PlayLines;
        }
        public static int GetLinesByIndexV3(Games gameId, int index)
        {
            int[] playLines = GetGameConfig(gameId).PlayLines;
            if (playLines.Length <= index)
            {
                return -1;
            }
            return playLines[index];
        }
        public static int GetIndexForLinesV3(Games gameId, int lines)
        {
            GameConfig gameConfig = GetGameConfig(gameId);
            return Array.IndexOf(gameConfig.PlayLines, lines);
        }

        /*public static SlotDataResV3 getNonWinningCombination(Games gameId)
        {
            GameConfig gameConfig = GetGameConfig(gameId);
            ICombination nonWinningCombination = new GenericCombination(gameConfig);
            return new SlotDataResV3
            {
                win = 0,
                symbols = MatrixMapper.GetMatrix(gameConfig, nonWinningCombination, false),
                extra = ExtraDataMapper.getExtraData(new V3MapperParams(nonWinningCombination, gameId, gameConfig.NonWinningCombinationMatrix, 1, false, gameConfig)),
                wins = new WinLineV3[0],
                gratisGame = false
            };
        }*/

        public static SlotDataResV3 getNonWinningCombination(Games gameId, int gratisGamesLeft)
        {
            GameConfig gameConfig = GetGameConfig(gameId);
            ICombination nonWinningCombination = new GenericCombination(gameConfig, gratisGamesLeft);
            return toSlotDataResV3(nonWinningCombination, gameId, 1, 0, false);
        }

        public static string getNonWinningCombinationSerialized(Games gameId)
        {
            return JsonConvert.SerializeObject(getNonWinningCombination(gameId, 0));
        }
    }
}

