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

            var markedAngleSpan = new AngleSpan(args.Gauge.Hand.Angle, args.Gauge.TicksAngleSpan.EndAngle);
            var unmarkedAngleSpan = new AngleSpan(args.Gauge.TicksAngleSpan.StartAngle, args.Gauge.Hand.Angle);

            var edgeImage = args.ImageSet.GetFilteredImage(new CannyFilter()).DrawRadiusZone(args.Profile.MarkerZone, Constants.ImageMaskColor); ;

            var edgeOverlayMarkedImageMarked = edgeImage.Filter(new MaskFilter(Color.Pink)).MaskAngleSpan(markedAngleSpan.Opposite, Constants.ImageMaskColor);
            var edgeOverlayUnmarkedImageMarked = edgeImage.Filter(new MaskFilter(Color.Lime)).MaskAngleSpan(unmarkedAngleSpan.Opposite, Constants.ImageMaskColor);

            var tickOverlayMarkedImage = new Bitmap(edgeImage.Width, edgeImage.Height).Filter(new FillFilter(Color.FromArgb(50, Color.Red.R, Color.Red.G, Color.Red.B))).
                DrawRadiusZone(args.Profile.MarkerZone, Constants.ImageMaskColor).MaskAngleSpan(markedAngleSpan.Opposite, Constants.ImageMaskColor);

            var tickOverlayUnmarkedImage = new Bitmap(edgeImage.Width, edgeImage.Height).Filter(new FillFilter(Color.FromArgb(50, Color.Green.R, Color.Green.G, Color.Green.B))).
                DrawRadiusZone(args.Profile.MarkerZone, Constants.ImageMaskColor).MaskAngleSpan(unmarkedAngleSpan.Opposite, Constants.ImageMaskColor);

            edgeOverlayMarkedImageMarked.DrawRadialLine(args.Gauge.Hand.Angle, Color.FromArgb(100, Color.Yellow.R, Color.Yellow.G, Color.Yellow.B), 1);

            edgeOverlayMarkedImageMarked.SetResolution(resultImage.VerticalResolution, resultImage.HorizontalResolution);
            edgeOverlayUnmarkedImageMarked.SetResolution(resultImage.VerticalResolution, resultImage.HorizontalResolution);
            tickOverlayMarkedImage.SetResolution(resultImage.VerticalResolution, resultImage.HorizontalResolution);
            tickOverlayUnmarkedImage.SetResolution(resultImage.VerticalResolution, resultImage.HorizontalResolution);

            using (Graphics gr = Graphics.FromImage(resultImage))
            {
                gr.DrawImage(edgeOverlayMarkedImageMarked, new Point(args.ImageSet.Crop.X, args.ImageSet.Crop.Y));
                gr.DrawImage(edgeOverlayUnmarkedImageMarked, new Point(args.ImageSet.Crop.X, args.ImageSet.Crop.Y));
                gr.DrawImage(tickOverlayMarkedImage, new Point(args.ImageSet.Crop.X, args.ImageSet.Crop.Y));
                gr.DrawImage(tickOverlayUnmarkedImage, new Point(args.ImageSet.Crop.X, args.ImageSet.Crop.Y));
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
