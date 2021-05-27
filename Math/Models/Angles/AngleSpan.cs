namespace GaugeReader.Math.Models.Angles
{
    using GaugeReader.Math.Models.Coordinates;
    using GaugeReader.Math.Models.Lines;
    using System.Collections.Generic;
    using System.Drawing;

    public class AngleSpan
    {
        public AngleSpan(double startAngle, double endAngle) : this(new Angle(startAngle), new Angle(endAngle))
        {

        }

        public bool ZeroCross { get; }

        public AngleSpan(Angle startAngle, Angle endAngle)
        {
            StartAngle = startAngle;
            StartLine = new RadialLine(startAngle);
            EndAngle = endAngle;
            EndLine = new RadialLine(endAngle);
            ZeroCross = startAngle > endAngle;
        }

        public double Width
        {
            get
            {
                if (!ZeroCross) return EndAngle - StartAngle;
                return (Constants.PI2 - StartAngle + EndAngle);
            }
        }

        public AngleSpan Opposite => new AngleSpan(EndAngle, StartAngle);

        public Angle StartAngle { get; set; }

        public Angle EndAngle { get; set; }

        public RadialLine StartLine { get; }

        public RadialLine EndLine { get; }

        public List<CartesianCoordinate> Intersections(Bitmap image)
        {
            return new List<CartesianCoordinate>()
            {
                StartLine.Intersections(image)[1],
                EndLine.Intersections(image)[1]
            };
        }

        public bool Includes(double angle)
        {
            return Includes(new Angle(angle));
        }

        public bool Includes(ICoordinate coordinate)
        {
            return Includes(coordinate.ToSphericalCoordinate().Theta);
        }

        public bool Includes(Angle angle)
        {
            if (!ZeroCross)
                return angle >= StartAngle && angle <= EndAngle;
            return angle >= StartAngle || angle <= EndAngle;
        }


        public bool FuzzyIncludes(Angle angle)
        {
            if (!ZeroCross)
                return angle >= StartAngle + Constants.DegreeDelta && angle <= EndAngle - Constants.DegreeDelta;
            return angle >= StartAngle + Constants.DegreeDelta || angle <= EndAngle - Constants.DegreeDelta;
        }


    }
}
