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

            CurrentShape.Width = Math.Abs(StartPoint.X - EndPoint.X);
            CurrentShape.Height = Math.Abs(StartPoint.Y - EndPoint.Y);
            Canvas.SetTop(CurrentShape, Math.Min(StartPoint.Y, EndPoint.Y));
            Canvas.SetLeft(CurrentShape, Math.Min(StartPoint.X, EndPoint.X));

        }
    }
}
