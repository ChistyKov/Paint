using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Shapes;
using Paint.Interface;

namespace Paint.model
{
    public abstract class Oblick:IShape, IUpdateFigure
    {
        public Canvas CurrentCanvas { get; set; }
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
        public Brush FillColor { get; set; }

        public Brush LineColor { get; set; }

        public double LineThikness { get; set; }

        public Shape obliki { get; set; }
        

        public virtual void ShapeUpdeting(double LineThikness, Canvas Canvas, Brush LineColor, Point StartPoint, Point EndPoint, Brush FillColor)
        {
            CurrentCanvas = Canvas;
            this.LineThikness = LineThikness;
            this.StartPoint = StartPoint;
            this.LineColor = LineColor;
            this.FillColor = FillColor;
            this.EndPoint = EndPoint;
        }
        public virtual void UpdateFiqure()
        {
            if(!CurrentCanvas.Children.Contains(obliki)) CurrentCanvas.Children.Add(obliki);

            obliki.Stroke = LineColor;
            obliki.StrokeThickness = LineThikness;
            obliki.Fill = FillColor;
        }
    }

}
