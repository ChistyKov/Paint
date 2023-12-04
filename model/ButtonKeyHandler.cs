using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Paint;

namespace Paint.model
{
    public class ButtonKeyHandler : Canvas
    {
        
        public Canvas TempCanvas = new Canvas();

        public virtual void canvas_KeyDown_Z(object sender, KeyEventArgs e, Canvas MyCanvas, UIElement[] CopyMyCanvas)
        {
            if (MyCanvas.Children.Count > 0)
            {
                MyCanvas.Children.RemoveAt(MyCanvas.Children.Count - 1);
                TempCanvas.Children.Add(CopyMyCanvas[CopyMyCanvas.Length - 1]);
                MyCanvas.UpdateLayout();
            }
        }

        public virtual void canvas_KeyDown_Y(object sender, KeyEventArgs e, Canvas MyCanvas)
        {
            if (TempCanvas.Children.Count > 0)
            {
                var TemporaryСanvassElement = TempCanvas.Children[TempCanvas.Children.Count - 1];
                TempCanvas.Children.RemoveAt(TempCanvas.Children.Count - 1);
                MyCanvas.Children.Add(TemporaryСanvassElement);
                MyCanvas.UpdateLayout();
            }
        }

       

    }
}
