using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Paint.CanvasSize
{
    internal class ChangeCanvasSize
    {
        public static void CanvasSize(object sender, InkCanvas MyCanvas) 
        {
            var txtbox = sender as TextBox;
            string boxname = txtbox.Name;

            // Защита от пустоты
            if (String.IsNullOrEmpty(txtbox.Text))
            {
                txtbox.Text = "1";
            }

            // Сюда уже поступают только циферные значения благодаря txtOnlyDigit
            double val = Convert.ToDouble(txtbox.Text);

            // По имени текстбокса применяется ограничение по высоте и ширине (4к)
            // И значение сразу применяется к полотну
            if (boxname == "txt_CanvasWidth")
            {
                if (val > 3840)
                {
                    txtbox.Text = "3840";
                    val = 3840;
                }
                MyCanvas.Width = val;

            }
            else if (boxname == "txt_CanvasHeight")
            {
                if (val > 2160)
                {
                    txtbox.Text = "2160";
                    val = 2160;
                }
                MyCanvas.Height = val;
            }
        }
    }
}
    
