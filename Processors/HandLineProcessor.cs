namespace GaugeReader.Processors
{
    using AForge.Imaging;
    using GaugeReader.Extensions;
    using GaugeReader.Math.Models.Lines;
    using GaugeReader.Processors.Models;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    public class HandLineProcessor : Processor
    {
        public override string Name => nameof(HandLineProcessor);

        public override void Process(ProcessorArgs args, ProcessorResult result)
        {
            var angles = new List<double>();

            var processImage = args.ImageSet.GetFilteredImage(args.Profile.HandFilter);

            AddDebugImage(processImage.ToProcessImage());

            HoughLineTransformation lineTransform = new HoughLineTransformation();
            lineTransform.ProcessImage(processImage.ToProcessImage());

            var line = lineTransform.GetMostIntensiveLines(10).
                Select(l => new Line(l)).
                Where(l => args.Profile.CenterZone.Contains(l, args.DialRadius)).
                OrderByDescending(l => l.Intensity);

            if (line == null)
            {
                args.Aborted = true;
                return;
            }

            args.HandLine = line.FirstOrDefault();

            processImage.DrawLine(args.HandLine, Color.Green);

            AddDebugImage(processImage);
        }
    }
}
