using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee
{
    class ViewDieModel
    {
        private readonly Cell<int> face = new Cell<int>(1);
        private static Random rnd = new Random();
        public Cell<int> FaceDie
        {
            get
            {
                return face;
            }
        }
        public void Rolling()
        {
            var value = rnd.Next(6) + 1;
            FaceDie.Value = value;
        }
    }

    class RollCommands
    {

    }
}
