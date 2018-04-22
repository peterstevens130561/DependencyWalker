using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Construction;
using Microsoft.Build.Evaluation;

namespace BHI.ArchitectureTools.StabilityCalculator
{
    public class SolutionParser : ISolutionParser
    {
        public Action<ProjectInSolution> OnProject { get; set; }

        public void Parse(string solutionFile)
        {
            var solution = SolutionFile.Parse(solutionFile);
            solution.ProjectsInOrder.ToList().ForEach(p => OnProject.Invoke(new ProjectInSolution(p)));
        }
    }
}
