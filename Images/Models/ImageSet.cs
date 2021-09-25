namespace GaugeReader.Images.Models
{
    using GaugeReader.Extensions;
    using GaugeReader.Filters.Models;
    using System.Drawing;

    public class ImageSet : IImageable
    {
        public Bitmap OriginalImage { get; private set; }

        public Crop Crop { get; set; }

        public ImageSet(Bitmap image)
        {
            OriginalImage = image.ToBitmap();
        }

        public Bitmap GetFilteredImage(IFilter filter)
        {
            return Crop == null ? OriginalImage.Filter(filter) : Crop.Process(OriginalImage.Filter(filter));
        }

        public Bitmap GetUnfilteredImage()
        {
            return Crop == null ? OriginalImage.Copy() : Crop.Process(OriginalImage);
        }

        public void AddCrop(Crop crop)
        {
            if (this.Crop == null)
                this.Crop = crop;
            else
            {
                crop.Rectangle = new Rectangle(new Point(this.Crop.X + crop.X, this.Crop.Y + crop.Y), crop.Size);
                this.Crop = crop;
            }
        }

        public void Recrop(Crop crop)
        {
            this.Crop = crop;
        }

        public Bitmap ToImage()
        {
            return GetUnfilteredImage();
        }
    }
}