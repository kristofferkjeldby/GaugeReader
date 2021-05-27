namespace GaugeReader.Processors.TestProcessors
{
    using GaugeReader.Extensions;
    using GaugeReader.Math.Models.Lines;
    using GaugeReader.Processors.Models;
    using System.Drawing;

    public class MaskLineTestProcessor : Processor
    {
        public override string Name => nameof(MaskLineTestProcessor);

        public override void Process(ProcessorArgs args, ProcessorResult result)
        {
            var output = args.ImageSet.GetUnfilteredImage();
            var line = new Line(args.R1, args.Theta1);
            output.MaskLine(line, Color.Red);
            AddDebugImage(output);
        }
    }
}
