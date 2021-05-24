namespace GaugeReader.Processors.TestProcessors
{
    using GaugeReader.Extensions;
    using GaugeReader.Models.Angles;
    using GaugeReader.Models.Processors;
    using System.Drawing;

    public class MaskDialZoneTestProcessor : Processor
    {
        public override string Name => nameof(MaskDialZoneTestProcessor);

        public override void Process(ProcessorArgs args, ProcessorResult result)
        {
            var processImage = args.EdgeImage;;
            var dialZone = new RadiusZone(50d / 100d, 80d / 100d);
            processImage.MaskRadiusZone(dialZone, Color.Red);
            AddDebugImage(processImage);
        }
    }
}
