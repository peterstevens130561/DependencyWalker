using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stevpet.Tools.Build
{
    public class NodeFormattingService
    {
        public string FormatSolution(SolutionNode node)
        {
            var solutionName = node.Name;
            var suffixPos = solutionName.LastIndexOf(".sln");
            var pureName = solutionName.Remove(suffixPos);
            return Abbreviate(pureName);
        }


        public string FormatArtifact(ArtifactNode artifactNode)
        {
            return Abbreviate(artifactNode.Name);
        }

        private static string Abbreviate(string pureName)
        {
            var chars = pureName.ToCharArray();
            var dots = chars.Count(ch => ch == '.');
            StringBuilder sb = new StringBuilder(40);
            int dotsSeen = 0;
            for (int i = 0; i < pureName.Length; i++)
            {
                char ch = chars[i];
                if (ch == '.')
                {
                    dotsSeen++;

                }
                if (dotsSeen == dots || ch == '.' || (ch >= 'A' && ch <= 'Z'))
                {
                    sb.Append(ch);
                }
            }
            return sb.ToString();
        }
    }
}
