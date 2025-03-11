using ScratchCodeCompiler.Scratch;

namespace ScratchCodeCompiler.Parsing.AST
{
    internal class VariableNode : ExpressionNode
    {
        public string VariableName { get; }
        public ScratchVariable ScratchVariable { get; set; }

        public VariableNode(string variableName, ScratchVariable scratchVar)
        {
            VariableName = variableName;
            ScratchVariable = scratchVar;
        }

        public override string ToString()
        {
            return $"Variable({VariableName})";
        }
    }
}
