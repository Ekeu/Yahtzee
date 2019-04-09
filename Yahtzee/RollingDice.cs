using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee
{
    class RollingDice
    {
        private readonly int[] dice;
        
        public RollingDice(params int[] dice)
        {
            if(dice.Length != 5)
            {
                throw new ArgumentException();
            }
            else
            {
                this.dice = dice.ToArray();
            }
        }

        public int this[int index]
        {
            get
            {
                return dice[index];
            }
        }
        public IList<int> generateList()
        {
            return new List<int>(dice);
        }
        public ISet<int> generateSet()
        {
            return new HashSet<int>(dice);
        }
        /*public FrequencyTable<int> AsFrequencyTable()
        {
            return new FrequencyTable<int>(dice);
        }*/

    }
}
