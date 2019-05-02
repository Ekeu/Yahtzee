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
    class ViewDieModel
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
}
