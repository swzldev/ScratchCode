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

        public static int GetPrecedence(TokenType type)
        {
            return type switch
            {
                TokenType.OpAssign => 1,
                TokenType.OpDivide => 2,
                TokenType.OpMultiply => 3,
                TokenType.OpSubtract => 4,
                TokenType.OpAdd => 5,
                TokenType.OpEqual => 6,
                TokenType.OpNotEqual => 6,
                _ => throw new ArgumentException($"Invalid operator type: {type}"),
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
