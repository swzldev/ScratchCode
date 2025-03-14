using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Parsing.AST
{
    internal class SpriteDeclerationNode
    {
        public string Name { get; }

        public SpriteDeclerationNode(string name)
        {
            Name = name;
        }
    }
}
