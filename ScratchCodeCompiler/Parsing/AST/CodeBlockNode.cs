using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Parsing.AST
{
    internal class CodeBlockNode : ASTNode
    {
        public List<ASTNode> Children { get; set; }

        public CodeBlockNode()
        {
            Children = [];
        }

        public override string ToString()
        {
            string str = "{\n";
            foreach (ASTNode child in Children)
            {
                str += child.ToString() + "\n";
            }
            str += "}";
            return str;
        }
    }
}
