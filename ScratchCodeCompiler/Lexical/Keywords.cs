using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Lexical
{
    internal static class Keywords
    {
        public static bool TryGetKeyword(string word, out TokenType keywordType)
        {
            keywordType = word switch
            {
                "if" => TokenType.KwIf,
                "else" => TokenType.KwElse,
                "repeat" => TokenType.KwRepeat,
                _ => TokenType.Identifier
            };
            return keywordType != TokenType.Identifier;
        }
    }
}
