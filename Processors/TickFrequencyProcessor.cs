namespace GaugeReader.Processors
{
    using GaugeReader.Extensions;
    using GaugeReader.Filters;
    using GaugeReader.Gauge.Models;
    using GaugeReader.Processors.Models;
    using GaugeReader.Transformations;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    public class TickFrequencyProcessor : Processor
    {
        public override string Name => nameof(TickFrequencyProcessor);

        public TickFrequencyProcessor(params string[] profileNames) : base(profileNames)
        {

        }

        public override void Process(ProcessorArgs args, ProcessorResult result)
        {
            if (args.Profile.Correlation != null)
            {
                result.Skip();
                return;
            }

            var zone = args.Profile.MarkerZone;

            var processImage = args.ImageSet.GetFilteredImage(new CannyFilter()).DrawRadiusZone(zone, Constants.ImageMaskColor);

            var angleMapTransformation = new AngleMapTransformation();
            angleMapTransformation.ProcessImage(processImage);
            var map = angleMapTransformation.GetMap();

            List<TickAngleSpan> tickAngleSpans = new List<TickAngleSpan>();
            var count = args.Gauge.Ticks.Count();

            for (int i = 0; i < count; i++)
            {
                for (int j = 2; j <= count; j ++)
                {
                    if (i + j <= count)
                    {
                        tickAngleSpans.Add(new TickAngleSpan(args.Gauge.Ticks.Skip(i).Take(j).ToList()));
                    }
                    else
                    {
                        tickAngleSpans.Add(new TickAngleSpan((args.Gauge.Ticks.Skip(i).Take(count - i).Concat(args.Gauge.Ticks.Take(j - (count - i))).ToList())));
                    }
                }
            }

            foreach (var tick in args.Gauge.Ticks)
            {
                var tickOffset = tick.Offset;
                var previousTickRelativeOffset = System.Math.Abs(tick.Previous.Offset - tickOffset);
                var nextTickRelativeOffset = System.Math.Abs(tick.Next.Offset - tickOffset);
                tick.MiddleTickPoints = - (System.Math.Max(previousTickRelativeOffset, nextTickRelativeOffset));
                tick.StartTickPoints = - nextTickRelativeOffset;
                tick.EndTickPoints = -previousTickRelativeOffset;
            }

            var candidates = tickAngleSpans.
                Where(t => t.FuzzyIncludes(args.Gauge.Hand.Angle)).
                Where(t => t.Width > args.Profile.TicksAngle - Constants.DegreeDelta && t.Width < args.Profile.TicksAngle + Constants.DegreeDelta);

            var candidate = candidates.OrderByDescending(x => x.Points).FirstOrDefault();

            if (candidate == null)
            { 
                    args.Aborted = true;
                    return;
            }

            args.Gauge.TicksAngleSpan = candidate;

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

            foreach (var tickAngleSpan in candidates.OrderByDescending(x => x.Points).Take(1))
            {
                var ticks = tickAngleSpan.Ticks.ToList();
                tickAngleSpan.Ticks = ticks;

                var test = processImage.Copy();

                foreach (var tick in tickAngleSpan.Ticks)
                    test.DrawRadialLine(tick.Angle, Color.Lime);

                AddDebugImage(test, tickAngleSpan.Points.ToString());
            }
        }
    }
}
