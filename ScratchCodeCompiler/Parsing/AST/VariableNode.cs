using ScratchCodeCompiler.Scratch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Parsing.AST
{
    internal class VariableNode : ExpressionNode
    {
        public string VariableName { get; }

        public VariableNode(string variableName)
        {
            VariableName = variableName;
        }

        public override ScratchBlock[] ToScratchBlocks(out ScratchBlock? returnBlock)
        {
            returnBlock = null;
            return [];
        }

        public override string ToString()
        {
            return $"Variable({VariableName})";
        }
    }
}
