namespace GaugeReader.Processors
{
    using GaugeReader.Extensions;
    using GaugeReader.Images.Models;
    using GaugeReader.Processors.Models;

    public class LoadImageProcessor : Processor
    {
        public override string Name => nameof(LoadImageProcessor);

        public override void Process(ProcessorArgs args, ProcessorResult result)
        {
            if (!args.ImageFile.Exists())
            {
                args.Aborted = true;
                return;
            }

            args.ImageSet = new ImageSet(args.ImageFile.Load().Resize(Constants.ScaleWidth).ToBitmap());

            AddDebugImage(args.ImageSet);
        }
    }
}
