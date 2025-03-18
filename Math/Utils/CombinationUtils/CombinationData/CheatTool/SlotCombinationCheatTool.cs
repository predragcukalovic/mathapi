using System;
using System.Collections.Generic;
using System.Linq;
using MathCombination.ReaderData;
using MathForGames.Game3WildFruits;
using MathForGames.GameAlpinist;
using MathForGames.GameBookOfBruno;
using MathForGames.GameBookOfMayanGold;
using MathForGames.GameBurningIce;
using MathForGames.GameBurstingHot5;
using MathForGames.GameCaptainShark;
using MathForGames.GameCloverCash;
using MathForGames.GameCrissCross;
using MathForGames.GameCrystalHot40Deluxe;
using MathForGames.GameCrystalsOfMagic;
using MathForGames.GameDiamonds;
using MathForGames.GameFenixPlay;
using MathForGames.GameForestFruits;
using MathForGames.GameFruits;
using MathForGames.GameFruityJokerHot;
using MathForGames.GameGoldenClover;
using MathForGames.GameGoldenCrown;
using MathForGames.GameHot777;
using MathForGames.GameHotParty;
using MathForGames.GameHotStars;
using MathForGames.GameJuicyHot;
using MathForGames.GameJungle;
using MathForGames.GameLuckyTwister;
using MathForGames.GameMagicFruits;
using MathForGames.GameMagicOfTheRing;
using MathForGames.GameMagicTarget;
using MathForGames.GameMegaHot;
using MathForGames.GameMonsters;
using MathForGames.GameNeonHot5;
using MathForGames.GamePirates;
using MathForGames.GamePokerSlot;
using MathForGames.GamePostman;
using MathForGames.GamePyramid;
using MathForGames.GameSpellbook;
using MathForGames.GameStarGems;
using MathForGames.GameTemplarsQuest;
using MathForGames.GameTripleHot;
using MathForGames.GameTropicalHot;
using MathForGames.GameTurboHot40;
using MathForGames.GameVegasHot;
using MathForGames.GameVikingGold;
using MathForGames.GameWildClover40;
using MathForGames.GameWildWest;
using MathForGames.GameWizard;
using RNGUtils.RandomData;
using Utils.Games;

namespace MathCombination.CombinationData.CheatTool
{
    public static class SlotCombinationCheatTool
    {

        #region Private methods

        /// <summary>
        /// Daje kombinaciju za igru Postman.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGame"></param>
        /// <returns></returns>
        private static ICombination GetCombinationPostman(int[,] matrixArray, int numberOfLines, int bet, bool gratisGame)
        {
            var matrix = new MatrixPostman();
            matrix.FromMatrixArray(matrixArray);

            var combination = new Combination();
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGame);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru Jungle.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationJungle(int[,] matrixArray, int bet)
        {
            var matrix = new MatrixJungle();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru Wizard.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGame"></param>
        /// <returns></returns>
        private static ICombination GetCombinationWizard(int[,] matrixArray, int numberOfLines, int bet, bool gratisGame)
        {
            var matrix = new MatrixWizard();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGame);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru Fruits & Stars.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationFruits(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new MatrixFruits();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru Nevada Hot.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationNevadaHot(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new MatrixVegasHot();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination3();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru Golden Fenix.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationGoldenFenix(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new MatrixFenixPlay();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination3();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru Hot Beach.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationHotBeach(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new MatrixHotParty();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru King Of The Sea.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGame"> </param>
        /// <returns></returns>
        private static ICombination GetCombinationKingOfTheSea(int[,] matrixArray, int numberOfLines, int bet,
            bool gratisGame)
        {
            var matrix = new MatrixCaptainShark();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGame);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru Magic Joker.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGame"> </param>
        /// <returns></returns>
        private static ICombination GetCombinationMagicJoker(int[,] matrixArray, int numberOfLines, int bet,
            bool gratisGame)
        {
            var matrix = new MatrixMagicTarget();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGame);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru Rings Magic.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGame"> </param>
        /// <param name="gratisSymbol"> </param>
        /// <returns></returns>
        private static ICombination GetCombinationRingsMagic(int[,] matrixArray, int numberOfLines, int bet,
            bool gratisGame, byte gratisSymbol)
        {
            var matrix = new MatrixMagicOfTheRing();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGame, gratisSymbol);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru Triple 7 Hot.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGame"> </param>
        /// <param name="coins"> </param>
        /// <returns></returns>
        private static ICombination GetCombinationTriple7Hot(int[,] matrixArray, int numberOfLines, int bet,
            bool gratisGame, byte coins)
        {
            var matrix = new MatrixHot777();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination3();
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGame, coins);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru Magic Cherry.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationMagicCherry(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new MatrixMagicFruits();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination3();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru Spellbook.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGame"> </param>
        /// <param name="gratisSymbol"> </param>
        /// <returns></returns>
        private static ICombination GetCombinationSpellbook(int[,] matrixArray, int numberOfLines, int bet, bool gratisGame,
            byte gratisSymbol)
        {
            var matrix = new MatrixSpellbook();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGame, gratisSymbol);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru Wild West.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGame"></param>
        /// <param name="additionalInformation"></param>
        /// <returns></returns>
        private static ICombination GetCombinationWildWest(int[,] matrixArray, int bet, bool gratisGame,
            byte additionalInformation)
        {
            var matrix = new MatrixWildWest();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, bet, gratisGame, additionalInformation);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru Rollin' Dices 81.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <param name="additionalInformation"></param>
        /// <returns></returns>
        private static ICombination GetCombinationRollingDices81(int[,] matrixArray, int bet, byte additionalInformation)
        {
            var matrix = new MatrixCrissCross();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination4(additionalInformation);
            combination.MatrixToCombination(matrix, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru Pyramid.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGame"></param>
        /// <param name="pyramid"> </param>
        /// <returns></returns>
        private static ICombination GetCombinationPyramid(int[,] matrixArray, int numberOfLines, int bet, bool gratisGame,
            byte pyramid)
        {
            var matrix = new MatrixPyramid();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGame, pyramid);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru Alpinist.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGamesLeft"> </param>
        /// <returns></returns>
        private static ICombination GetCombinationAlpinist(int[,] matrixArray, int bet, int gratisGamesLeft)
        {
            var matrix = new MatrixAlpinist();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, bet, gratisGamesLeft);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru Diamonds.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationDiamonds(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new MatrixDiamonds();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru Juicy Hot.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationJuicyHot(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new MatrixJuicyHot();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru Hot Stars.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="stars"> </param>
        /// <returns></returns>
        private static ICombination GetCombinationHotStars(int[,] matrixArray, int numberOfLines, int bet, byte stars)
        {
            var matrix = new MatrixHotStars();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, numberOfLines, bet, stars);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru Mega Hot.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationMegaHot(int[,] matrixArray, int bet)
        {
            var matrix = new MatrixMegaHot();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru BurningIce.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGame"> </param>
        /// <param name="addInfo"></param>
        /// <param name="addArray"></param>
        /// <returns></returns>
        private static ICombination GetCombinationBurningIce(int[,] matrixArray, int bet, bool gratisGame, byte addInfo,
            ref byte[] addArray)
        {
            var matrix = new MatrixBurningIce();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination3();
            combination.MatrixToCombination(matrix, bet, gratisGame, addInfo, ref addArray);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru TurboHot40.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <param name="numberOfLines"></param>
        /// <returns></returns>
        private static ICombination GetCombinationTurboHot40(int[,] matrixArray, int bet, int numberOfLines)
        {
            var matrix = new MatrixTurboHot40();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, bet, numberOfLines);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru WildClover40.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="gratisGame"></param>
        /// <returns></returns>
        private static ICombination GetCombinationWildClover40(int[,] matrixArray, int bet, int numberOfLines,
            bool gratisGame)
        {
            var matrix = new MatrixWildClover40();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGame);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru CrystalHot40Deluxe.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <param name="numberOfLines"></param>
        /// <returns></returns>
        private static ICombination GetCombinationCrystalHot40Deluxe(int[,] matrixArray, int bet, int numberOfLines)
        {
            var matrix = new MatrixCrystalHot40Deluxe();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru Tropical Hot.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationTropicalHot(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new MatrixTropicalHot();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru Pirates.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGame"></param>
        /// <param name="pirate"> </param>
        /// <returns></returns>
        private static ICombination GetCombinationPirates(int[,] matrixArray, int numberOfLines, int bet, bool gratisGame,
            byte pirate)
        {
            var matrix = new MatrixPirates();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGame, pirate);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru Book Of Bruno.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGame"> </param>
        /// <param name="gratisSymbol"> </param>
        /// <returns></returns>
        private static ICombination GetCombinationBookOfBruno(int[,] matrixArray, int numberOfLines, int bet,
            bool gratisGame, byte gratisSymbol)
        {
            var matrix = new MatrixBookOfBruno();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGame, gratisSymbol);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru Crystals of Magic.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGame"> </param>
        /// <param name="addInfo"></param>
        /// <param name="addArray"></param>
        /// <param name="selectedField">Izabrano polje za Cartman bonus</param>
        /// <returns></returns>
        private static ICombination GetCombinationCrystalsOfMagic(int[,] matrixArray, int numberOfLines, int bet,
            bool gratisGame, byte addInfo, ref byte[] addArray, int selectedField)
        {
            var matrix = new MatrixCrystalsOfMagic();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationGame();
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGame, addInfo, ref addArray, selectedField);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru Monsters.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGame"></param>
        /// <param name="additionalInformation"></param>
        /// <returns></returns>
        private static ICombination GetCombinationMonsters(int[,] matrixArray, int bet, bool gratisGame,
            byte additionalInformation)
        {
            var matrix = new MatrixMonsters();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, bet, gratisGame, additionalInformation);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru Forest Fruits.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationForestFruits(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new MatrixForestFruits();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru Viking Gold.
        /// </summary>
        /// <param name="bet"></param>
        /// <param name="gratisLeft"></param>
        /// <param name="addArray"></param>
        /// <param name="reels"></param>
        /// <returns></returns>
        private static ICombination GetCombinationVikingGold(int bet, int gratisLeft, ref byte[] addArray,
            params List<byte>[] reels)
        {
            if (addArray == null)
            {
                addArray = new byte[74];
                addArray[35] = 1;
                addArray[73] = 1;
            }

            var matrix = new MatrixVikingGold();
            var combination = new Combination();
            combination.MatrixToCombination(matrix, bet, gratisLeft, ref addArray, reels);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru Star Gems.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="stars"> </param>
        /// <returns></returns>
        private static ICombination GetCombinationStarGems(int[,] matrixArray, int numberOfLines, int bet, byte stars)
        {
            var matrix = new MatrixStarGems();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, numberOfLines, bet, stars);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru TemplarsQuest.
        /// </summary>
        /// <param name="gratisGame"></param>
        /// <param name="bet"></param>
        /// <param name="addInfo"> </param>
        /// <param name="addArray"></param>
        /// <param name="selectField"></param>
        /// <param name="reels"></param>
        /// <returns></returns>
        private static ICombination GetCombinationTemplarsQuest(bool gratisGame, int bet, byte addInfo, ref byte[] addArray,
            int selectField, params List<byte>[] reels)
        {
            if (addArray == null)
            {
                addArray = new byte[16];
                addArray[0] = 1;
            }

            var matrix = new MatrixTemplarsQuest();
            var combination = new CombinationGame();
            combination.MatrixToCombination(matrix, bet, gratisGame, addInfo, ref addArray, selectField, reels);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru 5 Neon Hot.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationNeonHot5(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new MatrixNeonHot5();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru TripleHot.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationTripleHot(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new MatrixTripleHot();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination3();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru PokerSlot.
        /// </summary>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGame"></param>
        /// <returns></returns>
        private static ICombination GetCombinationPokerSlot(int numberOfLines, int bet, bool gratisGame)
        {
            var matrix = new MatrixPokerSlot();
            matrix.SetRandomMatrix(gratisGame);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGame);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru LuckyTwister.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationLuckyTwister(int[,] matrixArray, int bet)
        {
            var matrix = new MatrixLuckyTwister();
            matrix.FormMatrixFromArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru GoldenClover.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGame"> </param>
        /// <param name="addArray"></param>
        /// <param name="gameData"></param>
        /// <returns></returns>
        private static ICombination GetCombinationGoldenClover(int[,] matrixArray, int numberOfLines, int bet,
            bool gratisGame, ref byte[] addArray, object gameData)
        {
            if (addArray == null)
            {
                addArray = new byte[20];
            }

            if (gameData == null)
            {
                throw new Exception("Slot Combination Reader Exception: Golden Clover gameData object is NULL!");
            }

            var matrix = new MatrixGoldenClover();
            matrix.FromMatrixArray(matrixArray);
            MatrixGoldenClover.SetJackpotLimits((MatrixGoldenClover.GoldenCloverConfig)gameData, bet);
            matrix.SetAllData(gratisGame, ref addArray);
            var combination = new CombinationGame();
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGame, ref addArray,
                (MatrixGoldenClover.GoldenCloverConfig)gameData);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru BookOfMayanGold.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGame"> </param>
        /// <param name="gratisSymbol"> </param>
        /// <returns></returns>
        private static ICombination GetCombinationBookOfMayanGold(int[,] matrixArray, int numberOfLines, int bet,
            bool gratisGame, byte gratisSymbol)
        {
            var matrix = new MatrixBookOfMayanGold();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGame, gratisSymbol);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru Bursting Hot 5.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationBurstingHot5(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new MatrixBurstingHot5();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru 3WildFruits.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGame"> </param>
        /// <param name="addInfo"></param>
        /// <param name="addArray"></param>
        /// <returns></returns>
        private static ICombination GetCombination3WildFruits(int[,] matrixArray, int bet, bool gratisGame, byte addInfo,
            ref byte[] addArray)
        {
            if (addArray == null)
            {
                addArray = new byte[9];
            }

            var matrix = new Matrix3WildFruits();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination3();
            combination.MatrixToCombination(matrix, bet, gratisGame, addInfo, ref addArray);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru Golden Crown.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationGoldenCrown(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new MatrixGoldenCrown();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru FriutyJokerHot.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationFruityJokerHot(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new MatrixFruityJokerHot();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru Clover cash.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGame"> </param>
        /// <param name="addArray"></param>
        /// <returns></returns>
        private static ICombination GetCombinationCloverCash(int[,] matrixArray, int numberOfLines, int bet,
            bool gratisGame, ref byte[] addArray)
        {
            if (addArray == null)
            {
                addArray = new byte[17];
            }

            var matrix = new MatrixCloverCash();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGame, ref addArray);
            return combination;
        }

        /// <summary>
        /// Učitava rilove iz .fsl fajla.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="gratisGame"></param>
        /// <param name="addInfo"></param>
        /// <returns></returns>
        private static List<byte>[] ReadReelsFromSlotFile(Games game, bool gratisGame, byte addInfo)
        {
            try
            {
                var extension = gratisGame ? "G" : "";
                if (game == Games.CrystalsOfMagic && gratisGame && addInfo == 4)
                {
                    extension = "GG";
                }

                var byteArray = MathSlotFilesReader.GetSlotFileForGame(game + extension);
                if (byteArray == null)
                {
                    throw new Exception("No .fsl file for game " + game);
                }

                if (byteArray[0] != 0xFC || byteArray[1] != 0xFE || byteArray[2] != 0xDE || byteArray[3] != 0x03)
                {
                    throw new Exception("Bad .fsl file.");
                }

                var m = byteArray[9];
                var gameName = System.Text.Encoding.ASCII.GetString(byteArray.Skip(10).Take(m).ToArray());
                if (gameName != game.ToString())
                {
                    throw new Exception("Wrong game for .fsl file.");
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
                    throw new Exception("Bad .fsl file.");
                }

                return finalReels;
            }
            catch (Exception ex)
            {
                throw new Exception("SlotCombination Reader Exception: " + ex);
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
                if (cheatToolDataObj.StoppingReelsNotUsingMatrix)
                {
                    if (cheatToolDataObj.IndicesInReels != null)
                    {
                        for (var i = 0; i < reels.Length; i++)
                        {
                            var size = reels[i].Count;
                            var element = (cheatToolDataObj.IndicesInReels.Length > i) ? cheatToolDataObj.IndicesInReels[i] : 0;
                            if (element != 0) //NAPOMENA: Decrementira se jer su elementi reelova u PARSheet-u indexirani od jedinice
                            {
                                element--;
                            }
                            for (var j = 0; j < 7; j++)
                            {
                                matrixArray[i, j] = reels[i][(element + j) % size];
                            }
                        }
                        return matrixArray;
                    }
                }
                else if (cheatToolDataObj.NewMatrix != null)
                {
                    var reelsFromCheatData = cheatToolDataObj.NewMatrix.GetLength(0);
                    var rows = cheatToolDataObj.NewMatrix.GetLength(1);

                    for (int i = 0; i < reels.Length; i++)
                    {
                        for (int j = 0; j < 7; j++)
                        {
                            //NAPOMENA: Defaultno se dodeljuje vrednost 3, posto u do sada implementiranim igrama simbol sa ID-jem tri je regularan simbol
                            matrixArray[i, j] = (i < reelsFromCheatData && j < rows) ? cheatToolDataObj.NewMatrix[i, j] : 3;
                        }
                    }
                    return matrixArray;
                }
            }
            return ReadMatrixArrayFromReelsRegular(reels);
        }
        #endregion

        #region Public methods


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
        /// <param name="selectedField">Izabrano polje za Cartman bonus za CrystalsOfMagic</param>
        /// <param name="gameDataObj"></param>
        /// <returns></returns>
        public static ICombination GetCombination(ref CheatToolData cheatToolObject, Games game, int bet, int numberOfLines, int gratisGamesLeft,
        ref byte[] additionalArray, byte additionalInformation = 0, int selectedField = 0, object gameDataObj = null)
        {
            if (game == Games.SpinCards)
            {
                return GetCombinationPokerSlot(numberOfLines, bet, gratisGamesLeft > 0);
            }

            var reels = ReadReelsFromSlotFile(game, gratisGamesLeft > 0, additionalInformation);

            switch (game)
            {
                case Games.VikingGold:
                    if (numberOfLines != 20)
                    {
                        throw new Exception("Slot Combination Reader Exception: Viking Gold must have 20 lines, but have " +
                                            numberOfLines + " lines !");
                    }

                    return GetCombinationVikingGold(bet, gratisGamesLeft, ref additionalArray, reels);
                case Games.TemplarsQuest:
                    if (numberOfLines != 10)
                    {
                        throw new Exception(
                            "Slot Combination Reader Exception: Templars Quest must have 10 lines, but have " +
                            numberOfLines + " lines !");
                    }

                    return GetCombinationTemplarsQuest(gratisGamesLeft > 0, bet, additionalInformation, ref additionalArray,
                        selectedField, reels);
            }

            var matrixArray = ReadMatrixArrayFromReelsCheat(ref cheatToolObject, reels);

            switch (game)
            {
                case Games.Postman:
                case Games.AlohaCharm:
                case Games.DolphinsShine:
                    return GetCombinationPostman(matrixArray, numberOfLines, bet, gratisGamesLeft > 0);
                case Games.DeepJungle:
                    if (numberOfLines != 20)
                    {
                        throw new Exception("Slot Combination Reader Exception: Jungle must have 20 lines, but have " +
                                            numberOfLines + " lines !");
                    }

                    return GetCombinationJungle(matrixArray, bet);
                case Games.Wizard:
                case Games.KatanasOfTime:
                    return GetCombinationWizard(matrixArray, numberOfLines, bet, gratisGamesLeft > 0);
                case Games.FruitsAndStars:
                case Games.FruitsAndStars40:
                case Games.CubesAndStars:
                    return GetCombinationFruits(matrixArray, numberOfLines, bet);
                case Games.VegasHot:
                    return GetCombinationNevadaHot(matrixArray, numberOfLines, bet);
                case Games.FenixPlay:
                    return GetCombinationGoldenFenix(matrixArray, numberOfLines, bet);
                case Games.HotParty:
                    return GetCombinationHotBeach(matrixArray, numberOfLines, bet);
                case Games.CaptainShark:
                    return GetCombinationKingOfTheSea(matrixArray, numberOfLines, bet, gratisGamesLeft > 0);
                case Games.MagicTarget:
                    return GetCombinationMagicJoker(matrixArray, numberOfLines, bet, gratisGamesLeft > 0);
                case Games.MagicOfTheRing:
                    return GetCombinationRingsMagic(matrixArray, numberOfLines, bet, gratisGamesLeft > 0,
                        additionalInformation);
                case Games.Hot777:
                    return GetCombinationTriple7Hot(matrixArray, numberOfLines, bet, gratisGamesLeft > 0,
                        additionalInformation);
                case Games.MagicFruits:
                    return GetCombinationMagicCherry(matrixArray, numberOfLines, bet);
                case Games.Spellbook:
                case Games.BookOfSpellsV2:
                case Games.DiceOfSpells:
                case Games.BookOfSpellsDeluxe:
                    return GetCombinationSpellbook(matrixArray, numberOfLines, bet, gratisGamesLeft > 0,
                        additionalInformation);
                case Games.WildWest:
                    if (numberOfLines != 20)
                    {
                        throw new Exception("Slot Combination Reader Exception: Wild West must have 20 lines, but have " +
                                            numberOfLines + " lines !");
                    }

                    return GetCombinationWildWest(matrixArray, bet, gratisGamesLeft > 0, additionalInformation);
                case Games.RollingDices81:
                    return GetCombinationRollingDices81(matrixArray, bet, additionalInformation);
                case Games.Pyramid:
                    return GetCombinationPyramid(matrixArray, numberOfLines, bet, gratisGamesLeft > 0,
                        additionalInformation);
                case Games.Alpinist:
                    return GetCombinationAlpinist(matrixArray, bet, gratisGamesLeft);
                case Games.Diamonds:
                    return GetCombinationDiamonds(matrixArray, numberOfLines, bet);
                case Games.JuicyHot:
                    return GetCombinationJuicyHot(matrixArray, numberOfLines, bet);
                case Games.TwinklingHot5:
                case Games.JazzyFruits:
                    if (numberOfLines != 5)
                    {
                        throw new Exception(
                            "Slot Combination Reader Exception: TwinklingHot5 must have 5 lines, but have " +
                            numberOfLines + " lines !");
                    }

                    return GetCombinationJuicyHot(matrixArray, numberOfLines, bet);
                case Games.TwinklingHot40:
                    if (numberOfLines != 40)
                    {
                        throw new Exception(
                            "Slot Combination Reader Exception: TwinklingHot40 must have 40 lines, but have " +
                            numberOfLines + " lines !");
                    }

                    return GetCombinationJuicyHot(matrixArray, numberOfLines, bet);
                case Games.HotStars:
                case Games.SpaceGuardians:
                    return GetCombinationHotStars(matrixArray, numberOfLines, bet, additionalInformation);
                case Games.MegaHot:
                    if (numberOfLines != 5)
                    {
                        throw new Exception("Slot Combination Reader Exception: MegaHot must have 5 lines, but have " +
                                            numberOfLines + " lines !");
                    }

                    return GetCombinationMegaHot(matrixArray, bet);
                case Games.BurningIce:
                case Games.BurningIceDeluxe:
                case Games.FlashingDice:
                case Games.BurningIceGd:
                    return GetCombinationBurningIce(matrixArray, bet, gratisGamesLeft > 0, additionalInformation,
                        ref additionalArray);
                case Games.TurboHot40:
                case Games.CrystalHot40:
                case Games.TurboDice40:
                case Games.CrystalHot40Gd:
                case Games.CrystalHot40Gd2:
                case Games.WildHot40:
                case Games.CrystalHot401X:
                case Games.CrystalJokerHot:
                case Games.CrystalHotAdmiral:
                    if (numberOfLines != 40)
                    {
                        throw new Exception("Slot Combination Reader Exception: " + game +
                                            " must have 40 lines, but have " + numberOfLines + " lines !");
                    }

                    return GetCombinationTurboHot40(matrixArray, bet, 40);
                case Games.WildClover40:
                    if (numberOfLines != 40)
                    {
                        throw new Exception("Slot Combination Reader Exception: " + game +
                                            " must have 40 lines, but have " + numberOfLines + " lines !");
                    }

                    return GetCombinationWildClover40(matrixArray, bet, 40, gratisGamesLeft > 0);
                case Games.CrystalHot40Deluxe:
                    if (numberOfLines != 40)
                    {
                        throw new Exception("Slot Combination Reader Exception: " + game +
                                            " must have 40 lines, but have " + numberOfLines + " lines !");
                    }

                    return GetCombinationCrystalHot40Deluxe(matrixArray, bet, numberOfLines);
                case Games.CrystalHot80:
                    if (numberOfLines != 80)
                    {
                        throw new Exception("Slot Combination Reader Exception: " + game +
                                            " must have 80 lines, but have " + numberOfLines + " lines !");
                    }

                    return GetCombinationTurboHot40(matrixArray, bet, 80);
                case Games.TropicalHot:
                    return GetCombinationTropicalHot(matrixArray, numberOfLines, bet);
                case Games.BookOfBruno:
                    return GetCombinationBookOfBruno(matrixArray, numberOfLines, bet, gratisGamesLeft > 0,
                        additionalInformation);
                case Games.CrystalsOfMagic:
                    if (numberOfLines != 25 && !(gratisGamesLeft > 0))
                    {
                        throw new Exception(
                            "Slot Combination Reader Exception: CrystalsOfMagic must have 25 lines, but have " +
                            numberOfLines + " lines !");
                    }

                    return GetCombinationCrystalsOfMagic(matrixArray, numberOfLines, bet, gratisGamesLeft > 0,
                        additionalInformation, ref additionalArray, selectedField);
                case Games.Pirates:
                    return GetCombinationPirates(matrixArray, numberOfLines, bet, gratisGamesLeft > 0,
                        additionalInformation);
                case Games.Monsters:
                    if (numberOfLines != 20)
                    {
                        throw new Exception("Slot Combination Reader Exception: Monsters must have 20 lines, but have " +
                                            numberOfLines + " lines !");
                    }

                    return GetCombinationMonsters(matrixArray, bet, gratisGamesLeft > 0, additionalInformation);
                case Games.ForestFruits:
                    return GetCombinationForestFruits(matrixArray, numberOfLines, bet);
                case Games.StarGems:
                case Games.Starlight:
                case Games.StarRunner:
                case Games.CrystalWin:
                    return GetCombinationStarGems(matrixArray, numberOfLines, bet, additionalInformation);
                case Games.NeonHot5:
                case Games.NeonDice5:
                    return GetCombinationNeonHot5(matrixArray, numberOfLines, bet);
                case Games.TripleHot:
                case Games.TripleDice:
                case Games.Retro7Hot:
                    if (numberOfLines != 5)
                    {
                        throw new Exception("Slot Combination Reader Exception: " + game + " must have 5 lines, but have " +
                                            numberOfLines + " lines !");
                    }

                    return GetCombinationTripleHot(matrixArray, numberOfLines, bet);
                case Games.LuckyTwister:
                case Games.BuffaloFortune:
                    if (numberOfLines != 10)
                    {
                        throw new Exception("Slot Combination Reader Exception: " + game +
                                            " must have 10 lines, but have " + numberOfLines + " lines !");
                    }

                    return GetCombinationLuckyTwister(matrixArray, bet);
                case Games.GoldenClover:
                    return GetCombinationGoldenClover(matrixArray, numberOfLines, bet, gratisGamesLeft > 0,
                        ref additionalArray, gameDataObj);
                case Games.BookOfMayanGold:
                    return GetCombinationBookOfMayanGold(matrixArray, numberOfLines, bet, gratisGamesLeft > 0,
                        additionalInformation);
                case Games.BurstingHot5:
                case Games.FireClover5:
                    if (numberOfLines != 5)
                    {
                        throw new Exception("Slot Combination Reader Exception: " + game + " must have 5 lines, but have " +
                                            numberOfLines + " lines !");
                    }

                    return GetCombinationBurstingHot5(matrixArray, numberOfLines, bet);
                case Games.BurstingHot40:
                    if (numberOfLines != 40)
                    {
                        throw new Exception("Slot Combination Reader Exception: " + game +
                                            " must have 40 lines, but have " + numberOfLines + " lines !");
                    }

                    return GetCombinationBurstingHot5(matrixArray, numberOfLines, bet);
                case Games.GoldenCrown:
                case Games.WildCrown10:
                    if (numberOfLines != 10)
                    {
                        throw new Exception("Slot Combination Reader Exception: " + game +
                                            " must have 10 lines, but have " + numberOfLines + " lines !");
                    }

                    return GetCombinationGoldenCrown(matrixArray, numberOfLines, bet);
                case Games.MysteryJokerHot:
                    return GetCombination3WildFruits(matrixArray, bet, gratisGamesLeft > 0, additionalInformation,
                        ref additionalArray);
                case Games.FruityJokerHot:
                    return GetCombinationFruityJokerHot(matrixArray, numberOfLines, bet);
                case Games.CloverCash:
                    return GetCombinationCloverCash(matrixArray, numberOfLines, bet, gratisGamesLeft > 0,
                        ref additionalArray);
                default:
                    return null;
            }
        }

        public static object GetBonusGameData(Games game, byte[] addArray)
        {
            switch (game)
            {
                case Games.CrystalsOfMagic:
                    return MatrixCrystalsOfMagic.GetFirstBonusData(addArray);
                default:
                    return null;
            }
        }

        /// <summary>
        /// Daje dobitak za kaskadne igre tipa Viking Gold.
        /// </summary>
        /// <param name="combination"></param>
        /// <param name="game"></param>
        /// <returns></returns>
        public static long GetCascadeWin(ICombination combination, Games game)
        {
            switch (game)
            {
                case Games.VikingGold:
                case Games.TemplarsQuest:
                    long win = combination.TotalWin;
                    if (combination.CascadeList == null || combination.CascadeList.Count == 0)
                    {
                        return win;
                    }

                    win += combination.CascadeList.Sum(t => t.TotalWin);
                    return win;
                default:
                    return -1;
            }
        }

        #endregion

    }
}
