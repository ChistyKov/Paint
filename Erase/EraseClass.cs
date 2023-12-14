using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;
using System.Windows.Ink;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Paint.Erase
{
    abstract class EraseClass
    {
        private Shape CurrentEraseShape;

        public EraseClass(Shape CurrentShape)
        {
            CurrentEraseShape = CurrentShape;
        }
        public void CreateShape(InkCanvas inkCanvas)
        {
            var EraseColor = inkCanvas.Background;

        }
    }
}
