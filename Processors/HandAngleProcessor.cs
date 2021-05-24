namespace GaugeReader.Processors
{
    using AForge.Imaging;
    using GaugeReader.Extensions;
    using GaugeReader.Models.Angles;
    using GaugeReader.Models.Lines;
    using GaugeReader.Models.Processors;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    public class HandAngleProcessor : Processor
    {
        public override string Name => "HandAngleProcessor";

        public override void Process(ProcessorArgs args, ProcessorResult result)
        {
            var processImage = args.Profile.HandImage(args);

            var maskAngles = new List<Angle>()
                {
                    args.HandLine.Angle,
                    args.HandLine.Angle.Opposite,
                };

            var maskedLines = new Dictionary<Bitmap, Tuple<Angle, HoughLine>>();

            foreach (var maskAngle in maskAngles)
            {
                var maskAngleSpan = new AngleSpan(maskAngle + Constants.HandSearchAngle, maskAngle - Constants.HandSearchAngle);

                var maskedImage = processImage.Copy().MaskAngleSpan(maskAngleSpan, Constants.MaskColor);
                HoughLineTransformation maskedTransform = new HoughLineTransformation();
                maskedTransform.ProcessImage(maskedImage.ToProcessImage());

                var maskedLine = maskedTransform.GetMostIntensiveLines(10).
                Where(l => args.Profile.CenterZone.Contains(l, args.DialRadius)).
                OrderByDescending(l => l.Intensity).FirstOrDefault();

                if (maskedLine != null)
                    maskedLines.Add(maskedImage, new Tuple<Angle, HoughLine> (maskAngle, maskedLine));
            }

            if (!maskedLines.Any())
            {
                args.Aborted = true;
                return;
            }

            var kv = maskedLines.OrderByDescending(ml => ml.Value.Item2.Intensity).FirstOrDefault();
            args.HandLine = new RadialLine(kv.Value.Item1);
            kv.Key.DrawRadialLine(args.HandLine as RadialLine, Color.Green);
            args.HandAngle = args.HandLine.Angle;
            AddMessage($"Hand angle found: {args.HandAngle}", true);
            AddDebugImage(kv.Key);
        }
    }
}

