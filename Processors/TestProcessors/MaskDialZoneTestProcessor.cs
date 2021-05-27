namespace GaugeReader.Processors.TestProcessors
{
    using GaugeReader.Extensions;
    using GaugeReader.Filters;
    using GaugeReader.Math.Models.Angles;
    using GaugeReader.Processors.Models;
    using System.Drawing;

    public class MaskDialZoneTestProcessor : Processor
    {
        public override string Name => nameof(MaskDialZoneTestProcessor);

        public override void Process(ProcessorArgs args, ProcessorResult result)
        {
            var output = args.ImageSet.GetUnfilteredImage();
            var dialZone = new RadiusZone(50d / 100d, 80d / 100d);
            output.MaskRadiusZone(dialZone, Color.Red);
            AddDebugImage(output);
        }
    }
}
