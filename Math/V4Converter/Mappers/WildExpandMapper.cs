using Papi.GameServer.Math.Contracts.StructuresV3;
using System.Collections.Generic;
using V4Converter.DTOs;

namespace V4Converter
{
    public class WildExpandMapper
    {
        /*public static object getWildExpand(V3MapperParams v3MapperParams)
        {
            switch (v3MapperParams.GameId)
            {
                case Games.DeepJungle:
                case Games.WildHot40Blow:
                case Games.WildHot40BlowDice:
                case Games.PiratesPapi:
                    return GetWildExpandNeighboring(v3MapperParams.Combination, v3MapperParams.NumberOfReels, v3MapperParams.NumberOfRows, v3MapperParams.Matrix);
                case Games.CrystalHot40Max:
                    return GetWildExpandReelIndex(v3MapperParams.Combination, v3MapperParams.NumberOfReels, v3MapperParams.NumberOfRows, v3MapperParams.Matrix);
                default:
                    return GetWildExpandDefault(v3MapperParams.Combination, v3MapperParams.NumberOfReels, v3MapperParams.NumberOfRows, v3MapperParams.Matrix);
            }
        }*/

        public static object getWildExpand(WildExpandParams wildExpandParams)
        {
            GameConfig gameConfig = wildExpandParams.GameConfig;
            int numberOfReels = gameConfig.NumberOfReels;
            int numberOfRows = gameConfig.NumberOfRows;
            byte[] positionFor2 = wildExpandParams.PositionFor2;
            int[,] matrix = wildExpandParams.Matrix;
            switch (gameConfig.WildExpandStrategy)
            {
                case "Neighboring":
                    return GetWildExpandNeighboring(positionFor2, numberOfReels, numberOfRows, matrix);
                case "ReelIndex":
                    return GetWildExpandReelIndex(positionFor2, numberOfReels, numberOfRows, matrix);
                default:
                    return GetWildExpandDefault(positionFor2, numberOfReels, numberOfRows, matrix);
            }
        }

        private static WildExpandV3[] GetWildExpandDefault(byte[] positionFor2, int numberOfReels, int numberOfRows, int[,] matrix)
        {
            var wilds = new List<WildExpandV3>();
            for (var i = 0; i < numberOfReels; i++)
            {
                if (positionFor2[i] < matrix.Length)
                {
                    var wld = new WildExpandV3
                    {
                        type = "expand",
                        origin = new CoordinateV3 { reel = positionFor2[i] % numberOfReels, row = positionFor2[i] / numberOfReels }
                    };
                    var coords = new List<CoordinateV3>();
                    for (var j = 0; j < numberOfRows; j++)
                    {
                        if (j != wld.origin.row)
                        {
                            coords.Add(new CoordinateV3 { reel = wld.origin.reel, row = j });
                        }
                    }
                    wld.coordinates = coords.ToArray();
                    wilds.Add(wld);
                }
            }
            return wilds.ToArray();
        }

        private static WildExpandV3[] GetWildExpandPegasus(byte[] positionFor2, int numberOfReels, int numberOfRows, int[,] matrix)
        {
            var wilds = new List<WildExpandV3>();
            for (var i = 0; i < numberOfReels; i++)
            {
                if (positionFor2[i] < matrix.Length)
                {
                    var wld = new WildExpandV3
                    {
                        type = "expand",
                        origin = new CoordinateV3 { reel = positionFor2[i] % numberOfReels, row = positionFor2[i] / numberOfReels }
                    };
                    var coords = new List<CoordinateV3>();
                    for (var j = wld.origin.reel - 1; j < wld.origin.reel + 2; j++)
                    {
                        if (j != wld.origin.reel)
                        {
                            coords.Add(new CoordinateV3 { reel = j, row = wld.origin.row });
                        }
                    }
                    wld.coordinates = coords.ToArray();
                    wilds.Add(wld);
                }
            }
            return wilds.ToArray();
        }

        private static WildExpandV3[] GetWildExpandReelIndex(byte[] positionFor2, int numberOfReels, int numberOfRows, int[,] matrix, int symbol = 0)
        {
            var wilds = new List<WildExpandV3>();
            for (var i = 0; i < numberOfReels; i++)
            {
                if (positionFor2[i] == 1)
                {
                    int rowNumber = 0;
                    while (rowNumber < numberOfRows)
                    {
                        if (matrix[i, rowNumber] == symbol)
                        {
                            break;
                        }
                        else
                        {
                            rowNumber++;
                        }
                    }
                    var wld = new WildExpandV3
                    {
                        type = "expand",
                        origin = new CoordinateV3 { reel = i, row = rowNumber }
                    };
                    var coords = new List<CoordinateV3>();
                    for (var j = 0; j < numberOfRows; j++)
                    {
                        if (j != wld.origin.row)
                        {
                            coords.Add(new CoordinateV3 { reel = wld.origin.reel, row = j });
                        }
                    }
                    wld.coordinates = coords.ToArray();
                    wilds.Add(wld);
                }
            }
            return wilds.ToArray();
        }

        private static WildExpandV3[] GetWildExpandNeighboring(byte[] positionFor2, int numberOfReels, int numberOfRows, int[,] matrix)
        {
            var wilds = new List<WildExpandV3>();
            for (var i = 0; i < numberOfReels; i++)
            {
                if (positionFor2[i] < matrix.Length)
                {
                    var wld = new WildExpandV3
                    {
                        type = "expand",
                        origin = new CoordinateV3 { reel = positionFor2[i] % numberOfReels, row = positionFor2[i] / numberOfReels }
                    };
                    var coords = new List<CoordinateV3>();
                    for (var k = wld.origin.reel - 1; k < wld.origin.reel + 2; k++)
                    {
                        for (var j = wld.origin.row - 1; j < wld.origin.row + 2; j++)
                        {
                            if ((j != wld.origin.row || k != wld.origin.reel) && (j > -1 && j < numberOfRows && k > -1 && k < numberOfReels))
                            {
                                coords.Add(new CoordinateV3 { reel = k, row = j });
                            }
                        }
                        wld.coordinates = coords.ToArray();
                    }
                    wilds.Add(wld);
                }
            }
            return wilds.ToArray();
        }
    }
}
