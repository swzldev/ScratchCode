using ScratchCodeCompiler.Scratch;

namespace ScratchCodeCompiler.Parsing.AST
{
    internal class VariableNode : ExpressionNode
    {
        public string VariableName { get; }
        public ScratchType? VariableType { get; set; }
        public ScratchVariable ScratchVariable { get; set; }

        public override ScratchType GetReturnType() => VariableType!.Value;

        public VariableNode(string variableName, ScratchVariable scratchVariable)
        {
            VariableName = variableName;
            ScratchVariable = scratchVariable;
        }

        public override string ToString()
        {
            return $"Variable({VariableName})";
        }
    }
}
