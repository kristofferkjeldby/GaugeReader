namespace GaugeReader.Models
{
    using GaugeReader.Extensions;
    using GaugeReader.Models.Coordinates;
    using System.Drawing;

    public class Circle
    {
        public Circle(CartesianCoordinate center, double r)
        {
            Center = center;
            R = r;
        }

        public CartesianCoordinate Center { get; set; }

        public double R { get; set; }

        public Rectangle ToRectangle(Bitmap image)
        {
            var topLeft = new CartesianCoordinate(-R, R);
            return new Rectangle(topLeft.ToPoint(image), new Size((2*R).ToInt(), (2*R).ToInt()));
        }
    }
}
