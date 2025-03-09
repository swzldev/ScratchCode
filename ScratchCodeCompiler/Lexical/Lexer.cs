namespace ScratchCodeCompiler.Lexical
{
    internal class Lexer
    {
        private string code;
        private List<Token> tokens;

        public Lexer(string code)
        {
            this.code = code;
            tokens = [];
        }

        public List<Token> Tokenize()
        {
            string word = string.Empty;
            for (int i = 0; i < code.Length; i++)
            {
                char c = code[i];
                if (Operators.TryGetOperator(c, out TokenType type))
                {
                    TryAddWord(ref word);
                    tokens.Add(new Token(type, c.ToString()));
                    continue;
                }
                if (c == '\n')
                {
                    TryAddWord(ref word);
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
            TryAddWord(ref word);
            tokens.Add(new Token(TokenType.EOF, string.Empty));

            return tokens;
        }

        private bool TryAddWord(ref string word)
        {
            if (!string.IsNullOrEmpty(word))
            {
                if (long.TryParse(word, out _))
                {
                    tokens.Add(new Token(TokenType.Number, word));
                }
                else
                {
                    if (Keywords.TryGetKeyword(word, out TokenType type))
                    {
                        tokens.Add(new Token(type, word));
                    }
                    else tokens.Add(new Token(TokenType.Identifier, word));
                }
                word = string.Empty;
                return true;
            }
            return false;
        }
    }
}
