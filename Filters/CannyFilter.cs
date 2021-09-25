namespace GaugeReader.Filters
{
    using AForge.Imaging.Filters;

    public class CannyFilter : AForgeWrapperFilter
    {
        public override IFilter Filter => new CannyEdgeDetector();

        public override string Name => nameof(CannyEdgeDetector);
    }
}
