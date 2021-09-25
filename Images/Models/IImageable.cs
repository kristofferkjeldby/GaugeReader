namespace GaugeReader.Images.Models
{
    using System.Drawing;

    public interface IImageable
    {
        Bitmap ToImage();
    }
}
