using Paint.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Paint
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ChangeColor();
        }

        
     
        private Point StartPoint;

        private Point EndPoint;

        private Oblick Oblick;

        AllOblicks AllOblick;
        
        Brush AllBrush;

        // по сути выбор цвета тут
        private void ChangeColor()
        {
            AllBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("red"));

        }
        bool Drawning = true;
        bool MouseButtonClick = false;
        bool Dras = false;
        private void Window_MouseLeftbuttonDown(object sender,MouseButtonEventArgs e)
        {

            if (Drawning) 
            {
                Mouse.Capture(MyCanvas);
                IsDrawning = true;
                StartFigure(e.GetPosition(MyCanvas));
                Dras = true;
            }
            if (MouseButtonClick)
            {
                
                
                
                StartPoint = e.GetPosition(MyCanvas);

                if (Oblick == null)
                {
                    Oblick = OblickCreator.Creator(AllOblick);
                }
            }       
        }


        private void Window_MouseMove(object sender,MouseEventArgs e)
        {
            if (Drawning )
            {
                if(IsDrawning == true)
                AddFigurePoint(e.GetPosition(MyCanvas));
            }
            if (MouseButtonClick)
            {

                EndPoint = e.GetPosition(MyCanvas);
                if (Oblick != null)
                {
                    Oblick.ShapeUpdeting(2, MyCanvas, Brushes.Black, StartPoint, EndPoint, AllBrush);
                    Oblick.UpdateFiqure();
                }

            }
        }

        private void Window_MouseLeftButtonUp(object sender,MouseButtonEventArgs e)
        {

            if (Drawning && IsDrawning)
            {
                AddFigurePoint(e.GetPosition(MyCanvas));

                
                
                Mouse.Capture(null);
                EndFigure();
                
                IsDrawning = false;
            }
            if (MouseButtonClick)
            {
                Oblick = null;
                MouseButtonClick = false;
                Drawning = true;
            }
           
        }

        private void Cicle_click(object sender, RoutedEventArgs e)
        {
            MouseButtonClick = true;
            Drawning = false;
            AllOblick = AllOblicks.Circle;
        }

        private void Oval_click(object sender, RoutedEventArgs e)
        {
            MouseButtonClick = true;
            Drawning = false;
            AllOblick = AllOblicks.Oval;
        }
        private void Line_click(object sender, RoutedEventArgs e)
        {
            MouseButtonClick = true;
            Drawning = false;
            AllOblick = AllOblicks.Line;
        }
        private void Rectangle_click(object sender, RoutedEventArgs e)
        {
            MouseButtonClick = true;
            Drawning = false;
            AllOblick = AllOblicks.Rectangle;
        }

        #region Drawning
        void EndFigure()
        {
            currentFigure = null;
        }

        bool IsDrawning = false;
        
        PathFigure currentFigure;
        void StartFigure(Point start)
        {
            currentFigure = new PathFigure() { StartPoint = start };
            var currentPath =
                new Path()
                {
                    Stroke = Brushes.Black,
                    StrokeThickness = 3,
                    Data = new PathGeometry() { Figures = { currentFigure } }
                };
            MyCanvas.Children.Add(currentPath);
        }
        void AddFigurePoint(Point point)
        {
            currentFigure.Segments.Add(new LineSegment(point, isStroked: true));
        }


        #endregion






    }
}
