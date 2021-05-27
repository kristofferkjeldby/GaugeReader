namespace GaugeReader.Profiles
{
    using GaugeReader.Convolutions.Models;
    using GaugeReader.Filters;
    using GaugeReader.Filters.Models;
    using GaugeReader.Math.Models.Angles;
    using GaugeReader.Profiles.Models;
    using System;
    using System.Drawing;

    public class Thermometer : IProfile
    {
        public string Name => nameof(Thermometer);

        public RadiusZone CenterZone => new RadiusZone(0, 0.3);

        public RadiusZone MarkerZone => new RadiusZone(0.6, 0.8);

        public Angle MarkerAngle => (Constants.PI2 / 9) * 8;

        public Func<int, string> Reading => percent => $"{(((double)percent / 100) * 80) - 20} °C";

        public Convolution MarkerConvolution => new Convolution(Image.FromFile("Convolutions/Images/thermometer.png") as Bitmap);

        public IFilter MarkerFilter => new BrightnessCutoffFilter(0.4);

        public IFilter HandFilter => new RedFilter();
    }
}
