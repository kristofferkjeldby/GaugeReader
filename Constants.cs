namespace GaugeReader
{
    using GaugeReader.Extensions;
    using GaugeReader.Math.Models.Angles;
    using System.Drawing;
    using System.Drawing.Imaging;

    public static class Constants
    {
        // Image settings
        public static PixelFormat BitmapFormat = PixelFormat.Format32bppArgb;
        public static PixelFormat ProcessFormat = PixelFormat.Format8bppIndexed;
        public static int ImageDrawMargin = 10;
        public static Color ImageMaskColor = Color.FromArgb(0, 0, 0, 0);
        public static int ImageMapHeight = 16;

        // Deltas
        public const int DeltaDecimals = 2;
        public static string DeltaFormat = $"n{DeltaDecimals}";
        public static double Delta = System.Math.Pow(0.1f, DeltaDecimals);
        public static double DegreeDelta = 4d.ToRadians();

        // Angles
        public static double PI = System.Math.PI;
        public static double PI2 = PI * 2;
        public static int AngleResolution = 1024;
        public static Angle AngleStepSize = new Angle(PI2 / AngleResolution);


        // Search settings
        public static RadiusZone SearchRadius = new RadiusZone(0.2, 1); 
        public static int ScaleWidth = 600;
        public static double HandSearchAngle = PI / 8;
        public static int MinTicks = 12;

        public static string FallbackProfileName = "Simple";
        public static string DefaultPath = @"TestSets\Thermometer\thermometer_8_ex_57.jpg";

    }
}
