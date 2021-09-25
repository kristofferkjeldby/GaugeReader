namespace GaugeReader.Filters.Models
{
    using System.Drawing;

    public interface IFilter
    {
        Bitmap Process(Bitmap image);

        string Name { get; }
    }
}
