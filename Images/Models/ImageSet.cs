namespace GaugeReader.Images.Models
{
    using GaugeReader.Extensions;
    using GaugeReader.Filters.Models;
    using System.Drawing;

    public class ImageSet
    {
        public Bitmap OriginalImage { get; private set; }

        private ImageCrop crop { get; set; }

        public ImageSet(Bitmap image)
        {
            OriginalImage = image.ToBitmap();
        }

        public Bitmap GetFilteredImage(IFilter filter)
        {
            return crop == null ? OriginalImage.Filter(filter) : crop.Process(OriginalImage.Filter(filter));
        }

        public Bitmap GetUnfilteredImage()
        {
            return crop == null ? OriginalImage.Copy() : crop.Process(OriginalImage);
        }

        public void Crop(ImageCrop crop)
        {
            if (this.crop == null)
                this.crop = crop;
            else
            {
                crop.Rectangle = new Rectangle(new Point(this.crop.X + crop.X, this.crop.Y + crop.Y), crop.Size);
                this.crop = crop;
            }
        }

        public void Center(Point p)
        {
            if (this.crop == null)
                return;
            else
            {
                var newLocation = new Point(
                    crop.X + (p.X - (crop.Size.Width / 2)),
                    crop.Y + (p.Y - (crop.Size.Height / 2))
                );

                crop.Rectangle = new Rectangle(newLocation, crop.Size);
            }
        }

        public void Recrop(ImageCrop crop)
        {
            this.crop = crop;
        }
    }
}