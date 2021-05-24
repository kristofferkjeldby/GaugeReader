namespace GaugeReader.Processors
{
    using GaugeReader.Extensions;
    using GaugeReader.Models.Gauges;
    using GaugeReader.Models.Processors;

    public class CenterImageProcessor : Processor
    {
        public CenterImageProcessor(params GaugeProfile[] profiles) : base(profiles)
        {

        }

        public override string Name => nameof(CenterImageProcessor);

        public override void Process(ProcessorArgs args, ProcessorResult result)
        {
            args.UpdateImages(image => image.Center(args.HandLine.R * args.HandLine.Normal));
            AddDebugImage(args.ScaledImage);
        }
    }
}
