using BuyBonusDTO;
using Papi.GameServer.Utils.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CombinationExtras.ReaderData
{
    public static class MathBuyBonusFilesReader
    {
        private static readonly Dictionary<string, List<byte>[]> _AllBuyBonusReels = new Dictionary<string, List<byte>[]>();
        private static readonly Dictionary<string, List<BuyBonusParameter>> _AllBuyBonusParameters = new Dictionary<string, List<BuyBonusParameter>>();
        private static readonly Dictionary<string, int[]> _AllBuyBonusProbabilities = new Dictionary<string, int[]>();

        /// <summary>
        /// Učitava rilove iz .fsb fajla.
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        private static void ReadReelsFromSlotBonusFile(string game, byte[] byteArray)
        {
            try
            {
                if (byteArray == null)
                {
                    Logger.LogWarning($"No .fsb file for game {game}");
                }
                if (byteArray[0] != 0xFC || byteArray[1] != 0xFD || byteArray[2] != 0xDD || (byteArray[3] != 0x01 && byteArray[3] != 0x02))
                {
                    Logger.LogWarning($"Warning: Bad .fsb file for game {game}");
                }
                var m = byteArray[4];
                var gameName = System.Text.Encoding.ASCII.GetString(byteArray.Skip(5).Take(m).ToArray());
                if (gameName != game)
                {
                    Logger.LogWarning($"Warning: Wrong game for .fsb file, {game} received, .fsb for {gameName} loaded");
                }
                var buyBonusParam = new List<BuyBonusParameter>();
                var paramCount = byteArray[5 + m];
                var skip = 6 + m;
                for (var i = 0; i < paramCount; i++)
                {
                    var param = new BuyBonusParameter
                    {
                        Type = byteArray[skip++],
                        Lines = byteArray[skip++]
                    };
                    var j = byteArray.Skip(skip).Take(4).ToArray();
                    skip += 4;
                    var k = byteArray.Skip(skip).Take(4).ToArray();
                    skip += 4;
                    if (BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(j);
                        Array.Reverse(k);
                    }
                    param.Multiplier = BitConverter.ToInt32(j, 0);
                    param.Rtp = BitConverter.ToInt32(k, 0);
                    buyBonusParam.Add(param);
                }

                int[] probs = null;
                if (byteArray[3] == 2)
                {
                    var k = byteArray[skip++];
                    probs = new int[k];
                    for (var i = 0; i < k; i++)
                    {
                        var j = byteArray.Skip(skip).Take(4).ToArray();
                        if (BitConverter.IsLittleEndian)
                        {
                            Array.Reverse(j);
                        }
                        probs[i] = BitConverter.ToInt32(j, 0);
                        skip += 4;
                    }
                }

                var n = byteArray[skip++];
                var listSize = new List<int>();
                var finalReels = new List<byte>[n];
                for (var i = 0; i < n; i++)
                {
                    var j = byteArray.Skip(skip).Take(4).ToArray();
                    if (BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(j);
                    }
                    listSize.Add(BitConverter.ToInt32(j, 0));
                    skip += 4;
                }
                for (var i = 0; i < n; i++)
                {
                    var reel = byteArray.Skip(skip).Take(listSize[i]).ToList();
                    skip += listSize[i];
                    finalReels[i] = reel;
                }
                if (byteArray[skip] != 0xDD || byteArray[skip + 1] != 0xFD || byteArray[skip + 2] != 0xFC)
                {
                    Logger.LogWarning($"Warning: Bad .fsb file for game {game}");
                }
                _AllBuyBonusReels.Add(game, finalReels);
                _AllBuyBonusParameters.Add(game, buyBonusParam);
                _AllBuyBonusProbabilities.Add(game, probs);
            }
            catch (Exception ex)
            {
                Logger.LogWarning($"BuyBonusCombination Exception: {ex}");
                return;
            }
        }

        /// <summary>
        /// Učitava sve fajlove za igre koji postoje.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="games"></param>
        /// <param name="softwareversion"></param>
        public static void ReadAllFiles(string path, Enum games)
        {
            var names = Enum.GetNames(games.GetType());

            foreach (var name in names)
            {
                var filePath = Path.Combine(path, name, name + ".fsb");
                if (File.Exists(filePath))
                {
                    ReadReelsFromSlotBonusFile(name, File.ReadAllBytes(filePath));
                }
            }
        }

        /// <summary>
        /// Daje rilove iz fajla za buy bonus.
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        public static List<byte>[] GetBuyBonusReelsForGame(string game)
        {
            return _AllBuyBonusReels.ContainsKey(game) ? _AllBuyBonusReels[game] : null;
        }

        /// <summary>
        /// Daje sve parametre za igru za buy bonus.
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        public static List<BuyBonusParameter> GetBuyBonusParametersForGame(string game)
        {
            return _AllBuyBonusParameters.ContainsKey(game) ? _AllBuyBonusParameters[game] : null;
        }

        /// <summary>
        /// Daje verovatnoce iz fajla za buy bonus.
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        public static int[] GetBuyBonusProbabilitiesForGame(string game)
        {
            return _AllBuyBonusProbabilities.ContainsKey(game) ? _AllBuyBonusProbabilities[game] : null;
        }
    }
}
