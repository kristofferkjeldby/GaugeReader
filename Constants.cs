namespace GaugeReader
{
    using GaugeReader.Extensions;
    using GaugeReader.Filters;
    using GaugeReader.Models.Angles;
    using GaugeReader.Models.Gauges;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;

    public static class Constants
    {
        // Image settings
        public static PixelFormat DrawFormat = PixelFormat.Format24bppRgb;
        public static PixelFormat ProcessFormat = PixelFormat.Format8bppIndexed;
        public static int DrawMargin = 10;
        public static Color MaskColor = Color.FromArgb(0, 0, 0, 0);

        // Deltas
        public const int DeltaDecimals = 2;
        public static string DeltaFormat = $"n{DeltaDecimals}";
        public static double Delta = Math.Pow(0.1f, DeltaDecimals);
        public static double DegreeDelta = 3d.ToRadians();

        // Constants
        public static double PI = Math.PI;
        public static double PI2 = PI * 2;

        // Search settins
        public static int ScaleHeight = 600;
        public static double HandSearchAngle = PI / 8;

        public static int AngleResolution = 512;

        public static GaugeProfile Simple = new GaugeProfile()
        {
            Name = "Simple",
            CenterZone = new RadiusZone(0, 0.2),
            MarkerAngle = 270d.ToRadians(),
            MarkerZone = new RadiusZone(0.5, 0.7),
            MarkerImage = args => args.InvertedImage,
            HandImage = args => args.EdgeImage,
            DialImage = args => args.EdgeImage,
            Reading = percent => $"{percent}%"
        };

        public static GaugeProfile Themometer = new GaugeProfile()
        {
            Name = "Thermometer",
            CenterZone = new RadiusZone(0, 0.3),
            MarkerAngle = (PI2 / 9) * 8,
            MarkerZone = new RadiusZone(0.9, 1),
            MarkerImage = (args) =>
            {
                return Filter.Brightness(args.ScaledImage);
            },
            HandImage = args => Filter.Red(args.ScaledImage),
            DialImage = args => args.EdgeImage,
            Reading = percent => $"{(((double)percent / 100) * 80) - 20} °C",
            MarkerConvolution = new Convolution(Image.FromFile("Convolutions/thermometer.png") as Bitmap)
        };
    }
}
