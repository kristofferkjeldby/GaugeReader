namespace GaugeReader.Profiles.Models
{
    using GaugeReader.Convolutions.Models;
    using GaugeReader.Filters.Models;
    using GaugeReader.Math.Models.Angles;
    using System;

    public interface IProfile
    {
        string Name { get; }

        RadiusZone CenterZone { get; }

        RadiusZone MarkerZone { get; }

        Angle MarkerAngle { get; }

        IFilter MarkerFilter { get; }

        IFilter HandFilter { get; }

        Func<int, string> Reading { get; }

        Convolution MarkerConvolution { get; }
    }
}
