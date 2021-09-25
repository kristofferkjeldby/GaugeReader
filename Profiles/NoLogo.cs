namespace GaugeReader.Profiles
{
    using GaugeReader.Extensions;
    using GaugeReader.Filters;
    using GaugeReader.Filters.Models;
    using GaugeReader.Math.Models.Angles;
    using GaugeReader.Profiles.Models;
    using System;
    using System.Drawing;

    public class NoLogo : IProfile
    {
        public string Name => nameof(NoLogo);

        public RadiusZone DialZone => new RadiusZone(0.6, 1);

        public RadiusZone CenterZone => new RadiusZone(0, 0.1);

        public RadiusZone MarkerZone => new RadiusZone(0.6, 1);

        public Angle TicksAngle => 270d.ToRadians();

        public Func<int, string> Reading => percent => $"{percent}%";

        public IFilter MarkerFilter => new CannyFilter();

        public IFilter HandFilter => new CannyFilter();

        public Bitmap Correlation => (Bitmap)Image.FromFile(@"Profiles\Correlations\NoLogo.png");

        public bool ArrowHand => true;
    }
}
