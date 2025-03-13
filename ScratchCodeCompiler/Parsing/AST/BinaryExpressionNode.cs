using ScratchCodeCompiler.CodeGeneration;
using ScratchCodeCompiler.ErrorHandling;
using ScratchCodeCompiler.Lexical;
using ScratchCodeCompiler.Scratch;

namespace ScratchCodeCompiler.Parsing.AST
{
    internal class BinaryExpressionNode : ExpressionNode, IScratchBlockTranslatable
    {
        public ExpressionNode Left { get; }
        public ExpressionNode Right { get; }
        public TokenType Operator { get; }

        public ScratchType ResultType => Operators.GetOperatorReturnType(Operator);

        public BinaryExpressionNode(ExpressionNode left, ExpressionNode right, TokenType op)
        {
            Left = left;
            Right = right;
            Operator = op;
        }

        public override ScratchType GetReturnType() => ResultType;

        public ScratchBlock ToScratchBlock(ref List<ScratchBlock> blocks)
        {
            if (Operator == TokenType.OpAssign)
            {
                ScratchBlock setVarBlock = new(ScratchOpcode.Data_SetVariableTo);
                blocks.Add(setVarBlock);
                ScratchVariable assignmentVar = (Left as VariableNode)!.ScratchVariable;
                setVarBlock.Fields.Add(new("VARIABLE", assignmentVar));
                setVarBlock.AddInputExpression("VALUE", Right, ref blocks);
                return setVarBlock;
            }
            else if (Operator == TokenType.OpEqual || Operator == TokenType.OpNotEqual)
            {
                ScratchBlock ret;
                ScratchBlock comparisonBlock = new(ScratchOpcode.Operator_Equals);
                if (Operator == TokenType.OpEqual)
                {
                    ret = comparisonBlock;
                }
                else
                {
                    ret = new(ScratchOpcode.Operator_Not);
                    // Add the comparison block to the input of the NOT block
                    ret.Inputs.Add(new("OPERAND", comparisonBlock));
                    comparisonBlock.Parent = ret;
                    blocks.Add(comparisonBlock);
                }

                comparisonBlock.AddInputExpression("OPERAND1", Left, ref blocks);
                comparisonBlock.AddInputExpression("OPERAND2", Right, ref blocks);

                return ret;
            }
            else if (Operator == TokenType.OpGreaterThan || Operator == TokenType.OpLessThan)
            {
                ScratchOpcode opcode = Operator == TokenType.OpGreaterThan ? ScratchOpcode.Operator_GT : ScratchOpcode.Operator_LT;
                ScratchBlock comparisonBlock = new(opcode);

                comparisonBlock.AddInputExpression("OPERAND1", Left, ref blocks);
                comparisonBlock.AddInputExpression("OPERAND2", Right, ref blocks);

                return comparisonBlock;
            }
            else if (Operator == TokenType.OpOr)
            {
                ScratchBlock orBlock = new(ScratchOpcode.Operator_Or);

                orBlock.AddInputExpression("OPERAND1", Left, ref blocks);
                orBlock.AddInputExpression("OPERAND2", Right, ref blocks);

                return orBlock;
            }
            else if (Operator == TokenType.OpAnd)
            {
                ScratchBlock andBlock = new(ScratchOpcode.Operator_And);

                andBlock.AddInputExpression("OPERAND1", Left, ref blocks);
                andBlock.AddInputExpression("OPERAND2", Right, ref blocks);

                return andBlock;
            }
            else
            {
                ScratchOpcode opcode = Operator switch
                {
                    TokenType.OpAdd => ScratchOpcode.Operator_Add,
                    TokenType.OpSubtract => ScratchOpcode.Operator_Subtract,
                    TokenType.OpMultiply => ScratchOpcode.Operator_Multiply,
                    TokenType.OpDivide => ScratchOpcode.Operator_Divide,
                    _ => throw new Exception($"Invalid operator {Operator}")
                };
                ScratchBlock operationBlock = new(opcode);

                operationBlock.AddInputExpression("NUM1", Left, ref blocks);
                operationBlock.AddInputExpression("NUM2", Right, ref blocks);

                return operationBlock;
            }
        }

        public override string ToString()
        {
            return $"(BinaryExpression {{{Left} {Operator} {Right}}})";
        }
    }
}
