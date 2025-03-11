using ScratchCodeCompiler.ErrorHandling;
using ScratchCodeCompiler.Lexical;
using ScratchCodeCompiler.Parsing.AST;

namespace ScratchCodeCompiler.Parsing
{
    internal class Parser
    {
        private List<Token> tokens;
        private int currentTokenIndex = 0;

        public Parser(List<Token> tokens)
        {
            this.tokens = tokens;
        }

        public ProgramNode Parse()
        {
            ProgramNode program = new();
            while (!IsAtEnd())
            {
                program.AddExpression(ParseExpression());
            }
            return program;
        }

        private CodeBlockNode ParseCodeBlock()
        {
            if (!Match(TokenType.GmOpenBrace))
            {
                SCError.HandleError(SCErrors.CS4, Peek());
            }
            CodeBlockNode codeBlock = new();
            while (!IsAtEnd() && !Match(TokenType.GmCloseBrace))
            {
                codeBlock.Children.Add(ParseExpression());
            }
            if (Previous().Type != TokenType.GmCloseBrace)
            {
                SCError.HandleError(SCErrors.CS5, Previous());
            }
            return codeBlock;
        }

        private ASTNode ParseExpression()
        {
            if (Match(TokenType.KwFunc))
            {
                return ParseFunctionDeclaration();
            }
            if (Match(TokenType.KwIf))
            {
                return ParseIfStatement();
            }
            if (Match(TokenType.KwForever))
            {
                return new ForeverStatementNode(ParseCodeBlock());
            }
            if (Match(TokenType.KwRepeat))
            {
                return new RepeatStatementNode(ParseBinaryExpression(), ParseCodeBlock());
            }
            if (Match(TokenType.KwRepeatUntil))
            {

            }
            if (Match(TokenType.KwWait))
            {

            }
            if (Match(TokenType.KwWaitUntil))
            {

            }
            return ParseBinaryExpression();
        }

        private FunctionDeclerationNode ParseFunctionDeclaration()
        {
            if (!Match(TokenType.Identifier))
            {
                SCError.HandleError(SCErrors.CS1, Peek());
            }
            string identifier = Previous().Value;
            if (!Match(TokenType.GmOpenParen))
            {
                SCError.HandleError(SCErrors.CS2, Peek());
            }
            List<string> parameters = [];
            while (!IsAtEnd() && !Match(TokenType.GmCloseParen))
            {
                if (!Match(TokenType.Identifier))
                {
                    SCError.HandleError(SCErrors.CS1, Peek());
                }
                parameters.Add(Previous().Value);
                if (!Match(TokenType.GmComma))
                {
                    Advance();
                    break;
                }
            }
            if (Previous().Type != TokenType.GmCloseParen)
            {
                SCError.HandleError(SCErrors.CS3, Previous());
            }
            return new(identifier, parameters, ParseCodeBlock());
        }

        private IfStatementNode ParseIfStatement()
        {
            ExpressionNode condition = ParseBinaryExpression();
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
                return new(parent, ParseIfStatement());
            }
            return new(parent, ParseCodeBlock());
        }

        private ExpressionNode ParseBinaryExpression(int precedence = 0)
        {
            ExpressionNode left = ParsePrimaryExpression();
            while (!IsAtEnd() && Operators.IsOperator(Peek().Type) && Operators.GetPrecedence(Peek().Type) > precedence)
            {
                Token op = Consume();
                ExpressionNode right = ParseBinaryExpression(Operators.GetPrecedence(op.Type));
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
                string identifier = Previous().Value;
                if (Match(TokenType.GmOpenParen))
                {
                    return ParseFunctionCall(identifier);
                }
                return new VariableNode(Previous().Value);
            }
            SCError.HandleError(SCErrors.CS6, Peek());
            return default!;
        }

        private FunctionCallNode ParseFunctionCall(string identifier)
        {
            List<ExpressionNode> args = [];
            while (!IsAtEnd() && !Match(TokenType.GmCloseParen))
            {
                args.Add(ParseBinaryExpression());
                if (!Match(TokenType.GmComma))
                {
                    Advance();
                    break;
                }
            }
            if (Previous().Type != TokenType.GmCloseParen)
            {
                SCError.HandleError(SCErrors.CS3, Previous());
            }
            return new(identifier, args);
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
    }
}
