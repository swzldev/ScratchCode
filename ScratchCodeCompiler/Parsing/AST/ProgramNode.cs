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

        public ScratchBlock[] ToScratchBlocks()
        {
            List<ScratchBlock> blocks = [];
            ScratchBlock? lastBlock = null;
            foreach (ASTNode child in Code)
            {
                if (child is IScratchBlockTranslatable translatable)
                {
                    ScratchBlock scratchBlock = translatable.ToScratchBlock(ref blocks);
                    lastBlock?.Stitch(scratchBlock);
                    lastBlock = scratchBlock;
                }
                else
                {
                    throw new NotImplementedException("Found non translatable node");
                }
            }
            return [.. blocks];
        }

        public override string ToString()
        {
            return $"Program {{\n{string.Join("\n", Code)}\n}}";
        }
    }
}
