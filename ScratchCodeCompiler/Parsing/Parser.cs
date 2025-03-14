﻿using ScratchCodeCompiler.ErrorHandling;
using ScratchCodeCompiler.Lexical;
using ScratchCodeCompiler.Parsing.AST;
using ScratchCodeCompiler.Scratch;
using System.Linq;

namespace ScratchCodeCompiler.Parsing
{
    internal class Parser
    {
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
                functionDeclerations.Add(func.Name, func);
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

        private CodeBlockNode ParseCodeBlock(CodeBlockContext ctx)
        {
            if (!Match(TokenType.GmOpenBrace))
            {
                SCError.HandleError(SCErrors.CS4, Peek());
            }
            CodeBlockNode codeBlock = new();
            while (!IsAtEnd() && !Match(TokenType.GmCloseBrace))
            {
                if (codeBlock.Children.Where(x => x is ForeverStatementNode).Any())
                {
                    SCError.HandleWarning(SCWarnings.CSW1, Peek());
                }
                // Inside event
                if (ctx == CodeBlockContext.Event)
                {
                    // Event in event
                    if (Check(TokenType.KwEvent))
                    {
                        SCError.HandleError(SCErrors.CS16, Peek());
                    }
                    // Function in event
                    else if (Check(TokenType.KwFunc))
                    {
                        SCError.HandleError(SCErrors.CS15, Peek());
                    }
                }
                // Inside function
                else if (ctx == CodeBlockContext.Function && Check(TokenType.KwFunc))
                {
                    // Function in function
                    SCError.HandleError(SCErrors.CS15, Peek());
                }
                // Outside function and event
                else
                {
                    // Function outside function or event
                    if (Check(TokenType.KwFunc))
                    {
                        SCError.HandleError(SCErrors.CS15, Peek());
                    }
                    // Forever statement outside function or event
                    if (Check(TokenType.KwForever))
                    {
                        SCError.HandleError(SCErrors.CS17, Peek());
                    }
                    // Repeat statement outside function or event
                    if (Check(TokenType.KwRepeat))
                    {
                        SCError.HandleError(SCErrors.CS17, Peek());
                    }
                    // Repeat until statement outside function or event
                    if (Check(TokenType.KwRepeatUntil))
                    {
                        SCError.HandleError(SCErrors.CS17, Peek());
                    }
                    // Wait until statement outside function or event
                    if (Check(TokenType.KwWaitUntil))
                    {
                        SCError.HandleError(SCErrors.CS17, Peek());
                    }
                }

                codeBlock.Children.Add(ParseExpression());
            }
            if (Previous().Type != TokenType.GmCloseBrace)
            {
                SCError.HandleError(SCErrors.CS5, Previous());
            }
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
            CodeBlockNode eventBody = ParseCodeBlock(CodeBlockContext.Event);
            return new(scratchEvent, eventBody);
        }

        private ASTNode ParseExpression()
        {
            if (Match(TokenType.KwFunc))
            {
                FunctionDeclerationNode funcDecl = ParseFunctionDeclaration();
                functionDeclerations.Add(funcDecl.Name, funcDecl);
                return funcDecl;
            }
            if (Match(TokenType.KwEvent))
            {
                return ParseEventDecleration();
            }
            if (Match(TokenType.KwIf))
            {
                return ParseIfStatement();
            }
            if (Match(TokenType.KwForever))
            {
                return new ForeverStatementNode(ParseCodeBlock(CodeBlockContext.ForeverStatement));
            }
            if (Match(TokenType.KwRepeat))
            {
                return new RepeatStatementNode(ParseBinaryExpression(), ParseCodeBlock(CodeBlockContext.RepeatStatement));
            }
            if (Match(TokenType.KwRepeatUntil))
            {
                return new RepeatUntilStatementNode(ParseBinaryExpression(), ParseCodeBlock(CodeBlockContext.RepeatUntilStatement));
            }
            if (Match(TokenType.KwWaitUntil))
            {
                return new WaitUntilStatementNode(ParseBinaryExpression());
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
            List<ScratchFunctionParameter> parameters = [];
            while (!IsAtEnd() && !Match(TokenType.GmCloseParen))
            {
                if (!Match(TokenType.Identifier))
                {
                    SCError.HandleError(SCErrors.CS1, Peek());
                }
                parameters.Add(new(Previous().Value));
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
            // Add function parameters
            foreach (var param in parameters)
            {
                VariableNode varNode = new(param.Name, new(param.Name, param.Reporter.Id, ScratchVariableType.Parameter))
                {
                    // Give default type for now
                    VariableType = ScratchType.Number
                };
                variables.Add(param.Name, varNode);
            }
            CodeBlockNode functionBody = ParseCodeBlock(CodeBlockContext.Function);
            // Remove function parameters
            foreach (var param in parameters)
            {
                variables.Remove(param.Name);
            }
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
            IfStatementNode ifStmt = new(condition, ParseCodeBlock(CodeBlockContext.IfStatement));
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
            return new(parent, ParseCodeBlock(CodeBlockContext.ElseStatement));
        }

        private ExpressionNode ParseBinaryExpression(int precedence = 0)
        {
            Token lExprFirst = Peek();
            ExpressionNode left = ParsePrimaryExpression();
            while (!IsAtEnd() && Operators.IsOperator(Peek().Type) && Operators.GetPrecedence(Peek().Type) > precedence)
            {
                Token op = Consume();
                Token rExprFirst = Peek();
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
                // Handle and and or operators
                if (op.Type == TokenType.OpAnd || op.Type == TokenType.OpOr)
                {
                    if (left.GetReturnType() != ScratchType.Boolean)
                    {
                        SCError.HandleError(SCErrors.CS12, lExprFirst);
                    }
                    if (right.GetReturnType() != ScratchType.Boolean)
                    {
                        SCError.HandleError(SCErrors.CS12, rExprFirst);
                    }
                }
                // Handle type mismatches
                if (left.GetReturnType() != right.GetReturnType())
                {
                    SCError.HandleError(SCErrors.CS20, lExprFirst);
                }

                left = new BinaryExpressionNode(left, right, op.Type);
            }
            if (!Operators.IsOperator(Peek().Type) && left is VariableNode varNode)
            {
                // If variable has not been assigned a type, we know it is new
                if (varNode.VariableType == null)
                {
                    // Since its new and isnt being assigned, its undefined
                    SCError.HandleError(SCErrors.CS19, lExprFirst);
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
                return new VariableNode(Previous().Value, new(Previous().Value, ScratchVariableType.Regular));
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
            if (args.Count != decleration.Parameters.Count)
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
