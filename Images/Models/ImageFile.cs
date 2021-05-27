namespace GaugeReader.Images.Models
{
    using GaugeReader.Extensions;
    using System.Drawing;
    using System.IO;

    public class ImageFile
    {
        public ImageFile(string filepath)
        {
            Filepath = filepath;
        }

        public string Filepath { get; }

        public string Filename => Path.GetFileNameWithoutExtension(Filepath);

        public bool Exists()
        {
            return File.Exists(Filepath);
        }

        public Bitmap Load()
        {
            return (Image.FromFile(Filepath) as Bitmap).ToBitmap();
        }
    }
}
