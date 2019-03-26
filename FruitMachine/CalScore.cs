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
            var classifiedItem = ClassifyItem(reelResult);

            var score = 0;
            foreach (var item in classifiedItem)
            {
                switch ((GroupInfo)item.Amount)
                {
                    case GroupInfo.ThreeOfSame:
                        score += GetScore(item.Name, Bonus.ThreeOfSame);
                        break;

                    case GroupInfo.TwoOfSame:
                        switch ((WildAmount)wildRepeatNum)
                        {
                            case WildAmount.TwoWild:
                                score += GetScore(item.Name, Bonus.TwoWildOneOther);
                                break;

                            case WildAmount.OneWild:
                                score += GetScore(item.Name, Bonus.TwoOfSameOneWild);
                                break;

                            case WildAmount.NoWild:
                                score += GetScore(item.Name, Bonus.TwoOfSameNoWild);
                                break;
                        }
                        break;
                }
                break;
            }
            return score;
        }

        private int GetScore(string itemName, Bonus bonus)
        {
            return ItemScoringLookUp[itemName] * (int)bonus;
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

        private enum WildAmount
        {
            NoWild,
            OneWild,
            TwoWild
        }
    }

    public class Item
    {
        public string Name { get; set; }
        public int Amount { get; set; }
    }
}