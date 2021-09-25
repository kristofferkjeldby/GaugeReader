namespace GaugeReader.Filters
{
    using AForge.Imaging.Filters;

    public class EdgeFilter : AForgeWrapperFilter
    {
        public override IFilter Filter => new HomogenityEdgeDetector();

        public override string Name => nameof(EdgeFilter);
    }
}
