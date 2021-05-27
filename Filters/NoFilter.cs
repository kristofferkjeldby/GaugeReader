namespace GaugeReader.Filters
{
    using GaugeReader.Extensions;
    using GaugeReader.Filters.Models;
    using System.Drawing;

    public class NoFilter : IFilter
    {
        public string Key => nameof(RedFilter);

        public Bitmap Process(Bitmap input)
        {
            return input.Copy();
        }
    }
}
