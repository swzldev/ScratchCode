using ScratchCodeCompiler.Scratch;

namespace ScratchCodeCompiler.Parsing.AST
{
    internal class VariableNode : ExpressionNode
    {
        public static Dictionary<string, ScratchVariable> GlobalVariables { get; set; } = [];

        public string VariableName { get; }
        public ScratchVariable ScratchVariable { get; set; }

        public VariableNode(string variableName)
        {
            VariableName = variableName;
            if (!GlobalVariables.TryGetValue(variableName, out ScratchVariable? value))
            {
                value = new ScratchVariable(variableName);
                GlobalVariables[variableName] = value;
            }
            ScratchVariable = value;
        }

        public override string ToString()
        {
            return $"Variable({VariableName})";
        }
    }
}
