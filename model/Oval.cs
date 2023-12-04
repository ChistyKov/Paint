using Paint.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Shapes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Paint.model
{
    public class Oval : Oblick
    {
        public Oval() 
        {
            CurrentShape = new Ellipse();
        }

        public override void UpdateFiqure()
        {
            base.UpdateFiqure();
            CurrentShape.Width = Math.Abs(StartPoint.X - EndPoint.X);
            CurrentShape.Height = Math.Abs(StartPoint.Y - EndPoint.Y);
            Canvas.SetTop(CurrentShape, Math.Min(StartPoint.Y, EndPoint.Y));
            Canvas.SetLeft(CurrentShape, Math.Min(StartPoint.X, EndPoint.X));
        }
    }
}
