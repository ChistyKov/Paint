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
            //MyCanvas.MouseDown += Window_MouseLeftbuttonDown;
            //MyCanvas.MouseMove += Window_MouseMove;
            //MyCanvas.MouseUp += Window_MouseLeftButtonUp;
        }

        public Color wpfColor { get; set; }
       
        private void ColorButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
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

            }
            switch (clickedButton.Name)
            {
                case "ColorFillingButton":
                    ColorFilling = new SolidColorBrush(wpfColor);
                    break;
                case "ColorLinesButton":
                    ColorLines = new SolidColorBrush(wpfColor);
                    break;
            }

        }




        CursorPaint1 CursorPaint = new CursorPaint1();
        
        private Point StartPoint;

        private Point EndPoint;

        private Oblick Oblick;

        AllOblicks AllOblick;
        
        Brush ColorFilling;
        //Для удобства устанавливаем стандартный цвет линии
        Brush ColorLines = new SolidColorBrush((Color)ColorConverter.ConvertFromString("black"));
        
        private Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Создание объекта RenderTargetBitmap с размерами канваса
            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(
                (int)MyCanvas.ActualWidth, (int)MyCanvas.ActualHeight, 96, 96, PixelFormats.Pbgra32);

            // Рендеринг канваса в RenderTargetBitmap
            renderTargetBitmap.Render(MyCanvas);

            // Создание объекта PngBitmapEncoder для сохранения изображения в формате PNG
            PngBitmapEncoder pngEncoder = new PngBitmapEncoder();
            pngEncoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));

            

            saveFileDialog.Filter = "Picture files (*.png)|*.png|Picture files (*.jpg)|*.jpg|All files (*.*)|*.* ";
            if (saveFileDialog.ShowDialog() == true)
            {
                using (FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    pngEncoder.Save(fileStream);
                }
            }
        }
        
        bool Addtext = false;
        private void TextBox_Click(object sender, RoutedEventArgs e)
        {
            Addtext = true;
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
        

        // по сути выбор цвета тут
        private void ChangeColor()
        {
            ColorFilling = new SolidColorBrush(wpfColor);
        }
      
        bool Draw;
        bool button;


        private bool isDragging = false;
        private UIElement draggedObject;
        private Point lastMousePosition;
        private void Window_MouseLeftbuttonDown(object sender,MouseButtonEventArgs e)
        {
            if (Addtext)
            {
                // Проверяем, что клик был выполнен в Canvas
                if (sender is Canvas canvas)
                {
                    Point clickedPoint = e.GetPosition(canvas);

                    // Создаем TextBox
                    TextBox textBox = new TextBox();
                    textBox.Width = 100;
                    textBox.Height = Double.NaN;
                    textBox.FontSize = 12;
                    textBox.AcceptsReturn = true;
                    textBox.TextWrapping = TextWrapping.Wrap; // Чтобы поле могло расширяться

                    Canvas.SetLeft(textBox, clickedPoint.X);
                    Canvas.SetTop(textBox, clickedPoint.Y);

                    canvas.Children.Add(textBox);

                    // Устанавливаем фокус на TextBox
                    textBox.Focus();
                }
                Addtext = false;
            }





            if (Draw && (sender is Canvas))
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

        private void Cursor_click(object sender, RoutedEventArgs e)
        {
            Draw = false;
            button = false;
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
                    Oblick.ShapeUpdeting(2, MyCanvas, ColorLines, StartPoint, EndPoint, ColorFilling);
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
