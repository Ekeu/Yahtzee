using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Yahtzee
{
    /// <summary>
    /// Interaction logic for test.xaml
    /// </summary>
    public partial class ControlDie : UserControl
    {
        public ControlDie()
        {
            InitializeComponent();
            this.SizeChanged += (obj, args) => FaceDrawing();
        }
        public int DieValue
        {
            get { return (int)GetValue(DieValueProperty); }
            set { SetValue(DieValueProperty, value); }
        }
        private void OnDieValueChange()
        {
            FaceDrawing();
        }
        private void FaceDrawing()
        {
            switch (DieValue)
            {
                case 1:
                    FaceOne();
                    break;
                case 2:
                    FaceTwo();
                    break;
                case 3:
                    FaceThree();
                    break;
                case 4:
                    FaceFour();
                    break;
                case 5:
                    FaceFive();
                    break;
                case 6:
                    FaceSix();
                    break;
                default:
                    FaceDrawError();
                    break;
            }
        }
        private void FaceDrawError()
        {
            canvas.Children.Clear();
        }
        private void FaceOne()
        {
            canvas.Children.Clear();
            DrawEllipse(.5, .5);
         }
        private void FaceTwo()
        {
            canvas.Children.Clear();
            DrawEllipse(.25, .25);
            DrawEllipse(.75, .75);
        }
        private void FaceThree()
        {
            canvas.Children.Clear();
            DrawEllipse(.25, .75);
            DrawEllipse(.5, .5);
            DrawEllipse(.75, .25);
        }
        private void FaceFour()
        {
            canvas.Children.Clear();
            DrawEllipse(.25, .25);
            DrawEllipse(.25, .75);
            DrawEllipse(.75, .25);
            DrawEllipse(.75, .75);
        }
        private void FaceFive()
        {
            canvas.Children.Clear();
            DrawEllipse(.25, .25);
            DrawEllipse(.25, .75);
            DrawEllipse(.5, .5);
            DrawEllipse(.75, .25);
            DrawEllipse(.75, .75);
        }
        private void FaceSix()
        {
            canvas.Children.Clear();
            DrawEllipse(.25, .25);
            DrawEllipse(.25, .5);
            DrawEllipse(.25, .75);
            DrawEllipse(.75, .25);
            DrawEllipse(.75, .5);
            DrawEllipse(.75, .75);
        }
        private void DrawEllipse(double v1, double v2)
        {
            var dieEllipse = new Ellipse();
            dieEllipse.Fill = Brushes.Black;
            dieEllipse.Width = 5;
            dieEllipse.Height = 5;

            Canvas.SetLeft(dieEllipse, v1 * Width - dieEllipse.Width / 2);
            Canvas.SetTop(dieEllipse, v2 * Height - dieEllipse.Height / 2);
            canvas.Children.Add(dieEllipse);
        }

        public static readonly DependencyProperty DieValueProperty = DependencyProperty.Register("DieValue", typeof(int), typeof(ControlDie), new PropertyMetadata(1, DieValueChanged));
        public static void DieValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var controlDie = (ControlDie)obj;
            controlDie.OnDieValueChange();
        }
    }
}
