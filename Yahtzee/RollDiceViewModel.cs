using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee
{
    class RollDiceViewModel
    {
        readonly List<ViewDieModel> dice = new List<ViewDieModel> {
            new ViewDieModel(),
            new ViewDieModel(),
            new ViewDieModel(),
            new ViewDieModel(),
            new ViewDieModel()
        };

        public List<ViewDieModel> MyDice
        {
            get
            {
                return dice;
            }
        }

        void PerformRolling()
        {
            MyDice[0].Rolling();
            MyDice[1].Rolling();
            MyDice[2].Rolling();
            MyDice[3].Rolling();
            MyDice[4].Rolling();
        }
    }
}
