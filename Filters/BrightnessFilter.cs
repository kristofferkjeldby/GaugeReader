namespace GaugeReader.Filters
{
    using GaugeReader.Filters.Models;
    using System.Drawing;

    public class BrightnessCutoffFilter : IFilter
    {
        private double cutoff;

        public BrightnessCutoffFilter(double cutoff)
        {
            this.cutoff = cutoff;
        }

        public string Key => nameof(BrightnessCutoffFilter);

        public Bitmap Process(Bitmap input)
        {
            Bitmap output = new Bitmap(input.Width, input.Height);

            for (int i = 0; i < output.Width; i++)
            {
                for (int x = 0; x < output.Height; x++)
                {
                    Color c = input.GetPixel(i, x);
                    Color nc = c;
                    if (c.GetBrightness() > cutoff)
                        nc = Color.White;
                    else
                        nc = Color.Black;

                    output.SetPixel(i, x, nc);
                }
            }

            return output;
        }
    }
}
