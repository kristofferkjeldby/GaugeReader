namespace GaugeReader.Processors.Models
{
    using System.Drawing;

    public class OutputImage
    {
        public OutputImage(Bitmap image, int width, int height, string caption)
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
