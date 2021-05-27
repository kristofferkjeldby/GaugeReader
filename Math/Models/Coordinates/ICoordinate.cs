namespace GaugeReader.Math.Models.Coordinates
{
    using System.Drawing;

    public interface ICoordinate
    {
        double Length { get; }

        SphericalCoordinate ToSphericalCoordinate();

        CartesianCoordinate ToCartesianCoordinate();

        Point ToPoint(Bitmap image);

        PointF ToPointF(Bitmap image);

        bool IsValid();
    }
}
