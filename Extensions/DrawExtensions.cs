namespace GaugeReader.Extensions
{
    using GaugeReader.Math.Models.Angles;
    using GaugeReader.Math.Models.Circles;
    using GaugeReader.Math.Models.Coordinates;
    using GaugeReader.Math.Models.Lines;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Linq;

    public static class DrawExtensions
    {
 
        public static Bitmap DrawCoordinate(this Bitmap image, ICoordinate coordinate, Color color)
        {
            var c = coordinate.ToPoint(image);

            if (!image.Contains(c))
                return image;

            var radius = 5;

            using (var g = Graphics.FromImage(image))
            {
                g.CompositingMode = CompositingMode.SourceCopy;
                g.DrawEllipse(new Pen(color, 2 * radius), new Rectangle(new Point(c.X - radius, c.Y - radius), new Size(2 * radius, 2 * radius)));
            }

            return image;
        }

        public static Bitmap DrawRadiusZone(this Bitmap image, RadiusZone radiusZone, Color color)
        {
            var radius = (double)image.Width / 2;
            var center = new CartesianCoordinate(0, 0);

            var startCircle = new Circle(center, radius * radiusZone.Start);
            var endCircle = new Circle(center, radius * radiusZone.End);

            return image.DrawCircle(startCircle, color).MaskCircle(endCircle, color);
        }

        public static Bitmap DrawLine(this Bitmap image, Line line, Color color)
        {
            var intersections = line.Intersections(image).
                Where(c  => image.Contains(c)).
                ToArray();

            if (intersections.Count() < 2)
                return image;

            using (var g = Graphics.FromImage(image))
            {
                g.CompositingMode = CompositingMode.SourceCopy;
                g.DrawLine(new Pen(color, 5), intersections[0].ToPoint(image), intersections[1].ToPoint(image));
            }

            return image;
        }

        public static Bitmap DrawRadialLine(this Bitmap image, RadialLine line, Color color)
        {
            var intersections = line.Intersections(image).
                Where(c => image.Contains(c)).
                ToArray();

            if (intersections.Count() < 2)
                return image;

            using (var g = Graphics.FromImage(image))
            {
                g.CompositingMode = CompositingMode.SourceCopy;
                g.DrawLine(new Pen(color, 5), new CartesianCoordinate(0, 0).ToPoint(image), intersections[1].ToPoint(image));
            }

            return image;
        }

        public static Bitmap DrawText(this Bitmap image, string text, Color color)
        {
            Font font = new Font("", 14);
            SolidBrush brush = new SolidBrush(color);

            using (var g = Graphics.FromImage(image))
            {
                g.FillRectangle(new SolidBrush(Color.Black), 0, 0, image.Width, 30);
                g.DrawString(text, font, brush, new Point(3, 3));
            }

            return image;
        }


        public static Bitmap DrawCircle(this Bitmap image, Circle circle, Color color)
        {
            return image.DrawCircle(circle.ToRectangle(image), color);
        }

        public static Bitmap DrawCircle(this Bitmap image, Rectangle rectangle, Color color)
        {
            using (Graphics g = Graphics.FromImage(image))
            {
                g.CompositingMode = CompositingMode.SourceCopy;
                g.FillEllipse(new SolidBrush(color), rectangle);
            }

            return image;
        }

        public static Bitmap DrawAngleSpan(this Bitmap image, AngleSpan angleSpan, Color color)
        {
            return image.MaskAngleSpan(angleSpan.Opposite, color);
        }
    }
}
