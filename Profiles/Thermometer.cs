namespace GaugeReader.Profiles
{
    using GaugeReader.Filters;
    using GaugeReader.Filters.Models;
    using GaugeReader.Math.Models.Angles;
    using GaugeReader.Profiles.Models;
    using System;
    using System.Drawing;

    public class Thermometer : IProfile
    {
        public string Name => nameof(Thermometer);

        public RadiusZone DialZone => new RadiusZone(0.6, 0.8);

        public RadiusZone CenterZone => new RadiusZone(0, 0.2);

        public RadiusZone MarkerZone => new RadiusZone(0.85, 1);

        public Angle TicksAngle => (Constants.PI2 / 9) * 8;

        public Func<int, string> Reading => percent => $"{((double)percent/100 * 80) - 20} °C";

        public IFilter MarkerFilter => new CannyFilter();

        public IFilter HandFilter => new RedFilter();

        public Bitmap Correlation => (Bitmap)Image.FromFile(@"Profiles\Correlations\Thermometer.png");

        public bool ArrowHand => false;
    }
}
