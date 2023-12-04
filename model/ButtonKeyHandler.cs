using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Paint;

namespace Paint.model
{
    public class ButtonKeyHandler : Canvas
    {
        
        private Canvas TempCanvas = new Canvas();

        public virtual void canvas_KeyDown_Z(object sender, KeyEventArgs e, Canvas MyCanvas, UIElement[] TempCanvass)
        {
            if (MyCanvas.Children.Count > 0)
            {
                MyCanvas.Children.RemoveAt(MyCanvas.Children.Count - 1);
                TempCanvas.Children.Add(TempCanvass[TempCanvass.Length - 1]);
                MyCanvas.UpdateLayout();
            }
        }

        public virtual void canvas_KeyDown_Y(object sender, KeyEventArgs e, Canvas MyCanvas, UIElement[] TempCanvass)
        {
            if (TempCanvas.Children.Count > 0 && )
            {
                var TempCanvasss = TempCanvas.Children[TempCanvas.Children.Count - 1];
                TempCanvas.Children.RemoveAt(TempCanvas.Children.Count - 1);
                MyCanvas.Children.Add(TempCanvasss);
                MyCanvas.UpdateLayout();
            }
        }

    }
}
