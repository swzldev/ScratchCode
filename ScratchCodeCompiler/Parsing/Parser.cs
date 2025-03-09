using ScratchCodeCompiler.Lexical;
using ScratchCodeCompiler.Parsing.AST;

namespace ScratchCodeCompiler.Parsing
{
    internal class Parser
    {
        private List<Token> tokens;
        private int currentTokenIndex = 0;

        private Dictionary<string, VariableNode> variables = [];

        public Parser(List<Token> tokens)
        {
            this.tokens = tokens;
        }

        public ProgramNode Parse()
        {
            ProgramNode program = new();
            while (!IsAtEnd())
            {
                program.AddStatement(ParseExpression());
            }
            program.Variables = variables;
            return program;
        }

        private ASTNode ParseExpression()
        {
            if (Match(TokenType.KwIf))
            {

            }
            return ParseBinaryExpression();
        }

        private IfStatementNode ParseIfStatement()
        {
            ExpressionNode condition = ParseBinaryExpression();

        }

        private ExpressionNode ParseBinaryExpression(int precedence = 0)
        {
            ExpressionNode left = ParsePrimaryExpression();
            if (Match(TokenType.EOF) || Match(TokenType.LineTerminator))
            {
                return left;
            }
            while (!IsAtEnd() && Operators.GetPrecedence(Peek().Type) > precedence)
            {
                Token op = Consume();
                ASTNode right = ParseBinaryExpression(Operators.GetPrecedence(op.Type));
                left = new BinaryExpressionNode(left, right, op.Type);
            }
            return left;
        }

        private ExpressionNode ParsePrimaryExpression()
        {
            if (Match(TokenType.Number))
            {
                return new NumberLiteralNode(long.Parse(Previous().Value));
            }
            if (Match(TokenType.Identifier))
            {
                if (variables.ContainsKey(Previous().Value)) {
                    return variables[Previous().Value];
                }
                VariableNode variable = new(Previous().Value);
                variables.Add(variable.VariableName, variable);
                return variable;
            }
            throw new Exception("Expected expression");
        }

        private void Advance()
        {
            currentTokenIndex++;
        }

        private Token Previous()
        {
            return tokens[currentTokenIndex - 1];
        }

        private Token Peek()
        {
            return tokens[currentTokenIndex];
        }

        private bool Check(TokenType type)
        {
            return Peek().Type == type;
        }

        private Token Consume()
        {
            Token tk = Peek();
            Advance();
            return tk;
        }

        private bool Match(TokenType type)
        {
            if (Check(type))
            {
                Advance();
                return true;
            }
            return false;
        }

        private bool IsAtEnd()
        {
            return Peek().Type == TokenType.EOF;
        }

        private bool IsAtLineEnd()
        {
            return Peek().Type == TokenType.LineTerminator;
        }
    }
}
