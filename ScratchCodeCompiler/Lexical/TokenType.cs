namespace ScratchCodeCompiler.Lexical
{
    enum TokenType
    {
        Identifier,
        Number,

        // Keywords
        KwIf,
        KwElse,
        KwForever,
        KwRepeat,
        KwRepeatUntil,
        KwWait,
        KwWaitUntil,

        // Operators
        OpAssign,
        OpEqual,
        OpNotEqual,
        OpAdd,
        OpSubtract,
        OpMultiply,
        OpDivide,

        // Grammars
        GmOpenParen,
        GmCloseParen,
        GmOpenBrace,
        GmCloseBrace,
        GmCloseBracket,
        GmOpenBracket,
        GmComma,
        GmSemicolon,

        EOF
    }
}
