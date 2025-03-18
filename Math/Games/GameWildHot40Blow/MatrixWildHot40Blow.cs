using MathForGames.GameTurboHot40;
using System.Collections.Generic;

namespace GameWildHot40Blow
{
    public class MatrixWildHot40Blow : MatrixTurboHot40
    {
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
    }
}
