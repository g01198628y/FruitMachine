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
            var reelResult = reels.Select((r, i) => r[spins[i]]).ToList();
            return CalculateScore(reelResult);
        }

        private int CalculateScore(List<string> reelResult)
        {
            var wildRepeatNum = GetWildRepeatNum(reelResult);
            var repeatItemInfo = GetRepeatItemInfo(reelResult);

            if (repeatItemInfo != null)
            {
                switch (repeatItemInfo.Amount)
                {
                    case 3:
                        return GetScore(repeatItemInfo.Name, Bonus.ThreeOfSame);

                    case 2:
                        return _itemScoringLookUp[repeatItemInfo.Name] * _twoOfSameBonusLookUp[wildRepeatNum];
                }
            }
            return 0;
        }

        private Item GetRepeatItemInfo(List<string> reelResult)
        {
            if (hasRepeatItem(reelResult))
            {
                return reelResult.GroupBy(x => x)
                               .Where(x => x.Count() > 1)
                               .OrderBy(x => x.Key)
                               .Select(x => new Item { Name = x.Key, Amount = x.Count() }).First();
            }
            return null;
        }

        private bool hasRepeatItem(List<string> reelResult)
        {
            return reelResult.Count != reelResult.Distinct().ToList().Count;
        }

        private int GetScore(string itemName, Bonus bonus)
        {
            return _itemScoringLookUp[itemName] * (int)bonus;
        }

        private int GetWildRepeatNum(List<string> reelResult)
        {
            return reelResult.FindAll(x => x == "Wild").Count();
        }

        private readonly Dictionary<string, int> _itemScoringLookUp = new Dictionary<string, int>()
        {
            { "Wild",10 },
            { "Star",9},
            { "Bell",8},
            { "Shell",7},
            { "Seven",6},
            { "Cherry",5},
            { "Bar",4},
            { "King",3},
            { "Queen",2},
            { "Jack",1}
        };

        private readonly Dictionary<int, int> _twoOfSameBonusLookUp = new Dictionary<int, int>()
        {
            {0,1},{1,2},{2,1}
        };

        private enum GroupInfo
        {
            ThreeOfSame = 3,
            TwoOfSame = 2,
        }

        private enum Bonus
        {
            TwoWildOneOther = 1,
            TwoOfSameNoWild = 1,
            TwoOfSameOneWild = 2,
            ThreeOfSame = 10
        }
    }

    public class Item
    {
        public string Name { get; set; }
        public int Amount { get; set; }
    }
}