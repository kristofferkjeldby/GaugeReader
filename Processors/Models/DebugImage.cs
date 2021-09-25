namespace GaugeReader.Processors.Models
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

        public OutputImage(Bitmap image, string caption) : this(image, caption, image.Width, image.Height)
        {
        }

        public OutputImage(Bitmap image) : this(image, string.Empty, image.Width, image.Height)
        {
        }

        public Bitmap Image { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string Caption { get; set; }
    }
}
