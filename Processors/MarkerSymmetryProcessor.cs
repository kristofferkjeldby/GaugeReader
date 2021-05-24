namespace GaugeReader.Processors
{
    using GaugeReader.Extensions;
    using GaugeReader.Models.Angles;
    using GaugeReader.Models.Gauges;
    using GaugeReader.Models.Processors;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    public class MarkerSymmetryProcessor : Processor
    {
        public MarkerSymmetryProcessor(params GaugeProfile[] profiles) : base(profiles)
        {

        }

        public override string Name => nameof(MarkerSymmetryProcessor);

        public override void Process(ProcessorArgs args, ProcessorResult result)
        {
            var processImage = args.Profile.MarkerImage(args).DrawRadiusZone(args.Profile.MarkerZone, Constants.MaskColor);
            var debugImage = args.ScaledImage.Copy();

            // Create image of markers
            var angleMap = new AngleMap(processImage);
            var angleImage = angleMap.ToImage();

            // Flip image of markers
            var flippedAngleImage = angleMap.ToFlippedAngleImage();
            var flippedAngleImageConvolution = new Convolution(flippedAngleImage);

            // Analyse image using a mirror convolution
            var candidates = new List<Angle>();
            foreach (var c in angleMap.GetMostIntesiveConvolutions(flippedAngleImageConvolution, 15))
            { 
                candidates.Add(c.Angle + c.Angle.Complementary.Half);
            }

            // Rate candidates using a multiple of methods
            var ratedCandidates = new Dictionary<AngleSpan, double>();
            foreach (var a in candidates)
            {
                var middleAngle = a;
                var startAngle = middleAngle - args.Profile.MarkerAngle.Complementary.Half;
                var endAngle = middleAngle + args.Profile.MarkerAngle.Complementary.Half;
                var angleSpan = new AngleSpan(startAngle, endAngle);
                if (angleSpan.FuzzyIncludes(args.HandAngle))
                    continue;

                var convolution = angleMap.GetConvolution(angleSpan);
                var convolutionImage = convolution.ToImage();
                var match =  convolution.Symmetric() * -convolution.Dark();
                ratedCandidates.Add(angleSpan, match);
            }

            if (!ratedCandidates.Any())
            {
                args.Aborted = true;
                return;
            }

            var bestCandidate = ratedCandidates.OrderByDescending(x => x.Value).FirstOrDefault().Key.Opposite;

            debugImage.DrawRadialLine(bestCandidate.StartAngle, Color.Blue);
            debugImage.DrawRadialLine(bestCandidate.EndAngle, Color.Red);

            args.MarkerAngleSpan = bestCandidate;

            AddDebugImage(debugImage);
        }
    }
}
