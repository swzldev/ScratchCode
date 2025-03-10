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

        public override ScratchBlock[] ToScratchBlocks(out ScratchBlock? returnBlock, out ScratchVariable? returnVar)
        {
            List<ScratchBlock> blocks = [];
            returnBlock = null;
            returnVar = null;
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
                    ScratchBlock[] valueBlocks = Right.ToScratchBlocks(out var exprResult, out _);
                    setVarBlock.Inputs.Add(new("VALUE", exprResult ?? throw new NullReferenceException(), ScratchInputFormat.Number));
                    exprResult.Parent = setVarBlock;
                    blocks.AddRange(valueBlocks);
                }
                returnVar = assignmentVar;
            }
            else if (Operator == TokenType.OpEqual || Operator == TokenType.OpNotEqual)
            {
                ScratchBlock ret;
                ScratchBlock comparisonBlock = new(ScratchOpcode.Operator_Equals, new ScratchVector2(0, 0));
                blocks.Add(comparisonBlock);
                if (Operator == TokenType.OpEqual)
                {
                    ret = comparisonBlock;
                }
                else
                {
                    ret = new(ScratchOpcode.Operator_Not, new ScratchVector2(0, 0));
                    blocks.Add(ret);
                    // Add the comparison block to the input of the NOT block
                    ret.Inputs.Add(new("OPERAND", comparisonBlock));
                    comparisonBlock.Parent = ret;
                }
                returnBlock = ret;

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
                    ScratchBlock[] leftBlocks = Left.ToScratchBlocks(out var lExprResult, out _);
                    lExprResult!.Parent = comparisonBlock;
                    comparisonBlock.Inputs.Add(new("OPERAND1", lExprResult ?? throw new NullReferenceException(), ScratchInputFormat.Number));
                    blocks.AddRange(leftBlocks);
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
                    ScratchBlock[] rightBlocks = Right.ToScratchBlocks(out var rExprResult, out _);
                    rExprResult!.Parent = comparisonBlock;
                    comparisonBlock.Inputs.Add(new("OPERAND2", rExprResult ?? throw new NullReferenceException(), ScratchInputFormat.Number));
                    blocks.AddRange(rightBlocks);
                }
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
                blocks.Add(operationBlock);
                returnBlock = operationBlock;

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
                    ScratchBlock[] leftBlocks = Right.ToScratchBlocks(out var lExprResult, out _);
                    lExprResult!.Parent = operationBlock;
                    operationBlock.Inputs.Add(new("NUM1", lExprResult ?? throw new NullReferenceException(), ScratchInputFormat.Number));
                    blocks.AddRange(leftBlocks);
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
                    ScratchBlock[] rightBlocks = Right.ToScratchBlocks(out var rExprResult, out _);
                    rExprResult!.Parent = operationBlock;
                    operationBlock.Inputs.Add(new("NUM2", rExprResult ?? throw new NullReferenceException(), ScratchInputFormat.Number));
                    blocks.AddRange(rightBlocks);
                }
            }
            return blocks.ToArray();
        }

        public override string ToString()
        {
            return $"(BinaryExpression {{{Left} {Operator} {Right}}})";
        }
    }
}
