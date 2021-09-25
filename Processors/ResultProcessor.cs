namespace GaugeReader.Processors
{
    using GaugeReader.Extensions;
    using GaugeReader.Filters;
    using GaugeReader.Math.Models.Angles;
    using GaugeReader.Processors.Models;
    using System;
    using System.Drawing;

    public class ResultProcessor : Processor
    {
        public override string Name => nameof(ResultProcessor);

        public override void Process(ProcessorArgs args, ProcessorResult result)
        {
            var resultImage = args.ImageSet.OriginalImage.ToBitmap();
            var edgeOverlayImage = args.ImageSet.GetFilteredImage(new CannyFilter()).Filter(new MaskFilter(Color.Lime)).DrawRadiusZone(args.Profile.MarkerZone, Constants.ImageMaskColor);

            if (args.Gauge.Hand.Angle == null)
            {
                args.Abort();
                return;
            }

            edgeOverlayImage.DrawRadialLine(args.Gauge.Hand.Angle, Color.Red, 3);

            var tickOverlayImage = new Bitmap(edgeOverlayImage.Width, edgeOverlayImage.Height).Filter(new FillFilter(Color.FromArgb(50, Color.Green.R, Color.Green.G, Color.Green.B))).
                DrawRadiusZone(args.Profile.MarkerZone, Constants.ImageMaskColor).MaskAngleSpan(args.Gauge.TicksAngleSpan.Opposite, Constants.ImageMaskColor);


            edgeOverlayImage.SetResolution(resultImage.VerticalResolution, resultImage.HorizontalResolution);
            tickOverlayImage.SetResolution(resultImage.VerticalResolution, resultImage.HorizontalResolution);

            using (Graphics gr = Graphics.FromImage(resultImage))
            {
                gr.DrawImage(tickOverlayImage, new Point(args.ImageSet.Crop.X, args.ImageSet.Crop.Y));
                gr.DrawImage(edgeOverlayImage, new Point(args.ImageSet.Crop.X, args.ImageSet.Crop.Y));
            }


            var markedAngle = new AngleSpan(args.Gauge.Hand.Angle, args.Gauge.TicksAngleSpan.EndAngle);
            args.ActualValue = ((markedAngle.Width / args.Gauge.TicksAngleSpan.Width) * 100).ToInt();

            var color = Color.Yellow;
            var actualValueText = args.Profile.Reading(args.ActualValue.GetValueOrDefault(-1));

            if (args.ExpectedValue.HasValue)
            {
                var expectedValueText = args.Profile.Reading(args.ExpectedValue.GetValueOrDefault(-1));

                if (Math.Abs(args.ExpectedValue.GetValueOrDefault() - args.ActualValue.GetValueOrDefault()) < 5)
                {
                    args.Passed = true;
                    color = Color.Green;
                    actualValueText += " - Passed";
                }
                else
                {
                    args.Passed = true;
                    color = Color.Red;
                    actualValueText += $" - Failed ({expectedValueText} expected)";
                }
            }

            resultImage.DrawText(actualValueText, color);

            args.ResultImage = resultImage;
        }
    }
}
