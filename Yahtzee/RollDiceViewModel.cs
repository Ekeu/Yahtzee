using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Yahtzee
{
    class RollDiceViewModel
    {
        private static RollCommand rolling;

        readonly static List<ViewDieModel> dice = new List<ViewDieModel> {
            new ViewDieModel(),
            new ViewDieModel(),
            new ViewDieModel(),
            new ViewDieModel(),
            new ViewDieModel()
        };

        public  RollDiceViewModel()
        {
            RollsLeft = new Cell<int>(3);
            CanRoll = new Cell<bool>();
            CanRoll = Derived.Create(RollsLeft, n => n > 0);
            rolling = new RollCommand(this);
            //DiceRoll = Derived.Create(dice.Select(die => die.FaceDie), ns => new RollingDice(ns.ToArray()));
        }

        public ICommand Roll
        {
            get
            {
                return rolling;
            }
        }

        public Cell<int> RollsLeft
        {
            get; set;
        }

        public ICell<bool> CanRoll
        {
            get; set;
        }

        public ICell<RollingDice> DiceRoll { get; private set; }

        public List<ViewDieModel> MyDice
        {
            get
            {
                return dice;
            }
        }

        public void PerformRolling()
        {
            MyDice[0].Rolling();
            MyDice[1].Rolling();
            MyDice[2].Rolling();
            MyDice[3].Rolling();
            MyDice[4].Rolling();
            --RollsLeft.Value;
        }

    }
}
