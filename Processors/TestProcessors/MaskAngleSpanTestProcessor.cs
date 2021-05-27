namespace GaugeReader.Processors.TestProcessors
{
    using GaugeReader.Extensions;
    using GaugeReader.Filters;
    using GaugeReader.Math.Models.Angles;
    using GaugeReader.Processors.Models;
    using System.Drawing;

    public class MaskAngleSpanTestProcessor : Processor
    {
        public override string Name => nameof(MaskAngleSpanTestProcessor);

        public override void Process(ProcessorArgs args, ProcessorResult result)
        {
            var output = args.ImageSet.GetUnfilteredImage();
            var angleSpan = new AngleSpan(args.Theta1 + args.Theta2, args.Theta1 - args.Theta2);
            output.MaskAngleSpan(angleSpan, Color.Red);
            AddDebugImage(output);
        }
    }
}
