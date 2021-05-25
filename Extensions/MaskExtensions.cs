namespace GaugeReader.Extensions
{
    using GaugeReader.Models;
    using GaugeReader.Models.Angles;
    using GaugeReader.Models.Coordinates;
    using GaugeReader.Models.Lines;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Linq;

    public static class MaskExtensions
    {
        public static Bitmap MaskCircle(this Bitmap image, Rectangle rectangle, Color color)
        {
            Bitmap originalImage = image.Copy();
            using (Graphics g = Graphics.FromImage(image))
            {
                g.CompositingMode = CompositingMode.SourceCopy;
                g.FillRectangle(new SolidBrush(color), 0, 0, image.Width, image.Height);
                GraphicsPath path = new GraphicsPath();
                path.AddEllipse(rectangle);
                g.FillRectangle(new SolidBrush(color), rectangle);
                g.Clip = new Region(path);
                g.DrawImage(originalImage, 0, 0);
            }
            return image;
        }

        public static Bitmap MaskAngleSpan(this Bitmap image, AngleSpan angleSpan, Color color)
        {
            var maskCoordinates = new List<CartesianCoordinate>();
            maskCoordinates.Add(new CartesianCoordinate(0, 0));
           
            var intersections = angleSpan.Intersections(image);
            var corners = image.Corners().Where(c => angleSpan.Includes(c)).Select(c => c.ToSphericalCoordinate());

            maskCoordinates.Add(intersections[0]);
            maskCoordinates.AddRange(corners.Where(c => c.Theta >= angleSpan.StartAngle).OrderBy(c => c.Theta).Select(c => c.ToCartesianCoordinate()));
            maskCoordinates.AddRange(corners.Where(c => c.Theta < angleSpan.StartAngle).OrderBy(c => c.Theta).Select(c => c.ToCartesianCoordinate()));
            maskCoordinates.Add(intersections[1]);

            using (Graphics g = Graphics.FromImage(image))
            {
                g.CompositingMode = CompositingMode.SourceCopy;
                g.FillPolygon(new SolidBrush(color), maskCoordinates.Select(c => c.ToPoint(image)).ToArray());
            }

            return image;
        }

        public static Bitmap MaskRadiusZone(this Bitmap image, RadiusZone radiusZone, Color color)
        {
            var radius = (double)image.Width / 2;
            var center = new CartesianCoordinate(0, 0);
            var zoneRadius = (radiusZone.End - radiusZone.Start) / 2;

            var rectangle = new Circle(center, radius * (radiusZone.End - zoneRadius)).ToRectangle(image);

            using (Graphics g = Graphics.FromImage(image))
            {
                g.CompositingMode = CompositingMode.SourceCopy;
                g.DrawEllipse(new Pen(color, (float)(zoneRadius * radius) * 2), rectangle);
            }

            return image;
        }

        public static Bitmap MaskLine(this Bitmap image, Line line, Color color)
        {
            var intersections = line.Intersections(image).ToList();

            var r = intersections[1] - intersections[0];

            var n = new CartesianCoordinate(-r.Y, r.X);

            var corners = image.Corners().Where(c => line.Distance(c) > 0).OrderBy(c => (c - intersections[0]).Angle(r));

            var maskCoordinates = new List<CartesianCoordinate>();
            maskCoordinates.AddRange(intersections);
            maskCoordinates.AddRange(corners);

            using (Graphics g = Graphics.FromImage(image))
            {
                g.CompositingMode = CompositingMode.SourceCopy;
                g.FillPolygon(new SolidBrush(color), maskCoordinates.Select(c => c.ToPoint(image)).ToArray());
            }

            return image;
        }
    }
}
