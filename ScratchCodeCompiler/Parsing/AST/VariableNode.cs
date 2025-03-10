using ScratchCodeCompiler.Scratch;

namespace ScratchCodeCompiler.Parsing.AST
{
    internal class VariableNode : ExpressionNode
    {
        private static Dictionary<string, ScratchVariable> variables = [];

        public string VariableName { get; }
        public ScratchVariable ScratchVariable { get; set; }

        public VariableNode(string variableName)
        {
            VariableName = variableName;
            if (!variables.TryGetValue(variableName, out ScratchVariable? value))
            {
                value = new ScratchVariable(variableName);
                variables[variableName] = value;
            }
            ScratchVariable = value;
        }

        public override ScratchBlock[] ToScratchBlocks(out ScratchBlock? returnBlock, out ScratchVariable? returnVar)
        {
            returnBlock = null;
            returnVar = null;
            return [];
        }

        public override string ToString()
        {
            return $"Variable({VariableName})";
        }
    }
}
