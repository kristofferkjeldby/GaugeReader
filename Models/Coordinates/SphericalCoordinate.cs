namespace GaugeReader.Models.Coordinates
{
    using GaugeReader.Extensions;
    using System;
    using System.Drawing;

    public class SphericalCoordinate : ICoordinate, IEquatable<SphericalCoordinate>
    {
        public double R { get; set; }

        public double Theta { get; set; }

        public double Length => R;

        public SphericalCoordinate(double r, double theta)
        {
            R = r;
            Theta = theta;
        }

        public static implicit operator CartesianCoordinate(SphericalCoordinate s)
        {
            return s.ToCartesianCoordinate();
        }

        public CartesianCoordinate ToCartesianCoordinate()
        {
            return new CartesianCoordinate(Math.Cos(Theta) * R, Math.Sin(Theta) * R);
        }

        public SphericalCoordinate ToSphericalCoordinate()
        {
            return this;
        }

        public Point ToPoint(Bitmap image)
        {
            return this.ToCartesianCoordinate().ToPoint(image);
        }

        public PointF ToPointF(Bitmap image)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public bool IsValid()
        {
            return R.IsValid() && Theta.IsValid();
        }

        public override string ToString()
        {
            return $"({R.ToString(Constants.DeltaFormat)}, {Theta.ToString(Constants.DeltaFormat)})";
        }

        public bool Equals(SphericalCoordinate other)
        {
            return ToString().Equals(other.ToString());
        }

        public override bool Equals(object obj)
        {
            if (obj is SphericalCoordinate)
                return Equals(obj as SphericalCoordinate);
            return false;
        }
    }
}
