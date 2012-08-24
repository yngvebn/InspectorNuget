using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace InspectorNuget.Core
{
    public class VsSolution
    {
        public VsSolution()
        {
            Projects = new List<VsProject>();
        }
        private readonly string _solutionPath;
        public VsSolution(string file) : this()
        {
            if (string.IsNullOrEmpty(file)) throw new ArgumentException("File cannot be null", "file");
            _solutionPath = Path.GetDirectoryName(file);
            Debug.Assert(_solutionPath != null, "_solutionPath != null");

            Load(file);
            NugetConfig = NugetConfig.Load(Path.Combine(_solutionPath, "nuget.config"));
        }

        private void Load(string file)
        {
            using(var sr = new StreamReader(file))
            {

                while(!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    if(line != null && line.StartsWith("Project("))
                    {
                        AddProject(line);
                    }
                }
            }
        }

        private void AddProject(string line)
        {
            var segments = line.Split(',');
            if (segments.Length <= 1) return;

            var projectName = segments[0].Split('=')[1];
            var projectPath = segments[1].Replace("\"", "").Trim();
            if(projectPath.EndsWith(".csproj"))
            Projects.Add(new VsProject(projectName, Path.Combine(_solutionPath, projectPath)));
        }

        public NugetConfig NugetConfig { get; private set; }
        public List<VsProject> Projects { get; private set; } 
    }
}