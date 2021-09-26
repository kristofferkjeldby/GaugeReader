namespace GaugeReader.Processors
{
    using GaugeReader.Extensions;
    using GaugeReader.Filters;
    using GaugeReader.Processors.Models;
    using GaugeReader.Transformations;
    using System.Linq;

    public class TickCorrelationProcessor : Processor
    {
        public override string Name => nameof(TickCorrelationProcessor);

        public TickCorrelationProcessor(params string[] profileNames) : base(profileNames)
        {

        }

        public override void Process(ProcessorArgs args, ProcessorResult result)
        {
            if (args.Profile.Correlation == null)
            {
                result.Skip();
                return;
            }

            var zone = args.Profile.MarkerZone;

            var processImage = args.ImageSet.GetFilteredImage(new EdgeFilter()).DrawRadiusZone(zone, Constants.ImageMaskColor);

            AddDebugImage(processImage);

            var angleMapTransformation = new AngleMapTransformation();
            angleMapTransformation.ProcessImage(processImage);
            var map = angleMapTransformation.GetMap();
            var sample = args.Profile.Correlation.ToMap();

            var correlationTransformation = new CorrelationTransformation(map);
            correlationTransformation.ProcessSample(sample);

            var correlation = correlationTransformation.GetMostIntensiveCorrelations(1).First();

            AddDebugImage(correlation);

            var candidate = correlation.AngleSpan.Opposite;

            var handAngle = args.Gauge.Hand.Angle;

            if (!candidate.Includes(handAngle))
            {
                if (handAngle.FuzzyEquals(candidate.StartAngle))
                {
                    candidate.StartAngle = handAngle;
                }

                if (handAngle.FuzzyEquals(candidate.EndAngle))
                {
                    candidate.EndAngle = handAngle;
                }
            }

            args.Gauge.TicksAngleSpan = candidate;
        }
    }
}
