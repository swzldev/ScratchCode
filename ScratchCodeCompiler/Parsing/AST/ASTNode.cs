using ScratchCodeCompiler.Scratch;

namespace ScratchCodeCompiler.Parsing.AST
{
    internal abstract class ASTNode
    {
        public abstract ScratchBlock[] ToScratchBlocks(out ScratchBlock? returnBlock, out ScratchVariable? returnVar);
    }
}
