using MathBaseProject.StructuresV3;
using System.Collections.Generic;
using System.Linq;
using V4Converter.DTOs;
using V4Converter.Readers;

namespace V4Converter
{
    public class HelpConfigMapper
    {
        public static HelpSymbolConfigV3<object>[] getHelpSymbolConfigV3(GameConfig gameConfig)
        {
            if (gameConfig.GameName == "MysticJungle")
            {
                return GetHelpSymbolConfigV3MysticJungle(gameConfig);
            }
            else if (gameConfig.MissingSymbols != null)
            {
                return GetHelpSymbolConfigV3MissingSymbol(gameConfig);
            }
            else
            {
                return GetHelpSymbolConfigV3Default(gameConfig);
            }
        }

        public static HelpLineConfigV3[] getHelpLineConfigV3(GameConfig gameConfig)
        {
            switch (gameConfig.GameLineModifier)
            {
                case "Shift":
                    return GetHelpLineConfigV3Shifted(gameConfig);
                default:
                    return GetHelpLineConfigV3Default(gameConfig);
            }
        }
        private static HelpLineConfigV3[] GetHelpLineConfigV3Default(GameConfig gameConfig)
        {
            var numberOfLines = gameConfig.NumberOfLines;
            var lines = new HelpLineConfigV3[numberOfLines];
            var numberOfReels = gameConfig.NumberOfReels;
            for (var i = 0; i < numberOfLines; i++)
            {
                var pos = new int[numberOfReels];
                for (var j = 0; j < numberOfReels; j++)
                {
                    pos[j] = GameLineConfigReader.GetGameLineConfig(gameConfig.LineType)[i][j];
                }
                lines[i] = new HelpLineConfigV3 { id = i, positions = pos };
            }

            return lines;
        }

        private static HelpLineConfigV3[] GetHelpLineConfigV3Shifted(GameConfig gameConfig)
        {
            var numberOfLines = gameConfig.NumberOfLines;
            var lines = new HelpLineConfigV3[numberOfLines];
            var numberOfReels = gameConfig.NumberOfReels;
            for (var i = 0; i < numberOfLines; i++)
            {
                var pos = new int[numberOfReels];
                for (var j = 0; j < numberOfReels; j++)
                {
                    pos[j] = GameLineConfigReader.GetGameLineConfig(gameConfig.LineType)[i][j] - 1;
                }
                lines[i] = new HelpLineConfigV3 { id = i, positions = pos };
            }

            return lines;
        }

        private static HelpSymbolConfigV3<object>[] GetHelpSymbolConfigV3Default(GameConfig gameConfig)
        {
            var numberOfSymbols = gameConfig.NumberOfSymbols;
            var symbols = new HelpSymbolConfigV3<object>[numberOfSymbols];
            for (var i = 0; i < numberOfSymbols; i++)
            {
                symbols[i] = new HelpSymbolConfigV3<object>
                {
                    id = i,
                    features = new[] { HelpSymbolFeatureV3.Regular },
                    extra = new HelpSymbolExtraV3(),
                    coefficients = GetSymbolCoefficients(i, gameConfig)
                };
            }
            return symbols;
        }

        private static HelpSymbolConfigV3<object>[] GetHelpSymbolConfigV3MysticJungle(GameConfig gameConfig)
        {
            var numberOfSymbols = gameConfig.NumberOfSymbols;
            var symbols = new HelpSymbolConfigV3<object>[numberOfSymbols];
            for (var i = 0; i < numberOfSymbols; i++)
            {
                symbols[i] = new HelpSymbolConfigV3<object>
                {
                    id = i == 9 ? 18 : i,
                    features = new[] { HelpSymbolFeatureV3.Regular },
                    extra = new HelpSymbolExtraV3(),
                    coefficients = GetSymbolCoefficients(i, gameConfig)
                };
            }
            return symbols;
        }

        private static HelpSymbolConfigV3<object>[] GetHelpSymbolConfigV3MissingSymbol(GameConfig gameConfig)
        {
            var numberOfSymbols = gameConfig.NumberOfSymbols;
            var symbols = new List<HelpSymbolConfigV3<object>>();
            for (var i = 0; i < numberOfSymbols; i++)
            {
                if (!gameConfig.MissingSymbols.Contains(i))
                {
                    symbols.Add(new HelpSymbolConfigV3<object>
                    {
                        id = i,
                        features = new[] { HelpSymbolFeatureV3.Regular },
                        extra = new HelpSymbolExtraV3(),
                        coefficients = GetSymbolCoefficients(i, gameConfig)
                    });
                }
            }
            return symbols.ToArray();
        }

        private static int[] GetSymbolCoefficients(int symbol, GameConfig gameConfig)
        {
            var numberOfReels = gameConfig.NumberOfReels;
            var winForLines = gameConfig.WinForLines;
            var coefficients = new int[numberOfReels];
            for (var i = 0; i < numberOfReels; i++)
            {
                coefficients[i] = winForLines[symbol, i];
            }
            return coefficients;
        }

    }
}
