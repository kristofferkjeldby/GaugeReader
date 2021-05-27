namespace GaugeReader.Profiles
{
    using GaugeReader.Convolutions.Models;
    using GaugeReader.Extensions;
    using GaugeReader.Filters;
    using GaugeReader.Filters.Models;
    using GaugeReader.Math.Models.Angles;
    using GaugeReader.Profiles.Models;
    using System;

    public class Simple : IProfile
    {
        public string Name => nameof(Simple);

        public RadiusZone CenterZone => new RadiusZone(0, 0.1);

        public RadiusZone MarkerZone => new RadiusZone(0.7, 0.9);

        public Angle MarkerAngle => 270d.ToRadians();

        public Func<int, string> Reading => percent => $"{percent}%";

        public Convolution MarkerConvolution => null;

        public IFilter MarkerFilter => new BrightnessCutoffFilter(0.2);

        public IFilter HandFilter => new InvertFilter();
    }
}
