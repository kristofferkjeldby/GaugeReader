namespace GaugeReader.Processors
{
    using GaugeReader.Extensions;
    using GaugeReader.Images.Models;
    using GaugeReader.Math.Models.Angles;
    using GaugeReader.Processors.Models;
    using System;
    using System.Drawing;

    public class ResultProcessor : Processor
    {
        public override string Name => nameof(ResultProcessor);

        public override void Process(ProcessorArgs args, ProcessorResult result)
        {
            var processImage = args.ImageSet.GetUnfilteredImage().Resize(200);

            if (args.MarkerAngleSpan == null || args.HandAngle == null)
            {
                args.Abort();
                return;
            }

            processImage.DrawRadialLine(args.MarkerAngleSpan.StartAngle, Color.Blue);
            processImage.DrawRadialLine(args.MarkerAngleSpan.EndAngle, Color.Red);
            processImage.DrawRadialLine(args.HandAngle, Color.Green);

            var markedAngle = new AngleSpan(args.HandAngle, args.MarkerAngleSpan.EndAngle);
            args.ActualValue = ((markedAngle.Width / args.MarkerAngleSpan.Width) * 100).ToInt();

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

            processImage.DrawText(actualValueText, color);

            args.ResultImage = new ImageSet(processImage);
        }
    }
}
