using GameWildHot40Blow;
using MathForGames.BasicGameData;
using System.Collections.Generic;
using RNGUtils.RandomData;
using MathBaseProject.BaseMathData;
using System;
using MathBaseProject.StructuresV3;
namespace GameBlowFruits40
{
    public class MatrixBlowFruits40
    {
        #region Private fields

        private readonly int[,] _Matrix;

        #endregion

        #region Constructor or Singleton implementation

        /// <summary>
        /// konstruktor za matricu
        /// </summary>
        public MatrixBlowFruits40()
        {
            _Matrix = new int[5, 6];
        }

        #endregion

        #region Public properties

        public static int[] PlayLines = { 40 };

        public static readonly int[,] WinForLinesBlowFruits40 =
        {
            {0, 0, 0, 0, 0},
            {0, 0, 20, 50, 400},
            {0, 0, 20, 45, 300},
            {0, 0, 15, 40, 150},
            {0, 0, 15, 40, 150},
            {0, 0, 5, 20, 50},
            {0, 0, 5, 20, 50},
            {0, 0, 5, 20, 50},
            {0, 0, 5, 20, 50},
        };
        public static readonly int[] WinForWildsBlowFruits40 = { 0, 0, 50, 150, 750 };

        #endregion


        /// <summary>
        /// uzima liniju iz matrice
        /// </summary>
        /// <param name="lineNumber">broj linije, 1 -- 40</param>
        /// <param name="lines">Linije </param>
        /// <returns>vraća liniju pod datim brojem</returns>
        public Line GetLine(int lineNumber, int[,] lines)
        {
            try
            {
                var line = new Line();
                for (var i = 0; i < 5; i++)
                {
                    line.SetElement(i, _Matrix[i, lines[lineNumber - 1, i] + 1]);
                }
                return line;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// geter za element iz matrice na poziciji [i,j]
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns>vraća element iz matrice na poziciji [i,j]</returns>
        public int GetElement(int i, int j)
        {
            return _Matrix[i, (j + 1) % 6];
        }

        public int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLineTurbo).CalculateLineWin(WinForLinesBlowFruits40, WinForWildsBlowFruits40, 0, 1);
        }


        public int GetWinningElementForLine(int lineNumber, int wild, int[] winForWild, int lineWin, int[,] gameLines)
        {
            return GetLine(lineNumber, gameLines).GetWinningElement(wild, lineWin, WinForLinesBlowFruits40, winForWild);
        }

        /// <summary>
        /// Konstruiše matricu na osnovu dvodimenzionalnog niza.
        /// </summary>
        /// <param name="matrix"></param>
        public void FromMatrixArray(int[,] matrix)
        {
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 6; j++)
                {
                    _Matrix[i, j] = matrix[i, j];
                }
            }
        }

        /// <summary>
        /// Adding wilds to reels by reel probability and then by symbol position probability
        /// </summary>
        /// <returns></returns>
        public void AddWilds()
        {
            var wildProbsByReel = new[] { 70, 120, 119, 70, 70 };
            var wildProbsBySymbolPosition = new[] { 3, 2, 2, 3 };

            var wilds = new List<WildBlowFruits40>();
            for (var i = 0; i < 5; i++)
            {
                if (SoftwareRng.Next(wildProbsByReel[i]) == 0)
                {
                    var randNumber = SoftwareRng.Next(10);
                    var cumulativeProbability = 0;

                    for (int j = 0; j < wildProbsBySymbolPosition.Length; j++)
                    {
                        cumulativeProbability += wildProbsBySymbolPosition[j];
                        if (randNumber < cumulativeProbability)
                        {
                            wilds.Add(new WildBlowFruits40(i, j + 1));
                            break;
                        }
                    }

                }
            }
            if (wilds.Count == 0)
            {
                return;
            }
            var arr = new int[5, 6];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 6; j++)
                {
                    arr[i, j] = GetElement(i, j);
                }
            }
            foreach (var wild in wilds)
            {
                arr[wild.PosI, wild.PosJ] = 0;
            }
            FromMatrixArray(arr);
        }

        public void SetExpanding()
        {
            var wilds = new List<int>();
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    if (GetElement(i, j) == 0)
                    {
                        wilds.Add(j * 5 + i);
                    }
                }
            }
            if (wilds.Count == 0)
            {
                return;
            }
            var arr = new int[5, 6];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 6; j++)
                {
                    arr[i, j] = GetElement(i, j + 5);
                }
            }
            foreach (var wild in wilds)
            {
                var i = wild % 5;
                var j = wild / 5 + 1;
                for (var k = i - 1; k <= i + 1; k++)
                {
                    for (var l = j - 1; l <= j + 1; l++)
                    {
                        if (k >= 0 && k <= 4 && l >= 1 && l <= 4)
                        {
                            arr[k, l] = 0;
                        }
                    }
                }
            }
            FromMatrixArray(arr);
        }

        #region V3 structs

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 2, 2, 2, 6, 6, 6, 6, 3, 3, 3, 3, 2, 2, 4, 4, 4, 4, 8, 8, 8, 8, 5, 5, 5, 5, 7, 7, 7, 7, 4, 4, 4, 4, 1, 1, 5, 5, 5, 5, 7, 7, 7, 7, 2, 2, 6, 6, 6, 6, 8, 8, 8, 8, 0, 3, 3, 3, 3, 5, 5, 5, 5, 4, 4, 4, 4 };
            fakeReels[1] = new[] { 8, 8, 8, 8, 6, 6, 6, 6, 4, 4, 4, 4, 5, 5, 5, 5, 2, 2, 2, 2, 3, 3, 3, 3, 1, 1, 1, 1, 0, 8, 8, 8, 8, 6, 6, 6, 6, 7, 7, 7, 7, 5, 5, 5, 5, 2, 2, 2, 2, 4, 4, 4, 4, 7, 7, 7, 7, 3, 3, 3, 3, 8, 8, 8, 8, 5, 5, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7, 1, 1 };
            fakeReels[2] = new[] { 6, 6, 6, 6, 7, 7, 7, 7, 3, 3, 3, 3, 6, 6, 6, 6, 4, 4, 4, 4, 0, 1, 1, 2, 2, 6, 6, 6, 6, 5, 5, 5, 5, 4, 4, 4, 4, 7, 7, 7, 7, 8, 8, 8, 8, 3, 3, 3, 3, 5, 5, 5, 5, 6, 6, 6, 6, 3, 3, 3, 3, 7, 7, 7, 7, 8, 8, 8, 8, 1, 1, 5, 5, 5, 5, 2, 2 };
            fakeReels[3] = new[] { 7, 7, 7, 7, 2, 2, 6, 6, 6, 6, 4, 4, 4, 4, 5, 5, 5, 5, 7, 7, 7, 7, 4, 4, 4, 4, 2, 2, 1, 1, 8, 8, 8, 8, 6, 6, 6, 6, 3, 3, 3, 3, 2, 1, 1, 6, 6, 6, 6, 5, 5, 5, 5, 0, 8, 8, 8, 8, 3, 3, 3, 3, 4, 4, 4, 4, 8, 8, 8, 8 };
            fakeReels[4] = new[] { 4, 4, 4, 4, 8, 8, 8, 8, 6, 6, 6, 6, 7, 7, 7, 7, 5, 5, 5, 5, 2, 2, 2, 1, 1, 3, 3, 3, 3, 5, 5, 5, 5, 6, 6, 6, 6, 3, 3, 3, 3, 8, 8, 8, 8, 1, 1, 7, 7, 7, 7, 2, 2, 0, 4, 4, 4, 4, 3, 3, 3, 3, 6, 6, 6, 6 };
            return fakeReels;
        }

        /// <summary>
        /// Vraća niz koeficijenata za id simbola.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int[] GetSymbolCoefficients(int id)
        {
            if (id == 0)
            {
                return WinForWildsBlowFruits40;
            }
            var coefficients = new int[5];
            for (var i = 0; i < 5; i++)
            {
                coefficients[i] = WinForLinesBlowFruits40[id, i];
            }
            return coefficients;
        }

        public static HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)96.59,
                symbols = GetHelpSymbolConfigV3(),
                lines = GetHelpLineConfigV3()
            };

            return helpV3;
        }

        private static HelpSymbolConfigV3<object>[] GetHelpSymbolConfigV3()
        {
            var symbols = new HelpSymbolConfigV3<object>[9];
            for (var i = 0; i < 9; i++)
            {
                symbols[i] = new HelpSymbolConfigV3<object>
                {
                    id = i,
                    extra = new HelpSymbolExtraV3(),
                    coefficients = GetSymbolCoefficients(i),
                    features = new[] { HelpSymbolFeatureV3.Regular }
                };
            }

            return symbols;
        }

        private static HelpLineConfigV3[] GetHelpLineConfigV3()
        {
            var lines = new HelpLineConfigV3[40];
            for (var i = 0; i < 40; i++)
            {
                var pos = new int[5];
                for (var j = 0; j < 5; j++)
                {
                    pos[j] = GlobalData.GameLineTurbo[i, j];
                }
                lines[i] = new HelpLineConfigV3 { id = i, positions = pos };
            }

            return lines;
        }

        #endregion
    }
}

