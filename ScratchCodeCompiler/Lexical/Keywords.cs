﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Lexical
{
    internal static class Keywords
    {
        public static bool IsKeyword(string word)
        {
            return TryGetKeyword(word, out _);
        }

        public static bool TryGetKeyword(string word, out TokenType keywordType)
        {
            keywordType = word switch
            {
                "func" => TokenType.KwFunc,
                "if" => TokenType.KwIf,
                "else" => TokenType.KwElse,
                "forever" => TokenType.KwForever,
                "repeat" => TokenType.KwRepeat,
                "repeatuntil" => TokenType.KwRepeatUntil,
                "wait" => TokenType.KwWait,
                "waituntil" => TokenType.KwWaitUntil,
                _ => TokenType.Identifier
            };
            return keywordType != TokenType.Identifier;
        }
    }
}
