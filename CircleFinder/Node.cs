using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stevpet.Tools.Build
{
    public class Node
    {
        public Node(string v)
        {
            Name = v;
            References = new List<Node>();
        }
        public IList<Node> References { get; private set; }
        public string Name { get;private set;}
        public override bool Equals(object o)
        {
            Node other = o as Node;
            return !(other == null || other.Name != this.Name);
        }

        public Node Add(Node node)
        {
            References.Add(node);
            return this;
        }

        public override string ToString()
        {
            return Name.ToString();
        }

        internal virtual bool Matches(Node searchNode)
        {
            return this == searchNode;
        }
    }
}
