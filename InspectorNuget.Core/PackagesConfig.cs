using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace InspectorNuget.Core
{
    public class PackagesConfig
    {
        public List<NugetPackage> Packages { get; private set; }
         
        private PackagesConfig(string file)
        {
            Packages = XDocument.Load(file).Element("packages")
                                .Elements()
                                .Select(NugetPackage.Parse)
                                .ToList();
        }
        public static PackagesConfig Load(string file)
        {
            if(!File.Exists(file)) return null;

            return new PackagesConfig(file);
        }

        public override string ToString()
        {
            return String.Format("{0} Nuget packages referenced", Packages.Count());
        }
    }

    public class NugetPackage
    {
        public string Id { get; private set; }
        public Version Version { get; private set; }
        public string TargetFramework { get; private set; }

        public NugetPackage()
        {
            
        }
        public static NugetPackage Parse(XElement element)
        {
            return new NugetPackage(element);
        }
        public NugetPackage(XElement xelement)
        {
            var id = xelement.Attribute("id");
            if (id != null) Id = id.Value;
            
            var version = xelement.Attribute("version");
            if (version != null) Version = Version.Parse(version.Value);
            
            var targetFramework = xelement.Attribute("targetFramework");
            if (targetFramework != null) TargetFramework = targetFramework.Value;
        }

        public override string ToString()
        {
            return string.Format("{0}.{1} ({2})", Id, Version, TargetFramework);
        }
    }
}