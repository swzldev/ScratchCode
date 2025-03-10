using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Parsing.AST
{
    internal class ForeverStatementNode : StatementNode
    {
        public CodeBlockNode Body { get; }

        public ForeverStatementNode(CodeBlockNode body)
        {
            Body = body;
        }
    }
}
