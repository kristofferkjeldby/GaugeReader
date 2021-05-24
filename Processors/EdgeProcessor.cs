namespace GaugeReader.Processors
{
    using AForge.Imaging.Filters;
    using GaugeReader.Extensions;
    using GaugeReader.Models.Gauges;
    using GaugeReader.Models.Processors;

    public class EdgeProcessor : Processor
    {
        public override string Name => nameof(EdgeProcessor);

        public EdgeProcessor(params GaugeProfile[] profiles) : base(profiles)
        {

        }

        public override void Process(ProcessorArgs args, ProcessorResult result)
        {
            var processImage = args.ScaledImage.ToProcessImage();
            
            // Detect edges
            HomogenityEdgeDetector filter = new HomogenityEdgeDetector();
            filter.ApplyInPlace(processImage);
            args.EdgeImage = processImage.ToDrawImage();
            AddDebugImage(processImage);
        }
    }
}
