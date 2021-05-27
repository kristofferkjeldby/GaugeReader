namespace GaugeReader.Math.Models.Circles
{
    using AForge.Imaging;
    using GaugeReader.Extensions;
    using GaugeReader.Math.Models.Coordinates;
    using System.Drawing;

    public class Circle
    {
        public Circle(CartesianCoordinate center, double r)
        {
            Center = center;
            R = r;
            Intensity = 1;
        }

        public Circle(HoughCircle circle, UnmanagedImage image)
        {
            Center = new Point(circle.X, circle.Y).ToCartesianCoordinate(image);
            R = circle.Radius;
            Intensity = circle.Intensity;
            RelativeIntensity = circle.RelativeIntensity;
        }
        
        public double Intensity { get; set; }

        public double RelativeIntensity { get; set; }

        public CartesianCoordinate Center { get; set; }

        public double R { get; set; }

        public Rectangle ToRectangle(Bitmap image)
        {
            var topLeft = new CartesianCoordinate(- R + Center.X, R + Center.Y );

            return new Rectangle(topLeft.ToPoint(image), new Size((2 * R).ToInt(), (2 * R).ToInt()));
        }

        public Rectangle ToRectangle(UnmanagedImage image)
        {
            var topLeft = new CartesianCoordinate(-R + Center.X, R + Center.Y);

            return new Rectangle(topLeft.ToPoint(image), new Size((2 * R).ToInt(), (2 * R).ToInt()));
        }

    }
}
