using ScratchCodeCompiler.Scratch;

namespace ScratchCodeCompiler.Parsing.AST
{
    internal class ExpressionNode : ASTNode
    {
        public virtual ScratchType GetReturnType()
        {
            throw new NotImplementedException();
        }
    }
}
