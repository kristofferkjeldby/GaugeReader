namespace GaugeReader.Processors
{
    using GaugeReader.Extensions;
    using GaugeReader.Filters;
    using GaugeReader.Gauge.Models;
    using GaugeReader.Math.Models.Angles;
    using GaugeReader.Processors.Models;
    using GaugeReader.Transformations;
    using System.Drawing;
    using System.Linq;

    public class TicksPreprocessor : Processor
    {
        public override string Name => nameof(TicksPreprocessor);

        public TicksPreprocessor(params string[] profileNames) : base(profileNames)
        {

        }

        public override void Process(ProcessorArgs args, ProcessorResult result)
        {
            var zone = args.Profile.MarkerZone;

            var processImage = args.ImageSet.GetFilteredImage(new CannyFilter()).DrawRadiusZone(zone, Constants.ImageMaskColor);

            var angleMapTransformation = new AngleMapTransformation();
            angleMapTransformation.ProcessImage(processImage);
            var map = angleMapTransformation.GetMap();

            var frequencyTransformation = new FrequencyTransformation();
            frequencyTransformation.ProcessMap(map);
            var ticksFrequency = frequencyTransformation.GetMostInvensiveFrequencies(1).First();

            var ticks = Constants.PI2 / ticksFrequency.WaveLength;
            var angle = ticksFrequency.Phase;

            // Adding ticks
            for (int i = 0; i < ticks; i++)
            {
                args.Gauge.Ticks.Add(new Tick(angle));
                angle += ticksFrequency.WaveLength;
            }

            // Linking list
            for (int i = 0; i < args.Gauge.Ticks.Count; i++)
            {
                var previousIndex = (i == 0) ? args.Gauge.Ticks.Count - 1 : i - 1;
                var nextIndex = (i == args.Gauge.Ticks.Count - 1) ? 0 : i + 1;
                args.Gauge.Ticks[i].Next = args.Gauge.Ticks[nextIndex];
                args.Gauge.Ticks[i].Previous = args.Gauge.Ticks[previousIndex];
            }
        }
    }
}
