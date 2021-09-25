namespace GaugeReader.Math.Models.Maps
{
    using System.Drawing;
    using GaugeReader.Images.Models;

    public abstract class Mapable : IImageable
    {
        public virtual Bitmap ToImage()
        {
            return ToMap().ToImage();
        }

        public abstract Map ToMap();
    }
}
