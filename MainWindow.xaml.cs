using Paint.model;
using Paint.CursorPaint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Ink;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using Paint.ChangeColor;
using Paint.CanvasSize;
using System.IO;
using System.Text.RegularExpressions;


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
        }

        #region поля 

        CursorPaint1 CursorPaint = new CursorPaint1();

        private Point StartPoint;

        private Point EndPoint;

        private Oblick Oblick;

        AllOblicks AllOblick;

        bool Draw;

        bool button;

        public int StrokeThick;

        Brush CurrentColor = ColorLines;
        Brush ColorFilling;
       

        //default color
        static Brush  ColorLines = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));

        #endregion

        private void ColorButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            // Создание и отображение диалогового окна выбора цвета
            
            if(clickedButton.Name == "ColorFillingButton")
            {
                ColorFilling= ChangeColorClass.ChangeColor(sender,e);
            }
            if (clickedButton.Name == "ColorLinesButton")
            {
                ColorLines=ChangeColorClass.ChangeColor(sender, e);
            }
        }



      

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            SaveCanvas.save(MyCanvas);
        }




        private ButtonKeyHandler BKH = new ButtonKeyHandler();

        public void canvas_KeyDown(object sender, KeyEventArgs e)
        {
            var TempCanvass = MyCanvas.Children.Cast<UIElement>().ToArray();

            if (e.Key == Key.Z && e.KeyboardDevice.Modifiers == ModifierKeys.Control)
            {
                BKH.canvas_KeyDown_Z(sender, e, MyCanvas, TempCanvass);
            }
            if (e.Key == Key.Y && e.KeyboardDevice.Modifiers == ModifierKeys.Control)
            {
                BKH.canvas_KeyDown_Y(sender, e, MyCanvas);

            }
            if (e.Key == Key.Escape)
            {
                Keyboard.ClearFocus();
                Draw = false;
                button = false;
            }

        }


       
      

      



        private void Window_MouseLeftbuttonDown(object sender, MouseButtonEventArgs e)
        {
            if (Draw && (sender is InkCanvas))
            {
                Mouse.Capture(MyCanvas);
                IsDrawning = true;
                CursorPaint.StartFigure(MyCanvas, e.GetPosition(MyCanvas), ColorLines);
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

       


        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (Draw)
            {
                if (IsDrawning == true && (sender is InkCanvas))
                {
                    CursorPaint.AddFigurePoint(e.GetPosition(MyCanvas));
                    //Очистка временного канваса
                    BKH.TempCanvas.Children.Clear();
                }


            }

            if (button)
            {

                EndPoint = e.GetPosition(MyCanvas);
                if (Oblick != null)
                {
                    Oblick.ShapeUpdeting(StrokeThick, MyCanvas, ColorLines, StartPoint, EndPoint, ColorFilling);
                    Oblick.UpdateFiqure();
                }
                BKH.TempCanvas.Children.Clear();
            }

        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            if (Draw && (sender is InkCanvas))
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
        #region button
        private void Cursor_click(object sender, RoutedEventArgs e)
        {
            Draw = false;
            button = false;
            MyCanvas.EditingMode = InkCanvasEditingMode.None;
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
        }
        private void Rectangle_click(object sender, RoutedEventArgs e)
        {
            AllOblick = AllOblicks.Rectangle;
            Draw = false;
            button = true;
        }
        #endregion



        

        private void Mode_Checked(object sender,RoutedEventArgs e)
        {
           
            var mode = sender as RadioButton;
            string modeName = mode.Name;

            if (modeName.Equals("CursorB"))
            {
                MyCanvas.EditingMode = InkCanvasEditingMode.None;
                Draw = false;
                button = false;
            }
            if (modeName.Equals("PaintB"))
            {
                //MyCanvas.EditingMode = InkCanvasEditingMode.Ink  ;
                AllOblick = AllOblicks.Paint;
                Draw = true;
                button = false;
                ColorLines = CurrentColor;
            }
            if (modeName.Equals("EaraseB"))
            { 
                Draw = true;
                button = false;
                AllOblick = AllOblicks.Paint;
                ColorLines = new SolidColorBrush((Color)ColorConverter.ConvertFromString("white"));
            }
            if (modeName.Equals("RectangleB"))
            {
                MyCanvas.EditingMode = InkCanvasEditingMode.None;
                AllOblick = AllOblicks.Rectangle;
                Draw = false;
                button = true;
                ColorLines = CurrentColor;
            }
            if (modeName.Equals("CircleB"))
            {
                MyCanvas.EditingMode = InkCanvasEditingMode.None;
                AllOblick = AllOblicks.Circle;
                Draw = false;
                button = true;
                ColorLines = CurrentColor;
            }
            if (modeName.Equals("OvalB"))
            {
                MyCanvas.EditingMode = InkCanvasEditingMode.None;
                AllOblick = AllOblicks.Oval;
                Draw = false;
                button = true;
                ColorLines = CurrentColor;
            }
            if (modeName.Equals("BackB"))
            {
                MyCanvas.Children.Clear();
            }
            if (modeName.Equals("ColorFillingB"))
            {

            }
            
        }

        //толщина кисти
        private void Slider_value_Stroke(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var slider = sender as Slider;
           
            CursorPaint = new CursorPaint1();

            CursorPaint.Strokethik = (int)slider.Value;
            StrokeThick = (int)slider.Value;





        }

        private void txtOnlyDigit_PreviewInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void txt_CanvasSize_TextChanged(object sender, TextChangedEventArgs e)
        {
            ChangeCanvasSize.CanvasSize(sender, MyCanvas);
        }


        #region Drawning


        bool IsDrawning = false;



        #endregion

    }
}
