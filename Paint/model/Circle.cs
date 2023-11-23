using Paint.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Paint.model
{
    public class Circle : Oval
    {
        public Circle() 
        {
            obliki = new Ellipse();
        }
        public override void UpdateFiqure()
        {
            base.UpdateFiqure();          
            if (obliki.Width>obliki.Height)
                obliki.Width = obliki.Height;
            else
                obliki.Height = obliki.Width;
        }
    }
}
