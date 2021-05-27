namespace GaugeReader.Processors
{
    using GaugeReader.Extensions;
    using GaugeReader.Math.Models.Angles;
    using GaugeReader.Processors.Models;
    using GaugeReader.Profiles.Models;
    using System.Drawing;
    using System.Linq;

    public class MarkerConvolutionProcessor : Processor
    {
        public override string Name => nameof(MarkerConvolutionProcessor);

        public MarkerConvolutionProcessor(params string[] profileNames) : base(profileNames)
        {

        }

        public override void Process(ProcessorArgs args, ProcessorResult result)
        {
            var zone = args.IsMarkerIsolated ? new RadiusZone(1 - args.Profile.MarkerZone.Width, 1) : args.Profile.MarkerZone;

            var processImage = args.ImageSet.GetFilteredImage(args.Profile.MarkerFilter).DrawRadiusZone(zone, Constants.MaskColor);
            AddDebugImage(processImage);
            var debugImage = args.ImageSet.GetUnfilteredImage();

            // Get convolution from profile
            var convolution = args.Profile.MarkerConvolution;
            var angleMap = new AngleMap(processImage);

            AddDebugImage(angleMap.ToImage(), 512, 16, null);
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
