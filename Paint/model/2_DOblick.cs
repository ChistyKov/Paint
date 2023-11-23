using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.model
{
    internal class _2_DOblick : Oblick
    {
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
