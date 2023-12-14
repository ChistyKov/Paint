using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using Paint.model;

namespace Paint.Interface
{
    internal interface IShape 
    {
       void ShapeUpdeting(int LineThikness, InkCanvas Canvas, Brush LineColor, Point StartPoint, Point EndPoint, Brush FillColor);        
    }
    
    internal interface IUpdateFigure
    {
        void UpdateFiqure();
    }
}

