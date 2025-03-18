using Papi.GameServer.Math.Contracts.StructuresV3;
using Papi.GameServer.Math.History.Models;
using Papi.GameServer.Utils.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Papi.GameServer.Math.History
{

    public class SpanHelper
    {
        #region Consts

        private static readonly int[,] _WinForLinesSpellbook =
        {
            {0, 0, 0, 0, 0},
            {0, 10, 100, 1000, 5000},
            {0, 5, 40, 400, 2000},
            {0, 5, 30, 100, 750},
            {0, 5, 30, 100, 750},
            {0, 0, 5, 40, 150},
            {0, 0, 5, 40, 150},
            {0, 0, 5, 25, 100},
            {0, 0, 5, 25, 100},
            {0, 0, 5, 25, 100},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}
        };

        #endregion

        #region Public Methods

        public static byte[] SpanSymbolsIfNeeded(int gameId, byte[] data, byte bonusElementId, int gameType, long win, byte[] scattrData)
        {
            byte[,] combination;
            const int fenixId = 0;
            const int bushId = 2;
            const int templarId = 12;
            const int cloverId = 0;
            switch (gameId)
            {
                case (int)Games.BookOfMayanGold:
                case (int)Games.DiceOfSpells:
                case (int)Games.BookOfSpellsDeluxe:
                case (int)Games.BookOfSpells:
                case (int)Games.BookOfSpellsV2:
                case (int)Games.BookOfBruno:
                case (int)Games.Spellbook:
                case (int)Games.BookOfLuxorDouble:
                case (int)Games.BookOfScorpionsDouble:
                case (int)Games.BookOfSpells2:
                case (int)Games.BookOfDread:
                case (int)Games.BookOfMozzart:
                case (int)Games.RedstoneBookOfScorpio:
                    var count = data.Where(x => x == bonusElementId).Count();
                    if (gameType == (byte)GameTypeEnum.FreeSpin && count > 0 && _WinForLinesSpellbook[bonusElementId, count - 1] > 0)
                    {
                        combination = GenerateMatrix(data, 3, 5);

                        for (var row = 0; row < 3; row++)
                        {
                            for (var column = 0; column < 5; column++)
                            {
                                if (combination[row, column] == bonusElementId)
                                {
                                    SetField(ref combination, 0, column, bonusElementId);
                                    SetField(ref combination, 1, column, bonusElementId);
                                    SetField(ref combination, 2, column, bonusElementId);
                                }
                            }
                        }
                        data = GenerateArray(combination);
                    }
                    break;
                case (int)Games.HotStars:
                case (int)Games.StarGems:
                case (int)Games.SpaceGuardians:
                case (int)Games.Starlight:
                case (int)Games.StarRunner:
                case (int)Games.CrystalWin:
                case (int)Games.CrystalJewels:
                case (int)Games.WinningStars:
                case (int)Games.VeryHot40Respin:
                case (int)Games.HotStarsChristmas:
                case (int)Games.TurboStars10:
                case (int)Games.TurboStars40:
                case (int)Games.TurboStars20:
                    combination = GenerateMatrix(data, 3, 5);
                    for (var row = 0; row < 3; row++)
                    {
                        for (var column = 0; column < 5; column++)
                        {
                            if ((column == 1 || column == 2 || column == 3) && (combination[row, column] == fenixId))
                            {
                                for (var i = 0; i < 3; i++)
                                {
                                    SetField(ref combination, i, column, fenixId);
                                }
                            }
                        }
                    }
                    data = GenerateArray(combination);
                    break;
                case (int)Games.DeepJungle:
                case (int)Games.BigBuddha:
                    if (win > 0)
                    {
                        combination = GenerateMatrix(data, 3, 5);
                        var fields = new int[2, 2];
                        var k = 0;
                        for (var row = 0; row < 3; row++)
                        {
                            for (var column = 0; column < 5; column++)
                            {
                                if ((column == 1 || column == 3) &&
                                    (combination[row, column] == bushId))
                                {
                                    fields[k, 0] = row;
                                    fields[k++, 1] = column;
                                }
                            }
                        }
                        for (var j = 0; j < k; j++)
                        {
                            var row = fields[j, 0];
                            var column = fields[j, 1];
                            for (var i = row - 1; i <= row + 1; i++)
                            {
                                SetField(ref combination, i, column - 1, bushId);
                                SetField(ref combination, i, column, bushId);
                                SetField(ref combination, i, column + 1, bushId);
                            }
                        }

                        data = GenerateArray(combination);
                    }
                    break;
                case (int)Games.TemplarsQuest:
                    combination = GenerateMatrix(data, 3, 5);
                    for (var row = 0; row < 3; row++)
                    {
                        for (var column = 0; column < 5; column++)
                        {
                            if ((column == 1 || column == 3) && (combination[row, column] == templarId))
                            {
                                for (var i = 0; i < 3; i++)
                                {
                                    SetField(ref combination, i, column, templarId);
                                }
                            }
                        }
                    }
                    data = GenerateArray(combination);
                    break;
                case (int)Games.BurstingHot5:
                case (int)Games.BurstingHot40:
                case (int)Games.GoldenCrown:
                case (int)Games.FireClover5:
                case (int)Games.WildCrown10:
                case (int)Games.FireClover40:
                case (int)Games.VeryHot5:
                case (int)Games.VeryHot40:
                case (int)Games.VeryHot40Dice:
                case (int)Games.BurstingHot40Mozzart:
                case (int)Games.GoldenCrownMaxbet:
                case (int)Games.FireDice40:
                case (int)Games.BurstingHot5Admiral:
                case (int)Games.GoldenCrown20BalkanBet:
                case (int)Games.VeryHot20:
                case (int)Games.WildDiamond10:
                case (int)Games.WildHeart5:
                case (int)Games.WildDice10:
                case (int)Games.AdmiralHot5:
                case (int)Games.GermaniaHot40:
                case (int)Games.WinXtip:
                case (int)Games.SuperCasinoCrown:
                case (int)Games.TopHot20:
                case (int)Games.WildPumpkin10:
                case (int)Games.GoldenCrownChristmas:
                case (int)Games.VeryHot5Christmas:
                case (int)Games.VeryHot40Christmas:
                case (int)Games.CrownFire5:
                case (int)Games.WildSanta10:
                case (int)Games.GoldenCrown40:
                case (int)Games.EpicCrown10:
                case (int)Games.Money5:
                case (int)Games.EpicCrown5:
                case (int)Games.HotLine1X:
                case (int)Games.GoldenVegas:
                case (int)Games.RedstoneChilliHot5:
                case (int)Games.RedstoneJollyPresents:
                case (int)Games.Redstone20WildCrown:
                case (int)Games.GrandpashabetCrown:
                case (int)Games.RedstoneHotRushCrownBurst:
                case (int)Games.MozzartHot5:
                case (int)Games.MozzartHot40:
                case (int)Games.CrownOfMozzart:
                case (int)Games.RedstoneLadyTripleFortune:
                case (int)Games.RedstoneSlotRoyale:
                case (int)Games.MajesticCrown10:
                case (int)Games.FortuneClover5:
                case (int)Games.PlayNetFortuneClover40:
                case (int)Games.PlayNetDiamondHeart5:
                case (int)Games.RedstoneVolcanoCrown:
                case (int)Games.PlayNetMajesticCrown20:
                case (int)Games.PlayNetDiamondHeart40:
                case (int)Games.FortuneParrot:
                case (int)Games.QuickWinCrown10:
                case (int)Games.PlayNetMajesticCrown20Xmas:
                case (int)Games.RedstoneFunkyFruits:
                case (int)Games.CrownsAndStars:
                case (int)Games.BetwoonHot5:
                    if (win > 0)
                    {
                        combination = GenerateMatrix(data, 3, 5);
                        var fields = new int[3, 3];
                        var k = 0;
                        for (var row = 0; row < 3; row++)
                        {
                            for (var column = 0; column < 5; column++)
                            {
                                if ((column == 1 || column == 2 || column == 3) &&
                                    (combination[row, column] == cloverId) && scattrData[0] != 255)
                                {
                                    fields[k, 0] = row;
                                    fields[k++, 1] = column;
                                }
                            }
                        }
                        for (var j = 0; j < k; j++)
                        {
                            var column = fields[j, 1];
                            for (var i = 0; i < 3; i++)
                            {
                                SetField(ref combination, i, column, cloverId);
                            }
                        }

                        data = GenerateArray(combination);
                    }
                    break;

                case (int)Games.WildJokerHot:
                case (int)Games.FashionNight:
                    if (win > 0)
                    {
                        combination = GenerateMatrix(data, 3, 5);
                        var fields = new int[3, 3];
                        var k = 0;
                        for (var row = 0; row < 3; row++)
                        {
                            for (var column = 1; column < 4; column++)
                            {
                                if ((combination[row, column] == 0 || combination[row, column] == 1) && scattrData[0] != 255)
                                {
                                    fields[k, 0] = row;
                                    fields[k, 1] = column;
                                    fields[k++, 2] = combination[row, column];
                                }
                            }
                        }
                        for (var j = 0; j < k; j++)
                        {
                            var column = fields[j, 1];
                            for (var i = 0; i < 3; i++)
                            {
                                SetField(ref combination, i, column, (byte)fields[j, 2]);
                            }
                        }
                        data = GenerateArray(combination);
                    }
                    break;
                case (int)Games.GoldenExplosion:
                    {
                        combination = GenerateMatrix(data, 3, 5);
                        var fields = new int[3, 3];
                        var k = 0;
                        for (var row = 0; row < 3; row++)
                        {
                            for (var column = 1; column < 4; column++)
                            {
                                if ((combination[row, column] <= 1 || combination[row, column] >= 12) && scattrData[0] != 255)
                                {
                                    fields[k, 0] = row;
                                    fields[k, 1] = column;
                                    fields[k++, 2] = combination[row, column];
                                }
                            }
                        }
                        for (var j = 0; j < k; j++)
                        {
                            var column = fields[j, 1];
                            for (var i = 0; i < 3; i++)
                            {
                                SetField(ref combination, i, column, (byte)fields[j, 2]);
                            }
                        }
                        data = GenerateArray(combination);
                    }
                    break;
                case (int)Games.TripleFieldsOfLuck:
                    if (win > 0)
                    {
                        combination = GenerateMatrix(data, 5, 3);
                        var fields = new int[3, 3];
                        var k = 0;
                        for (var row = 0; row < 3; row++)
                        {
                            for (var column = 0; column < 3; column++)
                            {
                                if (combination[row, column] == 0)
                                {
                                    fields[k, 0] = row;
                                    fields[k++, 1] = column;
                                }
                            }
                        }
                        for (var j = 0; j < k; j++)
                        {
                            var column = fields[j, 1];
                            for (var i = 0; i < 3; i++)
                            {
                                SetField(ref combination, i, column, 0);
                            }
                        }
                        data = GenerateArray(combination);
                    }
                    break;
                case (int)Games.EggspandingRush:
                    if (win > 0)
                    {
                        combination = GenerateMatrix(data, 3, 5);
                        var fields = new int[3, 3];
                        var k = 0;
                        for (var row = 0; row < 3; row++)
                        {
                            for (var column = 1; column < 4; column++)
                            {
                                if ((combination[row, column] == 0 || combination[row, column] >= 10) && scattrData[0] != 255)
                                {
                                    fields[k, 0] = row;
                                    fields[k, 1] = column;
                                    fields[k++, 2] = combination[row, column];
                                }
                            }
                        }
                        for (var j = 0; j < k; j++)
                        {
                            var column = fields[j, 1];
                            for (var i = 0; i < 3; i++)
                            {
                                SetField(ref combination, i, column, (byte)fields[j, 2]);
                            }
                        }
                        data = GenerateArray(combination);
                    }
                    break;
            }
            return data;
        }

        public static int ChangeNumOfLinesIfNeeded(int gameId, int numOfLines)
        {
            switch (gameId)
            {
                case (int)Games.BurningIce:
                case (int)Games.BurningIceDeluxe:
                case (int)Games.FlashingDice:
                case (int)Games.BurningIceGd:
                case (int)Games.MysteryJokerHot:
                case (int)Games.HeatingDice:
                case (int)Games.HeatingIce:
                case (int)Games.HeatingIceDeluxe:
                case (int)Games.Wild27:
                case (int)Games.RedstoneApollo27Classic:
                case (int)Games.PlayNet27WildStacks:
                case (int)Games.PlayNet27WildStacksXmas:
                case (int)Games.Redstone27DoubleFruit:
                case (int)Games.BetAndreasWild:
                    return 27;
                case (int)Games.VeryHot40Extreme:
                    return 40;
                case (int)Games.JokerTripleDouble:
                case (int)Games.HeatDouble:
                case (int)Games.BlazingHeat:
                case (int)Games.HotHotStereoWin:
                case (int)Games.CloverBlast5:
                case (int)Games.ChilliRespin:
                case (int)Games.RedstoneHotRushStarsDeluxe:
                case (int)Games.Redstone5WildFire:
                case (int)Games.TopHot5:
                case (int)Games.Wild5:
                    return 5;
                default:
                    return numOfLines;
            }
        }

        public static string SpanJsonSymbolsIfNeeded(string jsonData, int gameId) 
        {
            switch (gameId)
            {
                case (int)Games.CrystalHot40Max:
                case (int)Games.EpicClover40:
                case (int)Games.EpicClover100:
                case (int)Games.EpicDice100:
                case (int)Games.GoldenCrownMax:
                case (int)Games.BrilliantHeart:
                    var myData = JsonConvert.DeserializeObject<TurboHotMax>(jsonData);
                    if (myData.expandWild.Length != 5 && myData.symbols.Length != 20)
                    {
                        break;
                    }
                    for (var i = 1; i < 4; i++)
                    {
                        if (myData.expandWild[i] > 0)
                        {
                            myData.symbols[i] = 0;
                            myData.symbols[i + 5] = 0;
                            myData.symbols[i + 10] = 0;
                            myData.symbols[i + 15] = 0;
                        }
                    }
                    return JsonConvert.SerializeObject(myData);
                case (int)Games.BonusBells:
                case (int)Games.WinningClover5:
                    {
                        var myDataB = JsonConvert.DeserializeObject<SlotDataResV3>(jsonData);
                        if (myDataB.win > 0)
                        {
                            for (var i = 1; i < 4; i++)
                            {
                                for (var j = 0; j < 3; j++)
                                {
                                    if (myDataB.symbols[i, j] == 0)
                                    {
                                        myDataB.symbols[i, 0] = 0;
                                        myDataB.symbols[i, 1] = 0;
                                        myDataB.symbols[i, 2] = 0;
                                        break;
                                    }
                                }
                            }
                            return JsonConvert.SerializeObject(myDataB);
                        }
                        break;
                    }
                case (int)Games.VeryHot5Extreme:
                case (int)Games.VeryHot40Extreme:
                case (int)Games.PlayNetFortuneCloverX2:
                case (int) Games.PlayNetDiamondHeartX2:
                    {
                        var myDataB = JsonConvert.DeserializeObject<SlotDataResV3>(jsonData);
                        if (myDataB.win > 0)
                        {
                            for (var i = 1; i < 4; i++)
                            {
                                for (var j = 0; j < 3; j++)
                                {
                                    if (myDataB.symbols[i, j] == 0 || myDataB.symbols[i, j] == 1)
                                    {
                                        var symb = myDataB.symbols[i, j];
                                        myDataB.symbols[i, 0] = symb;
                                        myDataB.symbols[i, 1] = symb;
                                        myDataB.symbols[i, 2] = symb;
                                        break;
                                    }
                                }
                            }
                            return JsonConvert.SerializeObject(myDataB);
                        }
                        break;
                    }
            }

            return jsonData;
        }

        public static int[,] SpanV3SymbolsIfNeeded(int[,] gameData, int gameId, long win, dynamic extra)
        {
            switch (gameId)
            {
                case (int)Games.BonusBells:
                    {
                        if (win > 0)
                        {
                            for (var i = 1; i < 4; i++)
                            {
                                for (var j = 0; j < 3; j++)
                                {
                                    if (gameData[i, j] == 0)
                                    {
                                        gameData[i, 0] = 0;
                                        gameData[i, 1] = 0;
                                        gameData[i, 2] = 0;
                                        break;
                                    }
                                }
                            }
                        }
                        break;
                    }
                case (int)Games.VeryHot5Extreme:
                case (int)Games.VeryHot40Extreme:
                case (int)Games.RedstoneChilliDouble:
                case (int)Games.RedstoneCapsAndCrowns:
                case (int)Games.RedstoneDoubleFireClover:
                case (int)Games.RedstoneDiceDouble:
                case (int)Games.RedstoneChristmasDelight:
                case (int)Games.PlayNetFortuneCloverX2:
                case (int)Games.PlayNetDiamondHeartX2:
                    {
                        if (win > 0)
                        {
                            for (var i = 1; i < 4; i++)
                            {
                                for (var j = 0; j < 3; j++)
                                {
                                    if (gameData[i, j] == 0 || gameData[i, j] == 1)
                                    {
                                        var symb = gameData[i, j];
                                        gameData[i, 0] = symb;
                                        gameData[i, 1] = symb;
                                        gameData[i, 2] = symb;
                                        break;
                                    }
                                }
                            }
                        }
                        break;
                    }
                case (int)Games.PiratesPapi:
                case (int)Games.BlowFruits40:
                case (int)Games.CoinSplash:
                    {
                        var positionsI = new List<int>();
                        var positionsJ = new List<int>();
                        for (var i = 0; i < 5; i++)
                        {
                            for (var j = 0; j < 4; j++)
                            {
                                if (gameData[i, j] == 0)
                                {
                                    positionsI.Add(i);
                                    positionsJ.Add(j);
                                }
                            }
                        }
                        if (positionsI.Count == 0)
                        {
                            break;
                        }
                        for (var i = 0; i < positionsI.Count; i++)
                        {
                            for (var reel = positionsI[i] - 1; reel <= positionsI[i] + 1; reel++)
                            {
                                for (var row = positionsJ[i] - 1; row <= positionsJ[i] + 1; row++)
                                {
                                    if (reel >= 0 && reel < 5 && row >= 0 && row < 4)
                                    {
                                        gameData[reel, row] = 0;
                                    }
                                }
                            }
                        }
                        break;
                    }
                case (int)Games.JokerTripleDouble:
                case (int)Games.ChilliRespin:
                    {
                        WinSymbolV3[] sticky = extra.fixedSymbolsPrevious?.ToObject<WinSymbolV3[]>();
                        if (sticky == null)
                        {
                            break;
                        }
                        for (var i = 0; i < sticky.Length; i++)
                        {
                            gameData[sticky[i].reel, sticky[i].row] = sticky[i].id;
                        }
                        break;
                    }
                case (int)Games.WinningClover5:
                case (int)Games.WinningClover5Extreme:
                case (int)Games.BonusEpicCrown:
                case (int)Games.RedstoneSpookySpins:
                case (int)Games.BonusCrown:
                case (int)Games.CrownOfSecret:
                    {
                        WildExpandV3[] expand = extra.wildExpand?.ToObject<WildExpandV3[]>();
                        if (expand == null)
                        {
                            break;
                        }
                        for (var i = 0; i < expand.Length; i++)
                        {
                            for (var j = 0; j < expand[i].coordinates.Length; j++)
                            {
                                gameData[expand[i].coordinates[j].reel, expand[i].coordinates[j].row] = 0;
                            }
                        }
                        break;
                    }
                case (int)Games.SantasPresents:
                    {
                        WildExpandV3[] expand = extra.wildExpand?.ToObject<WildExpandV3[]>();
                        if (expand == null)
                        {
                            break;
                        }
                        for (var i = 0; i < expand.Length; i++)
                        {
                            var elem = gameData[expand[i].origin.reel, expand[i].origin.row];
                            for (var j = 0; j < expand[i].coordinates.Length; j++)
                            {
                                gameData[expand[i].coordinates[j].reel, expand[i].coordinates[j].row] = elem;
                            }
                        }
                        break;
                    }
                case (int)Games.ToxicHaze:
                case (int)Games.WildSunburst:
                    {
                        int[,] sticky = extra.recall?.ToObject<int[,]>();
                        if (sticky == null)
                        {
                            break;
                        }
                        gameData = sticky;
                        break;
                    }
                case (int)Games.BookOfSpells2:
                case (int)Games.VeryHot5:
                case (int)Games.GoldenCrown:
                case (int)Games.EpicClover40:
                case (int)Games.BetwoonHot5:
                    {
                        int[,] recall = extra.recall?.ToObject<int[,]>();
                        if (recall != null)
                        {
                            gameData = recall;
                        }
                        break;
                    }
            }
            return gameData;
        }

        #endregion

        #region Private Methods

        private static byte[] GenerateArray(byte[,] combination)
        {
            var i = 0;
            var data = new byte[combination.GetLength(0) * combination.GetLength(1)];
            for (var row = 0; row < combination.GetLength(0); row++)
            {
                for (var column = 0; column < combination.GetLength(1); column++)
                {
                    data[i++] = combination[row, column];
                }
            }
            return data;
        }

        private static byte[,] GenerateMatrix(byte[] data, int row, int column)
        {
            var combination = new byte[row, column];
            var k = 0;
            for (var i = 0; i < row; i++)
            {
                for (var j = 0; j < column; j++)
                {
                    combination[i, j] = data[k++];
                }
            }
            return combination;
        }

        private static void SetField(ref byte[,] combination, int i, int j, byte symbol)
        {
            try
            {
                combination[i, j] = symbol;
            }
            catch (Exception e)
            {
            }
        }

        #endregion
    }
}