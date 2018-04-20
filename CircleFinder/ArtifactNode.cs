using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Evaluation;

namespace Stevpet.Tools.Build
{
    public class ArtifactNode : Node
    {

       
        public ArtifactNode(SolutionNode parent,string node,string projectLocation) : base(node)
        {
            SolutionNode = parent;
            ProjectLocation = projectLocation;
        }

        public SolutionNode SolutionNode { get; private set; }
        public String ProjectLocation { get; internal set; }

        public override string ToString()
        {
            return SolutionNode.Name + ":" + Name;
        }

        public void DependsOn(ArtifactNode artifact)
        {
            base.Add(artifact);
        }

        public override bool  Matches(INode otherNode)
        {
            SolutionNode otherSolutionNode= otherNode as SolutionNode;
            return this.SolutionNode == otherSolutionNode;
        }
    }
}
