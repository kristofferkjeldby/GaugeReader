namespace GaugeReader.Filters
{
    using GaugeReader.Extensions;
    using System.Drawing;

    public static class Filter
    {
        public static Bitmap Invert(Bitmap image)
        {
            Bitmap filtered = new Bitmap(image.Width, image.Height);

            for (int i = 0; i < filtered.Width; i++)
            {
                for (int x = 0; x < filtered.Height; x++)
                {
                    Color c = image.GetPixel(i, x);
                    int grayScale = (byte.MaxValue - ((double)byte.MaxValue * c.GetBrightness())).ToInt();
                    Color nc = Color.FromArgb(c.A, grayScale, grayScale, grayScale);
                    filtered.SetPixel(i, x, nc);
                }
            }

            return filtered;
        }


        public static Bitmap Red(Bitmap image)
        {
            Bitmap filtered = new Bitmap(image.Width, image.Height);

            for (int i = 0; i < filtered.Width; i++)
            {
                for (int x = 0; x < filtered.Height; x++)
                {
                    Color c = image.GetPixel(i, x);
                    var r = c.R - c.G - c.B;
                    if (r < 0)
                        r = 0;
                    Color nc = Color.FromArgb(c.A, r, r, r);
                    filtered.SetPixel(i, x, nc);
                }
            }

            return filtered;
        }

        public static Bitmap Normalize(Bitmap image)
        {
            Bitmap normalized = new Bitmap(image.Width, image.Height);

            var maxBrightness = 0f;
            var minBrightness = 1f;

            for (int i = 0; i < image.Width; i++)
            {
                for (int x = 0; x < image.Height; x++)
                {
                    Color c = image.GetPixel(i, x);
                    if (c == Constants.MaskColor)
                        continue;
                    float brightness = c.GetBrightness();
                    if (brightness < minBrightness)
                        minBrightness = brightness;
                    if (brightness > maxBrightness)
                        maxBrightness = brightness;
                }
            }

            var factor = 1/(maxBrightness - minBrightness);

            for (int i = 0; i < normalized.Width; i++)
            {
                for (int x = 0; x < normalized.Height; x++)
                {
                    Color c = image.GetPixel(i, x);
                    int grayScale = (((double)byte.MaxValue * (minBrightness +  (c.GetBrightness() * factor)))).ToInt();
                    Color nc = Color.FromArgb(c.A, grayScale, grayScale, grayScale);
                    normalized.SetPixel(i, x, nc);
                }
            }

            return normalized;

        }

        public static Bitmap Brightness(Bitmap image)
        {
            Bitmap filtered = new Bitmap(image.Width, image.Height);

            for (int i = 0; i < filtered.Width; i++)
            {
                for (int x = 0; x < filtered.Height; x++)
                {
                    Color c = image.GetPixel(i, x);
                    Color nc = c;
                    if (c.GetBrightness() > 0.3)
                        nc = Color.White;
                    
                    
                    filtered.SetPixel(i, x, nc);
                }
            }

            return filtered;
        }
    }
}
