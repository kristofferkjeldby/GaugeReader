namespace GaugeReader.Profiles
{
    using GaugeReader.Convolutions.Models;
    using GaugeReader.Extensions;
    using GaugeReader.Filters;
    using GaugeReader.Filters.Models;
    using GaugeReader.Math.Models.Angles;
    using GaugeReader.Profiles.Models;
    using System;

    public class Hygrometer : IProfile
    {
        public string Name => nameof(Hygrometer);

        public RadiusZone CenterZone => new RadiusZone(0, 0.2);

        public RadiusZone MarkerZone => new RadiusZone(0.6, 0.8);

        public Angle MarkerAngle => 270d.ToRadians();

        public IFilter MarkerFilter => new NoFilter();

        public IFilter HandFilter => new ChainFilter(new InvertFilter(), new BrightnessCutoffFilter(0.7));

        public Func<int, string> Reading => percent => $"{percent}%";

        public Convolution MarkerConvolution => null;


    }
}