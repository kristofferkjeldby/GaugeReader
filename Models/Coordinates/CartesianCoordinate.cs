namespace GaugeReader.Models.Coordinates
{
    using GaugeReader.Extensions;
    using GaugeReader.Models.Angles;
    using System;
    using System.Drawing;

    public class CartesianCoordinate : ICoordinate, IEquatable<CartesianCoordinate>
    {
        public double X { get; set; }

        public double Y { get; set; }

        public double Length => Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));

        public CartesianCoordinate(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double Cross(CartesianCoordinate other)
        {
            return X * other.Y - Y * other.X;
        }

        public double Dot(CartesianCoordinate other)
        {
            return X * other.X + Y * other.Y;
        }

        public static implicit operator SphericalCoordinate(CartesianCoordinate c)
        {
            return c.ToSphericalCoordinate();
        }

        public SphericalCoordinate ToSphericalCoordinate()
        {
            return new SphericalCoordinate(
                Length,
                Angles.Angle.ToCyclicAngle(Math.Atan2(Y, X)).Limit(0, Math.PI * 2)
            );
        }

        public PointF ToPointF(Bitmap image)
        {
            return new PointF((float)(X + ((double)image.Width / 2)), (float)(-Y + ((double)image.Width / 2)));
        }

        public Point ToPoint(Bitmap image)
        {
            var pf = ToPointF(image);
            return new Point(pf.X.ToInt(), pf.Y.ToInt());
        }

        public CartesianCoordinate ToCartesianCoordinate()
        {
            return this;
        }

        public static CartesianCoordinate operator +(CartesianCoordinate a, CartesianCoordinate b)
        {
            return new CartesianCoordinate(a.X + b.X, a.Y + b.Y);
        }

        public static CartesianCoordinate operator -(CartesianCoordinate a, CartesianCoordinate b)
        {
            return new CartesianCoordinate(a.X - b.X, a.Y - b.Y);
        }

        public static CartesianCoordinate operator /(CartesianCoordinate a, double b)
        {
            return new CartesianCoordinate(a.X / b, a.Y / b);
        }

        public static CartesianCoordinate operator *(double a, CartesianCoordinate b)
        {
            return new CartesianCoordinate(a * b.X, a * b.Y);
        }

        public static double operator *(CartesianCoordinate a, CartesianCoordinate b)
        {
            return a.Dot(b);
        }

        public static double operator %(CartesianCoordinate a, CartesianCoordinate b)
        {
            return a.Cross(b);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public Angle Angle(CartesianCoordinate b)
        {
            return new Angle(Math.Acos((this * b) / (Length * b.Length)));
        }

        public bool IsValid()
        {
            return X.IsValid() && Y.IsValid();
        }

        public override string ToString()
        {
            return $"({X.ToString(Constants.DeltaFormat)}, {Y.ToString(Constants.DeltaFormat)})";
        }

        public bool Equals(CartesianCoordinate other)
        {
            return ToString().Equals(other.ToString());
        }

        public override bool Equals(object obj)
        {
            if (obj is CartesianCoordinate)
                return Equals(obj as CartesianCoordinate);
            return false;
        }
    }
}