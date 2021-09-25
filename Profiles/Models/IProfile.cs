namespace GaugeReader.Profiles.Models
{
    using GaugeReader.Filters.Models;
    using GaugeReader.Math.Models.Angles;
    using System;
    using System.Drawing;

    public interface IProfile
    {
        string Name { get; }

        RadiusZone DialZone { get; }

        RadiusZone CenterZone { get; }

        RadiusZone MarkerZone { get; }

        Angle TicksAngle { get; }

        IFilter HandFilter { get; }

        Bitmap Correlation { get; }

        Func<int, string> Reading { get; }

        bool ArrowHand { get; }
    }
}
