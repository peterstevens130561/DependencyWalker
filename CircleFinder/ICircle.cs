﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stevpet.Tools.Build
{
    public interface ICircle
    {

         ICircle Add(INode node);

        IList<INode> Nodes { get; }
    }
}
