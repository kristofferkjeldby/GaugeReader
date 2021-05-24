namespace GaugeReader.Processors
{
    using AForge.Imaging;
    using GaugeReader.Extensions;
    using GaugeReader.Models.Processors;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    public class HandLineProcessor : Processor
    {
        public override string Name => nameof(HandLineProcessor);

        public override void Process(ProcessorArgs args, ProcessorResult result)
        {
            var angles = new List<double>();

            var processImage = args.Profile.HandImage(args).Copy();
            processImage.MaskRadiusZone(args.Profile.CenterZone, Constants.MaskColor);

            HoughLineTransformation lineTransform = new HoughLineTransformation();
            lineTransform.ProcessImage(processImage.ToProcessImage());

            var line = lineTransform.GetMostIntensiveLines(500).
                Where(l => args.Profile.CenterZone.Contains(l, args.DialRadius)).
                OrderByDescending(l => l.Intensity).FirstOrDefault();

            if (line == null)
            {
                args.Aborted = true;
                return;
            }

            args.HandLine = line.ToLine();
            processImage.DrawLine(args.HandLine, Color.Green);
            AddDebugImage(processImage);
        }
    }
}
