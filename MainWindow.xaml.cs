﻿using Paint.model;
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
using Microsoft.Win32;
using System.IO;

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

        public Color wpfColor { get; set; }
       
        private void ColorButton_Click(object sender, RoutedEventArgs e)
        {
            // Создание и отображение диалогового окна выбора цвета
            var colorDialog = new System.Windows.Forms.ColorDialog();
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Преобразование выбранного цвета в объект System.Windows.Media.Color
                var selectedColor = System.Drawing.Color.FromArgb(colorDialog.Color.ToArgb());
                wpfColor = Color.FromArgb(
                    selectedColor.A,
                    selectedColor.R,
                    selectedColor.G,
                    selectedColor.B);

                // Установка выбранного цвета в элемент управления TextBlock
                ColorTextBlock.Background = new SolidColorBrush(wpfColor);
            }
        }













        CursorPaint1 CursorPaint = new CursorPaint1();
        
        private Point StartPoint;

        private Point EndPoint;

        private Oblick Oblick;

        AllOblicks AllOblick;
        
        Brush AllBrush;

        System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            int width = (int)Math.Ceiling(MyCanvas.ActualWidth);
            int height = (int)Math.Ceiling(MyCanvas.ActualHeight);

            // Create a bitmap image from the visual tree of the canvas
            RenderTargetBitmap renderBitmap = new RenderTargetBitmap(
                width, height, 96, 96, System.Windows.Media.PixelFormats.Pbgra32);
            renderBitmap.Render(MyCanvas);

            // Create a PNG encoder and save the bitmap to a file
            PngBitmapEncoder pngEncoder = new PngBitmapEncoder();
            pngEncoder.Frames.Add(BitmapFrame.Create(renderBitmap));

            // Feed the dummy name to the save dialog
            saveFileDialog.ShowDialog();
				
            using (FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create))
            {
                pngEncoder.Save(fileStream);
            }

            Rectangle rectangle = new Rectangle();
            rectangle.Width = 100;
            rectangle.Height = 100;

            MyCanvas.Children.Add(rectangle);
            
            MyCanvas.Focus();
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
            
        }
        

        // по сути выбор цвета тут
        private void ChangeColor()
        {
            AllBrush = new SolidColorBrush((Color)wpfColor);
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
                    //Очистка временного канваса
                    BKH.TempCanvas.Children.Clear();
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
                BKH.TempCanvas.Children.Clear();
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

        private void Cursor_click(object sender, RoutedEventArgs e)
        {
            Draw = false;
            button = false;
        }

        #region Drawning


        bool IsDrawning = false;
        
       

        #endregion

    }
}
