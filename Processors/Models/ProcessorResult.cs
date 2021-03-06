namespace GaugeReader.Processors.Models
{
    using System;
    using System.Collections.Generic;

    public class ProcessorResult
    {
        public ProcessorResult()
        {
            DebugImage = new List<OutputImage>();
            Messages = new List<ProcessorMessage>();
        }

        public List<OutputImage> DebugImage { get; set; }

        public List<ProcessorMessage> Messages { get; set; }

        public TimeSpan Elapsed { get; set; }

        public bool Skipped { get; set; }

        public void Skip()
        {
            Skipped = true;
        }
    }
}
