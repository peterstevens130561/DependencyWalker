using System.Collections.Generic;

namespace Stevpet.Tools.Build
{
    public interface INode
    {
        IList<INode> Children { get; }
        string Name { get; }

        Node Add(INode node);
        bool Matches(INode searchNode);
    }
}