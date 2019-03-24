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

            var reelResultList = new List<string> { firstReelResult, secondReelResult, thirdReelResult };
            var sameItemInfo = GroupSameItem(reelResultList);

            foreach (var item in sameItemInfo)
            {
                switch (item.Amount)
                {
                    case 2 when item.Name == "Wild":
                        return 10;

                    case 3:
                        return ItemScoringLookUp[item.Name] * (int)ScoreBonus.AllItemsAreSame;

                    default:
                        return ItemScoringLookUp[item.Name] * (int)ScoreBonus.TwoSameItemPlusWild;
                }
            }
            return 0;
        }

        private List<Item> GroupSameItem(List<string> reelResultList)
        {
            //return reelResultList.GroupBy(x => x).All(g => g.Count() > 1).ToString();

            return reelResultList.GroupBy(x => x)
                .Where(y => y.Count() > 1)
                .Select(x => new Item { Name = x.Key, Amount = x.Count() }).ToList();
        }

        private readonly Dictionary<string, int> ItemScoringLookUp = new Dictionary<string, int>()
        {
            {"Wild",10 },{"Star",9},{"Bell",8},{"Shell",7},{"Seven",6},{"Cherry",5},{"Bar",4},{"King",3},{"Queen",2},{"Jack",1}
        };

        private enum ScoreBonus
        {
            AllItemsAreSame = 10,
            TwoSameItemPlusWild = 2
        }
    }

    public class Item
    {
        public string Name { get; set; }
        public int Amount { get; set; }
    }
}