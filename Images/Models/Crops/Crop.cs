namespace GaugeReader.Images.Models
{
    using System.Drawing;

    public abstract class Crop
    {
        public Rectangle Rectangle { get; set; }

        public Crop(Rectangle rectangle)
        {
            Rectangle = rectangle;
        }

        public abstract Bitmap Process(Bitmap input);

        public int X => Rectangle.X;

        public int Y => Rectangle.Y;

        public Size Size => Rectangle.Size;
    }
}
