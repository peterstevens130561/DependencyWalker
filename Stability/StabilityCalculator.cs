using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stevpet.Tools.Build;

namespace BHI.ArchitectureTools.StabilityCalculator
{
    public class StabilityCalculator : IStabilityCalculator
    {
        public void Execute(SolutionNode solution)
        {
            solution.Children.ToList().ForEach(a =>
           {
               ((ArtifactNode)a).Children.ToList().First
           });
        }
    }
}
