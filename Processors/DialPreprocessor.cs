namespace GaugeReader.Processors
{
    using AForge.Imaging;
    using GaugeReader.Extensions;
    using GaugeReader.Filters;
    using GaugeReader.Images.Models;
    using GaugeReader.Math.Models.Circles;
    using GaugeReader.Processors.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class DialPreprocessor : Processor
    {
        public override string Name => nameof(DialPreprocessor);

        public override void Process(ProcessorArgs args, ProcessorResult result)
        {
            var scaleDownFactor = 2;

            var processImage = args.ImageSet.GetUnfilteredImage().Factor(scaleDownFactor).Filter(new EdgeFilter()).ToProcessImage();

            AddDebugImage(processImage);

            var radius = processImage.Width / 2;

            var maxRadius = (radius * Constants.SearchRadius.End).ToInt();
            var minRadius = (radius * Constants.SearchRadius.Start).ToInt();

            var circles = new List<Circle>();

            for (int i = minRadius; i < maxRadius; i += 5)
            {
                HoughCircleTransformation circleTransform = new HoughCircleTransformation(i);
                circleTransform.ProcessImage(processImage);
                circles.AddRange(circleTransform.GetMostIntensiveCircles(1).Select(c => new Circle(c, processImage)));
            }

            var bestCandidate = circles.OrderByDescending(c => c.Intensity).FirstOrDefault();

            if (bestCandidate == null)
            {
                args.Aborted = true;
                return;
            }

            args.ImageSet.AddCrop(new CircleCrop(bestCandidate.ToRectangle(processImage).Scale(scaleDownFactor)));

            AddDebugImage(args.ImageSet);
        }
    }
}
