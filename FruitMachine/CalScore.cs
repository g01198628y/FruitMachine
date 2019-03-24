using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitMachine
{
    public class CalScore
    {
        public int ShowResult(List<string[]> reels, int[] spins)
        {
            var reelOneItem = reels[0][spins[0]];
            var reelTwoItem = reels[1][spins[1]];
            var reelThreeItem = reels[2][spins[2]];

            if (reelOneItem == "Wild" && reelTwoItem == "Wild" && reelThreeItem == "Wild")
            {
                return 100;
            }
            return 0;
        }
    }
}