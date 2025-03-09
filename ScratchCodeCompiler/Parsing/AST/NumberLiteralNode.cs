using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Parsing.AST
{
    internal class NumberLiteralNode : ASTNode
    {
        long Value { get; }

        public NumberLiteralNode(long value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return $"NumberLiteral({Value})";
        }
    }
}
