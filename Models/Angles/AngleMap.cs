namespace GaugeReader.Models.Angles
{
    using AForge.Math;
    using GaugeReader.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    public class AngleMap
    {
        private Angle stepSize = new Angle(Constants.PI2 / Constants.AngleResolution);

        public double[] Map { get; set; }

        //private Complex[] frequencyMap;

        public AngleMap(Bitmap image)
        {
            var map = new double[Constants.AngleResolution, 2];

            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    var color = image.GetPixel(x, y);
                    var alpha = (double)color.A / byte.MaxValue;
                    var brightness = color.GetBrightness();
                    var angle = new Point(x, y).ToSphericalCoordinate(image).Theta;
                    var step = (angle / stepSize).ToInt() % Constants.AngleResolution;
                    map[step, 0] = map[step, 0] + alpha;
                    map[step, 1] = map[step, 1] + alpha * brightness;
                }
            }

            Map = new double[Constants.AngleResolution];

            for(int i = 0; i < Constants.AngleResolution; i++)
            {
                Map[i] = map[i, 1] / map[i, 0];
            }


            Map = Map.Normalize(1);
        }

        public AngleMap(double[] map)
        {
            Map = map.Normalize();
        }

        public List<AngleMapAngle> GetAngles()
        {
            var maxIntensity = Map.Max();
            var angles = new List<AngleMapAngle>();

            for (int i = 0; i < Map.Length; i++)
            {
                var value = Map[i];
                var angle = new Angle(i * stepSize);
                var relativeIntensity = value / maxIntensity;
                angles.Add(new AngleMapAngle(angle, value, relativeIntensity));
            }

            return angles;
        }

        public Convolution GetConvolution(AngleSpan angleSpan)
        {
            var startStep = (angleSpan.StartAngle / stepSize).ToInt();
            var endStep = (angleSpan.EndAngle / stepSize).ToInt();

            if (endStep < startStep)
                endStep += Map.Length;

            var steps = 128; // Todo - fix this

            var map = new double[steps];

            for (int step = 0; step < steps; step++)
            {
                map[step] = Map[(startStep + step) % Map.Length];
            }

            return new Convolution(map);
        }

        public List<AngleMapConvolution> GetConvolutions(Convolution convolution)
        {
            var angleMapConvolutions = new List<AngleMapConvolution>();

            for (var i = 0; i < Map.Length; i++)
                angleMapConvolutions.Add(GetConvolution(convolution, i));

            var maxIntensity = angleMapConvolutions.Max(c => c.Intensity);

            for (var i = 0; i < angleMapConvolutions.Count; i++)
                angleMapConvolutions[i].RelativeIntensity = angleMapConvolutions[i].Intensity / maxIntensity;

            return angleMapConvolutions;
        }

        private AngleMapConvolution GetConvolution(Convolution convolution, int step)
        {
            var angleMapConvolution = new AngleMapConvolution(new Angle(step * stepSize));

            for (int x = 0; x < convolution.Map.Length; x++)
            {
                var exceptedValue = convolution.Map[x];
                var actualValue = Map[(step + x) % Map.Length];

                var diff = Math.Pow(Math.Abs(actualValue - exceptedValue), 1);

                angleMapConvolution.Intensity -= diff;
            }

            return angleMapConvolution;
        }

        //public List<AngleMapFrequency> GetFrequencies()
        //{
        //    var maxIntensity = frequencyMap.Max(c => c.Magnitude);
        //    var frequencies = new List<AngleMapFrequency>();

        //    for (int i = 0; i < frequencyMap.Length; i++)
        //    {
        //        if (i == 0)
        //            frequencies.Add(new AngleMapFrequency(new Angle(Constants.PI2), 0, 0, 0, 0));

        //        var value = frequencyMap[i];
        //        var waveLength = new Angle(Constants.PI2 / (i));
        //        var phase = new Angle(value.Phase);
        //        var relativeIntensity = value.Magnitude / maxIntensity;
        //        frequencies.Add(new AngleMapFrequency(waveLength, phase, value.Magnitude, value.Magnitude, relativeIntensity));
        //    }

        //    return frequencies;
        //}

        public AngleMapAngle[] GetMostIntesiveAngles(int count)
        {
            return GetAngles().OrderByDescending(a => a.Intensity).Take(count).ToArray();
        }

        public AngleMapConvolution[] GetMostIntesiveConvolutions(Convolution convolution, int count)
        {
            return GetConvolutions(convolution).OrderByDescending(a => a.Intensity).Take(count).ToArray();
        }

        //public AngleMapFrequency[] GetMostIntesiveFrequencies(int count)
        //{
        //    return GetFrequencies().OrderByDescending(a => a.Intensity).Take(count).OrderByDescending(f => f.WaveLength).ToArray();
        //}

        public Bitmap ToImage(int height = 16, AngleSpan angleSpan = null)
        {
            var angles = GetAngles();

            var image = new Bitmap(angles.Count, height);

            for (int x = 0; x < image.Width; x++)
            {
                var color = angles[x].Color;
                for (int y = 0; y < height; y++)
                {
                    image.SetPixel(x, y, color);
                }
            }

            return image;
        }

        public Bitmap ToFlippedAngleImage(int height = 16)
        {
            var angles = GetAngles();

            var image = new Bitmap(angles.Count, height);

            for (int x = 0; x < image.Width; x++)
            {
                var color = angles[angles.Count - 1 -x].Color;
                for (int y = 0; y < height; y++)
                {
                    image.SetPixel(x, y, color);
                }
            }

            return image;
        }

        //public Bitmap ToFrequencyImage(int height = 16)
        //{
        //    var frequencies = GetFrequencies();

        //    var image = new Bitmap(frequencies.Count, height);

        //    for (int x = 0; x < image.Width; x++)
        //    {
        //        var color = frequencies[x].Color;
        //        for (int y = 0; y < height; y++)
        //        {
        //            image.SetPixel(x, y, color);
        //        }
        //    }

        //    return image;
        //}
    }
}
