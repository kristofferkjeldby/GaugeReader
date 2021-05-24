namespace GaugeReader.Processors
{
    using AForge.Imaging;
    using GaugeReader.Extensions;
    using GaugeReader.Models.Gauges;
    using GaugeReader.Models.Processors;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;

    public abstract class Processor
    {
        private Stopwatch stopWatch;
        ProcessorResult result;

        public Processor()
        {
            Profiles = new List<GaugeProfile>();
        }

        public Processor(params GaugeProfile[] profiles)
        {
            Profiles = profiles.ToList();
        }

        public abstract string Name { get; }

        public List<GaugeProfile> Profiles { get; set;}

        public ProcessorResult Run(ProcessorArgs args)
        {
            result = new ProcessorResult();

            if (Profiles.Any() && !Profiles.Any(p => p.Equals(args.Profile)))
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

        public void AddDebugImage(Bitmap image, string caption = null, int width = 200, int height = 200)
        {
            var debugImage = new OutputImage(image.Copy(), caption ?? Name, width, height);
            result.DebugImage.Add(debugImage);
        }

        public void AddDebugImage(UnmanagedImage image, string caption = null, int width = 200, int height = 200)
        {
            AddDebugImage(image.ToDrawImage(), caption, width, height);
        }

        public void AddMessage(string message, bool debug = true)
        {
            result.Messages.Add(new ProcessorMessage { Message = message, Debug = debug });
        }
    }
}
