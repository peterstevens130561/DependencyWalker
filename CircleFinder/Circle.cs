using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stevpet.Tools.Build
{
    public class Circle : ICircle
    {
        public Circle()
        {
            Nodes = new List<Node>();
        }

        public ICircle Add(Node startNode)
        {
            Nodes.Add(startNode);
            return this;
        }

        public IList<Node> Nodes { get; private set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(256);
            Nodes.ToList().ForEach(n => sb.Append(n.Name).Append(","));
            string result = sb.ToString();
            return result.Remove(result.Length-1);

                    }
    }
}