namespace GaugeReader.Filters
{
    using AForge.Imaging.Filters;

    public class DifferenceEdgeFilter : AForgeWrapperFilter
    {
        public override IFilter Filter => new DifferenceEdgeDetector();

        public override string Key => nameof(DifferenceEdgeFilter);
    }
}
