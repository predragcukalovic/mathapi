using CombinationExtras.ReaderData;
using CombinationExtras.V3Data;
using Papi.GameServer.Utils.Enums;
using Papi.GameServer.Utils.Helper;
using GameHeatingFruits.Config;
using GamesSimulator.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GamesSimulator
{
    internal class Program
    {
        static int Main(string[] args)
        {
            var exceptions = new ConcurrentQueue<string>();
            var gamesWithInvalidNumOfLines = new ConcurrentQueue<string>();
            var gameSimulationSuccessfulResult = new ConcurrentDictionary<string, GameSimulationResultDto>();
            var gameSimulationInvalidResult = new ConcurrentDictionary<string, GameSimulationResultDto>();
            GameSimulatorConfigDto gameSimulatorConfig = null;
            int processedGames = 0;
            var success = true;

            int processorCountDecrease = args.Length > 0 && int.TryParse(args[0], out int parsedValue) ? parsedValue : 0;

            try
            {
                var sw = Stopwatch.StartNew();
                MathSlotFilesReader.ReadAllFiles(@"Data", new Games(), " ");
                UnicornFileReader.ReadAllFiles(@"DataExt", new Games());

                using (StreamReader r = new StreamReader("simulatorConfig.json"))
                {
                    var json = r.ReadToEnd();
                    gameSimulatorConfig = JsonConvert.DeserializeObject<GameSimulatorConfigDto>(json);
                }

                // excluding novomatic games?
                // for now there are:
                // NovomaticBookOfRaDeluxe
                // NovomaticLuckyLadysCharmDeluxe
                // NovomaticRoaringForties
                // NovomaticSizzlingHotDeluxe
                gameSimulatorConfig.ExcludedGames.AddRange(Enum
                    .GetValues(typeof(Games))
                    .Cast<Games>()
                    .Where(p => p.ToString().ToLower().Contains("novomatic"))
                    .Select(p => p.ToString()));

                var games = Enum
                    .GetValues(typeof(Games))
                    .Cast<Games>()
                    .Where(p => !gameSimulatorConfig.ExcludedGames.Contains(p.ToString()))
                    .ToList();

                var maxDegreeOfParallelism = Environment.ProcessorCount - processorCountDecrease;

                Console.WriteLine($"Processor Count on this Machine: {Environment.ProcessorCount}");
                Console.WriteLine($"Max Degree Of Parallelism will be set to: {maxDegreeOfParallelism} \n");
                Console.WriteLine($"Number of iterations is set to: {gameSimulatorConfig.NumberOfSimulatorIterations}\n");

                var options = new ParallelOptions()
                {
                    MaxDegreeOfParallelism = maxDegreeOfParallelism
                };

                Parallel.For(0, games.Count, options, count =>
                {
                    try
                    {
                        byte[] addArray = null;
                        byte addInfo = 0;
                        var game = games[count];
                        var isUnicornGame = GameHelper.IsUnicornProviderGame((int)game);

                        int[] playLines;
                        var lines = -1;
                        if (isUnicornGame)
                        {
                            playLines = UnicornInitGameDataV3.GetPlayLines(game);
                        }
                        else
                        {
                            playLines = InitGameDataV3.GetPlayLines(game);
                        }

                        if (playLines != null && playLines.Length > 0)
                        {
                            lines = playLines.Max();
                        }

                        if (lines <= 0)
                        {
                            if (gameSimulatorConfig.MissingGamesNumOfLines.TryGetValue(game.ToString(), out var numOfLines))
                            {
                                lines = numOfLines;
                            }
                        }

                        if (lines != -1 || gameSimulatorConfig.GamesWithoutLineParameter.Contains(game.ToString()))
                        {
                            long totalBet = 0;
                            long totalWin = 0;
                            var maxGratisWin = 0;

                            var gratisCollect = 0;
                            var numberOfGratis = 0;
                            var dict = new Dictionary<int, long>();

                            object oo = null;
                            int md = 1, inc = 0;
                            if (game == Games.HeatingFruits)
                            {
                                oo = new HeatingFruitsConfig { LastBet = 1 };
                                oo = JsonConvert.SerializeObject(oo);
                            }
                            if (game == Games.FruityForce40)
                            {
                                oo = (int)addInfo;
                            }
                            if (game == Games.CrystalsOfMagic)
                            {
                                md = 8; inc = 1;
                            }
                            if (game == Games.TemplarsQuest)
                            {
                                md = 15;
                            }

                            for (var i = 0; i < gameSimulatorConfig.NumberOfSimulatorIterations; i++)
                            {
                                MathCombination.CombinationData.ICombination comb = null;

                                if (!isUnicornGame)
                                {
                                    comb = CombinationExtras.SlotCombination.GetCombination(game, 1, lines, numberOfGratis, ref addArray, 1, addInfo, (i % md) + inc , oo);
                                }
                                else
                                {
                                    comb = CombinationExtras.UnicornSlotCombination.GetCombination(game, 1, lines, numberOfGratis, addInfo, ref addArray);
                                }

                                totalBet += numberOfGratis > 0 ? 0 : lines;
                                totalWin += comb.TotalWin;
                                var vgwin = comb.TotalWin;

                                if (gameSimulatorConfig.GamesWithCascadeWin.Contains(game.ToString()))
                                {
                                    vgwin += comb.CascadeList.Sum(combination => combination.TotalWin); 
                                }

                                if (comb.GratisGame || numberOfGratis > 0)
                                {
                                    gratisCollect += vgwin;
                                }

                                if (!comb.GratisGame && numberOfGratis == 0)
                                {
                                    if (!dict.ContainsKey(vgwin))
                                    {
                                        dict.Add(vgwin, 0);
                                    }
                                    dict[vgwin]++;
                                }

                                if (numberOfGratis == 1 && !comb.GratisGame)
                                {
                                    if (!dict.ContainsKey(gratisCollect))
                                    {
                                        dict.Add(gratisCollect, 0);
                                    }
                                    dict[gratisCollect]++;

                                    if (maxGratisWin < gratisCollect)
                                    {
                                        maxGratisWin = gratisCollect;
                                    }
                                    gratisCollect = 0;
                                }
                                numberOfGratis = Math.Max(numberOfGratis - 1, 0);
                                numberOfGratis += comb.NumberOfGratisGames;
                                addInfo = comb.AdditionalInformation;
                            }

                            var total = (double)dict.Sum(x => x.Value);
                            var wp = dict.ToDictionary(w => w.Key / (double)lines, p => p.Value / total);

                            if (!wp.ContainsKey(0))
                            {
                                wp.Add(0, 0);
                            }

                            var mu = wp.Sum(x => x.Key * x.Value);
                            var sigma = wp.Sum(x => x.Value * Math.Pow(x.Key - mu, 2));
                            var sqs = Math.Sqrt(sigma);
                            var hf = 1.0 - wp[0];
                            var wf = 1.0 - wp.Where(x => x.Key <= 1.0).Sum(x => x.Value);

                            var gameSimulationResult = new GameSimulationResultDto
                            {
                                GameName = game.ToString(),
                                Rtp = mu,
                                HitFrequency = hf,
                                StandardDeviation = sqs,
                                MaxGratisWin = maxGratisWin,
                                MaxExposure = (double)dict.Max(x => x.Key) / lines
                            };
                            Console.WriteLine($"Completed simulation for {gameSimulationResult.GameName} with RTP: {gameSimulationResult.Rtp}");

                            if (gameSimulationResult.Rtp >= 1)
                            {
                                gameSimulationInvalidResult.TryAdd(game.ToString(), gameSimulationResult); 
                            }
                            else
                            {
                                gameSimulationSuccessfulResult.TryAdd(game.ToString(), gameSimulationResult);
                            }

                            Interlocked.Increment(ref processedGames);
                        }
                        else
                        {
                            gamesWithInvalidNumOfLines.Enqueue(game.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        exceptions.Enqueue($"Error while processing game {games[count]}: {ex.Message} \n Stack trace: {ex.StackTrace}");
                    }
                });

                if (!exceptions.IsEmpty)
                {
                    foreach (var exception in exceptions)
                    {
                        Console.WriteLine($"{exception}\n");
                    }

                    success = false;
                }

                if (!gamesWithInvalidNumOfLines.IsEmpty)
                {
                    Console.WriteLine("Games with invalid number of lines:");

                    foreach (var gameName in gamesWithInvalidNumOfLines)
                    {
                        Console.WriteLine(gameName);
                    }

                    success = false;
                }

                if (!gameSimulationInvalidResult.IsEmpty)
                {
                    Console.WriteLine("Games with invalid value of RTP: \n");

                    foreach (var gameSimulationItem in gameSimulationInvalidResult)
                    {
                        var gameSimulationItemValue = gameSimulationItem.Value;

                        Console.WriteLine($"Game: {gameSimulationItemValue.GameName}");
                        Console.WriteLine($"RTP: {gameSimulationItemValue.Rtp}");
                        Console.WriteLine($"Hit Frequency: {gameSimulationItemValue.HitFrequency}");
                        Console.WriteLine($"Standard Deviation: {gameSimulationItemValue.StandardDeviation}");
                        Console.WriteLine($"Max Gratis Exposure: {gameSimulationItemValue.MaxGratisWin}");
                        Console.WriteLine($"Max Exposure: {gameSimulationItemValue.MaxExposure}\n");
                    }

                    success = false;
                }

                if (!success)
                {
                    return 1;
                }

                double tickFrequency = Stopwatch.Frequency / 1000.0;
                sw.Stop();
                Console.WriteLine($"Total Elapsed Time: {sw.ElapsedTicks / tickFrequency} ms");

                Console.WriteLine($"Processed games: {processedGames}\n");

                if (!gameSimulationSuccessfulResult.IsEmpty)
                {
                    Console.WriteLine("Games with valid value of RTP: \n");

                    foreach (var gameSimulationItem in gameSimulationSuccessfulResult)
                    {
                        var gameSimulationItemValue = gameSimulationItem.Value;

                        Console.WriteLine($"Game: {gameSimulationItemValue.GameName}");
                        //Console.WriteLine($"Total RTP: {gameSimulationItemValue.TotalRtp}");
                        Console.WriteLine($"RTP: {gameSimulationItemValue.Rtp}");
                        Console.WriteLine($"Hit Frequency: {gameSimulationItemValue.HitFrequency}");
                        Console.WriteLine($"Standard Deviation: {gameSimulationItemValue.StandardDeviation}");
                        Console.WriteLine($"Max Gratis Exposure: {gameSimulationItemValue.MaxGratisWin}");
                        Console.WriteLine($"Max Exposure: {gameSimulationItemValue.MaxExposure}\n");
                    }

                    success = false;
                }

                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message} \n Stack trace: {e.StackTrace}");
                return 1;
            }
        }
    }
}
