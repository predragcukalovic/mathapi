using CombinationExtras;
using CombinationExtras.ReaderData;
using Papi.GameServer.Math.Contracts.Requests;
using Papi.GameServer.Utils.Enums;
using Papi.GameServer.Utils.Logging;
using Game20FireCash;
using Game40FireCash;
using Game5CloverBlast;
using GameAfricanTreasure;
using GameAmazingJoker;
using GameBlazingHeat;
using GameBonusEpicCrown;
using GameBookOfDouble;
using GameCashBells40;
using GameCloverHit;
using GameCrystalHot40Max;
using GameElGrandeToro;
using GameEpicClover40;
using GameEpicFire40;
using GameFearOfDark;
using GameFruityFace;
using GameGoldenExplosion;
using GameHeatDouble;
using GameHeatingFruits;
using GameHotHotStereoWin;
using GameHotRushFruitLines;
using GameJokerQueen;
using GameJokerTripleDouble;
using GameKingOfThunder;
using GameLollasWorld;
using GameMayansBattle;
using GameMegaHot;
using GameMysticJungle;
using GamePiratesPapi;
using GameSantasPresents;
using GameSevenClassicHot;
using GameSuperLucky;
using GameToxicHaze;
using GameTripleFieldsOfLuck;
using GameVeryHot5Extreme;
using GameVeryHotRespin;
using GameVintageFruits40;
using GameWild27;
using GameWild81;
using GameWildClover506;
using GameWildCraps;
using GameWildHeartBeat;
using GameWildHeat40;
using GameWildHot40Blow;
using GameWildJokerHot;
using GameWildLuckyClover;
using GameWildLuckyClover2;
using GameWildSunburst;
using GameWinningClover5Extreme;
using MathCombination.CombinationData;
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
using MathForGames.GameMegaCubesDeluxe;
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
using MathForNovomatic.GameBookOfRaDeluxe;
using MathForNovomatic.GameLuckyLadysCharmDeluxe;
using MathForNovomatic.GameRoaringForties;
using MathForNovomatic.GameSizzlingHotDeluxe;
using NewGameBonusBells;
using Newtonsoft.Json.Linq;
using RNGUtils.RandomData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Papi.GameServer.Math.MathCheatTool
{
    public static partial class SlotCombinationCheatTool
    {
        private enum TestCombinations
        {
            None = 0,
            ScatterandBonus = 1,
            BonusWin = 2,
            BonusAndNoWIn = 3,
            ScatterOnly = 4,
            BonusOnly = 5,
            ScatterAndWin = 6,
            NoWin = 7,
            OnlyWin = 8
        }

        #region Private methods

        private static void ValidateLines(Games game, int numberOfLines, int expectedNumberOfLines)
        {
            if (numberOfLines != expectedNumberOfLines)
            {
                throw new Exception("Slot Combination Reader Exception: " + game + " must have " + expectedNumberOfLines + " lines, but have " + numberOfLines + " lines !");
            }
        }

        /// <summary>
        /// Popravi niz za chaettool tamo gde ima red iznad
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="offset"></param>
        private static int[,] FixMatrixArray(int[,] matrixArray, int offset = 1)
        {
            var reels = matrixArray.GetLength(0);
            var rows = matrixArray.GetLength(1);
            var matArray2 = new int[reels, rows];
            for (var i = 0; i < reels; i++)
            {
                for (var off = 0; off < offset; off++)
                {
                    matArray2[i, off] = 3;
                }
                for (var j = offset; j < rows; j++)
                {
                    matArray2[i, j] = matrixArray[i, j - offset];
                }
            }
            return matArray2;
        }

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
        private static ICombination GetCombinationTurboHot40(int[,] matrixArray, int bet, int numberOfLines, bool v4 = false)
        {
            if (v4)
            {
                var matArray2 = new int[5, 6];
                for (var i = 0; i < 5; i++)
                {
                    matArray2[i, 0] = 3;
                    for (var j = 1; j < 6; j++)
                    {
                        matArray2[i, j] = matrixArray[i, j - 1];
                    }
                }
                matrixArray = matArray2;
            }
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
        /// Daje kombinaciju za igru CrystalHot40Free.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="gratisGame"></param>
        /// <returns></returns>
        private static ICombination GetCombinationCrystalHot40Free(int[,] matrixArray, int bet, int numberOfLines, bool gratisGame)
        {
            var matrix = new MatrixTurboHot40();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGame);
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

            var config = ((JObject)gameData).ToObject<MatrixGoldenClover.GoldenCloverConfig>();

            var matrix = new MatrixGoldenClover();
            matrix.FromMatrixArray(matrixArray);
            MatrixGoldenClover.SetJackpotLimits(config, bet);
            matrix.SetAllData(gratisGame, ref addArray);
            var combination = new CombinationGame();
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGame, ref addArray,
                config);
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
            var combination = new CombinationCloverHit();
            combination.MatrixToCombinationCloverHit(matrix, numberOfLines, bet, gratisGame, ref addArray);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru Mega Cubes Deluxe.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationMegaCubesDeluxe(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new MatrixMegaCubesDeluxe();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru BookOfLuxorDeluxe.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGame"> </param>
        /// <param name="gratisSymbols"> </param>
        /// <returns></returns>
        private static ICombination GetCombinationBookOfDouble(int[,] matrixArray, int numberOfLines, int bet, bool gratisGame, byte gratisSymbols)
        {
            var matrix = new MatrixBookOfDouble();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGame, gratisSymbols);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru 40WildClover6.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="gratisGame"></param>
        /// <returns></returns>
        private static ICombination GetCombination40WildClover6(int[,] matrixArray, int numberOfLines, int bet, bool gratisGame)
        {
            var matrix = new Matrix40WildClover6();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGame);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru LilaWild.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <param name="numberOfLines"></param>
        /// <returns></returns>
        private static ICombination GetCombinationLilaWild(int[,] matrixArray, int bet, int numberOfLines)
        {
            var matrix = new MatrixLilaWild();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru El Grande Toro.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGame"> </param>
        /// <param name="addArray"></param>
        /// <returns></returns>
        private static ICombination GetCombinationElGrandeToro(int[,] matrixArray, int numberOfLines, int bet, bool gratisGame, ref byte[] addArray)
        {
            if (addArray == null)
            {
                addArray = new byte[15];
            }
            var matrix = new MatrixElGrandeToro();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGame, ref addArray);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru SevenClassicHot.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="gratisGame"></param>
        /// <returns></returns>
        private static ICombination GetCombinationSevenClassicHot(int[,] matrixArray, int numberOfLines, int bet, bool gratisGame)
        {
            var matrix = new MatrixSevenClassicHot();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGame);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru BonusBells.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="gratisGame"></param>
        /// <returns></returns>
        private static ICombination GetCombinationBonusBells(int[,] matrixArray, int numberOfLines, int bet, bool gratisGame)
        {
            var matrix = new MatrixNewBonusBells();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationNewBonusBells();
            combination.MatrixToCombinationNew(matrix, numberOfLines, bet, gratisGame);
            return combination;
        }

        private static ICombination GetCombinationJokerQueen(int[,] matrixArray, int numberOfLines, int bet, bool gratisGame, byte additionalInfo, ref byte[] additionalArray)
        {
            if (additionalArray == null)
            {
                additionalArray = new byte[8];
                additionalArray[0] = 4;
                additionalArray[1] = 4;
            }
            var matrix = new MatrixJokerQueen();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination3();
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGame, additionalInfo, ref additionalArray);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru 40WildClover6.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="gratisGame"></param>
        /// <returns></returns>
        private static ICombination GetCombinationCashBells40(int[,] matrixArray, int numberOfLines, int bet, bool gratisGame)
        {
            var matrix = new MatrixCashBells40();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationCashBells();
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGame);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru KingOfThunder.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="gratisGames"></param>
        /// <returns></returns>
        private static ICombination GetCombinationKingOfThunder(int[,] matrixArray, int numberOfLines, int bet, int gratisGames)
        {
            var matrix = new MatrixKingOfThunder();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationKingOfThunder();
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGames);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru CrystalHot40Max.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <param name="numberOfLines"></param>
        /// <returns></returns>
        private static ICombination GetCombinationCrystalHot40Max(int[,] matrixArray, int bet, int numberOfLines)
        {
            var matrix = new MatrixCrystalHot40Max();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationCrystalHot40Max();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru FruityFace.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="gratisGames"></param>
        /// <returns></returns>
        private static ICombination GetCombinationFruityFace(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new MatrixFruityFace();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationFruityFace();
            combination.MatrixToCombination(matrix, bet, numberOfLines);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru WildJokerHot.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationWildJokerHot(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new MatrixWildJokerHot();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationWildJokerHot();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru HeatingFruits.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="addInfo"></param>
        /// <returns></returns>
        private static ICombination GetCombinationHeatingFruits(int[,] matrixArray, int numberOfLines, int bet, byte addInfo)
        {
            var matrix = new MatrixHeatingFruits();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationHeatingFruits();
            combination.MatrixToCombination(matrix, numberOfLines, bet, addInfo);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru WildLuckyClover.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGame"></param>
        /// <param name="addInfo"></param>
        /// <returns></returns>
        private static ICombination GetCombinationWildLuckyClover(int[,] matrixArray, int numberOfLines, int bet, bool gratisGame, byte addInfo)
        {
            var matrix = new MatrixWildLuckyClover();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationWildLuckyClover();
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGame, addInfo);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru Very Hot 40 Respin.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="addInfo"></param>
        /// <returns></returns>
        private static ICombination GetCombinationVeryHot40Respin(int[,] matrixArray, int numberOfLines, int bet, byte addInfo)
        {
            var matrix = new MatrixBurstingHot5();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationVeryHotRespin();
            combination.MatrixToCombination(matrix, numberOfLines, bet, addInfo);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru WildHot40Blow.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <param name="numberOfLines"></param>
        /// <returns></returns>
        private static ICombination GetCombinationWildHot40Blow(int[,] matrixArray, int bet, int numberOfLines)
        {
            var matrix = new MatrixWildHot40Blow();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationWildHot40Blow();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru AfricanTreasure.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="gratisGames"></param>
        /// <returns></returns>
        private static ICombination GetCombinationAfricanTreasure(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new MatrixAfricanTreasure();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationAfricanTreasure();
            combination.MatrixToCombinationAfricanTreasure(matrix, bet, numberOfLines);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru GoldenExplosion.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationGoldenExplosion(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new MatrixGoldenExplosion();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationGoldenExplosion();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru VeryHot5Extreme.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationVeryHot5Extreme(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new MatrixVeryHot5Extreme();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationVeryHot5Extreme();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru EpicFire40.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <param name="numberOfLines"></param>
        /// <returns></returns>
        private static ICombination GetCombinationEpicFire40(int[,] matrixArray, int bet, int numberOfLines)
        {
            var matrix = new MatrixEpicFire40();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationEpicFire40();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru Wild27. Izmenjeno da se ne salje gornji red.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <returns></returns>
        private static ICombination GetCombinationWild27(int[,] matrixArray, int bet)
        {
            var matrix = new MatrixWild27();
            var matArray2 = new int[3, 5];
            for (var i = 0; i < 3; i++)
            {
                matArray2[i, 0] = 3;
                for (var j = 1; j < 5; j++)
                {
                    matArray2[i, j] = matrixArray[i, j - 1];
                }
            }
            matrix.FromMatrixArray(matArray2);
            var combination = new CombinationWild27();
            combination.MatrixToCombination(matrix, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru 40FireCash.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <param name="numberOfLines"></param>
        /// <returns></returns>
        private static ICombination GetCombination40FireCash(int[,] matrixArray, int bet, int numberOfLines)
        {
            var matrix = new Matrix40FireCash();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination40FireCash();
            combination.MatrixToCombination(matrix, bet, numberOfLines);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru Super Lucky.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGame"> </param>
        /// <param name="addArray"></param>
        /// <returns></returns>
        private static ICombination GetCombinationSuperLucky(int[,] matrixArray, int numberOfLines, int bet, bool gratisGame, ref byte[] addArray)
        {
            if (addArray == null)
            {
                addArray = new byte[17];
            }
            var matrix = new MatrixSuperLucky();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationSuperLucky();
            combination.MatrixToCombinationSuperLucky(matrix, numberOfLines, bet, gratisGame, ref addArray);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru PiratesPapi.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <param name="numberOfLines"></param>
        /// <returns></returns>
        private static ICombination GetCombinationPiratesPapi(int[,] matrixArray, int bet, int numberOfLines)
        {
            var matrix = new MatrixPiratesPapi();
            var matArray2 = new int[5, 6];
            for (var i = 0; i < 5; i++)
            {
                matArray2[i, 0] = 3;
                for (var j = 1; j < 6; j++)
                {
                    matArray2[i, j] = matrixArray[i, j - 1];
                }
            }
            matrix.FromMatrixArray(matArray2);
            var combination = new CombinationPiratesPapi();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru WildLuckyClover.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGame"></param>
        /// <param name="addInfo"></param>
        /// <returns></returns>
        private static ICombination GetCombinationWildLuckyClover2(int[,] matrixArray, int numberOfLines, int bet, bool gratisGame, byte addInfo)
        {
            var matrix = new MatrixWildLuckyClover2();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationWildLuckyClover2();
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGame, addInfo);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru EpicClover40.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <param name="numberOfLines"></param>
        /// <returns></returns>
        private static ICombination GetCombinationEpicClover40(int[,] matrixArray, int bet, int numberOfLines)
        {
            var matrix = new MatrixEpicClover40();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationEpicClover40();
            combination.MatrixToCombinationEpicClover40(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru JokerTripleDouble. Izmenjeno da se ne salje gornji red.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <param name="addArray"></param>
        /// <returns></returns>
        private static ICombination GetCombinationJokerTripleDouble(int[,] matrixArray, int bet, bool gratisGame, byte addInfo, ref byte[] addArray)
        {
            var matrix = new MatrixJokerTripleDouble();
            var matArray2 = new int[3, 5];
            for (var i = 0; i < 3; i++)
            {
                matArray2[i, 0] = 3;
                for (var j = 1; j < 5; j++)
                {
                    matArray2[i, j] = matrixArray[i, j - 1];
                }
            }
            if (addArray == null)
            {
                addArray = new byte[9];
            }
            matrix.FromMatrixArray(matArray2);
            var combination = new CombinationJokerTripleDouble();
            combination.MatrixToCombination(matrix, bet, gratisGame, addInfo, ref addArray);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru HeatDouble.
        /// </summary>
        /// <param name="bet"></param>
        /// <returns></returns>
        public static ICombination GetCombinationHeatDouble(int[,] matrixArray, int bet)
        {
            var matrix = new MatrixHeatDouble();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationHeatDouble();
            combination.MatrixToCombination(matrix, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru BlazingHeat.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationBlazingHeat(int[,] matrixArray, int bet)
        {
            var matrix = new MatrixBlazingHeat();
            matrix.FromMatrixArrayBlazingHeat(matrixArray);
            var combination = new CombinationBlazingHeat();
            combination.MatrixToCombinationBlazingHeat(matrix, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru HotHotStereoWin.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationHotHotStereoWin(int[,] matrixArray, int bet)
        {
            var matrix = new MatrixHotHotStereoWin();
            matrix.FromMatrixArrayHotHotStereoWin(matrixArray);
            var combination = new CombinationHotHotStereoWin();
            combination.MatrixToCombinationHotHotStereoWin(matrix, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru 5CloverBlast.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombination5CloverBlast(int[,] matrixArray, int bet)
        {
            var matrix = new Matrix5CloverBlast();
            matrix.FromMatrixArray5CloverBlast(matrixArray);
            var combination = new Combination5CloverBlast();
            combination.MatrixToCombination5CloverBlast(matrix, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru WinningClover5Extreme.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationWinningClover5Extreme(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new MatrixWinningClover5Extreme();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationWinningClover5Extreme();
            combination.MatrixToCombinationWinningClover5Extreme(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru Mayan's Battle.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGame"> </param>
        /// <returns></returns>
        private static ICombination GetCombinationMayansBattle(int[,] matrixArray, int numberOfLines, int bet, bool gratisGame)
        {
            var matrix = new MatrixMayansBattle();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationMayansBattle();
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGame, true);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru WildCraps.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGame"> </param>
        /// <returns></returns>
        private static ICombination GetCombinationWildCraps(int[,] matrixArray, int numberOfLines, int bet, bool gratisGame, byte addInfo)
        {
            var matrix = new MatrixWildCraps();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationWildCraps();
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGame, addInfo);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru RedstoneFearOfDark.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <param name="numberOfLines"></param>
        /// <returns></returns>
        private static ICombination GetCombinationFearOfDark(int[,] matrixArray, int bet, int numberOfLines)
        {
            var matrix = new MatrixFearOfDark();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationFearOfDark();
            combination.MatrixToCombinationFearOfDark(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru SantasPresents.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGame"> </param>
        /// <returns></returns>
        private static ICombination GetCombinationSantasPresents(int[,] matrixArray, int numberOfLines, int bet, bool gratisGame)
        {
            var matrix = new MatrixSantasPresents();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationSantasPresents();
            combination.MatrixToCombinationSantasPresents(matrix, numberOfLines, bet, gratisGame);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru Redstone40FruitFrenzy.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombination40FruitFrenzy(int[,] matrixArray, int bet)
        {
            var matrix = new Matrix40FireCash();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination40FireCash();
            combination.MatrixToCombinationFruitFrenzy(matrix, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru Redstone20FruitFrenzy.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        public static ICombination GetCombination20FruitFrenzy(int[,] matrixArray, int bet)
        {
            var matrix = new Matrix20FireCash();
            matrix.FromMatrixArray20FireCash(matrixArray);
            var combination = new Combination20FireCash();
            combination.MatrixToCombination20FruitFrenzy(matrix, bet);
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
        private static ICombination GetCombinationBookOfRaDeluxe(int[,] matrixArray, int numberOfLines, int bet, bool gratisGame, byte gratisSymbol)
        {
            var matrix = new MatrixBookOfRa();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationBookOfRa();
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGame, gratisSymbol);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru Lucky Lady's charm.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGame"></param>
        /// <returns></returns>
        private static ICombination GetCombinationNovomaticLuckyLadysCharmDeluxe(int[,] matrixArray, int numberOfLines, int bet, bool gratisGame)
        {
            var matrix = new MatrixLuckyLadysCharmDeluxe();
            matrix.FromMatrixArray(matrixArray);

            var combination = new CombinationLuckyLadysCharmDeluxe();
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGame);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru RoaringForties.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <param name="numberOfLines"></param>
        /// <returns></returns>
        private static ICombination GetCombinationNovomaticRoaringForties(int[,] matrixArray, int bet, int numberOfLines)
        {
            var matrix = new MatrixRoaringForties();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationRoaringForties();
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
        private static ICombination GetCombinationNovomaticSizzlingHotDeluxe(int[,] matrixArray, int numberOfLines, int bet)
        {
            var matrix = new MatrixSizzlingHotDeluxe();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationSizzlingHotDeluxe();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru RedstoneWildHeartBeat.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationWildHeartBeat(int[,] matrixArray, int bet)
        {
            var matrix = new MatrixWildHeartBeat();
            matrix.FromMatrixArrayWildHeartBeat(matrixArray);
            var combination = new CombinationWildHeartBeat();
            combination.MatrixToCombinationWildHeartBeat(matrix, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru BonusEpicCrown.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationBonusEpicCrown(int[,] matrixArray, int bet, bool gratisGame, int numberOfLines, int bonusNumberOfLines)
        {
            var matrix = new MatrixBonusEpicCrown();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationBonusEpicCrown();
            combination.MatrixToCombinationBonusEpicCrown(matrix, bet, gratisGame, numberOfLines, bonusNumberOfLines);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru AmazingJoker.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="gratisGame"></param>
        /// <returns></returns>
        private static ICombination GetCombinationAmazingJoker(int[,] matrixArray, int bet, int numberOfLines, bool gratisGame)
        {
            var matrix = new MatrixAmazingJoker();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationAmazingJoker();
            combination.MatrixToCombinationAmazingJoker(matrix, numberOfLines, bet, gratisGame);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru TripleFieldsOfLuck.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <param name="numberOfLines"></param>
        /// <returns></returns>
        private static ICombination GetCombinationTripleFieldsOfLuck(int[,] matrixArray, int bet, int numberOfLines)
        {
            var matrix = new MatrixTripleFieldsOfLuck();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationTripleFieldsOfLuck();
            combination.MatrixToCombinationTripleFieldsOfLuck(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru MysticJungle.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <param name="numberOfLines"></param>
        /// <returns></returns>
        private static ICombination GetCombinationMysticJungle(int[,] matrixArray, int bet, int numberOfLines)
        {
            var cheatMystery = -1;
            var matArray2 = new int[5, 5];
            for (var i = 0; i < 5; i++)
            {
                matArray2[i, 0] = 3;
                for (var j = 1; j < 5; j++)
                {
                    if (cheatMystery == -1 && matrixArray[i, j - 1] >= 9)
                    {
                        cheatMystery = matrixArray[i, j - 1] % 9;
                    }
                    matArray2[i, j] = matrixArray[i, j - 1] >= 9 ? cheatMystery + 9 : matrixArray[i, j - 1];
                }
            }
            var matrix = new MatrixMysticJungle();
            matrix.FromMatrixArrayMysticJungle(matArray2);
            var combination = new CombinationMysticJungle();
            combination.MatrixToCombinationMysticJungle(matrix, numberOfLines, bet, cheatMystery);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru VintageFruits40.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <param name="numberOfLines"></param>
        /// <returns></returns>
        private static ICombination GetCombinationVintageFruits40(int[,] matrixArray, int bet, int numberOfLines)
        {
            matrixArray = FixMatrixArray(matrixArray);
            var matrix = new MatrixVintageFruits40();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationVintageFruits40();
            combination.MatrixToCombinationVintageFruits(matrix, numberOfLines, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru HotRushFruitLines.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationHotRushFruitLines(int[,] matrixArray, int bet)
        {
            var matrix = new MatrixHotRushFruitLines();
            matrix.FromMatrixArrayHotRushFruitLines(matrixArray);
            var combination = new CombinationHotRushFruitLines();
            combination.MatrixToCombinationHotRushFruitLines(matrix, bet);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru ToxicHaze.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="gratisGames"></param>
        /// <returns></returns>
        private static ICombination GetCombinationToxicHaze(int[,] matrixArray, int numberOfLines, int bet, int gratisGames)
        {
            matrixArray = FixMatrixArray(matrixArray);
            var matrix = new MatrixToxicHaze();
            matrix.FromMatrixArrayToxicHaze(matrixArray);
            var combination = new CombinationToxicHaze();
            combination.MatrixToCombinationToxicHaze(matrix, numberOfLines, bet, gratisGames);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru WildSunburst.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="gratisGame"></param>
        /// <returns></returns>
        private static ICombination GetCombinationWildSunburst(int[,] matrixArray, int bet, int numberOfLines, bool gratisGame)
        {
            var matrix = new MatrixWildSunburst();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationWildSunburst();
            combination.MatrixToCombinationWildSunburst(matrix, numberOfLines, bet, gratisGame, true);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru Mega Hot 10.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        private static ICombination GetCombinationMegaHot10(int[,] matrixArray, int bet, int numberOfLines)
        {
            var matrix = new MatrixMegaHot10();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationMegaHot();
            combination.MatrixToCombinationMegaHot10(matrix, bet, numberOfLines);
            return combination;
        }

        /// <summary>
        /// Daje kombinaciju za igru WildHeat40.
        /// </summary>
        /// <param name="matrixArray"></param>
        /// <param name="bet"></param>
        /// <param name="numberOfLines"></param>
        /// <returns></returns>
        private static ICombination GetCombinationWildHeat40(int[,] matrixArray, int bet, int numberOfLines)
        {
            matrixArray = FixMatrixArray(matrixArray);
            var matrix = new MatrixWildHeat40();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationWildHeat40();
            combination.MatrixToCombinationWildHeat(matrix, numberOfLines, bet);
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
                    var reelsFromCheatData = cheatToolDataObj.NewMatrix.GetLength(1);
                    var rows = cheatToolDataObj.NewMatrix.GetLength(0);

                    for (var i = 0; i < reels.Length; i++) //TODO: vidi kako da se reši za igre sa ugrađenim rilovima
                    {
                        for (var j = 0; j < 7; j++)
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
        ref byte[] additionalArray, long betMultiplier, byte additionalInformation = 0, int selectedField = 0, object gameDataObj = null)
        {
            if ((int)game > 1000 && (int)game <= 2000)
            {
                return GetCombinationTeam1(ref cheatToolObject, game, bet, numberOfLines, gratisGamesLeft, ref additionalArray, additionalInformation, selectedField, gameDataObj);
            }
            else if ((int)game > 2000 && (int)game <= 3000)
            {
                return GetCombinationTeam2(ref cheatToolObject, game, bet, numberOfLines, gratisGamesLeft, ref additionalArray, additionalInformation, selectedField, gameDataObj);
            }
            else if ((int)game > 3000 && (int)game <= 4000)
            {
                return GetCombinationTeam3(ref cheatToolObject, game, bet, numberOfLines, gratisGamesLeft, ref additionalArray, additionalInformation, selectedField, gameDataObj);
            }
            else if ((int)game > 4000 && (int)game <= 5000)
            {
                return GetCombinationTeam4(ref cheatToolObject, game, bet, numberOfLines, gratisGamesLeft, ref additionalArray, additionalInformation, selectedField, gameDataObj);
            }
            if (game == Games.SpinCards)
            {
                return GetCombinationPokerSlot(numberOfLines, bet, gratisGamesLeft > 0);
            }

            List<byte>[] reels;
            switch (game)
            {
                case Games.Wild81:
                case Games.Redstone81VegasCrown:
                case Games.Redstone81DoubleMagic:
                    var matW81 = MatrixWild81.GetMatixArray();
                    reels = new List<byte>[4];
                    for (var i = 0; i < 4; i++)
                    {
                        reels[i] = new List<byte>();
                        for (var j = 0; j < 5; j++)
                        {
                            reels[i].Add((byte)matW81[i, j]);
                        }
                    }
                    break;
                default:
                    reels = ReadReelsFromSlotFile(game, gratisGamesLeft > 0, additionalInformation);
                    break;
            }

            switch (game)
            {
                case Games.VikingGold:
                case Games.KingOfMyCastleDice:
                case Games.PowerOfTheGreat:
                    ValidateLines(game, numberOfLines, 20);
                    return GetCombinationVikingGold(bet, gratisGamesLeft, ref additionalArray, reels);
                case Games.TemplarsQuest:
                    ValidateLines(game, numberOfLines, 10);
                    return GetCombinationTemplarsQuest(gratisGamesLeft > 0, bet, additionalInformation, ref additionalArray,
                        selectedField, reels);
            }

            var matrixArray = ReadMatrixArrayFromReelsCheat(ref cheatToolObject, reels);

            if (cheatToolObject.AdditionalInfo > 0)
            {
                switch (game)
                {
                    case Games.Spellbook:
                    case Games.BookOfSpellsV2:
                    case Games.DiceOfSpells:
                    case Games.BookOfSpellsDeluxe:
                    case Games.LostBook:
                    case Games.BookOfSpells2:
                    case Games.BookOfIbis:
                    case Games.BookOfMozzart:
                    case Games.PlayNetBookOfAmun:
                        return GetCombinationSpellbook(matrixArray, numberOfLines, bet, gratisGamesLeft > 0, (byte)cheatToolObject.AdditionalInfo);
                }
            }

            switch (game)
            {
                case Games.Postman:
                case Games.AlohaCharm:
                case Games.DolphinsShine:
                    return GetCombinationPostman(matrixArray, numberOfLines, bet, gratisGamesLeft > 0);
                case Games.DeepJungle:
                case Games.BigBuddha:
                    ValidateLines(game, numberOfLines, 20);
                    return GetCombinationJungle(matrixArray, bet);
                case Games.Wizard:
                case Games.KatanasOfTime:
                    return GetCombinationWizard(matrixArray, numberOfLines, bet, gratisGamesLeft > 0);
                case Games.FruitsAndStars:
                case Games.FruitsAndStars40:
                case Games.CubesAndStars:
                case Games.FruityHot:
                case Games.StarsOfOktagon:
                case Games.ActionHot20:
                case Games.FruityHot5:
                case Games.FruitsAndStars20Deluxe:
                case Games.FruitsAndStarsChristmas:
                case Games.FruitsAndStars40Christmas:
                case Games.MozzartFruits:
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
                    return GetCombinationRingsMagic(matrixArray, numberOfLines, bet, gratisGamesLeft > 0, additionalInformation);
                case Games.Hot777:
                    return GetCombinationTriple7Hot(matrixArray, numberOfLines, bet, gratisGamesLeft > 0, additionalInformation);
                case Games.MagicFruits:
                    return GetCombinationMagicCherry(matrixArray, numberOfLines, bet);
                case Games.Spellbook:
                case Games.BookOfSpellsV2:
                case Games.DiceOfSpells:
                case Games.BookOfSpellsDeluxe:
                case Games.LostBook:
                case Games.BookOfSpells2:
                case Games.BookOfIbis:
                case Games.BookOfMozzart:
                case Games.PlayNetBookOfAmun:
                    return GetCombinationSpellbook(matrixArray, numberOfLines, bet, gratisGamesLeft > 0, additionalInformation);
                case Games.WildWest:
                    ValidateLines(game, numberOfLines, 20);
                    return GetCombinationWildWest(matrixArray, bet, gratisGamesLeft > 0, additionalInformation);
                case Games.RollingDices81:
                    return GetCombinationRollingDices81(matrixArray, bet, additionalInformation);
                case Games.Pyramid:
                case Games.WildKingdom:
                case Games.EyeOfTut:
                    return GetCombinationPyramid(matrixArray, numberOfLines, bet, gratisGamesLeft > 0, additionalInformation);
                case Games.Alpinist:
                    return GetCombinationAlpinist(matrixArray, bet, gratisGamesLeft);
                case Games.Diamonds:
                case Games.JewelsBeat:
                    return GetCombinationDiamonds(matrixArray, numberOfLines, bet);
                case Games.JuicyHot:
                    return GetCombinationJuicyHot(matrixArray, numberOfLines, bet);
                case Games.TwinklingHot5:
                case Games.JazzyFruits:
                case Games.PlayNetDashingHot5:
                    ValidateLines(game, numberOfLines, 5);
                    return GetCombinationJuicyHot(matrixArray, numberOfLines, bet);
                case Games.TwinklingHot40:
                case Games.TwinklingHot40Christmas:
                    ValidateLines(game, numberOfLines, 40);
                    return GetCombinationJuicyHot(matrixArray, numberOfLines, bet);
                case Games.TwinklingHot80:
                    ValidateLines(game, numberOfLines, 80);
                    return GetCombinationJuicyHot(matrixArray, numberOfLines, bet);
                case Games.LuckyBrilliants:
                    ValidateLines(game, numberOfLines, 20);
                    return GetCombinationJuicyHot(matrixArray, numberOfLines, bet);
                case Games.HotStars:
                case Games.SpaceGuardians:
                case Games.WinningStars:
                case Games.HotStarsChristmas:
                case Games.TurboStars10:
                    return GetCombinationHotStars(matrixArray, numberOfLines, bet, additionalInformation);
                case Games.MegaHot:
                    ValidateLines(game, numberOfLines, 5);
                    return GetCombinationMegaHot(matrixArray, bet);
                case Games.BurningIce:
                case Games.BurningIceDeluxe:
                case Games.FlashingDice:
                case Games.BurningIceGd:
                case Games.HeatingIce:
                case Games.HeatingIceDeluxe:
                case Games.HeatingDice:
                case Games.MozzartHeat:
                    return GetCombinationBurningIce(matrixArray, bet, gratisGamesLeft > 0, additionalInformation, ref additionalArray);
                case Games.TurboHot40:
                case Games.CrystalHot40:
                case Games.TurboDice40:
                case Games.CrystalHot40Gd:
                case Games.CrystalHot40Gd2:
                case Games.WildHot40:
                case Games.FruityWin40:
                case Games.CrystalHot401X:
                case Games.CrystalJokerHot:
                case Games.CrystalHotAdmiral:
                case Games.CrystalHot40Soccer:
                case Games.WildHot40Meridian:
                case Games.CrystalHot40Pw:
                case Games.ActionHot40:
                case Games.TurboPinn40:
                case Games.KingOfMyCastle:
                case Games.GigaHot40:
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
                case Games.RetroFire:
                case Games.TurboFire:
                case Games.WildHot40Dice:
                case Games.MozzartWild40:
                case Games.RizkHot40:
                    ValidateLines(game, numberOfLines, 40);
                    return GetCombinationTurboHot40(matrixArray, bet, 40, game == Games.TurboFire/* || game == Games.CrystalHot40*/);
                case Games.FruityWin20:
                    ValidateLines(game, numberOfLines, 20);
                    return GetCombinationTurboHot40(matrixArray, bet, 20);
                case Games.WildClover40:
                case Games.WildCloverDice40:
                case Games.RedstoneWildTrail:
                case Games.Redstone40JokersFreeGames:
                case Games.RedstoneMoonDog:
                case Games.RedstoneDivineStrike:
                    ValidateLines(game, numberOfLines, 40);
                    return GetCombinationWildClover40(matrixArray, bet, 40, gratisGamesLeft > 0);
                case Games.CrystalHot40Deluxe:
                    ValidateLines(game, numberOfLines, 40);
                    return GetCombinationCrystalHot40Deluxe(matrixArray, bet, numberOfLines);
                case Games.CrystalHot80:
                case Games.TurboHot80:
                case Games.OlimpHot:
                    ValidateLines(game, numberOfLines, 80);
                    return GetCombinationTurboHot40(matrixArray, bet, 80);
                case Games.CrystalHot100:
                case Games.TurboHot100:
                    ValidateLines(game, numberOfLines, 100);
                    return GetCombinationTurboHot40(matrixArray, bet, 100);
                case Games.CrystalHot40Free:
                case Games.WildHot40FreeSpins:
                case Games.CrystalsOfMozzart:
                    ValidateLines(game, numberOfLines, 40);
                    return GetCombinationCrystalHot40Free(matrixArray, bet, numberOfLines, gratisGamesLeft > 0);
                case Games.TropicalHot:
                    return GetCombinationTropicalHot(matrixArray, numberOfLines, bet);
                case Games.BookOfBruno:
                    return GetCombinationBookOfBruno(matrixArray, numberOfLines, bet, gratisGamesLeft > 0, additionalInformation);
                case Games.CrystalsOfMagic:
                    if (numberOfLines != 25 && !(gratisGamesLeft > 0))
                    {
                        throw new Exception("Slot Combination Reader Exception: CrystalsOfMagic must have 25 lines, but have " + numberOfLines + " lines !");
                    }
                    return GetCombinationCrystalsOfMagic(matrixArray, numberOfLines, bet, gratisGamesLeft > 0, additionalInformation, ref additionalArray, selectedField);
                case Games.Pirates:
                    return GetCombinationPirates(matrixArray, numberOfLines, bet, gratisGamesLeft > 0, additionalInformation);
                case Games.Monsters:
                    ValidateLines(game, numberOfLines, 20);
                    return GetCombinationMonsters(matrixArray, bet, gratisGamesLeft > 0, additionalInformation);
                case Games.ForestFruits:
                    return GetCombinationForestFruits(matrixArray, numberOfLines, bet);
                case Games.StarGems:
                case Games.Starlight:
                case Games.StarRunner:
                case Games.CrystalWin:
                case Games.CrystalJewels:
                    return GetCombinationStarGems(matrixArray, numberOfLines, bet, additionalInformation);
                case Games.NeonHot5:
                case Games.NeonDice5:
                case Games.FluoDice5:
                case Games.FluoHot5:
                    return GetCombinationNeonHot5(matrixArray, numberOfLines, bet);
                case Games.TripleHot:
                case Games.TripleDice:
                case Games.Retro7Hot:
                case Games.ClassicLuckySpin:
                case Games.Retro7HotChristmas:
                    ValidateLines(game, numberOfLines, 5);
                    return GetCombinationTripleHot(matrixArray, numberOfLines, bet);
                case Games.LuckyTwister:
                case Games.BuffaloFortune:
                    ValidateLines(game, numberOfLines, 10);
                    return GetCombinationLuckyTwister(matrixArray, bet);
                case Games.GoldenClover:
                    return GetCombinationGoldenClover(matrixArray, numberOfLines, bet, gratisGamesLeft > 0, ref additionalArray, gameDataObj);
                case Games.BookOfMayanGold:
                    return GetCombinationBookOfMayanGold(matrixArray, numberOfLines, bet, gratisGamesLeft > 0, additionalInformation);
                case Games.BurstingHot5:
                case Games.FireClover5:
                case Games.FireCloverDice5:
                case Games.VeryHot5:
                case Games.BurstingHot5Admiral:
                case Games.WildHeart5:
                case Games.AdmiralHot5:
                case Games.VeryHot5Christmas:
                case Games.CrownFire5:
                case Games.Money5:
                case Games.WinningClover5:
                case Games.HotLine1X:
                case Games.RedstoneChilliHot5:
                case Games.RedstoneJollyPresents:
                case Games.RedstoneHotRushCrownBurst:
                case Games.MozzartHot5:
                case Games.RedstoneLadyTripleFortune:
                case Games.FortuneClover5:
                case Games.PlayNetDiamondHeart5:
                case Games.RedstoneFunkyFruits:
                    ValidateLines(game, numberOfLines, 5);
                    return GetCombinationBurstingHot5(matrixArray, numberOfLines, bet);
                case Games.BurstingHot40:
                case Games.VeryHot40:
                case Games.VeryHot40Dice:
                case Games.FireClover40:
                case Games.BurstingHot40Mozzart:
                case Games.FireDice40:
                case Games.GermaniaHot40:
                case Games.VeryHot40Christmas:
                case Games.MozzartHot40:
                case Games.PlayNetFortuneClover40:
                case Games.PlayNetDiamondHeart40:
                    ValidateLines(game, numberOfLines, 40);
                    return GetCombinationBurstingHot5(matrixArray, numberOfLines, bet);
                case Games.VeryHot20:
                case Games.TopHot20:
                case Games.Redstone20WildCrown:
                case Games.PlayNetMajesticCrown20:
                case Games.PlayNetMajesticCrown20Xmas:
                    ValidateLines(game, numberOfLines, 20);
                    return GetCombinationBurstingHot5(matrixArray, numberOfLines, bet);
                case Games.GoldenCrown:
                case Games.WildCrown10:
                case Games.GoldenCrownMaxbet:
                case Games.WildDiamond10:
                case Games.WildDice10:
                case Games.WinXtip:
                case Games.SuperCasinoCrown:
                case Games.WildPumpkin10:
                case Games.GoldenCrownChristmas:
                case Games.WildSanta10:
                case Games.EpicCrown10:
                case Games.GoldenVegas:
                case Games.GrandpashabetCrown:
                case Games.CrownOfMozzart:
                case Games.RedstoneSlotRoyale:
                case Games.MajesticCrown10:
                case Games.RedstoneVolcanoCrown:
                    ValidateLines(game, numberOfLines, 10);
                    return GetCombinationGoldenCrown(matrixArray, numberOfLines, bet);
                case Games.GoldenCrown20BalkanBet:
                    ValidateLines(game, numberOfLines, 20);
                    return GetCombinationGoldenCrown(matrixArray, numberOfLines, bet);
                case Games.GoldenCrown40:
                    ValidateLines(game, numberOfLines, 40);
                    return GetCombinationGoldenCrown(matrixArray, numberOfLines, bet);
                case Games.EpicCrown5:
                    ValidateLines(game, numberOfLines, 5);
                    return GetCombinationGoldenCrown(matrixArray, numberOfLines, bet);
                case Games.MysteryJokerHot:
                    return GetCombination3WildFruits(matrixArray, bet, gratisGamesLeft > 0, additionalInformation, ref additionalArray);
                case Games.FruityJokerHot:
                    return GetCombinationFruityJokerHot(matrixArray, numberOfLines, bet);
                case Games.MayanCoins:
                case Games.CloverCoin:
                case Games.ShiningRush:
                    return GetCombinationCloverCash(matrixArray, numberOfLines, bet, gratisGamesLeft > 0, ref additionalArray);
                case Games.SuperLucky:
                case Games.RedstoneClover5LockAndCash:
                    return GetCombinationSuperLucky(matrixArray, numberOfLines, bet, gratisGamesLeft > 0, ref additionalArray);
                case Games.MegaCubesDeluxe:
                    ValidateLines(game, numberOfLines, 5);
                    return GetCombinationMegaCubesDeluxe(matrixArray, numberOfLines, bet);
                case Games.BookOfLuxorDouble:
                case Games.BookOfScorpionsDouble:
                case Games.BookOfDread:
                case Games.RedstoneBookOfScorpio:
                    return GetCombinationBookOfDouble(matrixArray, numberOfLines, bet, gratisGamesLeft > 0, additionalInformation);
                case Games.WildClover506:
                    ValidateLines(game, numberOfLines, 50);
                    return GetCombination40WildClover6(matrixArray, numberOfLines, bet, gratisGamesLeft > 0);
                case Games.LollasWorld:
                case Games.ZabranjenoVoce:
                case Games.LollasWorldChristmas:
                case Games.KettysFashionWorld:
                case Games.MozzartsWorld:
                case Games.PlayNetCrownStacks40:
                    ValidateLines(game, numberOfLines, 40);
                    return GetCombinationLilaWild(matrixArray, bet, 40);
                case Games.ElGrandeToro:
                    ValidateLines(game, numberOfLines, 10);
                    return GetCombinationElGrandeToro(matrixArray, numberOfLines, bet, gratisGamesLeft > 0, ref additionalArray);
                case Games.SevenClassicHot:
                    return GetCombinationSevenClassicHot(matrixArray, numberOfLines, bet, gratisGamesLeft > 0);
                case Games.BonusBells:
                    return GetCombinationBonusBells(matrixArray, numberOfLines, bet, gratisGamesLeft > 0);
                case Games.KingOfThunder:
                    return GetCombinationKingOfThunder(matrixArray, numberOfLines, bet, gratisGamesLeft);
                case Games.JokerQueen:
                    ValidateLines(game, numberOfLines, 5);
                    return GetCombinationJokerQueen(matrixArray, numberOfLines, bet, gratisGamesLeft > 0, additionalInformation, ref additionalArray);

                case Games.CashBells40:
                    ValidateLines(game, numberOfLines, 40);
                    return GetCombinationCashBells40(matrixArray, numberOfLines, bet, gratisGamesLeft > 0);
                case Games.CrystalHot40Max:
                    ValidateLines(game, numberOfLines, 40);
                    return GetCombinationCrystalHot40Max(matrixArray, bet, numberOfLines);
                case Games.FruityFace:
                    return GetCombinationFruityFace(matrixArray, numberOfLines, bet);
                case Games.WildJokerHot:
                case Games.FashionNight:
                    ValidateLines(game, numberOfLines, 10);
                    return GetCombinationWildJokerHot(matrixArray, numberOfLines, bet);
                case Games.HeatingFruits:
                    ValidateLines(game, numberOfLines, 40);
                    return GetCombinationHeatingFruits(matrixArray, numberOfLines, bet, additionalInformation);
                case Games.WildLuckyClover:
                case Games.DeadNight:
                case Games.MrFirstCryptonium:
                case Games.LuckyMozzart:
                case Games.WildLuckyDice:
                case Games.SummerRush:
                    ValidateLines(game, numberOfLines, 40);
                    return GetCombinationWildLuckyClover(matrixArray, numberOfLines, bet, gratisGamesLeft > 0, additionalInformation);
                case Games.VeryHot40Respin:
                    ValidateLines(game, numberOfLines, 40);
                    return GetCombinationVeryHot40Respin(matrixArray, numberOfLines, bet, additionalInformation);
                case Games.WildHot40Blow:
                case Games.WildHot40BlowDice:
                case Games.PlayNetWildSpread40:
                case Games.PlayNetCrownSpread40:
                    ValidateLines(game, numberOfLines, 40);
                    return GetCombinationWildHot40Blow(matrixArray, bet, numberOfLines);
                case Games.AfricanTreasure:
                    return GetCombinationAfricanTreasure(matrixArray, numberOfLines, bet);
                case Games.GoldenExplosion:
                    ValidateLines(game, numberOfLines, 10);
                    return GetCombinationGoldenExplosion(matrixArray, numberOfLines, bet);
                case Games.VeryHot5Extreme:
                case Games.RedstoneChilliDouble:
                case Games.RedstoneCapsAndCrowns:
                case Games.RedstoneDoubleFireClover:
                case Games.RedstoneChristmasDelight:
                case Games.PlayNetFortuneCloverX2:
                case Games.PlayNetDiamondHeartX2:
                    ValidateLines(game, numberOfLines, 5);
                    return GetCombinationVeryHot5Extreme(matrixArray, numberOfLines, bet);
                case Games.EpicFire40:
                    ValidateLines(game, numberOfLines, 40);
                    return GetCombinationEpicFire40(matrixArray, bet, numberOfLines);
                case Games.Wild27:
                case Games.RedstoneApollo27Classic:
                case Games.PlayNet27WildStacks:
                case Games.PlayNet27WildStacksXmas:
                case Games.Redstone27DoubleFruit:
                    return GetCombinationWild27(matrixArray, bet);
                case Games.FireCash40:
                    ValidateLines(game, numberOfLines, 40);
                    return GetCombination40FireCash(matrixArray, bet, 40);
                case Games.Redstone40FruitFrenzy:
                    ValidateLines(game, numberOfLines, 4);
                    return GetCombination40FruitFrenzy(matrixArray, bet);
                case Games.PiratesPapi:
                    ValidateLines(game, numberOfLines, 40);
                    return GetCombinationPiratesPapi(matrixArray, bet, numberOfLines);
                case Games.WildLuckyClover2:
                    ValidateLines(game, numberOfLines, 40);
                    return GetCombinationWildLuckyClover2(matrixArray, numberOfLines, bet, gratisGamesLeft > 0, additionalInformation);
                case Games.EpicClover40:
                    ValidateLines(game, numberOfLines, 40);
                    return GetCombinationEpicClover40(matrixArray, bet, numberOfLines);
                case Games.EpicClover100:
                case Games.EpicDice100:
                case Games.Redstone100CloverFire:
                case Games.PlayNetFortuneClover100:
                    ValidateLines(game, numberOfLines, 100);
                    return GetCombinationEpicClover40(matrixArray, bet, numberOfLines);
                case Games.JokerTripleDouble:
                case Games.ChilliRespin:
                    return GetCombinationJokerTripleDouble(matrixArray, bet, gratisGamesLeft > 0, additionalInformation, ref additionalArray);
                case Games.HeatDouble:
                    return GetCombinationHeatDouble(matrixArray, bet);
                case Games.BlazingHeat:
                case Games.RedstoneHotRushStarsDeluxe:
                    return GetCombinationBlazingHeat(matrixArray, bet);
                case Games.HotHotStereoWin:
                case Games.RedstoneHotRushBothWays:
                    return GetCombinationHotHotStereoWin(matrixArray, bet);
                case Games.CloverBlast5:
                case Games.Redstone5WildFire:
                    return GetCombination5CloverBlast(matrixArray, bet);
                case Games.WinningClover5Extreme:
                    ValidateLines(game, numberOfLines, 5);
                    return GetCombinationWinningClover5Extreme(matrixArray, numberOfLines, bet);
                case Games.MayansBattle:
                case Games.FootballVictory:
                    return GetCombinationMayansBattle(matrixArray, numberOfLines, bet, gratisGamesLeft > 0);
                case Games.WildCraps:
                    ValidateLines(game, numberOfLines, 10);
                    return GetCombinationWildCraps(matrixArray, numberOfLines, bet, gratisGamesLeft > 0, additionalInformation);
                case Games.RedstoneFearOfDark:
                case Games.Redstone40HotJoker:
                    ValidateLines(game, numberOfLines, 4);
                    return GetCombinationFearOfDark(matrixArray, bet, numberOfLines);
                case Games.SantasPresents:
                case Games.EggspandingRush:
                    return GetCombinationSantasPresents(matrixArray, numberOfLines, bet, gratisGamesLeft > 0);
                case Games.Redstone20FruitFrenzy:
                    ValidateLines(game, numberOfLines, 2);
                    return GetCombination20FruitFrenzy(matrixArray, bet);
                case Games.RedstoneWildHeartBeat:
                case Games.RedstoneSpinUp:
                case Games.RedstoneOnlyAnime:
                    return GetCombinationWildHeartBeat(matrixArray, bet);
                case Games.BonusEpicCrown:
                case Games.RedstoneSpookySpins:
                    ValidateLines(game, numberOfLines, 10);
                    return GetCombinationBonusEpicCrown(matrixArray, bet, gratisGamesLeft > 0, 10, 40);
                case Games.AmazingJoker:
                    ValidateLines(game, numberOfLines, 40);
                    return GetCombinationAmazingJoker(matrixArray, bet, numberOfLines, gratisGamesLeft > 0);
                case Games.TripleFieldsOfLuck:
                    return GetCombinationTripleFieldsOfLuck(matrixArray, bet, numberOfLines);
                case Games.Wild81:
                case Games.Redstone81VegasCrown:
                case Games.Redstone81DoubleMagic:
                    ValidateLines(game, numberOfLines, 5);
                    return CombinationWild81.GetCombinationWild81Cheat(matrixArray, bet);
                case Games.MysticJungle:
                    return GetCombinationMysticJungle(matrixArray, bet, numberOfLines);
                case Games.VintageFruits40:
                case Games.PlayNetWildForties:
                case Games.PlayNetWildFortiesXmas:
                    ValidateLines(game, numberOfLines, 40);
                    return GetCombinationVintageFruits40(matrixArray, bet, numberOfLines);
                case Games.RedstoneHotRushFruitLines:
                    ValidateLines(game, numberOfLines, 1);
                    return GetCombinationHotRushFruitLines(matrixArray, bet);
                case Games.ToxicHaze:
                    ValidateLines(game, numberOfLines, 40);
                    return GetCombinationToxicHaze(matrixArray, numberOfLines, bet, gratisGamesLeft);
                case Games.WildSunburst:
                    ValidateLines(game, numberOfLines, 40);
                    return GetCombinationWildSunburst(matrixArray, bet, numberOfLines, gratisGamesLeft > 0);
                case Games.MegaHot10:
                case Games.MegaHot20:
                case Games.RedstoneJuicyHeat20:
                    return GetCombinationMegaHot10(matrixArray, bet, numberOfLines);
                case Games.WildHeat40:
                    ValidateLines(game, numberOfLines, 40);
                    return GetCombinationWildHeat40(matrixArray, bet, numberOfLines);
                case Games.NovomaticBookOfRaDeluxe:
                case Games.NovomaticLordOfTheOcean:
                    return GetCombinationBookOfRaDeluxe(matrixArray, numberOfLines, bet, gratisGamesLeft > 0, additionalInformation);
                case Games.NovomaticLuckyLadysCharmDeluxe:
                    return GetCombinationNovomaticLuckyLadysCharmDeluxe(matrixArray, numberOfLines, bet, gratisGamesLeft > 0);
                case Games.NovomaticRoaringForties:
                    ValidateLines(game, numberOfLines, 40);
                    return GetCombinationNovomaticRoaringForties(matrixArray, bet, numberOfLines);
                case Games.NovomaticSizzlingHotDeluxe:
                    ValidateLines(game, numberOfLines, 5);
                    return GetCombinationNovomaticSizzlingHotDeluxe(matrixArray, numberOfLines, bet);

                default:
                    return null;
            }
        }

        /// <summary>
        /// Daje zeljenu kombinaciju za slot fajl.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="bet"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="gratisGamesLeft"></param>
        /// <param name="additionalInformation"></param>
        /// <param name="additionalArray"></param>
        /// <param name="selectedField">Izabrano polje za Cartman bonus za CrystalsOfMagic</param>
        /// <param name="gameDataObj"></param>
        /// <param name="testCombinations"></param>
        /// <returns></returns>
        public static ICombination GetCombination(Games game, int bet, int numberOfLines, int gratisGamesLeft,
        ref byte[] additionalArray, long betModifier, byte additionalInformation = 0, int selectedField = 0, object gameDataObj = null, int testCombinations = 0)
        {
            ICombination combinationResult = SlotCombination.GetCombination(game, bet, numberOfLines, gratisGamesLeft,
                                ref additionalArray, betModifier, additionalInformation, selectedField, gameDataObj); ;
            var countExtractions = 0;
            var prevAdditionalDataArray = additionalArray;

            switch ((TestCombinations)testCombinations)
            {
                case TestCombinations.BonusAndNoWIn:
                    if (game == Games.CrystalsOfMagic || game == Games.TemplarsQuest)
                    {
                        while (combinationResult.AdditionalInformation != 1 && countExtractions++ < 10000)
                        {
                            combinationResult = SlotCombination.GetCombination(game, bet, numberOfLines, gratisGamesLeft,
                                ref additionalArray, betModifier, additionalInformation, selectedField, gameDataObj);
                        }
                    }
                    else
                    {
                        while (!(combinationResult.GratisGame && combinationResult.NumberOfWinningLines == 0 || combinationResult.AdditionalInformation == 10) && countExtractions++ < 10000)
                        {
                            combinationResult = SlotCombination.GetCombination(game, bet, numberOfLines, gratisGamesLeft,
                                ref additionalArray, betModifier, additionalInformation, selectedField, gameDataObj);
                        }
                    }
                    break;

                case TestCombinations.ScatterandBonus:
                    if (game == Games.CrystalsOfMagic || game == Games.TemplarsQuest)
                    {
                        while (combinationResult.AdditionalInformation != 2 && countExtractions++ < 10000)
                        {
                            prevAdditionalDataArray = additionalArray;
                            combinationResult = SlotCombination.GetCombination(game, bet, numberOfLines, gratisGamesLeft,
                                ref additionalArray, betModifier, additionalInformation, selectedField, gameDataObj);
                        }
                        Logger.LogInfo("ScatterandBonus: AdditionalDataArray: ");
                        for (var i = 0; prevAdditionalDataArray != null && i < prevAdditionalDataArray.Length; i++)
                        {
                            Logger.LogInfo(string.Join(",", prevAdditionalDataArray[i]));
                        }
                    }
                    else
                    {
                        while (!(combinationResult.GratisGame && combinationResult.WinFor2 > 0) & countExtractions++ < 10000)
                        {
                            combinationResult = SlotCombination.GetCombination(game, bet, numberOfLines, gratisGamesLeft,
                                ref additionalArray, betModifier, additionalInformation, selectedField, gameDataObj);
                        }
                    }
                    break;
                case TestCombinations.BonusWin:
                    if (game == Games.CrystalsOfMagic)
                    {
                        while (combinationResult.AdditionalInformation != 3 && countExtractions++ < 10000)
                        {
                            prevAdditionalDataArray = additionalArray;
                            combinationResult = SlotCombination.GetCombination(game, bet, numberOfLines, gratisGamesLeft,
                                ref additionalArray, betModifier, additionalInformation, selectedField, gameDataObj);
                        }
                        Logger.LogInfo("BonusWin: AdditionalDataArray: ");
                        for (var i = 0; prevAdditionalDataArray != null && i < prevAdditionalDataArray.Length; i++)
                        {
                            Logger.LogInfo(string.Join(",", prevAdditionalDataArray[i]));
                        }
                    }
                    else
                    {
                        while (!(combinationResult.GratisGame && combinationResult.WinFor2 == 0 && combinationResult.NumberOfWinningLines > 0) && countExtractions++ < 10000)
                        {
                            combinationResult = SlotCombination.GetCombination(game, bet, numberOfLines, gratisGamesLeft,
                                ref additionalArray, betModifier, additionalInformation, selectedField, gameDataObj);
                        }
                    }
                    break;
                case TestCombinations.BonusOnly:
                    if (game == Games.CrystalsOfMagic)
                    {
                        while (combinationResult.AdditionalInformation != 4 && countExtractions++ < 10000)
                        {
                            combinationResult = SlotCombination.GetCombination(game, bet, numberOfLines, gratisGamesLeft,
                                ref additionalArray, betModifier, additionalInformation, selectedField, gameDataObj);
                        }
                    }
                    else
                    {
                        while (!(combinationResult.GratisGame && combinationResult.WinFor2 == 0 && combinationResult.NumberOfWinningLines == 0) && countExtractions++ < 10000)
                        {
                            combinationResult = SlotCombination.GetCombination(game, bet, numberOfLines, gratisGamesLeft,
                                ref additionalArray, betModifier, additionalInformation, selectedField, gameDataObj);
                        }
                    }
                    break;
                case TestCombinations.ScatterOnly:
                    while (!(!combinationResult.GratisGame && combinationResult.WinFor2 > 0 && combinationResult.NumberOfWinningLines == 0) && countExtractions++ < 10000)
                    {
                        combinationResult = SlotCombination.GetCombination(game, bet, numberOfLines, gratisGamesLeft,
                            ref additionalArray, betModifier, additionalInformation, selectedField, gameDataObj);
                    }
                    break;
                case TestCombinations.ScatterAndWin:
                    while (!(combinationResult.WinFor2 > 0 && combinationResult.NumberOfWinningLines > 0) && countExtractions++ < 10000)
                    {
                        combinationResult = SlotCombination.GetCombination(game, bet, numberOfLines, gratisGamesLeft,
                            ref additionalArray, betModifier, additionalInformation, selectedField, gameDataObj);
                    }
                    break;
                case TestCombinations.NoWin:
                    while (!(combinationResult.WinFor2 == 0 && combinationResult.NumberOfWinningLines == 0) && countExtractions++ < 10000)
                    {
                        combinationResult = SlotCombination.GetCombination(game, bet, numberOfLines, gratisGamesLeft,
                            ref additionalArray, betModifier, additionalInformation, selectedField, gameDataObj);
                    }
                    break;
                case TestCombinations.OnlyWin:
                    while (!(combinationResult.TotalWin > 0 && combinationResult.WinFor2 == 0 && combinationResult.NumberOfWinningLines > 0) && countExtractions++ < 10000)
                    {
                        combinationResult = SlotCombination.GetCombination(game, bet, numberOfLines, gratisGamesLeft,
                            ref additionalArray, betModifier, additionalInformation, selectedField, gameDataObj);
                    }
                    break;
            }

            return combinationResult;
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
                case Games.KingOfMyCastleDice:
                case Games.PowerOfTheGreat:
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