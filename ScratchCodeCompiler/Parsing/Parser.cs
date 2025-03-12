using ScratchCodeCompiler.ErrorHandling;
using ScratchCodeCompiler.Lexical;
using ScratchCodeCompiler.Parsing.AST;
using ScratchCodeCompiler.Scratch;
using System.Linq;

namespace ScratchCodeCompiler.Parsing
{
    internal class Parser
    {
        private ParserFlags parserFlags = ParserFlags.None;

        private readonly List<Token> tokens;
        private int currentTokenIndex = 0;

        private static readonly Dictionary<string, VariableNode> variables = [];
        private static readonly Dictionary<string, FunctionDeclerationNode> functionDeclerations = [];

        public Parser(List<Token> tokens)
        {
            this.tokens = tokens;
            // Add built-in functions
            foreach (var func in ScratchReservedFunctions.All)
            {
                functionDeclerations.Add(func.FunctionName, func);
            }
        }

        public ProgramNode Parse()
        {
            ProgramNode program = new();
            while (!IsAtEnd())
            {
                Token exprToken = Peek();
                ASTNode expr = ParseExpression();
                if (expr is EventDeclerationNode eventDecl)
                {
                    program.Entrys.Add(eventDecl);
                }
                else if (expr is BinaryExpressionNode binaryExpr)
                {
                    if (binaryExpr.Operator != TokenType.OpAssign)
                    {
                        SCError.HandleError(SCErrors.CS14, exprToken);
                    }
                    program.InitExpressions.Add(binaryExpr);
                }
            }
            // Add functions
            program.FunctionDeclerations.AddRange(functionDeclerations.Values.Where(fn => !fn.IsBuiltIn));
            // Add variables
            program.Variables.AddRange(variables.Values);
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
                if (parserFlags.HasFlag(ParserFlags.ForeverStatementInCurrentBlock))
                {
                    SCError.HandleWarning(SCWarnings.CSW1, Peek());
                    while (!IsAtEnd() && !Match(TokenType.GmCloseBrace))
                    {
                        ParseExpression();
                    }
                    break;
                }
                codeBlock.Children.Add(ParseExpression());
            }
            if (Previous().Type != TokenType.GmCloseBrace)
            {
                SCError.HandleError(SCErrors.CS5, Previous());
            }
            parserFlags &= ~ParserFlags.ForeverStatementInCurrentBlock;
            return codeBlock;
        }

        private EventDeclerationNode ParseEventDecleration()
        {
            if (!Match(TokenType.Identifier))
            {
                SCError.HandleError(SCErrors.CS1, Peek());
            }
            Token identifier = Previous();
            // Find event
            ScratchEvent? scratchEvent = null;
            foreach (var sEvent in ScratchEvents.All)
            {
                if (sEvent.Name == identifier.Value)
                {
                    scratchEvent = sEvent;
                    break;
                }
            }
            if (scratchEvent == null)
            {
                SCError.HandleError(SCErrors.CS11, identifier);
            }
            if (scratchEvent!.Parameters.Length > 0)
            {
                if (!Match(TokenType.GmOpenParen))
                {
                    SCError.HandleError(SCErrors.CS2, Peek());
                }
                List<string> parameters = [];
                while (!Match(TokenType.GmCloseParen))
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
            }
            parserFlags |= ParserFlags.WithinEvent;
            CodeBlockNode eventBody = ParseCodeBlock();
            parserFlags &= ~ParserFlags.WithinEvent;
            return new(scratchEvent, eventBody);
        }

        private ASTNode ParseExpression()
        {
            if (Match(TokenType.KwFunc))
            {
                // Cannot declare function within function
                if (parserFlags.HasFlag(ParserFlags.WithinFunction))
                {
                    SCError.HandleError(SCErrors.CS15, Previous());
                }
                FunctionDeclerationNode funcDecl = ParseFunctionDeclaration();
                functionDeclerations.Add(funcDecl.FunctionName, funcDecl);
                return funcDecl;
            }
            if (Match(TokenType.KwEvent))
            {
                // Cannot declare event within event
                if (parserFlags.HasFlag(ParserFlags.WithinEvent))
                {
                    SCError.HandleError(SCErrors.CS16, Previous());
                }
                return ParseEventDecleration();
            }
            if (Match(TokenType.KwIf))
            {
                // Cannot use if statement outside function or event
                if (!parserFlags.HasFlag(ParserFlags.WithinFunction) && !parserFlags.HasFlag(ParserFlags.WithinEvent))
                {
                    SCError.HandleError(SCErrors.CS17, Previous());
                }
                return ParseIfStatement();
            }
            if (Match(TokenType.KwForever))
            {
                // Cannot use forever statement outside function or event
                if (!parserFlags.HasFlag(ParserFlags.WithinFunction) && !parserFlags.HasFlag(ParserFlags.WithinEvent))
                {
                    SCError.HandleError(SCErrors.CS17, Previous());
                }
                ForeverStatementNode foreverNode = new(ParseCodeBlock());
                parserFlags |= ParserFlags.ForeverStatementInCurrentBlock;
                return foreverNode;
            }
            if (Match(TokenType.KwRepeat))
            {
                // Cannot use repeat statement outside function or event
                if (!parserFlags.HasFlag(ParserFlags.WithinFunction) && !parserFlags.HasFlag(ParserFlags.WithinEvent))
                {
                    SCError.HandleError(SCErrors.CS17, Previous());
                }
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
            if (functionDeclerations.ContainsKey(identifier))
            {
                SCError.HandleError(SCErrors.CS8, Previous());
            }
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
            parserFlags |= ParserFlags.WithinFunction;
            CodeBlockNode functionBody = ParseCodeBlock();
            parserFlags &= ~ParserFlags.WithinFunction;
            return new(identifier, parameters, functionBody);
        }

        private IfStatementNode ParseIfStatement()
        {
            Token expressionToken = Peek();
            ExpressionNode condition = ParseBinaryExpression();
            if (condition is BooleanLiteralNode boolLiteral)
            {
                if (boolLiteral.Value == "0")
                {
                    SCError.HandleWarning(SCWarnings.CSW2, expressionToken);
                }
            }
            else
            {
                if (condition.GetReturnType() != ScratchType.Boolean)
                {
                    SCError.HandleError(SCErrors.CS12, expressionToken);
                }
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
                return new(parent, ParseIfStatement());
            }
            return new(parent, ParseCodeBlock());
        }

        private ExpressionNode ParseBinaryExpression(int precedence = 0)
        {
            Token exprFirst = Peek();
            ExpressionNode left = ParsePrimaryExpression();
            while (!IsAtEnd() && Operators.IsOperator(Peek().Type) && Operators.GetPrecedence(Peek().Type) > precedence)
            {
                Token op = Consume();
                ExpressionNode right = ParseBinaryExpression(Operators.GetPrecedence(op.Type));

                if (op.Type == TokenType.OpAssign && left is not VariableNode)
                {
                    SCError.HandleError(SCErrors.CS7, Previous());
                }
                // Handle variable type assignment
                if (left is VariableNode var)
                {
                    if (var.VariableType == null)
                    {
                        var.VariableType = right.GetReturnType();
                        variables.Add(var.VariableName, var);
                    }
                }
                // Handle type mismatches
                if (left.GetReturnType() != right.GetReturnType())
                {
                    SCError.HandleError(SCErrors.CS20, op);
                }

                left = new BinaryExpressionNode(left, right, op.Type);
            }
            if (!Operators.IsOperator(Peek().Type) && left is VariableNode varNode)
            {
                // If variable has not been assigned a type, we know it is new
                if (varNode.VariableType == null)
                {
                    // Since its new and isnt being assigned, its undefined
                    SCError.HandleError(SCErrors.CS19, exprFirst);
                }
            }
            return left;
        }

        private ExpressionNode ParsePrimaryExpression()
        {
            if (Match(TokenType.KwTrue))
            {
                return new BooleanLiteralNode("1");
            }
            if (Match(TokenType.KwFalse))
            {
                return new BooleanLiteralNode("0");
            }
            if (Match(TokenType.Float))
            {
                return new FloatLiteralNode(Previous().Value);
            }
            if (Match(TokenType.Number))
            {
                return new NumberLiteralNode(Previous().Value);
            }
            if (Match(TokenType.Identifier))
            {
                Token identifier = Previous();
                if (Match(TokenType.GmOpenParen))
                {
                    return ParseFunctionCall(identifier);
                }
                if (variables.TryGetValue(identifier.Value, out VariableNode? value))
                {
                    return value;
                }
                return new VariableNode(Previous().Value, new(Previous().Value));
            }
            SCError.HandleError(SCErrors.CS6, Peek());
            return default!;
        }

        private FunctionCallNode ParseFunctionCall(Token identifier)
        {
            if (!functionDeclerations.ContainsKey(identifier.Value))
            {
                SCError.HandleError(SCErrors.CS9, identifier);
            }
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
            FunctionDeclerationNode decleration = functionDeclerations[identifier.Value];
            if (args.Count != decleration.FunctionParams.Count)
            {
                SCError.HandleError(SCErrors.CS10, identifier);
            }
            return new(identifier.Value, args, decleration);
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
