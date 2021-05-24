namespace GaugeReader.Processors
{
    using GaugeReader.Extensions;
    using GaugeReader.Filters;
    using GaugeReader.Models.Processors;
    using System.Drawing;

    public class InvertProcessor : Processor
    {
        public override string Name => nameof(InvertProcessor);

        public override void Process(ProcessorArgs args, ProcessorResult result)
        {
            Bitmap processImage = Filter.Invert(args.ScaledImage);

            var pi = processImage.ToProcessImage();

            var filter = new AForge.Imaging.Filters.ContrastCorrection(40);

            filter.ApplyInPlace(pi);

            args.InvertedImage = pi.ToDrawImage();

            AddDebugImage(args.InvertedImage);
        }
    }
}
