using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stevpet.Tools.Build
{
    public class NodeRepository
    {
        private IList<Node> nodes = new List<Node>();
        public void Add(Node node)
        {
            nodes.Add(node);
        }
    }
}
