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
            var reelResult = reels.Select((r, i) => r[spins[4]]).ToList();

            var repeatItem = GetRepeatItem(reelResult);
            var scoreBonus = GetScoreBonus(reelResult);
            return CalculateScore(repeatItem, scoreBonus);
        }

        private string GetRepeatItem(List<string> reelResult)
        {
            return hasRepeatItem(reelResult) ? reelResult.GroupBy(x => x).SelectMany(grp => grp.Skip(1)).ToList()[0] : "NoRepeatItem";
        }

        private bool hasRepeatItem(List<string> reelResult)
        {
            return reelResult.Count != reelResult.Distinct().ToList().Count;
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
                        return 1;
                }
            }
            return 0;
        }

        private int CalculateScore(string repeatItem, int bonus)
        {
            return ItemScoringLookUp[repeatItem] * bonus;
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
            {"Wild",10 },{"Star",9},{"Bell",8},{"Shell",7},{"Seven",6},{"Cherry",5},{"Bar",4},{"King",3},{"Queen",2},{"Jack",1},{"NoRepeatItem",0}
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