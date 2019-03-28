using System.Collections.Generic;
using System.Linq;

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
            return repeatItemInfo == null ? 0 : GetScore(repeatItemInfo, wildRepeatNum);
        }

        private int GetScore(Item repeatItemInfo, int wildRepeatNum)
        {
            return _itemScoringLookUp[repeatItemInfo.Name] * _repeatItemBonus[repeatItemInfo.Amount] * _wildBonusLookUp[wildRepeatNum];
        }

        private Item GetRepeatItemInfo(List<string> reelResult)
        {
            if (HasRepeatItem(reelResult))
            {
                return reelResult.GroupBy(x => x)
                               .Where(x => x.Count() > 1)
                               .OrderByDescending(x => x.Key)
                               .Select(x => new Item { Name = x.Key, Amount = x.Count() }).First();
            }
            return null;
        }

        private bool HasRepeatItem(List<string> reelResult)
        {
            return reelResult.Count != reelResult.Distinct().ToList().Count;
        }

        private int GetWildRepeatNum(List<string> reelResult)
        {
            return reelResult.FindAll(x => x == "Wild").Count();
        }

        private readonly Dictionary<string, int> _itemScoringLookUp = new Dictionary<string, int>()
        {
            { "Wild", 10 },
            { "Star", 9},
            { "Bell", 8},
            { "Shell", 7},
            { "Seven", 6},
            { "Cherry", 5},
            { "Bar", 4},
            { "King", 3},
            { "Queen", 2},
            { "Jack", 1}
        };

        private readonly Dictionary<int, int> _wildBonusLookUp = new Dictionary<int, int>()
        {
            {0,1},{1,2},{2,1},{3,1}
        };

        private readonly Dictionary<int, int> _repeatItemBonus = new Dictionary<int, int>()
        {
            {3,10},{2,1}
        };
    }

    public class Item
    {
        public string Name { get; set; }
        public int Amount { get; set; }
    }
}