using Stevpet.Tools.Build;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHI.ArchitectureTools.StabilityCalculator
{
    /// <summary>
    /// A node definition that is used for the stability calculations
    /// </summary>
    interface IStabilityNode : INode
    {
        int FanIn { get; set; }
        int FanOut { get; set; }

        double Stability { get; }
    }

}
