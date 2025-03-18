using Papi.GameServer.Utils.Enums;
using Papi.GameServer.Utils.Logging;
using MathCombination.CombinationData;
using System;

namespace Papi.GameServer.Math.Api.Helpers
{
    public static class SmBetHelper
    {
        public static string GenearateSmBetResult(ICombination combination, int smBetId, int gameId, bool isCurrentGameBonus,
            int numberOfLines, bool generateSmBetResult, string smBetResultInBonus, long betModifier)
        {
            try
            {
                if (!generateSmBetResult)
                {
                    return null;
                }

                string toReturn;
                if (!isCurrentGameBonus)
                {
                    toReturn = smBetId++ + ":" + GenerateSmResultForCombination(combination, betModifier);

                    if (combination.CascadeList != null)
                    {
                        foreach (var comb in combination.CascadeList)
                        {
                            toReturn += smBetId++ + ":" + GenerateSmResultForCombination(comb, betModifier);
                        }
                    }
                }
                else
                {
                    if (smBetResultInBonus == null)
                    {
                        smBetResultInBonus = smBetId + ":";
                    }
                    else
                    if (smBetResultInBonus.IndexOf("_") > -1)
                    {
                        smBetResultInBonus = smBetResultInBonus.Substring(0, smBetResultInBonus.IndexOf("_"));
                    }
                    smBetResultInBonus += GenerateSmResultForBonusCombination(combination, gameId, betModifier);
                    toReturn = smBetResultInBonus;
                }

                if (toReturn.Length > 4000)
                {
                    toReturn = toReturn.Substring(0, 4000);
                }
                //numoflines for the platform
                toReturn += "_" + numberOfLines;

                Logger.LogInfo("GetSMBetResult: " + toReturn);

                return toReturn;
            }
            catch (Exception e)
            {
                Logger.LogError(e, "GetSMBetResult error: ");
                return null;
            }
        }

        private static string GenerateSmResultForBonusCombination(ICombination combination, int gameId, long betModifier)
        {
            var toReturn = "";
            switch (gameId)
            {
                case (int)Games.CrystalsOfMagic:
                    switch (combination.AdditionalInformation)
                    {
                        case 1:
                            toReturn = "C#" + combination.AdditionalArray[14] + "#W#" + CreditHelper.ConvertInternalCredit2Money(combination.TotalWin * betModifier) + "#";
                            break;
                        case 2:
                            toReturn = "C#" + combination.AdditionalArray[4] + "#W#" + CreditHelper.ConvertInternalCredit2Money(combination.TotalWin * betModifier) + "#";
                            break;
                    }
                    break;
                case (int)Games.TemplarsQuest:
                    switch (combination.AdditionalInformation)
                    {
                        case 1:
                            var indexInArray = -1;
                            for (var i = 1; i < combination.AdditionalArray.Length; i++)
                            {
                                if (combination.AdditionalArray[i] == 2)
                                {
                                    indexInArray = i;
                                }
                            }
                            toReturn = "C#" + indexInArray + "#W#" + CreditHelper.ConvertInternalCredit2Money(combination.TotalWin * betModifier) + "#";
                            break;
                        case 2:
                            toReturn = "C#" + combination.AdditionalArray[1] % 3 + "#W#" + CreditHelper.ConvertInternalCredit2Money(combination.TotalWin * betModifier) + "#";
                            break;
                    }
                    break;
            }
            return toReturn;
        }

        private static string GenerateSmResultForCombination(ICombination combination, long betModifier)
        {
            var toReturn = "";
            for (var i = 0; i < combination.Matrix.GetLength(1); i++)
            {
                for (var j = 0; j < combination.Matrix.GetLength(0); j++)
                {
                    toReturn += (char)('A' + combination.Matrix[j, i]);
                    if (j != combination.Matrix.GetLength(0) - 1)
                    {
                        toReturn += ";";
                    }
                }
                toReturn += "#";
            }
            if (combination.TotalWin > 0)
            {
                for (var i = 0; i < combination.NumberOfWinningLines; i++)
                {
                    toReturn += "R#" + combination.LinesInformation[i].WinningElement + "#H" + combination.LinesInformation[i].WinningElement + "#";
                    for (var j = 0; j < combination.LinesInformation[i].WinningPosition.Length && combination.LinesInformation[i].WinningPosition[j] != 255; j++)
                    {
                        toReturn += combination.LinesInformation[i].WinningPosition[j] / combination.Matrix.GetLength(0);
                    }
                    //slanje dobitka
                    toReturn += "#MV#" + CreditHelper.ConvertInternalCredit2Money(combination.LinesInformation[i].Win * betModifier);
                    toReturn += "#MT#2#";
                }
            }
            return toReturn;
        }
    }
}