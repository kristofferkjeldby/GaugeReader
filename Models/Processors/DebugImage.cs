namespace GaugeReader.Models.Processors
{
    using System.Drawing;

    public class OutputImage
    {
        public OutputImage(Bitmap image, string caption, int width, int height)
        {
            Image = image;
            Caption = caption;
            Width = width;
            Height = height;
        }

        public Bitmap Image { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string Caption { get; set; }
    }
}
