using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stevpet.Tools.Build
{
    public class ArtifactNode : Node
    {

       
        public ArtifactNode(SolutionNode parent,string node) : base(node)
        {
            SolutionNode = parent;
        }

        public SolutionNode SolutionNode { get; private set; }

        public override string ToString()
        {
            return SolutionNode.Name + ":" + Name;
        }

        public void DependsOn(ArtifactNode artifact)
        {
            base.Add(artifact);
        }
    }
}
