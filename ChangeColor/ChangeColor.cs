using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Paint.ChangeColor
{
    
  
    public class CollorLine 
    {
        
        public static Color wpfColor;
        public static Brush ChangeColor(object sender, RoutedEventArgs e)
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

            }
            return new SolidColorBrush(wpfColor);
        }
    }

    public class FillingColor 
    {
        
        public static Color wpfColor;
        public static Brush ChangeColor(object sender, RoutedEventArgs e)
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

            }
            return new SolidColorBrush(wpfColor);
        }
    }
}
