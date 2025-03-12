using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Parsing.AST
{
    internal class NumberLiteralNode : ExpressionNode
    {
        public string Value { get; }

        public NumberLiteralNode(string value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return $"NumberLiteral({Value})";
        }
    }
}
