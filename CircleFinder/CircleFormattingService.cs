using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stevpet.Tools.Build
{
    public class CircleFormattingService
    {
        private readonly NodeFormattingService nodeFormattingService;

        public CircleFormattingService(NodeFormattingService nodeFormattingService)
        {
            this.nodeFormattingService = nodeFormattingService;
        }

        public string FormatCircle(ICircle circle)
        {
            SolutionNode currentSolutionNode = null;
            StringBuilder sb = new StringBuilder(1024);
            foreach(Node node in circle.Nodes)
            {
                ArtifactNode artifact=node  as ArtifactNode;
                if(artifact==null )
                {
                    continue;
                }
                if (artifact.SolutionNode != currentSolutionNode)
                {
                    if (currentSolutionNode != null)
                    {
                        sb.Append(",");

                    }
                    currentSolutionNode = artifact.SolutionNode;
                    sb.Append(nodeFormattingService.FormatSolution(currentSolutionNode));
                    sb.Append(":");
                } else
                {
                    sb.Append(" -> ");
                }
                sb.Append(nodeFormattingService.FormatArtifact(artifact));

            }
            return sb.ToString();
        }
    }
}
