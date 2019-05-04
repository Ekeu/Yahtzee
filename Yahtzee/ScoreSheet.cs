using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee
{
    public class ScoreSheet
    {
        private readonly IList<ScoreLine> lines;
        private readonly ICell<int> total;
        private static IEnumerable<ScoringCategory> categories
        {
            get
            {
                foreach(var e in Enumerable.Range(1, 6))
                {
                    yield return new NumberCategory(e);
                }

                yield return new ThreeOfAKindCategory();
                yield return new FourOfAKindCategory();
                yield return new FullHouseCategory();
                yield return new SmallStraightCategory();
                yield return new LargeStraightCategory();
                yield return new YahtzeeCategory();
                yield return new ChanceCategory();
            }
        }

        public ScoreSheet()
        {
            lines = (from category in categories select new ScoreLine(category)).ToList().AsReadOnly();
            total = Derived.Create(from line in lines select line.Score, ns => ns.Sum());
        }
        public IList<ScoreLine> ScoreLines
        {
            get
            {
                return lines;
            }
        }

        public ICell<int> Total { get { return total; } }
    }

    public class ScoreLine
    {
        private readonly ScoringCategory category;
        private Cell<int> score;
        private Cell<bool> assigned;

        public ScoreLine(ScoringCategory category)
        {
            this.category = category;
            this.score = new Cell<int>(0);
            this.assigned = new Cell<bool>(false);
        }

        public void Assign(RollingDice rolling)
        {
            if (assigned.Value)
            {
                throw new ArgumentException();
            }
            else
            {
                score.Value = category.Score(rolling);
                assigned.Value = true;
            }
        }

        public ScoringCategory Category
        {
            get { return category; }
        }
        public ICell<int> Score
        {
            get {return score; }
        }

        public ICell<bool> Assigned
        {
            get { return assigned; }
        }
    }
}
