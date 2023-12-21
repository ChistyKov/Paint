using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;

namespace Paint
{
    internal class SaveCanvas
    {
        public static void save(InkCanvas inkCanvas)
        {
            inkCanvas.EditingMode = InkCanvasEditingMode.None;
            Microsoft.Win32.SaveFileDialog savedialog = new Microsoft.Win32.SaveFileDialog();
            savedialog.Title = "Сохранить картинку как...";
            savedialog.FileName = "image";
            savedialog.DefaultExt = ".png";
            savedialog.OverwritePrompt = true;
            savedialog.CheckPathExists = true;
            savedialog.Filter = "Image (.png)|*.png";

            Nullable<bool> result = savedialog.ShowDialog();

            if (result == true)
            {
                string fileName = savedialog.FileName;

                var size = new Size(inkCanvas.ActualWidth, inkCanvas.ActualHeight);
                inkCanvas.Margin = new Thickness(0, 0, 0, 0);
                inkCanvas.Measure(size);
                inkCanvas.Arrange(new Rect(size));
                var encoder = new PngBitmapEncoder();
                var bitmapTarget = new RenderTargetBitmap((int)size.Width,
                    (int)size.Height, 96, 96, PixelFormats.Default);
                bitmapTarget.Render(inkCanvas);
                using (FileStream fs = new FileStream(fileName, FileMode.Create))
                {
                    encoder.Frames.Add(BitmapFrame.Create(bitmapTarget));
                    encoder.Save(fs);
                }
                inkCanvas.Margin = new Thickness(234, 0, 0, 0);
                inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
            }
        }
    }
}
