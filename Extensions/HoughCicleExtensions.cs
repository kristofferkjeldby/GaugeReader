namespace GaugeReader.Extensions
{
    using AForge.Imaging;
    using System.Drawing;

    public static class HoughCicleExtensions
    {
        public static Rectangle GetRectangle(this HoughCircle circle, int scaleFactor = 1)
        {
            return new Rectangle(scaleFactor * circle.X - scaleFactor * circle.Radius, scaleFactor * circle.Y - scaleFactor * circle.Radius, scaleFactor * circle.Radius * 2, scaleFactor * circle.Radius * 2);
        }

    }
}
