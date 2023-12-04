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
            CurrentShape = new System.Windows.Shapes.Line();
        }

        public override void UpdateFiqure()
        {
            base.UpdateFiqure();

            ((System.Windows.Shapes.Line)CurrentShape).X1 = StartPoint.X;
            ((System.Windows.Shapes.Line)CurrentShape).Y1 = StartPoint.Y;
            ((System.Windows.Shapes.Line)CurrentShape).X2 = EndPoint.X;
            ((System.Windows.Shapes.Line)CurrentShape).Y2 = EndPoint.Y;

        }
    }
}
