namespace ScratchCodeCompiler.Lexical
{
    internal class Lexer
    {
        public static List<Token> Tokenize(string code)
        {
            List<Token> tokens = new();

            bool lastWasNewline = false;
            string word = string.Empty;
            for (int i = 0; i < code.Length; i++)
            {
                char c = code[i];
                if (Operators.GetOperator(c, out OperatorType type))
                {
                    TokenType tokenType = Operators.OperatorToTokenType(type);
                    if (!string.IsNullOrEmpty(word))
                    {
                        tokens.Add(new Token(TokenType.Identifier, word));
                        word = string.Empty;
                    }
                    tokens.Add(new Token(tokenType, c.ToString()));
                    continue;
                }
                if (c == '\n')
                {
                    if (!string.IsNullOrEmpty(word))
                    {
                        if (long.TryParse(word, out _))
                        {
                            tokens.Add(new Token(TokenType.Number, word));
                        }
                        else
                        {
                            tokens.Add(new Token(TokenType.Identifier, word));
                        }
                    }
                    word = string.Empty;
                    tokens.Add(new Token(TokenType.LineTerminator, "\\n"));
                    continue;
                }
                if (char.IsWhiteSpace(c))
                {
                    if (!string.IsNullOrEmpty(word))
                    {
                        if (long.TryParse(word, out _))
                        {
                            tokens.Add(new Token(TokenType.Number, word));
                        }
                        else
                        {
                            tokens.Add(new Token(TokenType.Identifier, word));
                        }
                    }
                    word = string.Empty;
                    continue;
                }
                word += c;
            }
            if (!string.IsNullOrEmpty(word))
            {
                if (long.TryParse(word, out _))
                {
                    tokens.Add(new Token(TokenType.Number, word));
                }
                else
                {
                    tokens.Add(new Token(TokenType.Identifier, word));
                }
            }
            tokens.Add(new Token(TokenType.EOF, string.Empty));

            return tokens;
        }
    }
}
