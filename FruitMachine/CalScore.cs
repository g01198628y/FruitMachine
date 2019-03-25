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
            var reelResult = new List<string>();
            for (var i = 0; i < reels.Count(); i++)
            {
                reelResult.Add(reels[i][spins[i]]);
            }

            var repeatItemInfo = GetRepeatItemInfo(reelResult);
            foreach (var item in repeatItemInfo)
            {
                switch (item.Amount)
                {
                    case 3:
                        return ItemScoringLookUp[item.Name] * (int)ScoreBonus.AllItemsAreSame;

                    case 2 when item.Name == "Wild":
                        return ItemScoringLookUp[item.Name] * (int)ScoreBonus.TwoWildOneOther;

                    case 2 when reelResult.Contains("Wild"):
                        return ItemScoringLookUp[item.Name] * (int)ScoreBonus.TwoRepeatItemPlusWild;

                    case 2:
                        return ItemScoringLookUp[item.Name];
                }
            }
            return 0;
        }

        private List<Item> GetRepeatItemInfo(List<string> reelResultList)
        {
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
            TwoWildOneOther = 1,
            TwoRepeatItemPlusWild = 2,
            AllItemsAreSame = 10,
        }
    }

    public class Item
    {
        public string Name { get; set; }
        public int Amount { get; set; }
    }
}