namespace GaugeReader.Models.Gauges
{
    using GaugeReader.Models.Angles;
    using GaugeReader.Models.Processors;
    using System;
    using System.Drawing;

    public class GaugeProfile
    {
        public string Name { get; set; }

        public RadiusZone CenterZone { get; set; }

        public RadiusZone MarkerZone { get; set; }

        public Angle MarkerAngle { get; set; }

        public Func<ProcessorArgs, Bitmap> MarkerImage { get; set; }

        public Func<ProcessorArgs, Bitmap> HandImage { get; set; }

        public Func<ProcessorArgs, Bitmap> DialImage { get; set; }

        public Func<int, string> Reading { get; set; }

        public Convolution MarkerConvolution { get; set; }
    }
}
