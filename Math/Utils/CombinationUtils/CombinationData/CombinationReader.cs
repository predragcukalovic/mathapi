using System;
using RNGUtils.RandomData;
using Utils.Games;
using MathCombination.CombinationData.CheatTool;
using System.Configuration;

namespace MathCombination.CombinationData
{
    public class CombinationReader
    {
        #region Private fields

        private CheatToolData _CheatToolDataObject;

        #endregion

        #region Public methods

        /// <summary>
        /// Učitava nasumičnu matricu iz binarnog fajla
        /// </summary>
        /// <param name="path">Lokacija na disku gde su smešteni fajlovi</param>
        /// <param name="game">Vrsta igre</param>
        /// <param name="bet">Ulog</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="maxBet">Da li je maksimalan ulog</param>
        /// <param name="gratisGamesLeft">Koliko gratis igara je ostalo</param>
        /// <param name="additionalArray">Niz koji pamti prethodno stanje za igru BurningIce</param>
        /// <param name="additionalInformation">Polje za igre poput 'Fruits' i 'Pegasus'</param>
        /// <param name="beenWinning">Da li se već pala dobitna kombinacija u gratis igrama?</param>
        /// <param name="playerChoise">Dodatni parametar, polje koje je igrač izabrao</param>
        /// <param name="gameDataobj"></param>
        /// <returns></returns>
        public ICombination GetNextCombination(string path, Games game, int bet, int numberOfLines, bool maxBet,
            int gratisGamesLeft, ref byte[] additionalArray, byte additionalInformation = 0, bool beenWinning = true,
            int playerChoise = -1, object gameDataobj = null)
        {
            ICombination combination;
            if (!RngValidator.ValidGenerator)
            {
                throw new Exception("Random Number Generator Failed!");
            }
            if (game == Games.BookOfSpells)
            {
                game = Games.Spellbook;
            }
            try
            {
#if DEBUG
                if (ConfigurationManager.AppSettings["DebugMode"] == "True" && _CheatToolDataObject != null && _CheatToolDataObject.UsingCheatTool)
                {
                    try
                    {
                        combination = SlotCombinationCheatTool.GetCombination(ref _CheatToolDataObject, game, bet, numberOfLines, gratisGamesLeft, ref additionalArray, additionalInformation, playerChoise, gameDataobj);
                    }
                    catch (Exception)
                    { 
                        ///TODO:Loggirati
                        combination = SlotCombination.GetCombination(game, bet, numberOfLines, gratisGamesLeft, ref additionalArray, additionalInformation, playerChoise, gameDataobj);
                    }
                }
                else
                {
                    combination = SlotCombination.GetCombination(game, bet, numberOfLines, gratisGamesLeft, ref additionalArray, additionalInformation, playerChoise, gameDataobj);
                }
#else
                combination = SlotCombination.GetCombination(game, bet, numberOfLines, gratisGamesLeft, ref additionalArray, additionalInformation, playerChoise, gameDataobj);
#endif
            }
            catch (Exception exception)
            {
                throw new Exception("Combination Reader Exception: " + exception);
            }
            return combination;
        }

        public CombinationReader(string userId)
        {
            _CheatToolDataObject = null;
        }

        #endregion

        #region Private methods - CheatTool

        private void CreateCheatTool()
        {
#if DEBUG
            if (ConfigurationManager.AppSettings["DebugMode"] == "True")
            {
                _CheatToolDataObject = new CheatToolData();
            }
#endif
        }
        private void EnableCheatToolUsing()
        {
#if DEBUG
            if (ConfigurationManager.AppSettings["DebugMode"] == "True" && _CheatToolDataObject != null)
            {
                _CheatToolDataObject.UsingCheatTool = true;
            }
#endif
        }
        private bool SetMatrixForCheatTool(int[,] matrix)
        {
#if DEBUG
            if (ConfigurationManager.AppSettings["DebugMode"] == "True" && _CheatToolDataObject != null)
            {
                var rows = matrix.GetLength(0);
                var reels = matrix.GetLength(1);
                _CheatToolDataObject.StoppingReelsNotUsingMatrix = false;
                _CheatToolDataObject.NewMatrix = new int[reels, rows];

                for (int i = 0; i < reels; i++)
                {
                    for (int j = 0; j < rows; j++)
                    {
                        _CheatToolDataObject.NewMatrix[i, j] = matrix[j, i];
                    }
                }

                return true;
            }
#endif
            return false;
        }
        private bool StopReelsUsingCheatTool(int[] positionsInReels)
        {
#if DEBUG
            if (ConfigurationManager.AppSettings["DebugMode"] == "True" && _CheatToolDataObject != null)
            {
                var numberOfReels = positionsInReels.Length;
                _CheatToolDataObject.StoppingReelsNotUsingMatrix = true;
                _CheatToolDataObject.IndicesInReels = new int[numberOfReels];
                for (int i = 0; i < numberOfReels; i++)
                {
                    _CheatToolDataObject.IndicesInReels[i] = positionsInReels[i];
                }

                return true;
            }
#endif
            return false;
        }
        private void DisableCheatTool()
        {
            _CheatToolDataObject.UsingCheatTool = false;
        }
        private bool CheatToolInitialized()
        {
            return _CheatToolDataObject != null;
        }

        #endregion Private methods - CheatTool

        #region Public methods - CheatTool
        public void ManageCheatToolByJson(string json)
        {
            CheatToolData auxCheatToolData;
            try
            {
                auxCheatToolData = CheatToolData.CheatToolFromJson(json);
            }
            catch (Exception e)
            {
                ///TODO: Ukoliko neko smartra da ovaj exception treba necim da se zameni 
                throw new Exception("Cheat Tool Json Deserialization Exception:" + e.Message);
            }

            if (CheatToolInitialized())
            {
                if (auxCheatToolData != null && auxCheatToolData.UsingCheatTool)
                {
                    EnableCheatToolUsing();
                    if (auxCheatToolData.StoppingReelsNotUsingMatrix)
                    {
                        if (!StopReelsUsingCheatTool(auxCheatToolData.IndicesInReels))
                        {
                            DisableCheatTool();
                        }
                    }
                    else
                    {
                        if (!SetMatrixForCheatTool(auxCheatToolData.NewMatrix))
                        {
                            DisableCheatTool();
                        }
                    }
                }
                else
                {
                    DisableCheatTool();
                }
            }
            else
            {
                if (auxCheatToolData != null && auxCheatToolData.UsingCheatTool)
                {
                    CreateCheatTool();
                    if (CheatToolInitialized())
                    {
                        EnableCheatToolUsing();
                        if (auxCheatToolData.StoppingReelsNotUsingMatrix)
                        {
                            StopReelsUsingCheatTool(auxCheatToolData.IndicesInReels);
                        }
                        else
                        {
                            SetMatrixForCheatTool(auxCheatToolData.NewMatrix);
                        }
                    }
                }
            }
        }

        #endregion Public methods - CheatTool
    }
}
