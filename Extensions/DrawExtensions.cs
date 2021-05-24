namespace GaugeReader.Extensions
{
    using GaugeReader.Models;
    using GaugeReader.Models.Angles;
    using GaugeReader.Models.Coordinates;
    using GaugeReader.Models.Gauges;
    using GaugeReader.Models.Lines;
    using System.Drawing;
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
                g.DrawEllipse(new Pen(color, 2 * radius), new Rectangle(new Point(c.X - radius, c.Y - radius), new Size(2 * radius, 2 * radius)));
            }

            return image;
        }

        public static Bitmap DrawRadiusZone(this Bitmap image, RadiusZone radiusZone, Color color)
        {
            var radius = (double)image.Width / 2;
            var center = new CartesianCoordinate(0, 0);

            var startRectangle = new Circle(center, radius * radiusZone.Start).ToRectangle(image);
            var endRectangle = new Circle(center, radius * radiusZone.End).ToRectangle(image);

            image.DrawCircle(startRectangle, color);
            image.MaskCircle(endRectangle, color);

            return image;
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


        public static Bitmap DrawCircle(this Bitmap image, Rectangle rectangle, Color color)
        {
            using (Graphics g = Graphics.FromImage(image))
            {
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
