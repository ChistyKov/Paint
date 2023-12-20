using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace Paint
{
    internal class SaveCanvas
    {
        private static Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
        public static void save(InkCanvas inkCanvas)
        {
            // Создание объекта RenderTargetBitmap с размерами канваса
            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(
                (int)inkCanvas.ActualWidth, (int)inkCanvas.ActualHeight, 96, 96, PixelFormats.Pbgra32);

            // Рендеринг канваса в RenderTargetBitmap
            renderTargetBitmap.Render(inkCanvas);

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
    }
}
