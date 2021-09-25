namespace GaugeReader.Math.Models.Angles
{
    using GaugeReader.Extensions;
    using System;

    public class Angle : IComparable<Angle>
    {
        public Angle(double value)
        {
            Value = ToCyclicAngle(value);
        }

        public Angle(int step) : this(Constants.PI2 * ((double)step / Constants.AngleResolution))
        {
            
        }

        public static Angle operator +(Angle a, Angle b) => new Angle(a.Value + b.Value);
        public static Angle operator -(Angle a, Angle b) => new Angle(a.Value - b.Value);
        public static Angle operator *(int a, Angle b) => new Angle(a * b.Value);
        public static bool operator <(Angle a, Angle b) => a.Value < b.Value;
        public static bool operator >(Angle a, Angle b) => a.Value > b.Value;
        public static bool operator <=(Angle a, Angle b) => a.Value <= b.Value;
        public static bool operator >=(Angle a, Angle b) => a.Value >= b.Value;
        public static bool operator ==(Angle a, Angle b) => a.Value == b?.Value;
        public static bool operator !=(Angle a, Angle b) => a.Value != b?.Value;

        public static implicit operator double(Angle a) => a.Value;

        public static implicit operator Angle(double a) => new Angle(a);

        public double Value { get; }

        public bool IsReflect => IsReflectAngle(Value);

        public override string ToString()
        {
            return Value.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj is Angle otherAngle)
            {
                return Value.Equals(otherAngle.Value);
            }

            return false;
        }

        public Angle ToNormal()
        {
            return new Angle(Value + Constants.PI / 2);
        }

        public Angle FromNormal()
        {
            return new Angle(Value - Constants.PI / 2);
        }

        public override int GetHashCode()
        {
            return -1937169414 + Value.GetHashCode();
        }

        public static bool IsReflectAngle(double angle)
        {
            return angle > Constants.PI;
        }

        public static double ToCyclicAngle(double angle)
        {
            if (angle >= 0)
                return angle % Constants.PI2;
            else
                return Constants.PI2 + angle % Constants.PI2;
        }

        public int CompareTo(Angle other)
        {
            return Value.CompareTo(other.Value);
        }

        public bool FuzzyEquals(double otherAngle)
        {
            return Math.Abs(Value - otherAngle) < Constants.DegreeDelta;
        }

        public Angle Opposite => new Angle(Value + Constants.PI);

        public Angle Complementary => new Angle(Constants.PI2 - Value);

        public Angle Half => new Angle(Value / 2);

        public int Step => (Constants.AngleResolution / Constants.PI2 * Value).ToInt();
    }
}
