namespace GaugeReader.Processors
{
    using GaugeReader.Processors.Models;

    public class CenterImageProcessor : Processor
    {
        public CenterImageProcessor(params string[] profileNames) : base(profileNames)
        {

        }

        public override string Name => nameof(CenterImageProcessor);

        public override void Process(ProcessorArgs args, ProcessorResult result)
        {
            if (args.HandLine == null)
            {
                args.Abort();
                return;
            }

            args.ImageSet.Center((args.HandLine.R * args.HandLine.Normal).ToPoint(args.ImageSet.GetUnfilteredImage()));
            AddDebugImage(args.ImageSet);
        }
    }
}
