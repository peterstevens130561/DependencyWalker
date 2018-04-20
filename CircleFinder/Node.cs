using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stevpet.Tools.Build
{
    public class Node : INode
    {
        public Node(string v)
        {
            Name = v;
            Children = new List<INode>();
        }
        public IList<INode> Children { get; private set; }
        public string Name { get;private set;}
        public override bool Equals(object o)
        {
            Node other = o as Node;
            return !(other == null || other.Name != this.Name);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public Node Add(INode node)
        {
            Children.Add(node);
            return this;
        }

        public override string ToString()
        {
            return Name.ToString();
        }

        public virtual bool Matches(INode searchNode)
        {
            return this == searchNode;
        }
    }
}
