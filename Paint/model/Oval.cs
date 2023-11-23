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
            obliki = new Ellipse();
        }

        public override void UpdateFiqure()
        {
            base.UpdateFiqure();
            obliki.Width = Math.Abs(StartPoint.X - EndPoint.X);
            obliki.Height = Math.Abs(StartPoint.Y - EndPoint.Y);
            Canvas.SetTop(obliki, Math.Min(StartPoint.Y, EndPoint.Y));
            Canvas.SetLeft(obliki, Math.Min(StartPoint.X, EndPoint.X));
        }
    }
}
