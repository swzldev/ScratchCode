using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Parsing.AST
{
    internal class FunctionCallNode : ExpressionNode
    {
        public string FunctionName { get; }
        public List<ExpressionNode> Arguments { get; set; }

        public FunctionCallNode(string functionName)
        {
            FunctionName = functionName;
            Arguments = [];
        }

        public override string ToString()
        {
            return $"{FunctionName}({string.Join(", ", Arguments)})";
        }
    }
}
