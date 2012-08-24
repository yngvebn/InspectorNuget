using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace InspectorNuget.Core
{
    public class VsProject
    {
        public string Name { get; private set; }
        public string ProjectFile { get; private set; }
        public string ProjectPath { get; private set; }

        public PackagesConfig NugetPackageConfig { get; private set; }
        
        public VsProject(string name, string path)
        {
            Name = name;
            ProjectFile = path;
            ProjectPath = Path.GetDirectoryName(path);
            NugetPackageConfig = PackagesConfig.Load(Path.Combine(ProjectPath, "packages.config"));
            XNamespace xn = "http://schemas.microsoft.com/developer/msbuild/2003";
            ReferencedAssemblies = (from itemGroup in XDocument.Load(ProjectFile).Descendants(xn + "Reference")
                        select ReferencedAssembly.Load(this, itemGroup)).ToList();
        }
        public override string ToString()
        {
            return Name;
        }
        public List<ReferencedAssembly> ReferencedAssemblies { get; private set; }
    }
}
