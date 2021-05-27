namespace GaugeReader.Processors
{
    using GaugeReader.Processors.Models;
    using GaugeReader.Profiles.Helpers;
    using System;
    using System.Linq;

    public class SelectProfileProcessor : Processor
    {
        public override string Name => nameof(SelectProfileProcessor);

        public override void Process(ProcessorArgs args, ProcessorResult result)
        {
            if (args.Profile != null)
            {
                return;
            }

            // Override profile
            if (args.ImageFile.Filename.Contains("_"))
            {
                var profileName = args.ImageFile.Filename.Split('_').First();
                args.Profile = ProfileHelper.GetProfile(profileName);

                if (args.ImageFile.Filename.Contains("_ex_"))
                    args.ExpectedValue = Convert.ToInt32(args.ImageFile.Filename.Split('_').Last());


            }

            if (args.Profile == null)
                args.Profile = ProfileHelper.GetProfile(Constants.FallbackProfileName);

            if (args.Profile == null)
            {
                args.Abort();
                return;
            }

            AddMessage($"Profile {args.Profile.Name} selected", false);
        }
    }
}
