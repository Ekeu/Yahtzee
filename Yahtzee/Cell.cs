using System;
using System.ComponentModel;

namespace Yahtzee
{
    public interface ICell<T> : INotifyPropertyChanged
    {
        T Value { get; set; }
    }
    public class Cell<T> : ICell<T>
    {
        private T value;

        public event PropertyChangedEventHandler PropertyChanged;

        public Cell(T initialValue = default(T))
        {
            this.value = initialValue;
        }

        public virtual T Value
        {
            get
            {
                return value;
            }
            
            set
            {
                if(!AreEqual(this.value, value))
                {
                    this.value = value;
                    NotifyObservers();
                }
            }
        }

        private static bool AreEqual(T val1, T val2)
        {
            if(val1 == null)
            {
                return (val2 == null);
            }
            else
            {
                return val1.Equals(val2);
            }
        }

        public void NotifyObservers()
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Value"));
            }
        }
    }
}