﻿namespace ScratchCodeCompiler.Lexical
{
    internal static class Operators
    {
        public static int GetPrecedence(TokenType type)
        {
            return type switch
            {
                TokenType.OpAssign => 1,
                TokenType.OpDivide => 2,
                TokenType.OpMultiply => 3,
                TokenType.OpSubtract => 4,
                TokenType.OpAdd => 5,
                _ => throw new ArgumentException("Invalid operator type", nameof(type)),
            };
        }

        public static bool TryGetOperator(char op, out TokenType type)
        {
            type = op switch
            {
                '=' => TokenType.OpAssign,
                '+' => TokenType.OpAdd,
                '-' => TokenType.OpSubtract,
                '*' => TokenType.OpMultiply,
                '/' => TokenType.OpDivide,
                _ => TokenType.Identifier,
            };
            return type != TokenType.Identifier;
        }
    }
}
