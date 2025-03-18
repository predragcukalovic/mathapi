using RNGUtils.RandomData;
using System;
using System.Collections.Generic;
using System.IO;

namespace CombinationExtras.ReaderData
{
    public static class MathSlotFilesReader
    {
        private static readonly Dictionary<string, byte[]> _AllFiles = new Dictionary<string, byte[]>();

        /// <summary>
        /// Učitava sve fajlove za igre koji postoje.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="games"></param>
        /// <param name="softwareversion"></param>
        public static void ReadAllFiles(string path, Enum games, string softwareversion)
        {
            RngValidator.InitValidator(softwareversion);

            var names = Enum.GetNames(games.GetType());

            foreach (var name in names)
            {
                var filePath = Path.Combine(path, name, name + ".fsl");
                var gratisFilePath = Path.Combine(path, name, name + "G.fsl");
                var secondGratisFilePath = Path.Combine(path, name, name + "GG.fsl");
                if (File.Exists(filePath))
                {
                    _AllFiles.Add(name, File.ReadAllBytes(filePath));
                }
                if (File.Exists(gratisFilePath))
                {
                    _AllFiles.Add(name + "G", File.ReadAllBytes(gratisFilePath));
                }
                if (File.Exists(secondGratisFilePath))
                {
                    _AllFiles.Add(name + "GG", File.ReadAllBytes(secondGratisFilePath));
                }
            }
        }

        /// <summary>
        /// Daje sve bajtove iz fajla za igru ili za igruG.
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        public static byte[] GetSlotFileForGame(string game)
        {
            return _AllFiles.ContainsKey(game) ? _AllFiles[game] : null;
        }
    }
}
