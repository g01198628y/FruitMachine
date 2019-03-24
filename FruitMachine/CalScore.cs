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
            var firstReelResult = reels[0][spins[0]];
            var secondReelResult = reels[1][spins[1]];
            var thirdReelResult = reels[2][spins[2]];

            if (firstReelResult == secondReelResult && secondReelResult == thirdReelResult)
            {
                return AllResultAreSameLookUp[firstReelResult];
            }
            return 0;
        }

        private readonly Dictionary<string, int> AllResultAreSameLookUp = new Dictionary<string, int>()
        {
            {"Wild",100 },{"Star",90},{"Bell",80},{"Shell",70},{"Seven",60},{"Cherry",50},{"Bar",40},{"King",30},{"Queen",20},{"Jack",10}
        };
    }
}