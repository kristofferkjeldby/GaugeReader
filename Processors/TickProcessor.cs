namespace GaugeReader.Processors
{
    using GaugeReader.Extensions;
    using GaugeReader.Filters;
    using GaugeReader.Math.Models.Angles;
    using GaugeReader.Processors.Models;
    using GaugeReader.Transformations;
    using System.Drawing;
    using System.Linq;

    public class TicksProcessor : Processor
    {
        public override string Name => nameof(TicksProcessor);

        public TicksProcessor(params string[] profileNames) : base(profileNames)
        {

        }

        public override void Process(ProcessorArgs args, ProcessorResult result)
        {
            var zone = args.Profile.MarkerZone;

            var processImage = args.ImageSet.GetFilteredImage(new CannyFilter()).DrawRadiusZone(zone, Constants.ImageMaskColor);

            var angleMapTransformation = new AngleMapTransformation();
            angleMapTransformation.ProcessImage(processImage);
            var map = angleMapTransformation.GetMap();

            // Adjust ticks to hit maxima 
            foreach (var tick in args.Gauge.Ticks)
            {
                var startStep = tick.AngleSpan.StartAngle.Step;
                var endStep = tick.AngleSpan.EndAngle.Step;
                var oldStep = tick.Angle.Step;

                // As we are using a Canny filter, we will have two maxima (tick start and tick end). To get a better result, we will use the average.
                var tickMap = map.GetAngleSpan(tick.AngleSpan);
                var maxStep = tickMap.Select((value, index) => new { index, value }).OrderByDescending(kv => kv.value).Take(2).Average(kv => kv.index).ToInt();

                var newStep = (startStep + maxStep) % Constants.AngleResolution;

                tick.Angle = new Angle(newStep);
                tick.Intensity = tickMap.Max();
                tick.Offset = newStep - oldStep;
            }

            foreach (var tick in args.Gauge.Ticks)
            {
                processImage.DrawRadialLine(tick.Angle, Color.Lime);
            }

            AddDebugImage(processImage.DrawRadiusZone(zone, Constants.ImageMaskColor));
        }
    }
}
