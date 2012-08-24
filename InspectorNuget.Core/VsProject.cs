using System;
using System.Linq;
using System.Text;

namespace InspectorNuget.Core
{
    public class VsProject
    {
        public string Name { get; private set; }
        public string Path { get; private set; }

        public VsProject(string name, string path)
        {
            Name = name;
            Path = path;
        }
    }
}
