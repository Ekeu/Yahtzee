using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Yahtzee
{
    public class ViewDieModel
    {
        private readonly Cell<int> face = new Cell<int>(1);
        private static Random rnd = new Random();
        private Cell<bool> keep = new Cell<bool>(false);
        public Cell<int> FaceDie
        {
            get
            {
                return face;
            }
        }

        public Cell<bool> Keep
        {
            get => keep;
        }
        public void Rolling()
        {
            if(Keep.Value == false)
            {
                var value = rnd.Next(6) + 1;
                FaceDie.Value = value;
            }
        }
    }

    public class ScoreLineViewModel
    {
        private ScoreLine scoreLine;
        private readonly RollDiceViewModel rollingDice;
        public String CategoryName
        {
            get { return scoreLine.Category.Name; }
        }
        public int ScoreValue
        {
            get { return DiceRollScore.Value; }
        }
        ICell<int> AssignedScore
        {
            get
            {
                return scoreLine.Score;
            }
        }
        ICell<bool> IsAssigned
        {
            get
            {
                return scoreLine.Assigned;
            }
        }

        public ICell<int> DiceRollScore { get; private set; }
        public ScoreLineViewModel(ScoreLine scoreLine, RollDiceViewModel rollingDice)
        {
            this.scoreLine = scoreLine;
            this.rollingDice = rollingDice;
            DiceRollScore = Derived.Create(rollingDice.DiceRoll, scoreLine.Category.Score);
        }

        public ICommand Assign { get; private set; }

        private class AssignCommand : ICommand
        {
            private readonly ScoreLineViewModel scoreLineViewModel;

            public AssignCommand(ScoreLineViewModel scoreLine)
            {
                this.scoreLineViewModel = scoreLine;

                scoreLineViewModel.IsAssigned.PropertyChanged += (sender, args) =>
                {
                    if (CanExecuteChanged != null)
                    {
                        CanExecuteChanged(this, new EventArgs());
                    }
                };
            }

            public bool CanExecute(object parameter)
            {
                return !scoreLineViewModel.IsAssigned.Value;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                scoreLineViewModel.scoreLine.Assign(scoreLineViewModel.rollingDice.DiceRoll.Value);
                scoreLineViewModel.rollingDice.ResetGame();
            }
        }
    }

    public class ScoreSheetViewModel
    {
        private ScoreSheet scoreSheet;
        private readonly IList<ScoreLineViewModel> scoreLines;
        private readonly RollDiceViewModel rollingDice;

        public IList<ScoreLineViewModel> ScoreLines
        {
            get { return scoreLines; }
        }

        public ScoreSheetViewModel(ScoreSheet scoreSheet, RollDiceViewModel rollingDice)
        {
            this.scoreSheet = scoreSheet;
            scoreLines = (from line in scoreSheet.ScoreLines select new ScoreLineViewModel(line,rollingDice)).ToList().AsReadOnly();
            this.rollingDice = rollingDice;
        }
    }
}
