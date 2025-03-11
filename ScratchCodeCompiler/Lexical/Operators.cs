using ScratchCodeCompiler.ErrorHandling;
using ScratchCodeCompiler.Scratch;

namespace ScratchCodeCompiler.Lexical
{
    internal static class Operators
    {
        private static readonly Dictionary<string, TokenType> operatorTypeMap = new()
        {
            { "==" , TokenType.OpEqual },
            { "!=", TokenType.OpNotEqual },
            { "=", TokenType.OpAssign },
            { "+", TokenType.OpAdd },
            { "-", TokenType.OpSubtract },
            { "*", TokenType.OpMultiply },
            { "/", TokenType.OpDivide },
        };

        public static string GetOperatorString(TokenType op)
        {
            foreach (string key in operatorTypeMap.Keys)
            {
                if (operatorTypeMap[key] == op)
                {
                    return key;
                }
            }
            return "";
        }

        public static ScratchType GetOperatorReturnType(TokenType op)
        {
            return op switch
            {
                TokenType.OpAdd => ScratchType.Number,
                TokenType.OpSubtract => ScratchType.Number,
                TokenType.OpMultiply => ScratchType.Number,
                TokenType.OpDivide => ScratchType.Number,
                TokenType.OpEqual => ScratchType.Boolean,
                TokenType.OpNotEqual => ScratchType.Boolean,
                _ => throw new ArgumentException("op was not an Operator", nameof(op))
            };
        }

        public static int GetPrecedence(TokenType op)
        {
            return op switch
            {
                TokenType.OpEqual => 1,
                TokenType.OpNotEqual => 2,
                TokenType.OpAdd => 3,
                TokenType.OpSubtract => 4,
                TokenType.OpMultiply => 5,
                TokenType.OpDivide => 6,
                _ => throw new ArgumentException("op was not an Operator", nameof(op))
            };
        }

        public static int GetOperatorLength(TokenType op)
        {
            foreach (string key in operatorTypeMap.Keys)
            {
                if (operatorTypeMap[key] == op)
                {
                    return key.Length;
                }
            }
            return 0;
        }

        public static bool IsOperator(TokenType op)
        {
            return operatorTypeMap.ContainsValue(op);
        }

        public static bool TryGetOperator(string code, ref int index, out TokenType type)
        {
            int remain = code.Length - index;
            string op = code.Substring(index, Math.Min(3, remain));
            foreach (string key in operatorTypeMap.Keys)
            {
                if (op.StartsWith(key))
                {
                    type = operatorTypeMap[key];
                    index += key.Length - 1;
                    return true;
                }
            }
            type = TokenType.Identifier;
            return false;
        }
    }
}
