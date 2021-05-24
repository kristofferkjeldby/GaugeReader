namespace GaugeReader.Processors.TestProcessors
{
    using GaugeReader.Extensions;
    using GaugeReader.Models.Angles;
    using GaugeReader.Models.Processors;
    using System.Drawing;

    public class MaskAngleSpanTestProcessor : Processor
    {
        public override string Name => "MaskAngleSpanTestProcessor";

        public override void Process(ProcessorArgs args, ProcessorResult result)
        {
            var processImage = args.EdgeImage;
            var angleSpan = new AngleSpan(args.Theta1 + args.Theta2, args.Theta1 - args.Theta2);
            processImage.MaskAngleSpan(angleSpan, Color.Red);
            AddDebugImage(processImage);
        }
    }
}
