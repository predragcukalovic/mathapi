using Papi.GameServer.Math.Contracts.Responses;
using Papi.GameServer.Math.Contracts.StructuresV3;
using Papi.GameServer.Math.JollyPoker.PokerCombination;
using Papi.GameServer.Math.JollyPoker.Serialization;
using MathCombination.CombinationData;
using System.Linq;

namespace Papi.GameServer.Math.ApiCore.Extensions
{
    public static class Converters
    {
        public static CombinationModel ToCombinationModel(this ICombination combination)
        {
            var model = new CombinationModel
            {
                Matrix = ToJagged(combination.Matrix),
                AdditionalArray = combination.AdditionalArray,
                AdditionalInformation = combination.AdditionalInformation,
                GratisGame = combination.GratisGame,
                GratisGamesPositions = combination.GratisGamesPositions,
                NumberOfGratisGames = combination.NumberOfGratisGames,
                MultiplyFor2 = combination.MultiplyFor2,
                MultiplyFor2Alpinist = combination.MultiplyFor2Alpinist,
                WinFor2 = combination.WinFor2,
                PositionFor2 = combination.PositionFor2,
                NumberOfWinningLines = combination.NumberOfWinningLines,
                LinesInformation = combination.LinesInformation?.Select(ToLineInfoModel).ToArray(),
                GratisGamesValues = combination.GratisGamesValues,
                TotalWin = combination.TotalWin,
                CascadeList = combination.CascadeList?.Select(ToCombinationModel).ToList(),
            };

            return model;
        }

        public static LineInfoModel ToLineInfoModel(this LineInfo combination)
        {
            var model = new LineInfoModel
            {
                Id = combination.Id,
                Win = combination.Win,
                WinningElement = combination.WinningElement,
                WinningPosition = combination.WinningPosition
            };

            return model;
        }

        public static T[,] ToMultiD<T>(this T[][] jArray)
        {
            int i = jArray.Count();
            int j = jArray.Select(x => x.Count()).Aggregate(0, (current, c) => (current > c) ? current : c);


            var mArray = new T[i, j];

            for (int ii = 0; ii < i; ii++)
            {
                for (int jj = 0; jj < j; jj++)
                {
                    mArray[ii, jj] = jArray[ii][jj];
                }
            }

            return mArray;
        }

        public static T[][] ToJagged<T>(this T[,] mArray)
        {
            var cols = mArray.GetLength(0);
            var rows = mArray.GetLength(1);
            var jArray = new T[cols][];
            for (int i = 0; i < cols; i++)
            {
                jArray[i] = new T[rows];
                for (int j = 0; j < rows; j++)
                {
                    jArray[i][j] = mArray[i, j];
                }
            }
            return jArray;
        }

        public static PokerCombinationModel ToPokerCombinationModel(this PokerCombination _)
        {
            return new PokerCombinationModel
            {
                CardSign = _.CardSign,
                CardValue = _.CardValue,
                IsFirstDeal = _.IsFirstDeal,
                Win = _.Win,
                WinType = (Win)_.WinType,
                HoldCards = _.GetHoldCards(),
                CardHand = _.GetCardHand(),
                FrontedData = _.Serialize()
            };
        }

        public static HelpConfigV3<T> Map<T>(this MathBaseProject.StructuresV3.HelpConfigV3<T> _)
        {
            if (_ == null)
            {
                return null;
            }

            return new HelpConfigV3<T>
            {
                gambleLimit = _.gambleLimit,
                lines = _.lines?.Select(__ => __.Map()).ToArray(),
                rtp = _.rtp,
                symbols = _.symbols?.Select(__ => __.Map()).ToArray()
            };
        }

        public static HelpLineConfigV3 Map(this MathBaseProject.StructuresV3.HelpLineConfigV3 _)
        {
            if (_ == null)
            {
                return null;
            }

            return new HelpLineConfigV3
            {
                id = _.id,
                positions = _.positions
            };
        }

        public static HelpSymbolConfigV3<T> Map<T>(this MathBaseProject.StructuresV3.HelpSymbolConfigV3<T> _)
        {
            if (_ == null)
            {
                return null;
            }

            return new HelpSymbolConfigV3<T>
            {
                id = _.id,
                coefficients = _.coefficients,
                extra = _.extra,
                features = _.features?.Select(__ => (HelpSymbolFeatureV3)__).ToArray()
            };
        }

        public static ReelsV3 Map(this MathBaseProject.StructuresV3.ReelsV3 _)
        {
            if (_ == null)
            {
                return null;
            }

            return new ReelsV3
            {
                freeSpin = _.freeSpin,
                regular = _.regular,
                respin = _.respin
            };
        }
    }
}
