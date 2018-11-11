using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Utilities
{
    public static class ImageExt
    {
        public static Image Resize(this Image image, int sourceX, int sourceY, int sourceWidth, int sourceHeight, int desWidth, int desHeight)
        {
            var bmp = new Bitmap(desWidth, desHeight);
            using (var graphics = Graphics.FromImage(bmp))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    var desRect = new Rectangle(0, 0, desWidth, desHeight);
                    graphics.DrawImage(image, desRect, sourceX, sourceY, sourceWidth, sourceHeight, GraphicsUnit.Pixel, wrapMode);
                }
            }
            return bmp;
        }

        /// <summary>
        /// Scale image with new size
        /// </summary>
        /// <param name="image"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Image Resize(this Image image, int width, int height)
        {
            return image.Resize(0, 0, image.Width, image.Height, width, height);
        }

        public static Image ResizeProportional(this Image image, int width, int height, bool enlarge)
        {
            double ratio = Math.Max(image.Width / (double)width, image.Height / (double)height);
            if (ratio < 1 && !enlarge) return image;
            return image.Resize(0, 0, image.Width, image.Height, (int)Math.Round(image.Width / ratio), (int)Math.Round(image.Height / ratio));
        }

        public static Image ResizeCropExcess(this Image image, int desWidth, int desHeight)
        {
            double sourceRatio = image.Width / (double)image.Height;
            double desRatio = desWidth / (double)desHeight;
            int sourceX, sourceY, cropWidth, cropHeight;

            if (sourceRatio < desRatio) // trim top and bottom
            {
                cropHeight = desHeight * image.Width / desWidth;
                sourceY = (image.Height - cropHeight) / 2;
                cropWidth = image.Width;
                sourceX = 0;
            }
            else // trim left and right
            {
                cropWidth = desWidth * image.Height / desHeight;
                sourceX = (image.Width - cropWidth) / 2;
                cropHeight = image.Height;
                sourceY = 0;
            }

            return Resize(image, sourceX, sourceY, cropWidth, cropHeight, desWidth, desHeight);
        }
    }
}
