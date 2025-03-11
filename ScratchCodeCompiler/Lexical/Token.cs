namespace ScratchCodeCompiler.Lexical
{
    internal class Token
    {
        public TokenType Type { get; }
        public string Value { get; }

        public int Line { get; }
        public int Column { get; }

        public Token(TokenType type, string value, int line, int column)
        {
            Type = type;
            Value = value;
            Line = line;
            Column = column;
        }

        public override string ToString()
        {
            string output = $"Token({Type}";
            if (Type != TokenType.EOF)
            {
                output += $", {Value}";
            }
            return $"{output})";
        }
    }
}
