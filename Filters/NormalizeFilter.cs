namespace GaugeReader.Filters
{
    using GaugeReader.Extensions;
    using GaugeReader.Filters.Models;
    using System.Drawing;

    public class NormalizeFilter : IFilter
    {
        public string Name => nameof(NormalizeFilter);

        public Bitmap Process(Bitmap image)
        {
            Bitmap normalized = new Bitmap(image.Width, image.Height);

            var maxBrightness = 0f;
            var minBrightness = 1f;

            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color c = image.GetPixel(x, y);
                    if (c == Constants.ImageMaskColor)
                        continue;
                    float brightness = c.GetBrightness();
                    if (brightness < minBrightness)
                        minBrightness = brightness;
                    if (brightness > maxBrightness)
                        maxBrightness = brightness;
                }
            }

            var factor = 1 / (maxBrightness - minBrightness);

            for (int i = 0; i < normalized.Width; i++)
            {
                for (int x = 0; x < normalized.Height; x++)
                {
                    Color c = image.GetPixel(i, x);
                    int grayScale = (((double)byte.MaxValue * (minBrightness + (c.GetBrightness() * factor)))).ToInt();
                    Color nc = Color.FromArgb(c.A, grayScale, grayScale, grayScale);
                    normalized.SetPixel(i, x, nc);
                }
            }

            return normalized;
        }
    }
}
