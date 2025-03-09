using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Parsing.AST
{
    internal class ElseStatementNode
    {
        public IfStatementNode ParentIf { get; }
        public CodeBlockNode Body { get; set; }

        public ElseStatementNode(IfStatementNode parentIf, CodeBlockNode body)
        {
            ParentIf = parentIf;
            Body = body;
        }
    }
}
