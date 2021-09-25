namespace GaugeReader.Math.Models.Maps
{
    using GaugeReader.Extensions;
    using GaugeReader.Images.Models;
    using GaugeReader.Math.Models.Angles;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    public class Map : Mapable, IEnumerable<double>, IImageable
    {
        public Map(Map map)
        {
            this.InnerMap = map.InnerMap;
        }

        public Map(IEnumerable<double> map)
        {
            this.InnerMap = map.ToArray();
        }

        public Map(int capacity)
        {
            this.InnerMap = new double[capacity];
            for (int i = 0; i < Length; i++)
            {
                this[i] = -1;
            }
        }

        public Angle StepSize = new Angle(Constants.PI2 / Constants.AngleResolution);

        public int Length => InnerMap.Length;

        public Angle Width => StepSize * Length;

        public double[] InnerMap { get; set; }

        public Map GetAngleSpan(int startStep, int endStep)
        {
            startStep = GetIndex(startStep);
            endStep = GetIndex(endStep);

            return (startStep < endStep) ?
                new Map(this.Skip(startStep).Take(endStep - startStep)) :
                new Map(this.Skip(startStep).Concat(this.Take(endStep)));
        }

        public Map GetAngleSpan(AngleSpan angleSpan)
        {
            return GetAngleSpan(angleSpan.StartAngle.Step, angleSpan.EndAngle.Step);
        }

        public virtual int GetIndex(int step)
        {
            return step;
        }

        public double this[int step]
        {
            get
            {
                return InnerMap[GetIndex(step)];
            }

            set
            {
                InnerMap[GetIndex(step)] = value;
            }
        }

        public double this[Angle angle]
        {
            get
            {
                return InnerMap[angle.Step];
            }

            set
            {
                InnerMap[angle.Step] = value;
            }
        }

        public void Normalize()
        {
            InnerMap.Normalize();
        }

        public Map GetNormalized()
        {
            var normalized = Clone();
            normalized.Normalize();
            return normalized;
        }

        public Map Reverse()
        {
            var map = Clone();
            Array.Reverse(map.InnerMap);
            return map;
        }

        public IEnumerator<double> GetEnumerator()
        {
            return ((IEnumerable<double>)InnerMap).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return InnerMap.GetEnumerator();
        }

        public override Bitmap ToImage()
        {
            var image = new Bitmap(InnerMap.Length, Constants.ImageMapHeight);
            var min = this.InnerMap.Min();
            var max = this.InnerMap.Max();

            for (int x = 0; x < image.Width; x++)
            {
                var color = InnerMap[x].ToColor(min, max);

                for (int y = 0; y < Constants.ImageMapHeight; y++)
                {
                    image.SetPixel(x, y, color);
                }
            }

            return image;
        }

        public Map Clone()
        {
            return new Map(InnerMap.Clone() as double[]);
        }

        public override Map ToMap()
        {
            return this;
        }
    }
}
