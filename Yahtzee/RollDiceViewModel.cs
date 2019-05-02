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

        private readonly  IList<ViewDieModel> dice = new List<ViewDieModel> {
            new ViewDieModel(),
            new ViewDieModel(),
            new ViewDieModel(),
            new ViewDieModel(),
            new ViewDieModel()
        };

        public  RollDiceViewModel()
        {
            Initialize();
            //dice = Enumerable.Range(1, 5).Select(_ => new ViewDieModel()).ToList().AsReadOnly();
            RollsLeft = new Cell<int>(3);
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

        public IList<ViewDieModel> MyDice
        {
            get
            {
                return dice;
            }
        }

        private void PerformRolling()
        {
            MyDice[0].Rolling();
            MyDice[1].Rolling();
            MyDice[2].Rolling();
            MyDice[3].Rolling();
            MyDice[4].Rolling();
            --RollsLeft.Value;
        }

        public void ResetGame()
        {
            UnselectAllDice();
            RollDice();
            this.RollsLeft.Value = 2;
        }

        private void RollDice()
        {
            foreach (var die in dice)
            {
                die.Rolling();
            }
        }

        private void Initialize()
        {
            foreach (var die in dice)
            {
                while((die.FaceDie.Value) == 1)
                {
                    die.Rolling();
                }
            }
        }

        private void UnselectAllDice()
        {
            foreach(var die in dice)
            {
                die.Keep.Value = false;
            }
        }

        private class RollCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;
            private readonly RollDiceViewModel viewModel;

            public RollCommand(RollDiceViewModel viewModel)
            {
                this.viewModel = viewModel;
                viewModel.CanRoll.PropertyChanged += (sender, args) =>
                {
                    if (CanExecuteChanged != null)
                    {
                        CanExecuteChanged(this, new EventArgs());
                    }
                };
            }

            public bool CanExecute(object parameter)
            {
                return viewModel.CanRoll.Value;
            }

            public void Execute(object parameter)
            {
                viewModel.PerformRolling();
            }

        }
    }

}
