using ScratchCodeCompiler.Lexical;

namespace ScratchCodeCompiler.Parsing.AST
{
    internal class BinaryExpressionNode : ASTNode
    {
        public ASTNode Left { get; }
        public ASTNode Right { get; }
        public OperatorType Operator { get; }

        public BinaryExpressionNode(ASTNode left, ASTNode right, OperatorType op)
        {
            Left = left;
            Right = right;
            Operator = op;
        }

        public override string ToString()
        {
            return $"(BinaryExpression [{Left.ToString()} {Operator} {Right.ToString()}])";
        }
    }
}
