namespace GaugeReader.Filters
{
    using GaugeReader.Extensions;
    using GaugeReader.Filters.Models;
    using System.Drawing;

    public class InvertFilter : IFilter
    {
        public string Key => nameof(InvertFilter);

        public Bitmap Process(Bitmap image)
        {
            Bitmap output = new Bitmap(image.Width, image.Height);

            for (int i = 0; i < output.Width; i++)
            {
                for (int x = 0; x < output.Height; x++)
                {
                    Color c = image.GetPixel(i, x);
                    int grayScale = (byte.MaxValue - ((double)byte.MaxValue * c.GetBrightness())).ToInt();
                    Color nc = Color.FromArgb(c.A, grayScale, grayScale, grayScale);
                    output.SetPixel(i, x, nc);
                }
            }

            return output;
        }
    }
}
