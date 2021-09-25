namespace GaugeReader.Filters
{
    using GaugeReader.Extensions;
    using GaugeReader.Filters.Models;
    using System.Drawing;

    public class ChainFilter : IFilter
    {
        private IFilter[] filters;

        public ChainFilter(params IFilter[] filters)
        {
            this.filters = filters;
        }

        public string Name => nameof(RedFilter);

        public Bitmap Process(Bitmap input)
        {
            var image = input.Copy();
            foreach (var filter in filters)
                image = filter.Process(image);
            return image;
        }
    }
}
