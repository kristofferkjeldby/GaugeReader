namespace GaugeReader.Processors
{
    using GaugeReader.Extensions;
    using GaugeReader.Filters;
    using GaugeReader.Processors.Models;
    using GaugeReader.Transformations;
    using System.Drawing;
    using System.Linq;

    public class MarkersSymmetriProcessor : Processor
    {
        public override string Name => nameof(MarkersSymmetriProcessor);

        public MarkersSymmetriProcessor(params string[] profileNames) : base(profileNames)
        {

        }

        public override void Process(ProcessorArgs args, ProcessorResult result)
        {
            var zone = args.Profile.MarkerZone;

            var processImage = args.ImageSet.GetFilteredImage(new EdgeFilter()).DrawRadiusZone(zone, Constants.ImageMaskColor);

            var angleMapTransformation = new AngleMapTransformation();
            angleMapTransformation.ProcessImage(processImage);
            var map = angleMapTransformation.GetMap();
            var sample = map.Reverse();

            var correlationTransformation = new CorrelationTransformation(map);
            correlationTransformation.ProcessSample(sample);
            
            var correlation = correlationTransformation.GetMostIntensiveCorrelations(1).First();

            args.Gauge.SymmetriAxis = correlation.Angle / 2;

            var debugImage = args.ImageSet.GetUnfilteredImage();
            debugImage.DrawRadialLine(args.Gauge.SymmetriAxis, Color.Lime);
            debugImage.DrawRadialLine(args.Gauge.SymmetriAxis, Color.Lime);
            AddDebugImage(debugImage);
        }
    }
}
