namespace GaugeReader.Processors
{
    using GaugeReader.Extensions;
    using GaugeReader.Models.Processors;
    using System.Drawing;

    public class ScaleProcessor : Processor
    {
        public override string Name => nameof(ScaleProcessor);

        public override void Process(ProcessorArgs args, ProcessorResult result)
        {
            var scaleWidth = (int)((double)Constants.ScaleHeight / args.OriginalImage.Height * args.OriginalImage.Width);
            Bitmap scaledImage = args.OriginalImage.Resize(scaleWidth, Constants.ScaleHeight);
            args.ScaledImage = scaledImage;
            AddDebugImage(scaledImage);
        }
    }
}
