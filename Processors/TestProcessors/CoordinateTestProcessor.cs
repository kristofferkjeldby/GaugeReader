namespace GaugeReader.Processors.TestProcessors
{
    using GaugeReader.Extensions;
    using GaugeReader.Models.Coordinates;
    using GaugeReader.Models.Processors;
    using System.Drawing;

    public class CoordinateTestProcessor : Processor
    {
        public override string Name => nameof(CoordinateTestProcessor);

        public override void Process(ProcessorArgs args, ProcessorResult result)
        {
            var processImage = args.ScaledImage;
            processImage.DrawCoordinate(new SphericalCoordinate(args.R1, args.Theta1), Color.Red);
            processImage.DrawCoordinate(new SphericalCoordinate(args.R2, args.Theta2), Color.Green);
            processImage.DrawCoordinate(new CartesianCoordinate(args.X, args.Y), Color.Blue);
            AddDebugImage(args.ScaledImage);

        }
    }
}
