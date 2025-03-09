using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Parsing.AST
{
    internal class RepeatStatementNode
    {
        public int RepeatCount { get; }
        public CodeBlockNode Body { get; set; }

        public RepeatStatementNode(int repeatCount, CodeBlockNode body)
        {
            RepeatCount = repeatCount;
            Body = body;
        }
    }
}
