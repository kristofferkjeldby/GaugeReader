namespace GaugeReader.Math.Models.Maps
{
    using GaugeReader.Extensions;
    using GaugeReader.Images.Models;
    using GaugeReader.Math.Models.Angles;
    using GaugeReader.Math.Models.Maps;
    using System.Drawing;

    public class Correlation : IImageable
    {
        public Correlation(Angle angle, Map map, Map sampleMap, double intensity)
        {
            Angle = angle;
            Map = map;
            SampleMap = sampleMap;
            Intensity = intensity;
        }

        Map Map { get; }

        Map SampleMap { get; }

        public Angle Angle { get; set; }

        public double Intensity { get; set; }

        public Bitmap ToImage()
        {
            var image = new Bitmap(Map.Length, 2 * Constants.ImageMapHeight);

            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < Constants.ImageMapHeight; y++)
                {
                    image.SetPixel(x, y, Map[x].ToColor());
                }
            }

            for (int x = 0; x < SampleMap.Length; x++)
            {
                for (int y = Constants.ImageMapHeight; y < 2 * Constants.ImageMapHeight; y++)
                {
                    image.SetPixel((x + Angle.Step) % Map.Length, y, x == 0 ? Color.Red : SampleMap[x].ToColor());
                }
            }

            return image;
        }
    }
}
