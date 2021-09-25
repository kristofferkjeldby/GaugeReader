namespace GaugeReader.Filters
{
    using GaugeReader.Extensions;
    using GaugeReader.Filters.Models;
    using System.Drawing;

    public class FillFilter : IFilter
    {
        public Color Color { get; set; }

        public string Name => nameof(FillFilter);

        public FillFilter(Color color)
        {
            Color = color;
        }

        public Bitmap Process(Bitmap image)
        {
            Bitmap output = new Bitmap(image.Width, image.Height);

            for (int i = 0; i < output.Width; i++)
            {
                for (int x = 0; x < output.Height; x++)
                {
                    output.SetPixel(i, x, Color);
                }
            }

            return output;
        }
    }
}
