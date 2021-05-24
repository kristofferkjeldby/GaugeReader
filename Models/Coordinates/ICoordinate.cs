using System.Drawing;

namespace GaugeReader.Models.Coordinates
{
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
