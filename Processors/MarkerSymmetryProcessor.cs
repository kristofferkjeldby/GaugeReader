namespace GaugeReader.Processors
{
    using GaugeReader.Convolutions.Models;
    using GaugeReader.Extensions;
    using GaugeReader.Math.Models.Angles;
    using GaugeReader.Processors.Models;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    public class MarkerSymmetryProcessor : Processor
    {
        public MarkerSymmetryProcessor(params string[] profileNames) : base(profileNames)
        {

        }

        public override string Name => nameof(MarkerSymmetryProcessor);

        public override void Process(ProcessorArgs args, ProcessorResult result)
        {
            var processImage = args.ImageSet.GetFilteredImage(args.Profile.MarkerFilter).DrawRadiusZone(args.Profile.MarkerZone, Constants.MaskColor);

            var debugImage = args.ImageSet.GetUnfilteredImage();

            // Create image of markers
            var angleMap = new AngleMap(processImage);
            var angleImage = angleMap.ToImage();

            AddDebugImage(angleImage, angleImage.Width, angleImage.Height);

            // Flip image of markers
            var flippedAngleImage = angleMap.ToFlippedAngleImage();
            var flippedAngleImageConvolution = new Convolution(flippedAngleImage);

            AddDebugImage(flippedAngleImage, flippedAngleImage.Width, flippedAngleImage.Height);

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

                debugImage.DrawRadialLine(startAngle, Color.Blue);
                debugImage.DrawRadialLine(endAngle, Color.Red);

                var convolution = angleMap.ToConvolution(angleSpan);
                var convolutionImage = convolution.ToImage();
                AddDebugImage(convolutionImage, convolutionImage.Width, 16);
                var match =  convolution.Symmetric();
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
