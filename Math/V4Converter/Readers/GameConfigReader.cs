using Papi.GameServer.Utils.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using V4Converter.DTOs;

namespace V4Converter.Readers
{
    public static class GameConfigReader
    {
        private static Dictionary<string, GameConfig> _gameConfigData = new Dictionary<string, GameConfig>();
        private static Dictionary<string, string> _clonesList = new Dictionary<string, string>();
        private static Dictionary<string, GameCopiesParams> _copiesList = new Dictionary<string, GameCopiesParams>();


        private static void LoadClonesList()
        {
            var configPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Substring(6), "GameConfiguration", "ClonesList", "clonesList.json");
            var json = File.ReadAllText(configPath);
            _clonesList = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }

        private static void LoadCopiesList()
        {
            var configPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Substring(6), "GameConfiguration", "CopiesList", "copiesList.json");
            var json = File.ReadAllText(configPath);
            _copiesList = JsonConvert.DeserializeObject<Dictionary<string, GameCopiesParams>>(json);
        }

        private static GameConfig GetCloneGameConfig(string game)
        {
            string original = _clonesList[game];
            if (_gameConfigData.ContainsKey(original))
            {
                GameConfig gameConfig = _gameConfigData[original];
                gameConfig.GameName = game;
                return gameConfig;
            }
            else
            {
                return null;
            }
        }

        private static GameConfig GetCopyGameConfig(string game)
        {
            string original = _copiesList[game].CopyOf;
            if (_gameConfigData.ContainsKey(original))
            {
                var properties = _copiesList[game].GetType().GetProperties();
                GameConfig gameConfig = _gameConfigData[original];
                foreach (var prop in properties)
                {
                    if (prop.Name != "CopyOf")
                    {
                        if (prop.PropertyType == typeof(int))
                        {
                            if ((int)prop.GetValue(_copiesList[game], null) != -1)
                            {
                                prop.SetValue(gameConfig, prop.GetValue(_copiesList[game], null));
                            }
                        }
                        else if (prop.GetValue(_copiesList[game], null) != null)
                        {
                            prop.SetValue(gameConfig, prop.GetValue(_copiesList[game], null));
                        }
                    }
                }
                return gameConfig;
            }
            else
            {
                return null;
            }
        }

        public static void ReadGameConfigData(List<Games> convertedGames)
        {
            LoadClonesList();
            //LoadCopiesList();
            foreach (var game in convertedGames)
            {
                var name = Enum.GetName(typeof(Games), game);
                if (_clonesList.ContainsKey(name) || _copiesList.ContainsKey(name))
                {
                    continue;
                }
                var configPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Substring(6);
                var filePath = Path.Combine(configPath, "GameConfiguration", name, name + ".json");
                if (File.Exists(filePath))
                {
                    _gameConfigData.Add(name, JsonConvert.DeserializeObject<GameConfig>(File.ReadAllText(filePath)));
                }
            }
        }

        public static GameConfig GetGameConfig(string game)
        {
            if (_clonesList.ContainsKey(game))
            {
                return GetCloneGameConfig(game);
            }
            else if (_copiesList.ContainsKey(game))
            {
                return GetCopyGameConfig(game);
            }
            return _gameConfigData.ContainsKey(game) ? _gameConfigData[game] : null;
        }
    }
}
