namespace GaugeReader.Processors
{
    using AForge.Imaging;
    using GaugeReader.Extensions;
    using GaugeReader.Math.Models.Lines;
    using GaugeReader.Processors.Models;
    using System.Drawing;
    using System.Linq;

    public class HandPreprocessor : Processor
    {
        public override string Name => nameof(HandPreprocessor);

        public override void Process(ProcessorArgs args, ProcessorResult result)
        {
            var processImage = args.ImageSet.GetFilteredImage(args.Profile.HandFilter);

            AddDebugImage(processImage.ToProcessImage());

            HoughLineTransformation lineTransform = new HoughLineTransformation();
            lineTransform.ProcessImage(processImage.ToProcessImage());

            var lines = lineTransform.GetMostIntensiveLines(10).
                Select(l => new Line(l)).
                Where(l => args.Profile.CenterZone.Contains(l, args.DialRadius)).
                OrderByDescending(l => l.Intensity).ToArray();

            if (lines.Count() < 2)
            {
                args.Aborted = true;
                return;
            }

            args.Gauge.Hand.Line1 = lines[0];
            args.Gauge.Hand.Line2 = args.Profile.ArrowHand ? lines[1] : lines [0];

            processImage.DrawLine(args.Gauge.Hand.Line1, Color.Lime);
            processImage.DrawLine(args.Gauge.Hand.Line2, Color.Lime);

            AddDebugImage(processImage);
        }
    }
}
