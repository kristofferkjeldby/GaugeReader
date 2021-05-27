namespace GaugeReader.Processors.TestProcessors
{
    using GaugeReader.Extensions;
    using GaugeReader.Math.Models.Coordinates;
    using GaugeReader.Processors.Models;
    using System.Drawing;

    public class CoordinateTestProcessor : Processor
    {
        public override string Name => nameof(CoordinateTestProcessor);

        public override void Process(ProcessorArgs args, ProcessorResult result)
        {
            var output = args.ImageSet.GetUnfilteredImage();
            output.DrawCoordinate(new SphericalCoordinate(args.R1, args.Theta1), Color.Red);
            output.DrawCoordinate(new SphericalCoordinate(args.R2, args.Theta2), Color.Green);
            output.DrawCoordinate(new CartesianCoordinate(args.X, args.Y), Color.Blue);
            AddDebugImage(output);
        }
    }
}
