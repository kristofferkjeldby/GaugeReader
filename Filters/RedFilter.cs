namespace GaugeReader.Filters
{
    using GaugeReader.Filters.Models;
    using System.Drawing;

    public class RedFilter : IFilter
    {
        public string Name => nameof(RedFilter);

        public Bitmap Process(Bitmap input)
        {
            Bitmap output = new Bitmap(input.Width, input.Height);

            for (int i = 0; i < output.Width; i++)
            {
                for (int x = 0; x < output.Height; x++)
                {
                    Color c = input.GetPixel(i, x);
                    var r = c.R - c.G - c.B;
                    if (r < 0)
                        r = 0;
                    Color nc = Color.FromArgb(c.A, r, r, r);
                    output.SetPixel(i, x, nc);
                }
            }

            return output;
        }
    }
}
