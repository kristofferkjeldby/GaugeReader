namespace GaugeReader.Processors.TestProcessors
{
    using GaugeReader.Extensions;
    using GaugeReader.Models.Lines;
    using GaugeReader.Models.Processors;
    using System.Drawing;

    public class MaskLineTestProcessor : Processor
    {
        public override string Name => "MaskLineTestProcessor";

        public override void Process(ProcessorArgs args, ProcessorResult result)
        {
            var processImage = args.EdgeImage;
            var line1 = new Line(args.R1, args.Theta1);
            processImage.MaskLine(line1, Color.Red);
            AddDebugImage(processImage);
        }
    }
}
