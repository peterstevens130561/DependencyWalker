using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stevpet.Tools.Build
{
    public class SolutionNode : Node
    {
        public SolutionNode(string name) : base(name)
        {
        }

        public ArtifactNode CreatesArtifact(String artifactName,String projectLocation)
        {
            var artifact = new ArtifactNode(this, artifactName,projectLocation);
            Add(artifact);
            return artifact;
        }
        public override string ToString()
        {
            return "S: " + base.ToString();
        }
    }
}
