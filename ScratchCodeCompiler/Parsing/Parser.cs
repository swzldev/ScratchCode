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

        private CodeBlockNode ParseCodeBlock()
        {
            if (!Match(TokenType.GmOpenBrace))
            {
                throw new Exception("Expected '{'");
            }
            CodeBlockNode codeBlock = new();
            while (!IsAtEnd() && !Match(TokenType.GmCloseBrace))
            {
                codeBlock.Children.Add(ParseExpression());
            }
            if (Previous().Type != TokenType.GmCloseBrace)
            {
                throw new Exception("Expected '}'");
            }
            return codeBlock;
        }

        private ASTNode ParseExpression()
        {
            if (Match(TokenType.KwIf))
            {
                return ParseIfStatement();
            }
            return ParseBinaryExpression();
        }

        private IfStatementNode ParseIfStatement()
        {
            ExpressionNode condition = ParseBinaryExpression();
            if (!Match(TokenType.GmOpenBrace))
            {
                throw new Exception("Expected '('");
            }
            IfStatementNode ifStmt = new(condition, ParseCodeBlock());
            if (Match(TokenType.KwElse))
            {
                ifStmt.ElseNode = ParseElseStatement(ifStmt);
            }
            return ifStmt;
        }

        private ElseStatementNode ParseElseStatement(IfStatementNode parent)
        {
            if (Match(TokenType.KwIf))
            {
                return new(parent, ParseIfStatement().Body);
            }
            return new(parent, ParseCodeBlock());
        }

        private ExpressionNode ParseBinaryExpression(int precedence = 0)
        {
            ExpressionNode left = ParsePrimaryExpression();
            if (Match(TokenType.EOF) || Match(TokenType.LineTerminator) || CheckGrammar())
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
                if (variables.ContainsKey(Previous().Value))
                {
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

        private bool CheckGrammar()
        {
            return Check(TokenType.GmOpenParen) || Check(TokenType.GmCloseParen) || Check(TokenType.GmOpenBracket) || Check(TokenType.GmCloseBracket) || Check(TokenType.GmOpenBrace) || Check(TokenType.GmCloseBrace);
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
