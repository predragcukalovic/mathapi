using Papi.GameServer.Utils.Enums;
using Papi.GameServer.Utils.Logging;
using Papi.GameServer.Utils.Xml;
using RNGUtils.RandomData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace MathForGames.Data
{
    public static class MathSlotGameParameters
    {
        #region Private structs

        private struct FullSlotInformation
        {
            public Games Game;
            public List<SlotGameInformation> SlotGameInformations;
        }

        private struct SlotGameInformation
        {
            public string FileName;
            public int Count;
            public string Gratis;
        }

        #endregion

        #region Private fields

        private static List<FullSlotInformation> _AllGamesInformation;
        private static List<FullSlotInformation> _CurrentGameInformation;

        #endregion

        #region Private methods

        /// <summary>
        /// Daje listu informacija za igru.
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        private static List<SlotGameInformation> GetGameInformations(IEnumerable nodes)
        {
            return (from XmlNode node in nodes
                    select new SlotGameInformation
                    {
                        FileName = SettingsReader.GetStringByName(node, "GameFileName"),
                        Count = SettingsReader.GetIntegerByName(node, "Count"),
                        Gratis = SettingsReader.GetStringByName(node, "GratisFileName")
                    }).ToList();
        }

        /// <summary>
        /// Daje sve informacije za igru.
        /// </summary>
        /// <param name="xmlNode"></param>
        /// <returns></returns>
        private static FullSlotInformation GetFullGameInformations(XmlNode xmlNode)
        {
            return new FullSlotInformation
            {
                Game = (Games)Enum.Parse(typeof(Games), SettingsReader.GetStringByName(xmlNode, "GameName")),
                SlotGameInformations = GetGameInformations(xmlNode.SelectNodes("GameItem"))
            };
        }

        /// <summary>
        /// Daje sve informacije za jednu igru.
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        private static List<SlotGameInformation> GetAllInformationsForGame(Games game)
        {
            return _AllGamesInformation.FirstOrDefault(x => x.Game == game).SlotGameInformations;
        }

        /// <summary>
        /// Učitava podatke iz XML-a.
        /// </summary>
        /// <param name="dataPath">Putanja xml fajla</param>
        /// <returns></returns>
        private static List<FullSlotInformation> GetAllGamesInformations(string dataPath)
        {
            var xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(dataPath);
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Error loading MathSlotGameSettings.xml: ");
            }
            try
            {
                var itemNodes = xmlDoc.GetElementsByTagName("Game");
                var fullParameters = new List<FullSlotInformation>(itemNodes.Count);
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

        /// <summary>
        /// Kada se isčitaju sve kombinacije resetuje parametre.
        /// </summary>
        /// <param name="game"></param>
        private static void SetNewInformation(Games game)
        {
            var informations = GetAllInformationsForGame(game);
            _CurrentGameInformation.FirstOrDefault(c => c.Game == game).SlotGameInformations[0] = informations[(int)SoftwareRng.Next(informations.Count)];
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Učitava podatke za sve igre.
        /// </summary>
        /// <param name="dataPath"></param>
        public static void ReadAllGamesData(string dataPath)
        {
            _AllGamesInformation = GetAllGamesInformations(dataPath);

            _CurrentGameInformation = new List<FullSlotInformation>();
            foreach (Games game in Enum.GetValues(typeof(Games)))
            {
                var fullGameInfo = new FullSlotInformation { Game = game };
                var allInfo = GetAllInformationsForGame(game);
                if (allInfo != null && allInfo.Count > 0)
                {
                    fullGameInfo.SlotGameInformations = new List<SlotGameInformation> { allInfo[(int)SoftwareRng.Next(allInfo.Count)] };
                }

                _CurrentGameInformation.Add(fullGameInfo);
            }
        }

        /// <summary>
        /// Daje dodatnu putanju za zadatu igru i ulog. Ako ne obradi informacije uspešno vraća podrazumevane vrednosti.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="gratisGame"> </param>
        /// <returns></returns>
        public static string GetPathForGame(Games game, bool gratisGame)
        {
            var currentGameInformation = _CurrentGameInformation.FirstOrDefault(c => c.Game == game);
            if (currentGameInformation.SlotGameInformations == null)
            {
                return game + @"\" + game + (gratisGame ? "G" : "") + ".fsl";
            }
            if (currentGameInformation.SlotGameInformations[0].Count <= 0)
            {
                SetNewInformation(game);
            }
            if (!gratisGame)
            {
                var tempInformation = currentGameInformation.SlotGameInformations[0];
                tempInformation.Count--;
                currentGameInformation.SlotGameInformations[0] = tempInformation;
            }
            Console.WriteLine("==================\r\n" + game + @"\" + (gratisGame ? currentGameInformation.SlotGameInformations[0].Gratis : currentGameInformation.SlotGameInformations[0].FileName) + ".fsl     " + currentGameInformation.SlotGameInformations[0].Count + "\r\n==================");
            return game + @"\" + (gratisGame ? currentGameInformation.SlotGameInformations[0].Gratis : currentGameInformation.SlotGameInformations[0].FileName) + ".fsl";
        }

        #endregion
    }
}
