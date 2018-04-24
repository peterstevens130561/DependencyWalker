using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Construction;
using Microsoft.Build.Evaluation;

namespace BHI.ArchitectureTools.StabilityCalculator
{
    public class ProjectInSolution : IProjectInSolution
    {
        public ProjectInSolution(String assemblyName,String projectFile)
        {
            AssemblyName = assemblyName;
            ProjectFile = projectFile;
        }

        public string AssemblyName { get; private set; }

        public string ProjectFile { get; private set; }
    }
}
