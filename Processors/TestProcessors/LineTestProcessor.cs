namespace GaugeReader.Processors.TestProcessors
{
    using GaugeReader.Extensions;
    using GaugeReader.Math.Models.Lines;
    using GaugeReader.Processors.Models;
    using System.Drawing;

    public class LinesTestProcessor : Processor
    {
        public override string Name => nameof(LinesTestProcessor);

        public override void Process(ProcessorArgs args, ProcessorResult result)
        {
            var output = args.ImageSet.GetUnfilteredImage();

            var line1 = new Line(args.R1, args.Theta1);
            output.DrawLine(line1, Color.Red);
            var intersections1 = line1.Intersections(output);
            output.DrawCoordinate(intersections1[0], Color.DarkRed);
            output.DrawCoordinate(intersections1[1], Color.Pink);

            var line2 = new Line(args.R2, args.Theta2);
            output.DrawLine(line2, Color.Green);
            var intersections2 = line2.Intersections(output);
            output.DrawCoordinate(intersections2[0], Color.DarkGreen);
            output.DrawCoordinate(intersections2[1], Color.LightGreen);

            AddDebugImage(output);
        }
    }
}
