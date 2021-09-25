namespace GaugeReader.Processors
{
    using AForge.Imaging;
    using GaugeReader.Extensions;
    using GaugeReader.Filters;
    using GaugeReader.Images.Models;
    using GaugeReader.Math.Models.Circles;
    using GaugeReader.Processors.Models;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    public class DialProcessor : Processor
    {
        public override string Name => nameof(DialProcessor);

        public override void Process(ProcessorArgs args, ProcessorResult result)
        {
            var processImage = args.ImageSet.GetFilteredImage(new CannyFilter()).ToProcessImage();

            AddDebugImage(processImage);

            var radius = processImage.Width / 2;

            var maxRadius = (radius * args.Profile.DialZone.End).ToInt();
            var minRadius = (radius * args.Profile.DialZone.Start).ToInt();

            var circles = new List<Circle>();
            for (int i = minRadius; i < maxRadius; i++)
            {
                HoughCircleTransformation circleTransform = new HoughCircleTransformation(i);
                circleTransform.ProcessImage(processImage);
                circles.AddRange(circleTransform.GetMostIntensiveCircles(1).Select(c => new Circle(c, processImage)));
            }

            var bestCandidate = circles.OrderByDescending(c => c.Intensity).Take(3).OrderBy(c => c.R).FirstOrDefault();

            var debugImage = processImage.Copy().ToBitmap();
            debugImage.DrawCircle(bestCandidate, Color.Lime, false);

            AddDebugImage(debugImage);

            if (bestCandidate == null)
            {
                args.Aborted = true;
                return;
            }

            args.DialRadius = bestCandidate.R.ToInt();
            args.ImageSet.AddCrop(new CircleCrop(bestCandidate.ToRectangle(processImage)));
            AddDebugImage(args.ImageSet);
        }
    }
}
