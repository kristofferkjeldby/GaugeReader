namespace GaugeReader.Math.Models.Lines
{
    using AForge.Imaging;
    using GaugeReader.Extensions;
    using GaugeReader.Math.Models.Angles;
    using GaugeReader.Math.Models.Coordinates;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    public class Line
    {
        public double R { get; set; }

        public Angle Theta { get; set; }

        public Angle Angle { get; set; }

        public CartesianCoordinate Normal { get; set; }

        public double Intensity { get; set; }

        public double RelativeIntensity { get; set; }

        public Line(double r, Angle theta)
        {
            R = r;
            Theta = theta;
            Angle = theta.FromNormal();
            Normal = new CartesianCoordinate(Math.Cos(Theta), Math.Sin(Theta));
        }

        public Line(HoughLine line)
        {
            R = line.Radius;
            Theta = line.Theta.ToRadians();
            Angle = Theta.FromNormal();
            Normal = new CartesianCoordinate(Math.Cos(Theta), Math.Sin(Theta));
            RelativeIntensity = line.RelativeIntensity;
            Intensity = line.Intensity;
        }

        public Line(double r, double theta) : this(r, new Angle(theta))
        {

        }

        public List<CartesianCoordinate> Intersections(Bitmap image)
        {
            var topLeftCorner = new Point(0, 0).ToCartesianCoordinate(image);
            var bottomRightCorner = new Point(image.Width, image.Height).ToCartesianCoordinate(image);

            return new List<CartesianCoordinate>()
            {
                new CartesianCoordinate((R - topLeftCorner.Y * Normal.Y) / Normal.X, topLeftCorner.Y),
                new CartesianCoordinate((R - bottomRightCorner.Y * Normal.Y) / Normal.X, bottomRightCorner.Y),
                new CartesianCoordinate(topLeftCorner.X, (R - topLeftCorner.X * Normal.X) / Normal.Y),
                new CartesianCoordinate(bottomRightCorner.X, (R - bottomRightCorner.X * Normal.X) / Normal.Y),
            }
            .Where(c => image.Contains(c, true)).Distinct().OrderBy(c => c.Cross(Normal)).Take(2).ToList();
        }

        public double Distance(CartesianCoordinate coordinate)
        {
            return (coordinate * Normal) - R;
        }
    }
}
