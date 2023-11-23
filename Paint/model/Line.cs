using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;
using System.Threading.Tasks;

namespace Paint.model
{
    internal class Line : Oblick
    {
        public Line() 
        {
            obliki = new System.Windows.Shapes.Line();
        }

        public override void UpdateFiqure()
        {
            base.UpdateFiqure();

            ((System.Windows.Shapes.Line)obliki).X1 = StartPoint.X;
            ((System.Windows.Shapes.Line)obliki).Y1 = StartPoint.Y;
            ((System.Windows.Shapes.Line)obliki).X2 = EndPoint.X;
            ((System.Windows.Shapes.Line)obliki).Y2 = EndPoint.Y;

        }
    }
}
