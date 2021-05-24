namespace GaugeReader.Extensions
{
    using GaugeReader.Models.Coordinates;
    using System.Drawing;

    public static class PointExtensions
    {
        public static CartesianCoordinate ToCartesianCoordinate(this Point point, Bitmap image)
        {
            return new CartesianCoordinate(point.X - ((double)image.Width / 2), ((double)image.Width / 2) - point.Y);
        }

        public static SphericalCoordinate ToSphericalCoordinate(this Point point, Bitmap image)
        {
            return point.ToCartesianCoordinate(image).ToSphericalCoordinate();
        }
    }
}
