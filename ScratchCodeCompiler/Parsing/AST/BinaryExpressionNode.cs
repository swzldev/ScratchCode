using ScratchCodeCompiler.CodeGeneration;
using ScratchCodeCompiler.Lexical;
using ScratchCodeCompiler.Scratch;

namespace ScratchCodeCompiler.Parsing.AST
{
    internal class BinaryExpressionNode : ExpressionNode
    {
        public ExpressionNode Left { get; }
        public ExpressionNode Right { get; }
        public TokenType Operator { get; }

        public BinaryExpressionNode(ExpressionNode left, ExpressionNode right, TokenType op)
        {
            if (left is not VariableNode && op == TokenType.OpAssign)
            {
                throw new Exception("Left side of assignment must be a variable");
            }
            Left = left;
            Right = right;
            Operator = op;
        }

        public override ScratchBlock[] ToScratchBlocks(out ScratchBlock? returnBlock)
        {
            List<ScratchBlock> blocks = [];
            returnBlock = null;
            if (Operator == TokenType.OpAssign)
            {
                ScratchBlock setVarBlock = new(ScratchOpcode.Data_SetVariableTo, new ScratchVector2(0, 0));
                if (Right is VariableNode variable)
                {
                    // Set the variable to the value of another variable
                    //setVarBlock.Inputs.Add(new("VALUE", ))
                }
                else if (Right is NumberLiteralNode literal)
                {
                    // Set the variable to the literal value
                }
                else
                {
                    // Set the variable to the result of an expression
                    ScratchBlock[] valueBlocks = Right.ToScratchBlocks(out var ret1);
                }
            }
            else if (Operator == TokenType.OpEqual || Operator == TokenType.OpNotEqual)
            {
                ScratchBlock ret;
                ScratchBlock comparisonBlock = new(ScratchOpcode.Operator_Equals, new ScratchVector2(0, 0));
                if (Operator == TokenType.OpEqual)
                {
                    ret = comparisonBlock;
                }
                else
                {
                    ret = new(ScratchOpcode.Operator_Not, new ScratchVector2(0, 0));
                    // Add the comparison block to the input of the NOT block
                }
                returnBlock = ret;

                if (Left is VariableNode lVariable)
                {
                    // Set the variable to the value of another variable
                }
                else if (Left is NumberLiteralNode lLiteral)
                {
                    // Set the variable to the literal value
                }
                else
                {
                    // Set the variable to the result of an expression
                    ScratchBlock[] leftBlocks = Right.ToScratchBlocks(out var ret2);
                }

                if (Right is VariableNode rVariable)
                {
                    // Set the variable to the value of another variable
                }
                else if (Right is NumberLiteralNode rLiteral)
                {
                    // Set the variable to the literal value
                }
                else
                {
                    // Set the variable to the result of an expression
                    ScratchBlock[] rightBlocks = Right.ToScratchBlocks(out var ret3);
                }
            }
            else
            {

            }
            return blocks.ToArray();
        }

        public override string ToString()
        {
            return $"(BinaryExpression {{{Left} {Operator} {Right}}})";
        }
    }
}
