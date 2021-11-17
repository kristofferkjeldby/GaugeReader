namespace GaugeReader.Profiles
{
    using GaugeReader.Extensions;
    using GaugeReader.Filters;
    using GaugeReader.Filters.Models;
    using GaugeReader.Math.Models.Angles;
    using GaugeReader.Profiles.Models;
    using System;
    using System.Drawing;

    public class Demo : IProfile
    {
        public string Name => nameof(Demo);

        public RadiusZone DialZone => new RadiusZone(0.6, 1);

        public RadiusZone CenterZone => new RadiusZone(0, 0.1);

        public RadiusZone MarkerZone => new RadiusZone(0.65, 0.8);

        public Angle TicksAngle => 270d.ToRadians();

        public Func<int, string> Reading => percent => $"{Math.Round(percent + 1.9 * Math.Pow(percent, 0.5))} °C";

        public IFilter MarkerFilter => new CannyFilter();

        public IFilter HandFilter => new CannyFilter();

        public Bitmap Correlation => (Bitmap)Image.FromFile(@"Profiles\Correlations\Demo.png");

        public bool ArrowHand => true;
    }
}
