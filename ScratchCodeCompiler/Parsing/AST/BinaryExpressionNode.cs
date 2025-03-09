using ScratchCodeCompiler.Lexical;

namespace ScratchCodeCompiler.Parsing.AST
{
    internal class BinaryExpressionNode : ExpressionNode
    {
        public ASTNode Left { get; }
        public ASTNode Right { get; }
        public TokenType Operator { get; }

        public BinaryExpressionNode(ASTNode left, ASTNode right, TokenType op)
        {
            Left = left;
            Right = right;
            Operator = op;
        }

        public override string ToString()
        {
            return $"(BinaryExpression {{{Left} {Operator} {Right}}})";
        }
    }
}
