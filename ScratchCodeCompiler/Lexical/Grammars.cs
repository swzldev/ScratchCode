using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Lexical
{
    internal static class Grammars
    {
        public static bool TryGetGrammar(char grammar, out TokenType grammarType)
        {
            grammarType = grammar switch
            {
                '(' => TokenType.GmOpenParen,
                ')' => TokenType.GmCloseParen,
                '{' => TokenType.GmOpenBrace,
                '}' => TokenType.GmCloseBrace,
                '[' => TokenType.GmOpenBracket,
                ']' => TokenType.GmCloseBracket,
                ',' => TokenType.GmComma,
                ';' => TokenType.GmSemicolon,
                _ => TokenType.Identifier
            };
            return grammarType != TokenType.Identifier;
        }
    }
}
