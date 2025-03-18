using CombinationExtras.ReaderData;
using CombinationUnicornUtils;
using CombinationUtils.UnicornCombinationData;
using Papi.GameServer.Math.Contracts.Requests;
using Papi.GameServer.Utils.Enums;
using MathCombination.CombinationData;
using MathForUnicornGames.Game10JingleFruits;
using MathForUnicornGames.Game20HotStrike;
using MathForUnicornGames.Game20MegaFlames;
using MathForUnicornGames.Game20SuperFlames;
using MathForUnicornGames.Game40FruitReels;
using MathForUnicornGames.Game40MegaFlames;
using MathForUnicornGames.GameBigHitSevens;
using MathForUnicornGames.GameBikiniFruits;
using MathForUnicornGames.GameChristmasPresents;
using MathForUnicornGames.GameCoyoteSevens;
using MathForUnicornGames.GameEpicMegaCash;
using MathForUnicornGames.GameFastFruits;
using MathForUnicornGames.GameFireStars;
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
using RNGUtils.RandomData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Papi.GameServer.Math.MathCheatTool
{

    public static class UnicornSlotCombinationCheatTool
    {
        #region Private methods

        /// <summary>
        /// Daje kombinaciju za igru VegasDice.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationVegasDice(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new MatrixVegasDice();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationUnicorn();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru ReelDice.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationReelDice(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new MatrixReelDice();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationUnicorn();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru WildParadice.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationWildParadice(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new MatrixWildParadice();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationUnicorn();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru HavanaDice.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationHavanaDice(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new MatrixHavanaDice();
            matrix.FromMatrixArrayHavanaDice(matrixArray);
            var combination = new CombinationUnicorn();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru TwentyFruits i sve njene klonove.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationTwentyFruits(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new MatrixTwentyFruits();
            matrix.FromMatrixArrayTwentyFruits(matrixArray);
            var combination = new CombinationUnicorn();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru TwentyFruits i sve njene klonove.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGame"></param>
        /// <returns></returns>
        private static ICombination GetCombinationIslandRespins(int[,] matrixArray, int numberOfLines, int bet, bool gratisGame)
        {
            var matrix = new MatrixIslandRespins();
            matrix.FromMatrixArrayIslandRespins(matrixArray);
            var combination = new CombinationUnicornNew();
            combination.MatrixToCombinationNew(matrix, numberOfLines, bet, gratisGame);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru IslandRespins2.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGame"></param>
        /// <param name="addInfo"></param>
        /// <returns></returns>
        private static ICombination GetCombinationIslandRespins2(int[,] matrixArray, int numberOfLines, int bet, bool gratisGame, byte addInfo)
        {
            var matrix = new MatrixIslandRespins2();
            matrix.FromMatrixArrayIslandRespins2(matrixArray);
            var combination = new CombinationUnicornNew();
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGame, addInfo);
            return combination;
        }

        /// <summary>
        ///Daje kombinaciju za igru GreatWhale i sve njene klonove
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGame"></param>
        /// <param name="additionalArray"></param>
        /// <param name="fslFileId"></param>
        /// <returns></returns>
        private static ICombination GetCombinationGreatWhale(int[,] matrixArray, int numberOfLines, int bet, bool gratisGame, ref byte[] additionalArray, byte fslFileId)
        {
            if (additionalArray == null)
            {
                additionalArray = new byte[50];
            }
            var matrix = new MatrixGreatWhale();
            matrix.FromMatrixArrayGreatWhale(matrixArray);
            var combination = new CombinationUnicornNew();
            combination.MatrixToCombinationNew(matrix, numberOfLines, bet, gratisGame, ref additionalArray, fslFileId);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru 20 Mega Flames i sve njene klonove.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombination20MegaFlames(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new Matrix20MegaFlames();
            matrix.FromMatrixArray20MegaFlames(matrixArray);
            var combination = new CombinationUnicornNew();
            combination.MatrixToCombinationNew(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru MiniMegaCash.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationMiniMegaCash(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new MatrixMiniMegaCash();
            matrix.FromMatrixArrayMiniMegaCash(matrixArray);
            var combination = new CombinationUnicornNew();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru Surfin Heat i sve njene klonove.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGamesLeft"></param>
        /// <param name="addInfo"></param>
        /// <returns></returns>
        private static ICombination GetCombinationSurfinHeat(int[,] matrixArray, int numberOfLines, int bet, int gratisGamesLeft, byte addInfo)
        {
            var matrix = new MatrixSurfinHeat();
            matrix.FromMatrixArraySurfinHeat(matrixArray);
            var combination = new CombinationUnicornNew();
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGamesLeft, addInfo);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru 40MegaFlames.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombination40MegaFlames(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new Matrix40MegaFlames();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationUnicornNew();
            combination.MatrixToCombination(matrix, bet, numberOfLines);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru WiterFruits.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationWinterFruits(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new MatrixWinterFruits();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationUnicornNew();
            combination.MatrixToCombination(matrix, bet, numberOfLines);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru FastFruits.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationFastFruits(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new MatrixFastFruits();
            matrix.FromMatrixArrayFastFruits(matrixArray);
            var combination = new CombinationUnicornNew();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru MoneyStandardWild.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationMoneyStandardWild(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new MatrixMoneyStandardWild();
            matrix.FromMatrixArrayMoneyStandardWild(matrixArray);
            var combination = new CombinationUnicornNew();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru CoyoteSevens.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationCoyoteSevens(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new MatrixCoyoteSevens();
            matrix.FromMatrixArrayCoyoteSevens(matrixArray);
            var combination = new CombinationUnicornNew();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru 40FruitReels.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombination40FruitReels(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new Matrix40FruitReels();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationUnicornNew();
            combination.MatrixToCombination(matrix, bet, numberOfLines);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru BikiniFruits.
        /// </summary>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationBikiniFruits(int[,] matrixArray, int numberOfLines, int bet, int gratisGamesLeft, byte additionalInformation)
        {
            var matrix = new MatrixBikiniFruits();
            matrix.FromMatrixArrayBikiniFruits(matrixArray);
            var combination = new CombinationUnicornNew();
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGamesLeft, additionalInformation);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru QueenOfPyramids.
        /// </summary>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationQueenOfPyramids(int[,] matrixArray, int numberOfLines, int bet, int gratisGamesLeft, byte additionalInformation)
        {
            var matrix = new MatrixQueenOfPyramids();
            matrix.FromMatrixArrayBikiniFruits(matrixArray);
            var combination = new CombinationUnicornNew();
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGamesLeft, additionalInformation);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru FireStars.
        /// </summary>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationFireStars(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new MatrixFireStars();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationUnicornNew();
            combination.MatrixToCombination(matrix, bet, numberOfLines);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru SimplySevens.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationSimplySevens(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new MatrixSimplySevens();
            matrix.FromMatrixArraySimplySevens(matrixArray);
            var combination = new CombinationUnicornNew();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru 10JingleFruits.
        /// </summary>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombination10JingleFruits(int[,] matrixArray, int numberOfLines, int bet)
        {
            for (var i = 0; i < matrixArray.GetLength(0); i++)
            {
                for (var j = 0; j < matrixArray.GetLength(1); j++)
                {
                    if (matrixArray[i, j] > 9)
                    {
                        matrixArray[i, j] = matrixArray[i, j] / 10 + 6;
                    }
                }
            }
            var matrix = new Matrix10JingleFruits();
            matrix.FromMatrixArray10JingleFruits(matrixArray);
            var combination = new CombinationUnicornNew();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru GoldLine.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationGoldLine(int[,] matrixArray, int bet)
        {
            var matrix = new MatrixGoldLine();
            matrix.FromMatrixArrayGoldLine(matrixArray);
            var combination = new CombinationUnicornNew();
            combination.MatrixToCombination2(matrix, bet, false);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru ChristmasPresents.
        /// </summary>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationChristmasPresents(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new MatrixChristmasPresents();
            matrix.FromMatrixArrayChristmasPresents(matrixArray);
            var combination = new CombinationUnicornNew();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru Sticky Hot.
        /// </summary>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationStickyHot(int[,] matrixArray, int numberOfLines, int bet, int gratisGamesLeft, ref byte[] addArray)
        {
            if (addArray == null)
            {
                addArray = new byte[16];
            }
            var matrix = new MatrixStickyHot();
            matrix.FromMatrixArrayStickyHot(matrixArray);
            var combination = new CombinationUnicornNew();
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGamesLeft, ref addArray);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru PumpkinHorror.
        /// </summary>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationPumpkinHorror(int[,] matrixArray, int numberOfLines, int bet)
        {
            for (var i = 0; i < matrixArray.GetLength(0); i++)
            {
                for (var j = 0; j < matrixArray.GetLength(1); j++)
                {
                    if (matrixArray[i, j] == 10)
                    {
                        matrixArray[i, j] = 20;
                    }
                    if (matrixArray[i, j] == 8)
                    {
                        matrixArray[i, j] = 10;
                    }
                    if (matrixArray[i, j] == 9)
                    {
                        matrixArray[i, j] = 11;
                    }
                }
            }
            var matrix = new MatrixPumpkinHorror();
            matrix.FromMatrixArrayPumpkinHorror(matrixArray);
            var combination = new CombinationUnicornNew();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru 20SuperFlames.
        /// </summary>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombination20SuperFlames(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new Matrix20SuperFlames();
            matrix.FromMatrixArray20SuperFlames(matrixArray);
            var combination = new CombinationUnicornNew();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru 20HotStrikeJackpot.
        /// </summary>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombination20HotStrikeJackpot(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new Matrix20HotStrikeJackpot();
            matrix.FromMatrixArray20HotStrikeJackpot(matrixArray);
            var combination = new CombinationUnicornNew();
            combination.MatrixToCombination(matrix, numberOfLines, bet, true);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru HitLine.
        /// </summary>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationHitLine(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new MatrixHitLine();
            matrix.FromMatrixArrayHitLine(matrixArray);
            var combination = new CombinationUnicornNew();
            combination.MatrixToCombination(matrix, numberOfLines, bet, true);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru EpicMegaCash.
        /// </summary>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationEpicMegaCash(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new MatrixEpicMegaCash();
            matrix.FromMatrixArrayEpicMegaCash(matrixArray);
            var combination = new CombinationUnicornNew();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru Big Hit Sevens.
        /// </summary>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationBigHitSevens(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new MatrixBigHitSevens();
            matrix.FromMatrixArrayBigHitSevens(matrixArray);
            var combination = new CombinationUnicornNew();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Učitava rilove iz .fsl fajla.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="gratisGame"></param>
        /// <param name="fslFileId"></param>
        /// <returns></returns>
        private static List<byte>[] ReadReelsFromSlotFile(Games game, bool gratisGame, out int usedFsl, int desiredFsl = 0)
        {
            try
            {
                var byteArray = UnicornFileReader.GetSlotFileForGame(game, gratisGame, out usedFsl, desiredFsl);
                if (byteArray == null)
                {
                    throw new Exception("No .fsl file for Unicorn game " + game);
                }
                if (byteArray[0] != 0xFC || byteArray[1] != 0xFE || byteArray[2] != 0xDE || byteArray[3] != 0x03)
                {
                    throw new Exception("Bad .fsl file.");
                }
                var m = byteArray[9];
                var gameName = System.Text.Encoding.ASCII.GetString(byteArray.Skip(10).Take(m).ToArray());
                if (gameName != game.ToString())
                {
                    throw new Exception("Wrong game for Unicorn .fsl file.");
                }
                var skip = 10 + m;
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
                if (byteArray[skip] != 0xDE || byteArray[skip + 1] != 0xFE || byteArray[skip + 2] != 0xFC)
                {
                    throw new Exception("Bad Unicorn .fsl file.");
                }
                return finalReels;
            }
            catch (Exception ex)
            {
                throw new Exception("Unicorn SlotCombination Reader Exception: " + ex);
            }
        }

        /// <summary>
        /// Daje matricu n×6 na osnovu rilova, koriscenjem Random Generatora.
        /// </summary>
        /// <param name="reels"></param>
        /// <returns></returns>
        private static int[,] ReadMatrixArrayFromReelsRegular(params List<byte>[] reels)
        {
            var matrixArray = new int[reels.Length, 7];
            for (var i = 0; i < reels.Length; i++)
            {
                var size = reels[i].Count;
                var random = (int)SoftwareRng.Next(size);
                for (var j = 0; j < 7; j++)
                {
                    matrixArray[i, j] = reels[i][(random + j) % size];
                }
            }
            return matrixArray;
        }

        /// <summary>
        /// Daje matricu n×6 na osnovu Cheat Toola, ukoliko odgovarajuca polja CheatToolData objekta nisu postavljena tada koristi RANDOM GENERATOR
        /// </summary>
        /// <param name="cheatToolDataObj"></param>
        /// <param name="reels"></param>
        /// <returns></returns>
        private static int[,] ReadMatrixArrayFromReelsCheat(ref CheatToolData cheatToolDataObj, params List<byte>[] reels)
        {
            if (cheatToolDataObj.UsingCheatTool)
            {
                var matrixArray = new int[reels.Length, 7];
                if (cheatToolDataObj.NewMatrix != null && !cheatToolDataObj.StoppingReelsNotUsingMatrix)
                {
                    var reelsFromCheatData = cheatToolDataObj.NewMatrix.GetLength(1);
                    var rows = cheatToolDataObj.NewMatrix.GetLength(0);

                    for (int i = 0; i < reels.Length; i++)
                    {
                        for (int j = 0; j < 7; j++)
                        {
                            //NAPOMENA: Defaultno se dodeljuje vrednost 3, posto u do sada implementiranim igrama simbol sa ID-jem tri je regularan simbol
                            matrixArray[i, j] = (i < reelsFromCheatData && j < rows) ? cheatToolDataObj.NewMatrix[j, i] : 3;
                        }
                    }
                    return matrixArray;
                }
            }
            return ReadMatrixArrayFromReelsRegular(reels);
        }
        #endregion

        /// <summary>
        /// Daje kombinaciju za slot fajl.
        /// </summary>
        /// <param name="cheatToolObject"></param>
        /// <param name="game"></param>
        /// <param name="bet"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="gratisGamesLeft"></param>
        /// <param name="additionalInformation"></param>
        /// <param name="additionalArray"></param>
        public static ICombination GetCombination(ref CheatToolData cheatToolObject, Games game, int bet, int numberOfLines, int gratisGamesLeft, byte additionalInformation, ref byte[] additionalArray)
        {
            List<byte>[] reels;
            int usedFSL;
            switch (game)
            {
                case Games.UnicornGreatWhale:
                    reels = ReadReelsFromSlotFile(game, gratisGamesLeft > 0, out usedFSL, additionalInformation);
                    if (gratisGamesLeft > 0 && additionalInformation != usedFSL)
                    {
                        additionalInformation = (byte)usedFSL;
                    }
                    else
                    {
                        if (additionalInformation != usedFSL)
                            additionalInformation = 0;
                    }
                    break;
                case Games.UnicornIslandRespins:
                    var matIsR = MatrixIslandRespins2.GetMatixArray(additionalInformation);
                    reels = new List<byte>[5];
                    for (var i = 0; i < 5; i++)
                    {
                        var arr = new List<byte>();
                        for (var j = 0; j < 5; j++)
                        {
                            arr.Add((byte)matIsR[i, j]);
                        }
                        reels[i] = arr;
                    }
                    break;
                case Games.UnicornFireStars:
                case Games.UnicornStarDiceChristmas:
                    var matFs = MatrixFireStars.GetMatixArray();
                    reels = new List<byte>[5];
                    for (var i = 0; i < 5; i++)
                    {
                        reels[i] = new List<byte>();
                        for (var j = 0; j < 6; j++)
                        {
                            reels[i].Add((byte)matFs[i, j]);
                        }
                    }
                    break;
                case Games.UnicornSimplySevens:
                case Games.UnicornSimplySevensDice:
                case Games.UnicornLavaDiamonds:
                case Games.UnicornLavaDice:
                case Games.UnicornLavaDiamondsChristmas:
                case Games.UnicornSimplySevensDiceEaster:
                case Games.UnicornBunnyDice:
                    var matSs = MatrixSimplySevens.GetMatixArray();
                    reels = new List<byte>[5];
                    for (var i = 0; i < 5; i++)
                    {
                        reels[i] = new List<byte>();
                        for (var j = 0; j < 5; j++)
                        {
                            reels[i].Add((byte)matSs[i, j]);
                        }
                    }
                    break;
                case Games.Unicorn10JingleFruits:
                    var matJf = Matrix10JingleFruits.GetMatixArray();
                    reels = new List<byte>[5];
                    for (var i = 0; i < 5; i++)
                    {
                        reels[i] = new List<byte>();
                        for (var j = 0; j < 5; j++)
                        {
                            reels[i].Add((byte)matJf[i, j]);
                        }
                    }
                    break;
                case Games.UnicornChristmasPresents:
                case Games.UnicornMisterLuck:
                    var matCp = MatrixChristmasPresents.GetMatixArray();
                    reels = new List<byte>[5];
                    for (var i = 0; i < 5; i++)
                    {
                        reels[i] = new List<byte>();
                        for (var j = 0; j < 5; j++)
                        {
                            reels[i].Add((byte)matCp[i, j]);
                        }
                    }
                    break;
                case Games.UnicornGoldLine:
                case Games.UnicornGoldLineDice:
                    var matGl = MatrixGoldLine.GetMatixArray(out bool goldLine);
                    reels = new List<byte>[5];
                    for (var i = 0; i < 5; i++)
                    {
                        reels[i] = new List<byte>();
                        for (var j = 0; j < 5; j++)
                        {
                            reels[i].Add((byte)matGl[i, j]);
                        }
                    }
                    break;
                case Games.UnicornPumpkinHorror:
                case Games.UnicornWildHoppers:
                    var matPh = MatrixPumpkinHorror.GetMatixArray();
                    reels = new List<byte>[5];
                    for (var i = 0; i < 5; i++)
                    {
                        reels[i] = new List<byte>();
                        for (var j = 0; j < 5; j++)
                        {
                            reels[i].Add((byte)matPh[i, j]);
                        }
                    }
                    break;
                case Games.Unicorn20HotStrikeJackpot:
                case Games.Unicorn20HotStrikeDiceJackpot:
                    var mat20hsj = Matrix20HotStrikeJackpot.GetMatixArray();
                    reels = new List<byte>[5];
                    for (var i = 0; i < 5; i++)
                    {
                        reels[i] = new List<byte>();
                        for (var j = 0; j < 5; j++)
                        {
                            reels[i].Add((byte)mat20hsj[i, j]);
                        }
                    }
                    break;
                default:
                    reels = ReadReelsFromSlotFile(game, gratisGamesLeft > 0, out usedFSL);
                    break;
            }

            var matrixArray = ReadMatrixArrayFromReelsCheat(ref cheatToolObject, reels);

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
                    return GetCombinationVegasDice(matrixArray, numberOfLines, bet);
                case Games.UnicornReelDice:
                case Games.UnicornRainbowSevens:
                case Games.UnicornThunderFruits:
                    return GetCombinationReelDice(matrixArray, numberOfLines, bet);
                case Games.UnicornWildParadise:
                case Games.UnicornTheCrownFruit:
                case Games.UnicornFruitMagic:
                case Games.UnicornDynamiteRun:
                case Games.UnicornDynamiteRunPlatinum:
                    return GetCombinationWildParadice(matrixArray, numberOfLines, bet);
                case Games.UnicornHavanaDice:
                case Games.UnicornCasinoFruits:
                case Games.UnicornReelyWildReels:
                    return GetCombinationHavanaDice(matrixArray, numberOfLines, bet);
                case Games.UnicornTwentyFruits:
                case Games.UnicornTwentyDice:
                case Games.UnicornMoneyStandard:
                case Games.UnicornMoneyStandardDice:
                    return GetCombinationTwentyFruits(matrixArray, numberOfLines, bet);
                case Games.UnicornIslandRespins:
                    return GetCombinationIslandRespins2(matrixArray, numberOfLines, bet, gratisGamesLeft > 0, 0);
                case Games.UnicornDiceRespins:
                    return GetCombinationIslandRespins(matrixArray, numberOfLines, bet, gratisGamesLeft > 0);
                case Games.UnicornGreatWhale:
                    return GetCombinationGreatWhale(matrixArray, numberOfLines, bet, gratisGamesLeft > 0, ref additionalArray, additionalInformation);
                case Games.Unicorn20MegaFlames:
                case Games.Unicorn20DiceFlames:
                    return GetCombination20MegaFlames(matrixArray, numberOfLines, bet);
                case Games.UnicornMiniMegaCash:
                case Games.UnicornDiceMegaCash:
                    return GetCombinationMiniMegaCash(matrixArray, numberOfLines, bet);
                case Games.UnicornSurfinHeat:
                    return GetCombinationSurfinHeat(matrixArray, numberOfLines, bet, gratisGamesLeft, additionalInformation);
                case Games.Unicorn40MegaFlames:
                case Games.Unicorn40DiceFlames:
                    return GetCombination40MegaFlames(matrixArray, numberOfLines, bet);
                case Games.UnicornWinterFruits:
                case Games.Unicorn40HotStrike:
                case Games.Unicorn40HotStrikeDice:
                    return GetCombinationWinterFruits(matrixArray, numberOfLines, bet);
                case Games.UnicornFastFruits:
                    return GetCombinationFastFruits(matrixArray, numberOfLines, bet);
                case Games.UnicornMoneyStandardWild:
                    return GetCombinationMoneyStandardWild(matrixArray, numberOfLines, bet);
                case Games.UnicornCoyoteSevens:
                case Games.UnicornCoyoteDice:
                case Games.UnicornSavanaFruits:
                    return GetCombinationCoyoteSevens(matrixArray, numberOfLines, bet);
                case Games.Unicorn40FruitReels:
                    return GetCombination40FruitReels(matrixArray, numberOfLines, bet);
                case Games.UnicornBikiniFruits:
                case Games.UnicornBikiniDice:
                    return GetCombinationBikiniFruits(matrixArray, numberOfLines, bet, gratisGamesLeft, additionalInformation);
                case Games.UnicornQueenOfPyramids:
                    return GetCombinationQueenOfPyramids(matrixArray, numberOfLines, bet, gratisGamesLeft, additionalInformation);
                case Games.UnicornFireStars:
                case Games.UnicornStarDiceChristmas:
                    return GetCombinationFireStars(matrixArray, numberOfLines, bet);
                case Games.UnicornSimplySevens:
                case Games.UnicornSimplySevensDice:
                case Games.UnicornLavaDiamonds:
                case Games.UnicornLavaDice:
                case Games.UnicornLavaDiamondsChristmas:
                case Games.UnicornSimplySevensDiceEaster:
                case Games.UnicornBunnyDice:
                    return GetCombinationSimplySevens(matrixArray, numberOfLines, bet);
                case Games.Unicorn10JingleFruits:
                    return GetCombination10JingleFruits(matrixArray, numberOfLines, bet);
                case Games.UnicornGoldLine:
                case Games.UnicornGoldLineDice:
                    return GetCombinationGoldLine(matrixArray, bet);
                case Games.UnicornChristmasPresents:
                case Games.UnicornMisterLuck:
                    return GetCombinationChristmasPresents(matrixArray, numberOfLines, bet);
                case Games.UnicornStickyHot:
                    return GetCombinationStickyHot(matrixArray, numberOfLines, bet, gratisGamesLeft, ref additionalArray);
                case Games.UnicornPumpkinHorror:
                case Games.UnicornWildHoppers:
                    return GetCombinationPumpkinHorror(matrixArray, numberOfLines, bet);
                case Games.UnicornHitLine:
                case Games.UnicornHitLineDice:
                    return GetCombinationHitLine(matrixArray, numberOfLines, bet);
                case Games.Unicorn20SuperFlames:
                    return GetCombination20SuperFlames(matrixArray, numberOfLines, bet);
                case Games.Unicorn20HotStrikeJackpot:
                case Games.Unicorn20HotStrikeDiceJackpot:
                    return GetCombination20HotStrikeJackpot(matrixArray, numberOfLines, bet);
                case Games.UnicornEpicMegaCash:
                    return GetCombinationEpicMegaCash(matrixArray, numberOfLines, bet);
                case Games.UnicornBigHitSevens:
                    return GetCombinationBigHitSevens(matrixArray, numberOfLines, bet);
            }

            return null;
        }
    }
}