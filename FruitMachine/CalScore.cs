using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

            var reelResultList = new List<string>() { firstReelResult, secondReelResult, thirdReelResult };
            var sameItemList = GetSameItemList(reelResultList);

            if (firstReelResult == secondReelResult && secondReelResult == thirdReelResult)
            {
                return ItemScoreLookUp[firstReelResult] * (int)ScoreBonus.AllItemsAreSame;
            }
            else if (reelResultList.Contains("Wild") && sameItemList.Any())
            {
                return ItemScoreLookUp[sameItemList[0]] * (int)ScoreBonus.TwoSameItemPlusWild;
            }

            return 0;
        }

        private List<string> GetSameItemList(List<string> reelResultList)
        {
            return reelResultList.GroupBy(x => x).Where(i => i.Count() > 1).Select(i => i.ElementAt(0)).ToList();
        }

        private readonly Dictionary<string, int> ItemScoreLookUp = new Dictionary<string, int>()
        {
            {"Wild",10 },{"Star",9},{"Bell",8},{"Shell",7},{"Seven",6},{"Cherry",5},{"Bar",4},{"King",3},{"Queen",2},{"Jack",1}
        };

        private enum ScoreBonus
        {
            AllItemsAreSame = 10,
            TwoSameItemPlusWild = 2
        }
    }
}