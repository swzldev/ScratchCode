using ScratchCodeCompiler.Scratch;

namespace ScratchCodeCompiler.Parsing.AST
{
    internal class ProgramNode : ASTNode
    {
        public List<ASTNode> Code { get; private set; } = [];

        public void AddExpression(ASTNode expr)
        {
            Code.Add(expr);
        }

        public override ScratchBlock[] ToScratchBlocks(out ScratchBlock? returnBlock, out ScratchVariable? returnVar)
        {
            returnBlock = null;
            returnVar = null;
            List<ScratchBlock> blocks = [];
            foreach (var node in Code)
            {
                blocks.AddRange(node.ToScratchBlocks(out _, out _));
            }
            return blocks.ToArray();
        }

        public override string ToString()
        {
            return $"Program {{\n{string.Join("\n", Code)}\n}}";
        }
    }
}
