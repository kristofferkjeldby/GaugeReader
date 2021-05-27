namespace GaugeReader.Profiles.Helpers
{
    using GaugeReader.Profiles.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class ProfileHelper
    {
        private static List<IProfile> profiles;

        public static List<IProfile> GetProfiles()
        {
            if (profiles == null)
            {
                profiles = AppDomain.CurrentDomain.GetAssemblies().
                SelectMany(s => s.GetTypes()).
                Where(p => typeof(IProfile).IsAssignableFrom(p) && p.IsClass).
                Select(t => (IProfile)Activator.CreateInstance(t)).ToList();
            }

            return profiles;
        }

        public static IProfile GetProfile(string profileName)
        {
            return profiles.FirstOrDefault(p => p.Name.Equals(profileName, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
