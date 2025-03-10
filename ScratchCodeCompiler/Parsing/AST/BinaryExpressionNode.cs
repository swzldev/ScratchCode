using ScratchCodeCompiler.CodeGeneration;
using ScratchCodeCompiler.Lexical;
using ScratchCodeCompiler.Scratch;

namespace ScratchCodeCompiler.Parsing.AST
{
    internal class BinaryExpressionNode : ExpressionNode, IScratchBlockTranslatable
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

        public ScratchBlock ToScratchBlock(ref List<ScratchBlock> blocks)
        {
            if (Operator == TokenType.OpAssign)
            {
                ScratchBlock setVarBlock = new(ScratchOpcode.Data_SetVariableTo, new ScratchVector2(0, 0));
                blocks.Add(setVarBlock);
                ScratchVariable assignmentVar = (Left as VariableNode)!.ScratchVariable;
                setVarBlock.Fields.Add(new("VARIABLE", assignmentVar));
                if (Right is VariableNode variable)
                {
                    // Set the variable to the value of another variable
                    setVarBlock.Inputs.Add(new("VALUE", ScratchInputFormat.Number, variable.ScratchVariable));
                }
                else if (Right is NumberLiteralNode literal)
                {
                    // Set the variable to the literal value
                    setVarBlock.Inputs.Add(new("VALUE", ScratchInputFormat.Number, literal.Value.ToString()));
                }
                else
                {
                    // Set the variable to the result of an expression
                    ScratchBlock exprResult = (Right as IScratchBlockTranslatable)!.ToScratchBlock(ref blocks);
                    setVarBlock.Inputs.Add(new("VALUE", exprResult ?? throw new NullReferenceException(), ScratchInputFormat.Number));
                    exprResult.Parent = setVarBlock;
                    blocks.Add(exprResult);
                }
                return setVarBlock;
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
                    ret.Inputs.Add(new("OPERAND", comparisonBlock));
                    comparisonBlock.Parent = ret;
                    blocks.Add(comparisonBlock);
                }

                if (Left is VariableNode lVariable)
                {
                    // Compare variable
                    comparisonBlock.Inputs.Add(new("OPERAND1", ScratchInputFormat.Number, lVariable.ScratchVariable));
                }
                else if (Left is NumberLiteralNode lLiteral)
                {
                    // Compare literal
                    comparisonBlock.Inputs.Add(new("OPERAND1", ScratchInputFormat.Number, lLiteral.Value.ToString()));
                }
                else
                {
                    // Compare expression
                    ScratchBlock lExprResult = (Left as IScratchBlockTranslatable)!.ToScratchBlock(ref blocks);
                    lExprResult!.Parent = comparisonBlock;
                    comparisonBlock.Inputs.Add(new("OPERAND1", lExprResult ?? throw new NullReferenceException(), ScratchInputFormat.Number));
                    blocks.Add(lExprResult);
                }

                if (Right is VariableNode rVariable)
                {
                    // Compare variable
                    comparisonBlock.Inputs.Add(new("OPERAND2", ScratchInputFormat.Number, rVariable.ScratchVariable));
                }
                else if (Right is NumberLiteralNode rLiteral)
                {
                    // Compare literal
                    comparisonBlock.Inputs.Add(new("OPERAND2", ScratchInputFormat.Number, rLiteral.Value.ToString()));
                }
                else
                {
                    // Compare expression
                    ScratchBlock rExprResult = (Right as IScratchBlockTranslatable)!.ToScratchBlock(ref blocks);
                    rExprResult!.Parent = comparisonBlock;
                    comparisonBlock.Inputs.Add(new("OPERAND2", rExprResult ?? throw new NullReferenceException(), ScratchInputFormat.Number));
                    blocks.Add(rExprResult);
                }
                return ret;
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
                ScratchBlock operationBlock = new(opcode, new ScratchVector2(0, 0));

                if (Left is VariableNode lVariable)
                {
                    // Compare variable
                    operationBlock.Inputs.Add(new("NUM1", ScratchInputFormat.Number, lVariable.ScratchVariable));
                }
                else if (Left is NumberLiteralNode lLiteral)
                {
                    // Compare literal
                    operationBlock.Inputs.Add(new("NUM1", ScratchInputFormat.Number, lLiteral.Value.ToString()));
                }
                else
                {
                    // Compare expression
                    ScratchBlock lExprResult = (Left as IScratchBlockTranslatable)!.ToScratchBlock(ref blocks);
                    lExprResult!.Parent = operationBlock;
                    operationBlock.Inputs.Add(new("NUM1", lExprResult ?? throw new NullReferenceException(), ScratchInputFormat.Number));
                    blocks.Add(lExprResult);
                }

                if (Right is VariableNode rVariable)
                {
                    // Compare variable
                    operationBlock.Inputs.Add(new("NUM2", ScratchInputFormat.Number, rVariable.ScratchVariable));
                }
                else if (Right is NumberLiteralNode rLiteral)
                {
                    // Compare literal
                    operationBlock.Inputs.Add(new("NUM2", ScratchInputFormat.Number, rLiteral.Value.ToString()));
                }
                else
                {
                    // Compare expression
                    ScratchBlock rExprResult = (Right as IScratchBlockTranslatable)!.ToScratchBlock(ref blocks);
                    rExprResult!.Parent = operationBlock;
                    operationBlock.Inputs.Add(new("NUM2", rExprResult ?? throw new NullReferenceException(), ScratchInputFormat.Number));
                    blocks.Add(rExprResult);
                }
                return operationBlock;
            }
        }

        public override string ToString()
        {
            return $"(BinaryExpression {{{Left} {Operator} {Right}}})";
        }
    }
}
