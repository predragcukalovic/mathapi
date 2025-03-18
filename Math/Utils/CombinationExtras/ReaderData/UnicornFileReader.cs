using Papi.GameServer.Utils.Enums;
using RNGUtils.RandomData;
using System;
using System.Collections.Generic;
using System.IO;

namespace CombinationExtras.ReaderData
{
    public static class UnicornFileReader
    {
        private static readonly Dictionary<string, byte[]> _AllFiles = new Dictionary<string, byte[]>();
        private static readonly Dictionary<string, double[]> _AllReelSetGroups = new Dictionary<string, double[]>();
        private static readonly Dictionary<string, double[]> _AllReelSets = new Dictionary<string, double[]>();

        private static void InitReelGroups()
        {
            //VegasDice
            _AllReelSets.Add(Games.UnicornVegasDice.ToString(), new[] { 0.497333, 0.497333, 0.002667, 0.002667 });
            _AllReelSets.Add(Games.UnicornReelDice.ToString(), new[] { 0.123030, 0.376970, 0.368174, 0.131826 });
            _AllReelSets.Add(Games.UnicornFruitIsland.ToString(), new[] { 0.497333, 0.497333, 0.002667, 0.002667 });
            _AllReelSets.Add(Games.UnicornDaVincisFruits.ToString(), new[] { 0.497333, 0.497333, 0.002667, 0.002667 });
            _AllReelSets.Add(Games.UnicornDaVincisDice.ToString(), new[] { 0.497333, 0.497333, 0.002667, 0.002667 });
            _AllReelSets.Add(Games.UnicornFrootMachine.ToString(), new[] { 0.497333, 0.497333, 0.002667, 0.002667 });
            _AllReelSets.Add(Games.UnicornFruitIslandChristmas.ToString(), new[] { 0.497333, 0.497333, 0.002667, 0.002667 });
            _AllReelSets.Add(Games.UnicornWildParadise.ToString(), new[] { 0.374947, 0.374947, 0.108949, 0.073896, 0.067261 });
            _AllReelSets.Add(Games.UnicornRainbowSevens.ToString(), new[] { 0.123030, 0.376970, 0.368174, 0.131826 });
            _AllReelSets.Add(Games.UnicornHavanaDice.ToString(), new[] { 0.391907, 0.108093, 0.375603, 0.124397 });
            _AllReelSets.Add(Games.UnicornCasinoFruits.ToString(), new[] { 0.391907, 0.108093, 0.375603, 0.124397 });
            _AllReelSets.Add(Games.UnicornTheCrownFruit.ToString(), new[] { 0.374947, 0.374947, 0.108949, 0.073896, 0.067261 });
            _AllReelSets.Add(Games.UnicornFruitMagic.ToString(), new[] { 0.374947, 0.374947, 0.108949, 0.073896, 0.067261 });
            _AllReelSets.Add(Games.UnicornTwentyFruits.ToString(), new[] { 0.81411747, 0.16547437, 0.02040816 });
            _AllReelSets.Add(Games.UnicornMoneyStandard.ToString(), new[] { 0.81411747, 0.16547437, 0.02040816 });
            _AllReelSets.Add(Games.UnicornMoneyStandardDice.ToString(), new[] { 0.81411747, 0.16547437, 0.02040816 });
            _AllReelSets.Add(Games.UnicornTwentyDice.ToString(), new[] { 0.81411747, 0.16547437, 0.02040816 });
            //_AllReelSets.Add(Games.UnicornIslandRespins.ToString(), new[] { 0.451500, 0.451500, 0.048500, 0.048500 });
            _AllReelSets.Add(Games.UnicornDiceRespins.ToString(), new[] { 0.451500, 0.451500, 0.048500, 0.048500 });
            _AllReelSets.Add(Games.UnicornGreatWhale.ToString(), new[] { 0.36, 0.11, 0.30, 0.23 });
            _AllReelSets.Add(Games.UnicornGreatWhale + "G", new[] { 0.883292, 0.116708 });
            _AllReelSets.Add(Games.UnicornCoyoteSevens.ToString(), new[] { 1.0 });
            _AllReelSets.Add(Games.UnicornCoyoteDice.ToString(), new[] { 1.0 });
            _AllReelSets.Add(Games.Unicorn40FruitReels.ToString(), new[] { 0.81411747, 0.165474367, 0.020408163 });
            _AllReelSets.Add(Games.UnicornBigSpinSevens.ToString(), new[] { 0.8237, 0.1763 });
            _AllReelSets.Add(Games.UnicornBuffaloSevens.ToString(), new[] { 0.96282, 0.03718 });
            _AllReelSets.Add(Games.UnicornFrootClassic.ToString(), new[] { 1.0 });
            _AllReelSets.Add(Games.UnicornStickyHot.ToString(), new[] { 0.24, 0.24, 0.26, 0.26 });
            _AllReelSets.Add(Games.UnicornStickyHot + "G", new[] { 0.5, 0.5 });
            _AllReelSets.Add(Games.UnicornBuffaloDice.ToString(), new[] { 0.96282, 0.03718 });
            _AllReelSets.Add(Games.UnicornHitLine.ToString(), new[] { 1.0 });
            _AllReelSets.Add(Games.UnicornHitLineDice.ToString(), new[] { 1.0 });
            _AllReelSets.Add(Games.Unicorn20SuperFlames.ToString(), new[] { 1.0 });
            _AllReelSets.Add(Games.UnicornReelyWildReels.ToString(), new[] { 0.391907, 0.108093, 0.375603, 0.124397 });
            _AllReelSets.Add(Games.UnicornDynamiteRun.ToString(), new[] { 0.374947, 0.374947, 0.108949, 0.073896, 0.067261 });
            _AllReelSets.Add(Games.UnicornSavanaFruits.ToString(), new[] { 1.0 });
            _AllReelSets.Add(Games.UnicornThunderFruits.ToString(), new[] { 0.123030, 0.376970, 0.368174, 0.131826 });
            _AllReelSets.Add(Games.UnicornDiceMachine.ToString(), new[] { 0.497333, 0.497333, 0.002667, 0.002667 });
            _AllReelSets.Add(Games.UnicornSupremeSevens.ToString(), new[] { 0.497333, 0.497333, 0.002667, 0.002667 });
            _AllReelSets.Add(Games.UnicornTripleStackedDiamonds.ToString(), new[] { 0.96282, 0.03718 });
            _AllReelSets.Add(Games.UnicornFruitIslandGems.ToString(), new[] { 0.497333, 0.497333, 0.002667, 0.002667 });
            _AllReelSets.Add(Games.UnicornBigHitSevens.ToString(), new[] { 1.0 });
            _AllReelSets.Add(Games.UnicornDynamiteRunPlatinum.ToString(), new[] { 0.374947, 0.374947, 0.108949, 0.073896, 0.067261 });
            //DEMO - PRIMER KAKO BI IZGLEDALO ZA FAJLOVE G1R1 itd
            //AllReelSetGroups.Add(Games.UnicornVegasDice.ToString(), new[] { 0.5, 0.5 });
            //_AllReelSets.Add(Games.UnicornVegasDice + "1", new[] { 0.497333, 0.497333, 0.002667, 0.002667 });
            //_AllReelSets.Add(Games.UnicornVegasDice + "2", new[] { 0.497333, 0.497333, 0.002667, 0.002667 });
            _AllReelSetGroups.Add(Games.Unicorn20MegaFlames.ToString(), new[] { 0.06503522, 0.32017339, 0.03001625, 0.16920008, 0.00010005, 0.01000542, 0.01000542, 0.00200108, 0.00025014, 0.00300163, 0.08004335, 0.10005418, 0.21011379 });
            _AllReelSetGroups.Add(Games.Unicorn20DiceFlames.ToString(), new[] { 0.06503522, 0.32017339, 0.03001625, 0.16920008, 0.00010005, 0.01000542, 0.01000542, 0.00200108, 0.00025014, 0.00300163, 0.08004335, 0.10005418, 0.21011379 });
            _AllReelSetGroups.Add(Games.UnicornSurfinHeat.ToString(), new[] { 0.997585, 0.002415 });
            _AllReelSetGroups.Add(Games.Unicorn40MegaFlames.ToString(), new[] { 0.44, 0.05, 0.05, 0.05, 0.02, 0.18, 0.08, 0.02, 0.02, 0.01, 0.006923, 0.0028, 0.00105, 0.00005, 0.0103, 0.058877 });
            _AllReelSetGroups.Add(Games.Unicorn40DiceFlames.ToString(), new[] { 0.44, 0.05, 0.05, 0.05, 0.02, 0.18, 0.08, 0.02, 0.02, 0.01, 0.006923, 0.0028, 0.00105, 0.00005, 0.0103, 0.058877 });
            _AllReelSetGroups.Add(Games.UnicornWinterFruits.ToString(), new[] { 0.014544, 0.2384019719, 0.0911036107, 0.098553678, 0.098553678, 0.1017329253, 0.0340574245, 0.0008514356, 0.0911036107, 0.0340574246, 0.0340574246, 0.1229828161, 0.04 });
            _AllReelSetGroups.Add(Games.Unicorn40HotStrike.ToString(), new[] { 0.014544, 0.2384019719, 0.0911036107, 0.098553678, 0.098553678, 0.1017329253, 0.0340574245, 0.0008514356, 0.0911036107, 0.0340574246, 0.0340574246, 0.1229828161, 0.04 });
            _AllReelSetGroups.Add(Games.Unicorn40HotStrikeDice.ToString(), new[] { 0.014544, 0.2384019719, 0.0911036107, 0.098553678, 0.098553678, 0.1017329253, 0.0340574245, 0.0008514356, 0.0911036107, 0.0340574246, 0.0340574246, 0.1229828161, 0.04 });
            _AllReelSetGroups.Add(Games.UnicornFastFruits.ToString(), new[] { 0.23671, 0.23671, 0.23671, 0.047987059, 0.047987059, 0.047987059, 0.04863627433, 0.04863627434, 0.04863627433 });
            _AllReelSetGroups.Add(Games.UnicornMoneyStandardWild.ToString(), new[] { 0.01644571, 0.01644571, 0.086065891, 0.065600114, 0.104034354, 0.00822286, 0.164457115, 0.164457115, 0.164457115, 0.166693139, 0.043120877 });
            _AllReelSets.Add(Games.UnicornSurfinHeat + "1", new[] { 0.125, 0.125, 0.125, 0.125, 0.125, 0.125, 0.125, 0.125 });
            _AllReelSets.Add(Games.UnicornSurfinHeat + "2", new[] { 0.125, 0.125, 0.125, 0.125, 0.125, 0.125, 0.125, 0.125 });
            _AllReelSets.Add(Games.UnicornSurfinHeat + "G", new[] { 0.125, 0.125, 0.125, 0.125, 0.125, 0.125, 0.125, 0.125 });
            for (var i = 1; i <= 13; i++)
            {
                _AllReelSets.Add(Games.Unicorn20MegaFlames + i.ToString(), new[] { 0.125, 0.125, 0.125, 0.125, 0.125, 0.125, 0.125, 0.125 });
                _AllReelSets.Add(Games.Unicorn20DiceFlames + i.ToString(), new[] { 0.125, 0.125, 0.125, 0.125, 0.125, 0.125, 0.125, 0.125 });
                _AllReelSets.Add(Games.UnicornWinterFruits + i.ToString(), new[] { 0.125, 0.125, 0.125, 0.125, 0.125, 0.125, 0.125, 0.125 });
                _AllReelSets.Add(Games.Unicorn40HotStrike + i.ToString(), new[] { 0.125, 0.125, 0.125, 0.125, 0.125, 0.125, 0.125, 0.125 });
                _AllReelSets.Add(Games.Unicorn40HotStrikeDice + i.ToString(), new[] { 0.125, 0.125, 0.125, 0.125, 0.125, 0.125, 0.125, 0.125 });
            }
            for (var i = 1; i <= 9; i++)
            {
                _AllReelSets.Add(Games.UnicornFastFruits + i.ToString(), new[] { 0.125, 0.125, 0.125, 0.125, 0.125, 0.125, 0.125, 0.125 });
            }
            for (var i = 1; i <= 11; i++)
            {
                _AllReelSets.Add(Games.UnicornMoneyStandardWild + i.ToString(), new[] { 0.125, 0.125, 0.125, 0.125, 0.125, 0.125, 0.125, 0.125 });
            }
            for (var i = 1; i <= 16; i++)
            {
                _AllReelSets.Add(Games.Unicorn40MegaFlames + i.ToString(), new[] { 0.125, 0.125, 0.125, 0.125, 0.125, 0.125, 0.125, 0.125 });
                _AllReelSets.Add(Games.Unicorn40DiceFlames + i.ToString(), new[] { 0.125, 0.125, 0.125, 0.125, 0.125, 0.125, 0.125, 0.125 });
            }
            _AllReelSetGroups.Add(Games.UnicornMiniMegaCash.ToString(), new[] { 0.01710705, 0.0343851705, 0.01710705, 0.008553525, 0.008553525, 0.008553525, 0.00570235, 0.0114047, 0.00570235, 0.00570235, 0.00570235, 0.00570235, 0.00570235, 0.00570235, 0.124482464805, 0.01710705, 0.001710705, 0.0042767625, 0.00213838125, 0.0014255875, 0.009665, 0.003866, 0.0048325, 0.000950388445, 0.001710705, 0.001710705, 0.001710705, 0.01710705, 0.260955, 0.21263, 0.07732, 0.07732, 0.0335 });
            _AllReelSetGroups.Add(Games.UnicornDiceMegaCash.ToString(), new[] { 0.01710705, 0.0343851705, 0.01710705, 0.008553525, 0.008553525, 0.008553525, 0.00570235, 0.0114047, 0.00570235, 0.00570235, 0.00570235, 0.00570235, 0.00570235, 0.00570235, 0.124482464805, 0.01710705, 0.001710705, 0.0042767625, 0.00213838125, 0.0014255875, 0.009665, 0.003866, 0.0048325, 0.000950388445, 0.001710705, 0.001710705, 0.001710705, 0.01710705, 0.260955, 0.21263, 0.07732, 0.07732, 0.0335 });
            for (var i = 1; i <= 33; i++)
            {
                _AllReelSets.Add(Games.UnicornMiniMegaCash + i.ToString(), new[] { 0.125, 0.125, 0.125, 0.125, 0.125, 0.125, 0.125, 0.125 });
                _AllReelSets.Add(Games.UnicornDiceMegaCash + i.ToString(), new[] { 0.125, 0.125, 0.125, 0.125, 0.125, 0.125, 0.125, 0.125 });
            }
        }

        /// <summary>
        /// Učitava sve fajlove za igre koji postoje.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="games"></param>
        public static void ReadAllFiles(string path, Enum games)
        {
            var names = Enum.GetNames(games.GetType());

            foreach (var name in names)
            {
                var gameDirectory = new DirectoryInfo(Path.Combine(path, name));
                if (gameDirectory.Exists)
                {
                    var gameFiles = gameDirectory.GetFiles(name + "*.fsl");
                    if (gameFiles.Length > 0)
                    {
                        foreach (var gameFile in gameFiles)
                        {
                            _AllFiles.Add(gameFile.Name.Replace(".fsl", ""), File.ReadAllBytes(gameFile.FullName));
                        }
                    }
                }
            }

            InitReelGroups();
        }

        /// <summary>
        /// Daje sve bajtove iz fajla za igru ili za igruG.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="gratis"></param>
        /// <param name="usedFsl"></param>
        /// <param name="fslFileId"></param>
        /// <returns></returns>
        public static byte[] GetSlotFileForGame(Games game, bool gratis, out int usedFSL, int fslFileId = 0)
        {
            var finalFile = game.ToString();
            usedFSL = -1;
            switch (game)
            {
                case Games.Unicorn20MegaFlames:
                case Games.UnicornMiniMegaCash:
                case Games.Unicorn20DiceFlames:
                case Games.Unicorn40MegaFlames:
                case Games.Unicorn40DiceFlames:
                case Games.UnicornWinterFruits:
                case Games.UnicornFastFruits:
                case Games.UnicornMoneyStandardWild:
                case Games.UnicornDiceMegaCash:
                case Games.Unicorn40HotStrike:
                case Games.Unicorn40HotStrikeDice:
                    return GetSlotFileForGame(finalFile);
                case Games.UnicornSurfinHeat:
                    if (!gratis)
                    {
                        return GetSlotFileForGame(finalFile);
                    }
                    break;
            }

            if (gratis)
            {
                finalFile += "G";
            }
            int reelSet = 0;
            if (_AllReelSets.ContainsKey(finalFile))
            {
                reelSet = GetReelSet(_AllReelSets[finalFile], fslFileId);
            }
            if (gratis && reelSet != 0)
            {
                finalFile += reelSet;
            }
            else
            {
                finalFile += gratis ? "" : ("R" + reelSet);
            }
            usedFSL = reelSet;
            return _AllFiles.ContainsKey(finalFile) ? _AllFiles[finalFile] : null;
        }

        /// <summary>
        /// Daje sve bajtove iz fajla za igru ili za igruG.
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        public static byte[] GetSlotFileForGame(string game)
        {
            if (!_AllReelSets.ContainsKey(game) && _AllFiles.ContainsKey(game))
            {
                return _AllFiles[game];
            }
            var finalFile = game;
            var group = -1;
            if (_AllReelSetGroups.ContainsKey(game))
            {
                group = GetReelSet(_AllReelSetGroups[game]);
            }
            int reelSet;
            if (group == -1)
            {
                reelSet = GetReelSet(_AllReelSets[game]);
            }
            else
            {
                finalFile += "B" + group;
                reelSet = GetReelSet(_AllReelSets[game + group]);
            }

            finalFile += "R" + reelSet;
            return _AllFiles.ContainsKey(finalFile) ? _AllFiles[finalFile] : null;
        }

        private static int GetReelSet(double[] probs, int fslFileId = 0)
        {
            if (fslFileId > 0)
            {
                return fslFileId;
            }

            var sum = 0.0;
            var rnd = SoftwareRng.Next();
            var n = probs.Length;
            for (var i = 0; i < n; i++)
            {
                sum += probs[i];
                if (rnd < sum)
                {
                    return i + 1;
                }
            }
            return 0;
        }
    }
}
