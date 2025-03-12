using ScratchCodeCompiler.ErrorHandling;

namespace ScratchCodeCompiler.Lexical
{
    internal class Lexer
    {
        private readonly string[] codeLines;
        private readonly List<Token> tokens;

        private int line = 1;
        private int column = 1;

        public Lexer(string[] codeLines)
        {
            this.codeLines = codeLines.Select(str => str.Replace("\t", "    ")).ToArray();
            SCErrorHelper.InputLines = this.codeLines;
            tokens = [];
        }

        public List<Token> Tokenize()
        {
            string word = string.Empty;
            foreach (string codeLine in codeLines)
            {
                for (int i = 0; i < codeLine.Length; i++)
                {
                    char c = codeLine[i];
                    if (Operators.TryGetOperator(codeLine, ref i, out TokenType opType))
                    {
                        TryAddWord(ref word, line, column);
                        tokens.Add(new Token(opType, Operators.GetOperatorString(opType), line, column));
                        column += Operators.GetOperatorLength(opType) - 1;
                    }
                    else if (Grammars.TryGetGrammar(c, out TokenType gmType))
                    {
                        TryAddWord(ref word, line, column);
                        tokens.Add(new Token(gmType, c.ToString(), line, column));
                    }
                    else if (char.IsWhiteSpace(c))
                    {
                        TryAddWord(ref word, line, column);
                        word = string.Empty;
                    }
                    else word += c;
                    column++;
                }
                TryAddWord(ref word, line, column);
                line++;
                column = 1;
            }
            TryAddWord(ref word, line, column);
            tokens.Add(new Token(TokenType.EOF, string.Empty, line, column));

            return tokens;
        }

        private bool TryAddWord(ref string word, int l, int c)
        {
            if (!string.IsNullOrEmpty(word))
            {
                if (long.TryParse(word, out _))
                {
                    tokens.Add(new Token(TokenType.Number, word, l, c - word.Length));
                }
                else if (float.TryParse(word, out _))
                {
                    tokens.Add(new Token(TokenType.Float, word, l, c - word.Length));
                }
                else
                {
                    if (Keywords.TryGetKeyword(word, out TokenType type))
                    {
                        tokens.Add(new Token(type, word, l, c - word.Length));
                    }
                    else
                    {
                        Token tk = new(TokenType.Identifier, word, l, c - word.Length);
                        if (tk.Value.Contains('.'))
                        {
                            SCError.HandleError(SCErrors.CS18, tk);
                        }
                        tokens.Add(tk);
                    }
                }
                word = string.Empty;
                return true;
            }
            return false;
        }
    }
}
