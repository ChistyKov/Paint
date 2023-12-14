using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Paint.model;
using System.Xml.Linq;

namespace Paint.CursorPaint
{
    internal class CursorPaint1 : Oblick
    {

        PathFigure currentFigure { get; set; }
        
        public CursorPaint1()
        {
            currentFigure = new PathFigure();
        }



        public void EndFigure()
        {
            currentFigure = null;
        }
        public int Strokethik = 3;
        
        public void StartFigure(InkCanvas MyCanvas, Point start, Brush ColorLine)
        {
            currentFigure = new PathFigure() { StartPoint = start };
            var currentPath =
                new Path()
                {
                    Stroke = ColorLine,
                    StrokeThickness = Strokethik,
                    Data = new PathGeometry() { Figures = { currentFigure } }
                };
            MyCanvas.Children.Add(currentPath);
            
           
        }
        public void AddFigurePoint(Point point)
        {
            currentFigure.Segments.Add(new LineSegment(point, isStroked: true));
        }

    }
}
