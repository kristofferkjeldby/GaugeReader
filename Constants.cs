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
        public static int DrawMargin = 10;
        public static Color MaskColor = Color.FromArgb(0, 0, 0, 0);

        // Deltas
        public const int DeltaDecimals = 2;
        public static string DeltaFormat = $"n{DeltaDecimals}";
        public static double Delta = System.Math.Pow(0.1f, DeltaDecimals);
        public static double DegreeDelta = 3d.ToRadians();

        // Constants
        public static double PI = System.Math.PI;
        public static double PI2 = PI * 2;

        // Search settins
        public static RadiusZone SearchRadius = new RadiusZone(0.2, 1); 
        public static int ScaleWidth = 600;
        public static double HandSearchAngle = PI / 8;

        public static int AngleResolution = 512;

        public static string FallbackProfileName = "Simple";
        public static string DefaultPath = "TestSets/Hygrometer/hygrometer_1_ex_48.jpg";


    }
}
