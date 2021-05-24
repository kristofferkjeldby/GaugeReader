namespace GaugeReader.Processors
{
    using GaugeReader.Extensions;
    using GaugeReader.Models.Angles;
    using GaugeReader.Models.Gauges;
    using GaugeReader.Models.Processors;
    using System.Drawing;
    using System.Linq;

    public class MarkerConvolutionProcessor : Processor
    {
        public MarkerConvolutionProcessor(params GaugeProfile[] profiles) : base(profiles)
        {

        }

        public override string Name => nameof(MarkerConvolutionProcessor);

        public override void Process(ProcessorArgs args, ProcessorResult result)
        {
            var processImage = args.Profile.MarkerImage(args).Copy().DrawRadiusZone(args.Profile.MarkerZone, Constants.MaskColor);
            AddDebugImage(processImage, null);
            var debugImage = args.ScaledImage.Copy();

            // Get convolution from profile
            var convolution = args.Profile.MarkerConvolution;
            var angleMap = new AngleMap(processImage);

            AddDebugImage(angleMap.ToImage(), null, 512, 16);
            var candidate = angleMap.GetMostIntesiveConvolutions(convolution, 1).First();

            if (candidate == null)
            {
                args.Aborted = true;
                return;
            }

            var bestCandidate = new AngleSpan(candidate.Angle + args.Profile.MarkerAngle.Complementary, candidate.Angle);

            debugImage.DrawRadialLine(bestCandidate.StartAngle, Color.Blue);
            debugImage.DrawRadialLine(bestCandidate.EndAngle, Color.Red);

            AddDebugImage(debugImage);
            args.MarkerAngleSpan = bestCandidate;

        }
    }
}
