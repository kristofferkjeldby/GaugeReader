namespace GaugeReader.Images.Models
{
    using GaugeReader.Extensions;
    using System.Drawing;

    public class CircleCrop : Crop
    {
        public CircleCrop(Rectangle rectangle) : base(rectangle)
        {

        }

        public override Bitmap Process(Bitmap input)
        {
            return input.MaskCircle(Rectangle, Constants.ImageMaskColor).Crop(Rectangle);
        }
    }
}
