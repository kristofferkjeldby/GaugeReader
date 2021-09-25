namespace GaugeReader.Filters
{
    using GaugeReader.Extensions;
    using GaugeReader.Filters.Models;
    using System.Drawing;

    public class MaskFilter : IFilter
    {
        public Color Color { get; set; }

        public string Name => nameof(MaskFilter);

        public MaskFilter(Color color)
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
                    Color c = image.GetPixel(i, x);
                    Color nc = Color.FromArgb((c.GetBrightness() * byte.MaxValue).ToInt(), Color.R, Color.G, Color.B);
                    output.SetPixel(i, x, nc);
                }
            }

            return output;
        }
    }
}
