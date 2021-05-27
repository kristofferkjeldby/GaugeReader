namespace GaugeReader.Extensions
{
    using System.Drawing;

    public static class RectangleExtensions
    {
        public static Rectangle Scale(this Rectangle rectangle, int factor)
        {
            return new Rectangle(new Point(rectangle.X * factor, rectangle.Y * factor), new Size(rectangle.Width * factor, rectangle.Height * factor));
        }
    }
}
