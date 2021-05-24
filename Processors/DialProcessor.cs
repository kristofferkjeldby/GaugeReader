namespace GaugeReader.Processors
{
    using AForge.Imaging;
    using GaugeReader.Extensions;
    using GaugeReader.Models.Angles;
    using GaugeReader.Models.Gauges;
    using GaugeReader.Models.Processors;
    using System.Collections.Generic;
    using System.Linq;

    public class DialProcessor : Processor
    {
        RadiusZone RadiusZone { get; set; }

        public DialProcessor(RadiusZone radiusZone, params GaugeProfile[] profiles) : base(profiles)
        {
            RadiusZone = radiusZone;
        }

        public override string Name => nameof(DialProcessor);

        public override void Process(ProcessorArgs args, ProcessorResult result)
        {
            // As this is a really labor intensive operation
            // We are going to do it on a scaled down version of 
            // the processing image.
            var processImage = args.Profile.DialImage(args).Resize(args.EdgeImage.Width / 4, args.EdgeImage.Height / 4).ToProcessImage();

            var radius = processImage.Width / 2;

            var maxRadius = (radius * RadiusZone.End).ToInt();
            var minRadius = (radius * RadiusZone.Start).ToInt();

            var circles = new List<HoughCircle>();
            for (int i = minRadius; i < maxRadius; i++)
            {
                HoughCircleTransformation circleTransform = new HoughCircleTransformation(i);
                circleTransform.ProcessImage(processImage);
                circles.AddRange(circleTransform.GetMostIntensiveCircles(1));
            }

            var circle = circles.OrderByDescending(c => c.Intensity).FirstOrDefault();

            if (circle == null)
            {
                args.Aborted = true;
                return;
            }

            args.DialRadius = circle.Radius;

            var rectangle = circle.GetRectangle(4);

            args.UpdateImages(image => image.MaskCircle(rectangle, Constants.MaskColor).Crop(rectangle));

            AddDebugImage(args.ScaledImage);
        }
    }
}
