using CombinationExtras.ReaderData;
using CombinationUnicornUtils;
using CombinationUtils.UnicornCombinationData;
using Papi.GameServer.Utils.Enums;
using MathCombination.CombinationData;
using MathCombination.ReelsData;
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
using MathForUnicornGames.GameTopPrizeWilds;
using MathForUnicornGames.GameTwentyFruits;
using MathForUnicornGames.GameVegasDice;
using MathForUnicornGames.GameWildParadice;
using MathForUnicornGames.GameWinterFruits;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CombinationExtras
{
    public static class UnicornSlotCombination
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
        private static ICombination GetCombinationIslandRespins2(int numberOfLines, int bet, bool gratisGame, byte addInfo)
        {
            var matrixArray = MatrixIslandRespins2.GetMatixArray(addInfo);
            var matrix = new MatrixIslandRespins2();
            matrix.FromMatrixArrayIslandRespins2(matrixArray);
            var combination = new CombinationUnicornNew();
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGame, addInfo);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru GreatWhale i sve njene klonove.
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
        /// Daje kombinaciju za igru 20HotStrike.
        /// </summary>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombination20HotStrike(int numberOfLines, int bet)
        {
            var matrixArray = Matrix20HotStrike.GetMatixArray();
            var matrix = new Matrix20HotStrike();
            matrix.FromMatrixArray20HotStrikes(matrixArray);
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
        /// Daje kombinaciju za igru FruitWildLines.
        /// </summary>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationFruitWildLines(int numberOfLines, int bet)
        {
            int wild;
            var matrixArray = MatrixFruitWildLines.GetMatixArray(out wild);
            var matrix = new MatrixFruitWildLines();
            matrix.FromMatrixArrayFruitWildLines(matrixArray);
            var combination = new CombinationUnicornNew();
            combination.MatrixToCombination(matrix, numberOfLines, bet, wild);
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
        /// Daje kombinaciju za igru 5HotStrike.
        /// </summary>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombination5HotStrike(int numberOfLines, int bet)
        {
            var matrixArray = Matrix5HotStrike.GetMatixArray();
            var matrix = new Matrix5HotStrike();
            matrix.FromMatrixArray5HotStrike(matrixArray);
            var combination = new CombinationUnicornNew();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru BikiniFruits.
        /// </summary>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationBikiniFruits(int numberOfLines, int bet, int gratisGamesLeft, byte additionalInformation)
        {
            var matrixArray = MatrixBikiniFruits.GetMatixArray(gratisGamesLeft > 0);
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
        private static ICombination GetCombinationQueenOfPyramids(int numberOfLines, int bet, int gratisGamesLeft, byte additionalInformation)
        {
            var matrixArray = MatrixQueenOfPyramids.GetMatixArray(gratisGamesLeft > 0);
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
        private static ICombination GetCombinationFireStars(int numberOfLines, int bet)
        {
            var matrixArray = MatrixFireStars.GetMatixArray();
            var matrix = new MatrixFireStars();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationUnicornNew();
            combination.MatrixToCombination(matrix, bet, numberOfLines);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru SimplySevens.
        /// </summary>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationSimplySevens(int numberOfLines, int bet)
        {
            var matrixArray = MatrixSimplySevens.GetMatixArray();
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
        private static ICombination GetCombination10JingleFruits(int numberOfLines, int bet)
        {
            var matrixArray = Matrix10JingleFruits.GetMatixArray();
            var matrix = new Matrix10JingleFruits();
            matrix.FromMatrixArray10JingleFruits(matrixArray);
            var combination = new CombinationUnicornNew();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru ChristmasPresents.
        /// </summary>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationChristmasPresents(int numberOfLines, int bet)
        {
            var matrixArray = MatrixChristmasPresents.GetMatixArray();
            var matrix = new MatrixChristmasPresents();
            matrix.FromMatrixArrayChristmasPresents(matrixArray);
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
        private static ICombination GetCombinationGoldLine(int bet)
        {
            var matrixArray = MatrixGoldLine.GetMatixArray(out bool goldLine);
            var matrix = new MatrixGoldLine();
            matrix.FromMatrixArrayGoldLine(matrixArray);
            var combination = new CombinationUnicornNew();
            combination.MatrixToCombination2(matrix, bet, goldLine);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru Big Spin Sevens.
        /// </summary>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationBigSpinSevens(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new MatrixBigSpinSevens();
            matrix.FromMatrixArrayBigSpinSevens(matrixArray);
            var combination = new CombinationUnicornNew();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru Buffalo Sevens.
        /// </summary>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationBuffaloSevens(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new MatrixBuffaloSevens();
            matrix.FromMatrixArrayBuffaloSevens(matrixArray);
            var combination = new CombinationUnicornNew();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru Fruit Classic.
        /// </summary>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationFrootClassic(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new MatrixFrootClassic();
            matrix.FromMatrixArrayFrootClassic(matrixArray);
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
        private static ICombination GetCombinationPumpkinHorror(int numberOfLines, int bet)
        {
            var matrixArray = MatrixPumpkinHorror.GetMatixArray();
            var matrix = new MatrixPumpkinHorror();
            matrix.FromMatrixArrayPumpkinHorror(matrixArray);
            var combination = new CombinationUnicornNew();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
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
        private static ICombination GetCombination20HotStrikeJackpot(int numberOfLines, int bet)
        {
            var matrixArray = Matrix20HotStrikeJackpot.GetMatixArray();
            var matrix = new Matrix20HotStrikeJackpot();
            matrix.FromMatrixArray20HotStrikeJackpot(matrixArray);
            var combination = new CombinationUnicornNew();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru EpicMegaCash.
        /// </summary>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationEpicMegaCash(int numberOfLines, int bet)
        {
            var matrixArray = MatrixEpicMegaCash.GetMatixArray();
            var matrix = new MatrixEpicMegaCash();
            matrix.FromMatrixArrayEpicMegaCash(matrixArray);
            var combination = new CombinationUnicornNew();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru TopPrizeWilds.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationTopPrizeWilds(int numberOfLines, int bet)
        {
            var matrix = new MatrixTopPrizeWilds();
            var matrixArray = MatrixTopPrizeWilds.GetMatixArray();
            matrix.FromMatrixArrayTopPrizeWilds(matrixArray);
            var combination = new CombinationUnicornNew();
            var diamondCount = matrix.GetDiamondCount(matrixArray);
            combination.MatrixToCombination(matrix, numberOfLines, bet, diamondCount);
            if (combination.TotalWin > 0 && matrix.ShouldRepeatCalculation(diamondCount))
            {
                matrixArray = MatrixTopPrizeWilds.GetMatixArray();
                matrix.FromMatrixArrayTopPrizeWilds(matrixArray);
                diamondCount = matrix.GetDiamondCount(matrixArray);
                combination = new CombinationUnicornNew();
                combination.MatrixToCombination(matrix, numberOfLines, bet, diamondCount);
            }
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
        /// <param name="usedFsl"></param>
        /// <param name="desiredFsl"></param>
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

        #endregion

        /// <summary>
        /// Daje kombinaciju za slot fajl.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="bet"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="gratisGamesLeft"></param>
        /// <param name="additionalInformation"></param>
        /// <param name="additionalArray"></param>
        public static ICombination GetCombination(Games game, int bet, int numberOfLines, int gratisGamesLeft, byte additionalInformation, ref byte[] additionalArray)
        {
            switch (game)
            {
                case Games.UnicornIslandRespins:
                    return GetCombinationIslandRespins2(numberOfLines, bet, gratisGamesLeft > 0, additionalInformation);
                case Games.Unicorn20HotStrike:
                case Games.UnicornDoubleWildDice:
                case Games.Unicorn20HotStrikeDice:
                    return GetCombination20HotStrike(numberOfLines, bet);
                case Games.UnicornFruitWildLines:
                case Games.UnicornDiceWildLines:
                    return GetCombinationFruitWildLines(numberOfLines, bet);
                case Games.Unicorn5HotStrike:
                case Games.Unicorn5HotStrikeDice:
                    return GetCombination5HotStrike(numberOfLines, bet);
                case Games.UnicornBikiniFruits:
                case Games.UnicornBikiniDice:
                    return GetCombinationBikiniFruits(numberOfLines, bet, gratisGamesLeft, additionalInformation);
                case Games.UnicornQueenOfPyramids:
                    return GetCombinationQueenOfPyramids(numberOfLines, bet, gratisGamesLeft, additionalInformation);
                case Games.UnicornFireStars:
                case Games.UnicornStarDiceChristmas:
                    return GetCombinationFireStars(numberOfLines, bet);
                case Games.UnicornSimplySevens:
                case Games.UnicornSimplySevensDice:
                case Games.UnicornLavaDiamonds:
                case Games.UnicornLavaDice:
                case Games.UnicornLavaDiamondsChristmas:
                case Games.UnicornSimplySevensDiceEaster:
                case Games.UnicornBunnyDice:
                    return GetCombinationSimplySevens(numberOfLines, bet);
                case Games.Unicorn10JingleFruits:
                    return GetCombination10JingleFruits(numberOfLines, bet);
                case Games.UnicornChristmasPresents:
                case Games.UnicornMisterLuck:
                    return GetCombinationChristmasPresents(numberOfLines, bet);
                case Games.UnicornGoldLine:
                case Games.UnicornGoldLineDice:
                    return GetCombinationGoldLine(bet);
                case Games.UnicornPumpkinHorror:
                case Games.UnicornWildHoppers:
                    return GetCombinationPumpkinHorror(numberOfLines, bet);
                case Games.Unicorn20HotStrikeJackpot:
                case Games.Unicorn20HotStrikeDiceJackpot:
                    return GetCombination20HotStrikeJackpot(numberOfLines, bet);
                case Games.UnicornEpicMegaCash:
                    return GetCombinationEpicMegaCash(numberOfLines, bet);
                case Games.UnicornTopPrizeWilds:
                    return GetCombinationTopPrizeWilds(numberOfLines, bet);

            }
            List<byte>[] reels;
            int usedFsl;
            switch (game)
            {
                case Games.UnicornGreatWhale:
                    reels = ReadReelsFromSlotFile(game, gratisGamesLeft > 0, out usedFsl, additionalInformation);
                    if (gratisGamesLeft > 0 && additionalInformation != usedFsl)
                    {
                        additionalInformation = (byte)usedFsl;
                    }
                    else
                    {
                        if (additionalInformation != usedFsl)
                            additionalInformation = 0;
                    }
                    break;
                default:
                    reels = ReadReelsFromSlotFile(game, gratisGamesLeft > 0, out usedFsl);
                    break;
            }

            var matrixArray = ReelsReader.ReadMatrixArrayFromReels(reels);

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
                //case Games.UnicornIslandRespins:
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
                case Games.UnicornBigSpinSevens:
                    return GetCombinationBigSpinSevens(matrixArray, numberOfLines, bet);
                case Games.UnicornBuffaloSevens:
                case Games.UnicornBuffaloDice:
                case Games.UnicornTripleStackedDiamonds:
                    return GetCombinationBuffaloSevens(matrixArray, numberOfLines, bet);
                case Games.UnicornFrootClassic:
                    return GetCombinationFrootClassic(matrixArray, numberOfLines, bet);
                case Games.UnicornStickyHot:
                    return GetCombinationStickyHot(matrixArray, numberOfLines, bet, gratisGamesLeft, ref additionalArray);
                case Games.UnicornHitLine:
                case Games.UnicornHitLineDice:
                    return GetCombinationHitLine(matrixArray, numberOfLines, bet);
                case Games.Unicorn20SuperFlames:
                    return GetCombination20SuperFlames(matrixArray, numberOfLines, bet);
                case Games.UnicornBigHitSevens:
                    return GetCombinationBigHitSevens(matrixArray, numberOfLines, bet);
            }

            return null;
        }
    }
}
