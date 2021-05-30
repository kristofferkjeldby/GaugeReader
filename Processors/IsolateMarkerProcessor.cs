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
    using Math.Models.Angles;

    public class IsolateMarkerProcessor : Processor
    {
        public IsolateMarkerProcessor(params string[] profileNames) : base(profileNames)
        {

        }

        public override string Name => nameof(IsolateMarkerProcessor);

        public override void Process(ProcessorArgs args, ProcessorResult result)
        {
            var scaleDownFactor = 2;

            // As this is a really labor intensive operation
            // We are going to do it on a scaled down version of 
            // the processing image.
            var processImage = args.ImageSet.GetFilteredImage(new EdgeFilter()).Factor(scaleDownFactor).ToProcessImage();

            AddDebugImage(processImage.ToBitmap().MaskRadiusZone(args.Profile.MarkerZone, Color.Red));

            var radius = processImage.Width / 2;

            var maxRadius = (radius * args.Profile.MarkerZone.End).ToInt();
            var minRadius = (radius * args.Profile.MarkerZone.Start).ToInt();

            var circles = new List<Circle>();
            for (int i = minRadius; i < maxRadius; i++)
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

            args.IsMarkerIsolated = true;

            args.ImageSet.Crop(new CircleImageCrop(bestCandidate.ToRectangle(processImage).Scale(scaleDownFactor)));

            AddDebugImage(args.ImageSet);
        }
    }
}
