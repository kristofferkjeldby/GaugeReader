namespace GaugeReader.Processors.Models
{
    using AForge.Imaging;
    using GaugeReader.Extensions;
    using GaugeReader.Images.Models;
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;

    public abstract class Processor
    {
        private Stopwatch stopWatch;
        ProcessorResult result;

        public Processor()
        {
            ProfileNames = new string[0];
        }

        public Processor(params string[] profileNames)
        {
            ProfileNames = profileNames;
        }

        public abstract string Name { get; }

        public string[] ProfileNames { get; set;}

        public ProcessorResult Run(ProcessorArgs args)
        {
            result = new ProcessorResult();

            if (ProfileNames.Any() && !ProfileNames.Any(p => p.Equals(args.Profile.Name)))
            {
                result.Skipped = true;
                return result;
            }

            stopWatch = new Stopwatch();
            stopWatch.Start();
            Process(args, result);
            stopWatch.Stop();
            result.Elapsed = stopWatch.Elapsed;
            AddMessage($"{Name} completed in {Math.Ceiling(result.Elapsed.TotalMilliseconds)} ms");
            return result;
        }

        public abstract void Process(ProcessorArgs args, ProcessorResult result);

        public void AddDebugImage(Bitmap image, string caption = null)
        {
            var debugImage = new OutputImage(image, caption ?? Name, image.Width, image.Height);
            result.DebugImage.Add(debugImage);
        }

        public void AddDebugImage(IImageable imageable, string caption = null)
        {
            AddDebugImage(imageable.ToImage(), caption);
        }

        public void AddDebugImage(UnmanagedImage image, string caption = null)
        {
            AddDebugImage(image.ToBitmap(), caption);
        }

        public void AddMessage(string message, bool debug = true)
        {
            result.Messages.Add(new ProcessorMessage { Message = message, Debug = debug });
        }
    }
}
