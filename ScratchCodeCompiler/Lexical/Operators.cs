namespace ScratchCodeCompiler.Lexical
{
    public enum OperatorType
    {
        None,
        Assign,
        Add,
        Subtract,
        Multiply,
        Divide,
    }

    internal static class Operators
    {
        public static TokenType OperatorToTokenType(OperatorType type)
        {
            return type switch
            {
                OperatorType.Assign => TokenType.OpAssign,
                OperatorType.Add => TokenType.OpAdd,
                OperatorType.Subtract => TokenType.OpSubtract,
                OperatorType.Multiply => TokenType.OpMultiply,
                OperatorType.Divide => TokenType.OpDivide,
                _ => throw new ArgumentException("Invalid operator type", nameof(type)),
            };
        }

        public static OperatorType TokenTypeToOperator(TokenType type)
        {
            return type switch
            {
                TokenType.OpAssign => OperatorType.Assign,
                TokenType.OpAdd => OperatorType.Add,
                TokenType.OpSubtract => OperatorType.Subtract,
                TokenType.OpMultiply => OperatorType.Multiply,
                TokenType.OpDivide => OperatorType.Divide,
                _ => throw new ArgumentException("Invalid token type", nameof(type)),
            };
        }

        public static int GetPrecedence(OperatorType type)
        {
            return type switch
            {
                OperatorType.Assign => 1,
                OperatorType.Add => 2,
                OperatorType.Subtract => 2,
                OperatorType.Multiply => 3,
                OperatorType.Divide => 3,
                _ => throw new ArgumentException("Invalid operator type", nameof(type)),
            };
        }

        public static bool GetOperator(char op, out OperatorType type)
        {
            type = op switch
            {
                '=' => OperatorType.Assign,
                '+' => OperatorType.Add,
                '-' => OperatorType.Subtract,
                '*' => OperatorType.Multiply,
                '/' => OperatorType.Divide,
                _ => OperatorType.None
            };
            return type != OperatorType.None;
        }
    }
}
