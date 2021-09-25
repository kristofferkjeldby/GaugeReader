namespace GaugeReader.Transformations
{
    using GaugeReader.Extensions;
    using GaugeReader.Math.Models.Maps;
    using System.Drawing;

    public class AngleMapTransformation
    {
        private Map map;

        public void ProcessImage(Bitmap image)
        {
            map = new Map(Constants.AngleResolution);

            var alphaMap = new double[Constants.AngleResolution, 2];

            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    var color = image.GetPixel(x, y);
                    var alpha = (double)color.A / byte.MaxValue;
                    var brightness = color.GetBrightness();
                    var angle = new Point(x, y).ToSphericalCoordinate(image).Theta;
                    var step = (angle / map.StepSize).ToInt() % Constants.AngleResolution;
                    alphaMap[step, 0] = alphaMap[step, 0] + alpha;
                    alphaMap[step, 1] = alphaMap[step, 1] + alpha * brightness;
                }
            }

            for (int i = 0; i < Constants.AngleResolution; i++)
            {
                map[i] = alphaMap[i, 1] / alphaMap[i, 0];
            }

            map.Normalize();
        }

        public Map GetMap()
        {
            return map;
        }
    }
}
