using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stevpet.Tools.Build
{
    public class NodeRepository
    {

        public NodeRepository()
        {
            Nodes = new List<Node>();
        }
        public void Add(Node node)
        {
            Nodes.Add(node);
        }

        public IList<Node> Nodes { get; private set; }
    }
}
