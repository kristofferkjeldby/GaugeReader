namespace GaugeReader.Models.Processors
{
    using GaugeReader.Models.Angles;
    using GaugeReader.Models.Gauges;
    using GaugeReader.Models.Lines;
    using System;
    using System.Drawing;

    public class ProcessorArgs
    {
        public ProcessorArgs()
        {
            Aborted = false;
        }

        public Line HandLine { get; set; }

        public Angle HandAngle { get; set; }

        public GaugeProfile Profile { get; set; }

        public Bitmap OriginalImage { get; set; }

        public Bitmap ScaledImage { get; set; }

        public Bitmap EdgeImage { get; set; }

        public Bitmap ResultImage { get; set; }

        public Bitmap InvertedImage { get; set; }

        public AngleMap MarkerMap { get; set; }

        public string Path { get; set; }

        public bool Aborted { get; set; }

        public int DialDiameter => DialRadius * 2;

        public int DialRadius { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public Angle Theta1 { get; set; }

        public Angle Theta2 { get; set; }

        public double R1 { get; set; }

        public double R2 { get; set; }

        public void UpdateImages(Func<Bitmap, Bitmap> func)
        {
            EdgeImage = func(EdgeImage);
            InvertedImage = func(InvertedImage);
            ScaledImage = func(ScaledImage);
        }

        public AngleSpan MarkerAngleSpan { get; set; }

        public int? ExpectedValue { get; set; }

        public int? ActualValue { get; set; }

        public bool? Passed { get; set; }
    }
}
