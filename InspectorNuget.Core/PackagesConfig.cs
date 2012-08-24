using System.IO;

namespace InspectorNuget.Core
{
    public class PackagesConfig
    {
        private PackagesConfig(string file)
        {
            
        }
        public static PackagesConfig Load(string file)
        {
            if(!File.Exists(file)) return null;

            return new PackagesConfig(file);
        }
    }
}