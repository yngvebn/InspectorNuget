using System.IO;

namespace InspectorNuget.Core
{
    public class NugetConfig
    {
        public string PackagesLocation { get; private set; }


        private NugetConfig(string packagesLocation)
        {
            PackagesLocation = packagesLocation;
        }

        public static NugetConfig Load(string file)
        {
            if (!File.Exists(file)) return new NugetConfig(Path.Combine(Path.GetDirectoryName(file), "packages"));
            
            return new NugetConfig(file);
        }

    }
}