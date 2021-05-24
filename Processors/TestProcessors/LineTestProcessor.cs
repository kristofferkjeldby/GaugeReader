namespace GaugeReader.Processors.TestProcessors
{
    using GaugeReader.Extensions;
    using GaugeReader.Models.Lines;
    using GaugeReader.Models.Processors;
    using System.Drawing;

    public class LinesTestProcessor : Processor
    {
        public override string Name => "LineTestProcessor";

        public override void Process(ProcessorArgs args, ProcessorResult result)
        {
            var processImage = args.ScaledImage;

            var line1 = new Line(args.R1, args.Theta1);
            processImage.DrawLine(line1, Color.Red);
            var intersections1 = line1.Intersections(processImage);
            processImage.DrawCoordinate(intersections1[0], Color.DarkRed);
            processImage.DrawCoordinate(intersections1[1], Color.Pink);

            var line2 = new Line(args.R2, args.Theta2);
            processImage.DrawLine(line2, Color.Green);
            var intersections2 = line2.Intersections(processImage);
            processImage.DrawCoordinate(intersections2[0], Color.DarkGreen);
            processImage.DrawCoordinate(intersections2[1], Color.LightGreen);

            AddDebugImage(processImage);
        }
    }
}
