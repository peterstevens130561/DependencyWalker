using Microsoft.Build.Evaluation;
using System;

namespace BHI.ArchitectureTools.StabilityCalculator
{
    /// <summary>
    /// Implementations will parse a solution and throw an event for each project found
    /// </summary>
    public interface ISolutionParser
    {
        Action<IProjectInSolution> OnProject { get; set; }

        void Parse(string resource);
    }
}