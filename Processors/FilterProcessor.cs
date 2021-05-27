namespace GaugeReader.Processors
{
    using GaugeReader.Extensions;
    using GaugeReader.Filters.Models;
    using GaugeReader.Processors.Models;
    using System;
    using System.Drawing;

    public abstract class FilterProcessor : Processor
    {
        public abstract IFilter Filter { get; } 

        public abstract Func<ProcessorArgs, Bitmap> InputDelegate { get; }

        public abstract Action<Bitmap, ProcessorArgs> OutputDelegate { get; }

        protected FilterProcessor(params string[] profileNames) : base(profileNames)
        {

        }

        public override void Process(ProcessorArgs args, ProcessorResult result)
        {
            var processed = InputDelegate(args).Filter(Filter);

            AddDebugImage(processed);

            OutputDelegate(processed, args);
        }
    }
}
