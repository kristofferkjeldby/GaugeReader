using System.Drawing;

namespace GaugeReader.Images.Models
{
    public abstract class ImageCrop
    {
        public Rectangle Rectangle { get; set; }

        public ImageCrop(Rectangle rectangle)
        {
            Rectangle = rectangle;
        }

        public abstract Bitmap Process(Bitmap input);

        public int X => Rectangle.X;

        public int Y => Rectangle.Y;

        public Size Size => Rectangle.Size;
    }
}
