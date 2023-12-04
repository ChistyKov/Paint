using Paint.model;
using Paint.CursorPaint;
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

        CursorPaint1 CursorPaint = new CursorPaint1();
        
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
      
        bool Draw;
        bool button;
        
        private void Window_MouseLeftbuttonDown(object sender,MouseButtonEventArgs e)
        {
            if (Draw && (sender is Canvas))
            {                
                Mouse.Capture(MyCanvas);
                IsDrawning = true;
                CursorPaint.StartFigure(MyCanvas, e.GetPosition(MyCanvas));              
            }
            if (button)
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

            
            if (Draw)
            {
                if (IsDrawning == true && (sender is Canvas))
                {
                    CursorPaint.AddFigurePoint(e.GetPosition(MyCanvas));
                }
                

            }
            if (button)
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


            if (Draw && (sender is Canvas))
            {
                CursorPaint.AddFigurePoint(e.GetPosition(MyCanvas));
                Mouse.Capture(null);
                CursorPaint.EndFigure();

                IsDrawning = false;
            }
            if (button)
            {
                Oblick = null;
  
            }
          
        }

        private void Paint_click(object sender, RoutedEventArgs e)
        {

            AllOblick = AllOblicks.Paint;
            Draw = true;
            button = false;
        }

        private void Cicle_click(object sender, RoutedEventArgs e)
        {

            AllOblick = AllOblicks.Circle;
            Draw = false;
            button = true;
        }

        private void Oval_click(object sender, RoutedEventArgs e)
        {
  
            AllOblick = AllOblicks.Oval;
            Draw = false;
            button = true;
        }
        private void Line_click(object sender, RoutedEventArgs e)
        {   
            AllOblick = AllOblicks.Line;
            Draw = false;
            button = true;
        }
        private void Polygon_click(object sender, RoutedEventArgs e)
        {   
            AllOblick = AllOblicks.Pylygon;
            Draw = false;
            button = true;
        }
        private void Rectangle_click(object sender, RoutedEventArgs e)
        {      
            AllOblick = AllOblicks.Rectangle;
            Draw = false;
            button = true;
        }

        #region Drawning
        

        bool IsDrawning = false;
        
       

        #endregion

    }
}
