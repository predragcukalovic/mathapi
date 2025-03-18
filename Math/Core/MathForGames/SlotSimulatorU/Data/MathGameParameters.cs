using Papi.GameServer.Utils.Enums;
using Papi.GameServer.Utils.Logging;
using Papi.GameServer.Utils.Xml;
using RNGUtils.RandomData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace MathForGames.Data
{

    public struct FullGameInformation
    {
        public Games Game;
        public List<GameInformation> GameInformations;
    }

    public struct GameInformation
    {
        public int Count;
        public string Percent;
        public List<BetAndSet> BetsAndSets;
    }

    public struct BetAndSet
    {
        public int Bet;
        public string Set;
    }

    public static class MathGameParameters
    {
        #region Private fields

        private static List<FullGameInformation> _AllGamesInformation;
        private static List<FullGameInformation> _CurrentGameInformation;

        #endregion

        #region Private methods

        /// <summary>
        /// Daje set iz kog se učitavaju kombinacije za dati ulog.
        /// </summary>
        /// <param name="betsAndSets">Lista setova za različite uloge.</param>
        /// <param name="bet">Trenutni ulog.</param>
        /// <returns></returns>
        private static string GetSetForBet(List<BetAndSet> betsAndSets, int bet)
        {
            if (betsAndSets == null || !betsAndSets.Any())
            {
                return "";
            }
            return betsAndSets.Last().Bet <= bet ? betsAndSets.Last().Set : betsAndSets.First(x => x.Bet > bet).Set;
        }

        /// <summary>
        /// Daje sve informacije za jednu igru.
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        private static List<GameInformation> GetAllInformationsForGame(Games game)
        {
            return _AllGamesInformation.FirstOrDefault(x => x.Game == game).GameInformations;
        }

        /// <summary>
        /// Kada se isčitaju sve kombinacije resetuje parametre.
        /// </summary>
        /// <param name="game"></param>
        private static void SetNewInformation(Games game)
        {
            var informations = GetAllInformationsForGame(game);
            _CurrentGameInformation.FirstOrDefault(c => c.Game == game).GameInformations[0] = informations[(int)SoftwareRng.Next(informations.Count)];
        }

        /// <summary>
        /// Daje sve informacije za igru.
        /// </summary>
        /// <param name="xmlNode"></param>
        /// <returns></returns>
        private static FullGameInformation GetFullGameInformations(XmlNode xmlNode)
        {
            return new FullGameInformation
            {
                Game = (Games)Enum.Parse(typeof(Games), SettingsReader.GetStringByName(xmlNode, "GameName")),
                GameInformations = GetGameInformations(xmlNode.SelectNodes("PercentItem"))
            };
        }

        /// <summary>
        /// Daje listu informacija za igru.
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        private static List<GameInformation> GetGameInformations(IEnumerable nodes)
        {
            return (from XmlNode node in nodes
                    select new GameInformation
                    {
                        Percent = SettingsReader.GetStringByName(node, "PercentValue"),
                        Count = SettingsReader.GetIntegerByName(node, "Count"),
                        BetsAndSets = GetBetsAndSetsFromNodes(node.SelectNodes("BetItem"))
                    }).ToList();
        }

        /// <summary>
        /// Daje listu BetAndSet iz xml čvorova.
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        private static List<BetAndSet> GetBetsAndSetsFromNodes(IEnumerable nodes)
        {
            return (from XmlNode betAndSet in nodes
                    select new BetAndSet
                    {
                        Bet = SettingsReader.GetIntegerByName(betAndSet, "MaxBet"),
                        Set = SettingsReader.GetStringByName(betAndSet, "Set")
                    }).ToList();
        }

        /// <summary>
        /// Učitava podatke iz XML-a.
        /// </summary>
        /// <param name="dataPath">Putanja xml fajla</param>
        /// <returns></returns>
        private static List<FullGameInformation> GetAllGamesInformations(string dataPath)
        {
            var xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(dataPath);
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Error loading MathGameSettings.xml: ");
            }
            try
            {
                var itemNodes = xmlDoc.GetElementsByTagName("GameNode");
                var fullParameters = new List<FullGameInformation>(itemNodes.Count);
                for (var i = 0; i < itemNodes.Count; i++)
                {
                    fullParameters.Add(GetFullGameInformations(itemNodes[i]));
                }

                return fullParameters;
            }
            catch (Exception exc)
            {
                Logger.LogError(exc, "GetAllGamesInformations: ");
                return null;
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Učitava podatke za sve igre.
        /// </summary>
        /// <param name="dataPath">Putanja XML fajla</param>
        public static void ReadAllGamesData(string dataPath)
        {
            _AllGamesInformation = GetAllGamesInformations(dataPath);

            _CurrentGameInformation = new List<FullGameInformation>();
            foreach (Games game in Enum.GetValues(typeof(Games)))
            {
                var fullGameInfo = new FullGameInformation { Game = game };
                var allInfo = GetAllInformationsForGame(game);
                if (allInfo != null && allInfo.Count > 0)
                {
                    fullGameInfo.GameInformations = new List<GameInformation> { allInfo[(int)SoftwareRng.Next(allInfo.Count)] };
                }

                _CurrentGameInformation.Add(fullGameInfo);
            }
        }

        /// <summary>
        /// Daje dodatnu putanju za zadatu igru i ulog. Ako vrati prazan string znači da podaci za tu igru nisu definisani.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="bet"></param>
        /// <param name="gratisGame"> </param>
        /// <returns></returns>
        public static string GetPathForGame(Games game, int bet, bool gratisGame)
        {
            var currentGameInformation = _CurrentGameInformation.FirstOrDefault(c => c.Game == game);
            if (currentGameInformation.GameInformations == null)
            {
                return "";
            }
            if (currentGameInformation.GameInformations[0].Count <= 0)
            {
                SetNewInformation(game);
            }
            if (!gratisGame)
            {
                var tempInformation = currentGameInformation.GameInformations[0];
                tempInformation.Count--;
                currentGameInformation.GameInformations[0] = tempInformation;
            }
            return Path.Combine(currentGameInformation.GameInformations[0].Percent, GetSetForBet(currentGameInformation.GameInformations[0].BetsAndSets, bet)) + @"\";
        }

        #endregion
    }
}
