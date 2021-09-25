namespace GaugeReader.Extensions
{
    using AForge.Imaging;
    using GaugeReader.Filters.Models;
    using GaugeReader.Math.Models.Coordinates;
    using GaugeReader.Math.Models.Maps;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;

    public static class BitmapExtensions
    {
        public static Bitmap Crop(this Bitmap image, Rectangle rectangle)
        {
            Bitmap output = new Bitmap(rectangle.Width, rectangle.Height);

            using (Graphics g = Graphics.FromImage(output))
            {
                g.DrawImage(image, new Rectangle(0, 0, output.Width, output.Height),
                                 rectangle,
                                 GraphicsUnit.Pixel);
            }

            return output;
        }

        public static UnmanagedImage ToProcessImage(this Bitmap image)
        {
            return UnmanagedImage.FromManagedImage(image.Clone(new Rectangle(0, 0, image.Width, image.Height), Constants.ProcessFormat));
        }

        public static Bitmap ToBitmap(this UnmanagedImage image)
        {
            return image.ToManagedImage().Clone(new Rectangle(0, 0, image.Width, image.Height), Constants.BitmapFormat);
        }

        public static Bitmap ToBitmap(this Bitmap image)
        {
            return image.Clone(new Rectangle(0, 0, image.Width, image.Height), Constants.BitmapFormat);
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

        public static Bitmap Factor(this Bitmap image, int factor)
        {
            return image.Resize(image.Width / factor);
        }

        public static Bitmap Resize(this Bitmap image, int width)
        {
            int height = (image.Height * (width / (double)image.Width)).ToInt();

            var rectangle = new Rectangle(0, 0, width, height);
            var output = new Bitmap(width, height);

            output.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(output))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, rectangle, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return output;
        }

        public static bool Contains(this Bitmap image, ICoordinate c, bool strict = false)
        {
            if (!c.IsValid())
                return false;
            return image.Contains(c.ToPointF(image), strict);
        }

        public static bool Contains(this Bitmap image, PointF p, bool strict = false)
        {
            var delta = strict ? 0 : Constants.ImageDrawMargin;
            var value = p.X >= 0 - delta && p.X <= image.Width + delta && p.Y >= 0 - delta && p.Y <= image.Height + delta;
            return value;
        }

        public static Bitmap Filter(this Bitmap input, IFilter filter)
        {
            return filter.Process(input);
        }

        public static Map ToMap(this Bitmap image)
        {
            var map = new double[image.Width];

            for (int i = 0; i < image.Width; i++)
            {
                map[i] = image.GetPixel(i, 0).GetBrightness();
            }

            map.Normalize();

            return new Map(map);
        }
    }
}
