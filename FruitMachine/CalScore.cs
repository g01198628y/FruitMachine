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

            var itemGroupInfo = ClassifyItem(reelResult);
            var scoreBonus = GetScoreBonus(reelResult);

            foreach (var item in itemGroupInfo)
            {
                return CalculateScore(item, scoreBonus);
            }
            return 0;
        }

        private int GetScoreBonus(List<string> reelResult)
        {
            var wildRepeatNum = GetWildRepeatNum(reelResult);
            var itemGroupInfo = ClassifyItem(reelResult);

            foreach (var item in itemGroupInfo)
            {
                switch (item.Amount)
                {
                    case 3:
                        return 10;

                    case 2:
                        switch (wildRepeatNum)
                        {
                            case 2:
                                return 1;

                            case 1:
                                return 2;
                        }

                        break;
                }
            }
            return 1;
        }

        private int CalculateScore(Item item, int bonus)
        {
            return ItemScoringLookUp[item.Name] * bonus;
        }

        private int GetWildRepeatNum(List<string> reelResult)
        {
            return reelResult.FindAll(x => x == "Wild").Count();
        }

        private List<Item> ClassifyItem(List<string> reelResultList)
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