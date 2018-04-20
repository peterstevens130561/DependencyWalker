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
            Nodes = new List<INode>();
        }

        public Circle(SolutionNode solutionNode)
        {
            SolutionNode = solutionNode;
            Nodes = new List<INode>();
        }

        public SolutionNode SolutionNode { get; private set; }
        public ICircle Add(INode startNode)
        {
            Nodes.Add(startNode);
            return this;
        }

        public IList<INode> Nodes { get; private set; }

        public string ToStringX()
        {
            StringBuilder sb = new StringBuilder(256);
            Nodes.ToList().ForEach(n => sb.Append(n.ToString()).Append(","));
            string result = sb.ToString();
            return result.Remove(result.Length-1);

                    }

        public override string ToString()
        {
            var formattingService = new CircleFormattingService(new NodeFormattingService());
            return formattingService.FormatCircle(this);

        }
    }
}