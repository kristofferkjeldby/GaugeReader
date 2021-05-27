namespace GaugeReader.Images.Models
{
    using GaugeReader.Extensions;
    using System.Drawing;

    public class CircleImageCrop : ImageCrop
    {
        public CircleImageCrop(Rectangle rectangle) : base(rectangle)
        {

        }

        public override Bitmap Process(Bitmap input)
        {
            return input.MaskCircle(Rectangle, Constants.MaskColor).Crop(Rectangle);
        }
    }
}
