using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Construction;

namespace BHI.ArchitectureTools.StabilityCalculator
{
    public class ProjectInSolution : IProjectInSolution
    {
        private Microsoft.Build.Construction.ProjectInSolution p;

        public ProjectInSolution(Microsoft.Build.Construction.ProjectInSolution p)
        {
            this.p = p;
        }

    }
}
