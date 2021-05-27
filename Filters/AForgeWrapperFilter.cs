namespace GaugeReader.Filters
{
    using GaugeReader.Extensions;
    using GaugeReader.Filters.Models;
    using System.Drawing;

    public abstract class AForgeWrapperFilter : IFilter
    {
        public abstract AForge.Imaging.Filters.IFilter Filter { get; }

        public abstract string Key { get; }

        public Bitmap Process(Bitmap image)
        {
            return Filter.Apply(image.ToProcessImage()).ToBitmap();
        }
    }
}
