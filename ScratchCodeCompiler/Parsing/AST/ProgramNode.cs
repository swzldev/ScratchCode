using ScratchCodeCompiler.Scratch;

namespace ScratchCodeCompiler.Parsing.AST
{
    internal class ProgramNode : ASTNode
    {
        public List<EventDeclerationNode> Entrys { get; set; } = [];

        public ScratchBlock[] ToScratchBlocks()
        {
            List<ScratchBlock> blocks = [];
            foreach (EventDeclerationNode entry in Entrys)
            {
                entry.ToScratchBlock(ref blocks);
            }
            return [.. blocks];
        }

        public override string ToString()
        {
            return $"Program {{\n{string.Join("\n", Entrys)}\n}}";
        }
    }
}
