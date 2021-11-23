using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace key
{
    public class Capture
    {
        #region Capture

        static int imageCount = 0;

        /// <summary>
        /// Capture al screen then save into ImagePath
        /// </summary>
        public static void CaptureScreen()
        {
            //Create a new bitmap.
            var bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                           Screen.PrimaryScreen.Bounds.Height,
                                           PixelFormat.Format32bppArgb);

            // Create a graphics object from the bitmap.
            var gfxScreenshot = Graphics.FromImage(bmpScreenshot);

            // Take the screenshot from the upper left corner to the right bottom corner.
            gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                                        Screen.PrimaryScreen.Bounds.Y,
                                        0,
                                        0,
                                        Screen.PrimaryScreen.Bounds.Size,
                                        CopyPixelOperation.SourceCopy);

            //create folder
            API.CreateFolderImage();
            string directoryImage = API.FolderImage + "/"+ API.imagePath + DateTime.Now.ToLongDateString();

            if (!Directory.Exists(directoryImage))
            {
                Directory.CreateDirectory(directoryImage);
            }
            // Save the screenshot to the specified path that the user has chosen.
            //string imageName = string.Format("{0}\\{1}{2}", directoryImage, fileName, API.imageExtendtion);
            string path1 = directoryImage;
            var prefix = "Image_" + DateTime.Now.ToLongDateString();
            var fileName = Enumerable.Range(1, 10000)
                            .Select(n => Path.Combine(path1, $"{prefix}-{n}.png"))
                            .First(p => !File.Exists(p));   
            //string imageName = string.Format("{0}\\{1}{2}", directoryImage, fileName, API.imageExtendtion);
            try
            {
                bmpScreenshot.Save(fileName, ImageFormat.Png);
                //Clipboard.SetText(fileName);
                //bmpScreenshot.Save(imageName, ImageFormat.Png);
            }
            catch
            {

            }
        }

        #endregion
    }
}
