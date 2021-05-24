namespace GaugeReader.Extensions
{
    using AForge.Imaging;
    using GaugeReader.Models.Coordinates;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;

    public static class BitmapExtensions
    {
        public static Bitmap Crop(this Bitmap image, Rectangle rectangle)
        {
            Bitmap cropped = new Bitmap(rectangle.Width, rectangle.Height);

            using (Graphics g = Graphics.FromImage(cropped))
            {
                g.DrawImage(image, new Rectangle(0, 0, cropped.Width, cropped.Height),
                                 rectangle,
                                 GraphicsUnit.Pixel);
            }

            return cropped;
        }

        public static UnmanagedImage ToProcessImage(this Bitmap image)
        {
            return UnmanagedImage.FromManagedImage(image.Clone(new Rectangle(0, 0, image.Width, image.Height), Constants.ProcessFormat));
        }

        public static Bitmap ToDrawImage(this UnmanagedImage image)
        {
            return image.ToManagedImage().Clone(new Rectangle(0, 0, image.Width, image.Height), Constants.DrawFormat);
        }

        public static Bitmap ToDrawImage(this Bitmap image)
        {
            return image.Clone(new Rectangle(0, 0, image.Width, image.Height), Constants.DrawFormat);
        }

        public static Bitmap Copy(this Bitmap image)
        {
            return image.Clone() as Bitmap;
        }

        public static UnmanagedImage Copy(this UnmanagedImage image)
        {
            return image.Clone();
        }

        public static List<CartesianCoordinate> Corners(this Bitmap image)
        {
            return new List<CartesianCoordinate>()
            {
                 new Point(0, 0).ToCartesianCoordinate(image),
                 new Point(image.Width, 0).ToCartesianCoordinate(image),
                 new Point(image.Width, image.Height).ToCartesianCoordinate(image),
                 new Point(0, image.Height).ToCartesianCoordinate(image)
            };
        }

        public static Bitmap Resize(this Bitmap image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public static bool Contains(this Bitmap image, ICoordinate c, bool strict = false)
        {
            if (!c.IsValid())
                return false;
            return image.Contains(c.ToPointF(image), strict);
        }

        public static bool Contains(this Bitmap image, PointF p, bool strict = false)
        {
            var d = strict ? 0 : Constants.DrawMargin;
            var value = p.X >= 0 - d && p.X <= image.Width + d && p.Y >= 0 - d && p.Y <= image.Height + d;
            return value;
        }

        public static Bitmap Center(this Bitmap image, CartesianCoordinate coordinate)
        {
            Bitmap centered = new Bitmap(image.Width, image.Height);

            var center = coordinate.ToPoint(image);

            var x = (double)image.Width / 2 - center.X;
            var y = (double)image.Height / 2 - center.Y;

            using (Graphics g = Graphics.FromImage(centered))
            {
                g.FillRectangle(new SolidBrush(Constants.MaskColor), new Rectangle(0, 0, centered.Width, centered.Height));
                g.DrawImage(image, new Point(x.ToInt(), y.ToInt()));
            }

            image = centered;

            return centered;
        }
    }
}
