using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace InspectorNuget.Core
{
    public class ReferencedAssembly
    {
        public string NugetPackageName { get; set; }
        public string AssemblyName { get; set; }
        public Version AssemblyVersion { get; set; }
        public string FullName { get; private set; }
        public string AssemblyPath { get; private set; }
        private ReferencedAssembly(VsProject project, XElement xElement)
        {
            FullName = xElement.Attribute("Include").Value;
            XNamespace xn = "http://schemas.microsoft.com/developer/msbuild/2003";
            var hintPath = xElement.Element(xn + "HintPath");
            if(hintPath != null)
            {
                AssemblyPath = Path.Combine(project.ProjectPath, hintPath.Value);
            }
            ParseSegments(FullName.Split(','));
        }

        private void ParseSegments(string[] split)
        {
            AssemblyName = split[0];
            if(split.Length > 1)
            {
                AssemblyVersion = Version.Parse(split[1].Split('=')[1]);
            }
        }

        public static ReferencedAssembly Load(VsProject project, XElement xElement)
        {
            return new ReferencedAssembly(project, xElement);
        }

        public override string ToString()
        {
            return FullName;
        }
    }
}