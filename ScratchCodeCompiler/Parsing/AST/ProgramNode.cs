namespace ScratchCodeCompiler.Parsing.AST
{
    internal class ProgramNode : ASTNode
    {
        public List<ASTNode> Statements { get; private set; } = [];
        public Dictionary<string, VariableNode> Variables { get; set; } = [];

        public void AddStatement(ASTNode statement)
        {
            Statements.Add(statement);
        }

        public override string ToString()
        {
            return $"Program {{\n{string.Join("\n", Statements)}\n}}";
        }
    }
}
