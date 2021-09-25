namespace GaugeReader.Filters
{
    using AForge.Imaging.Filters;

    public class BrightnessFilter : AForgeWrapperFilter
    {
        public override IFilter Filter => new BrightnessCorrection();

        public override string Name => nameof(BrightnessFilter);
    }
}
