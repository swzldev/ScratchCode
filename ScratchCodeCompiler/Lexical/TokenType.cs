namespace ScratchCodeCompiler.Lexical
{
    enum TokenType
    {
        Identifier,
        Float,
        Number,

        // Keywords
        KwTrue,
        KwFalse,
        KwFunc,
        KwEvent,
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
        OpGreaterThan,
        OpLessThan,
        OpAdd,
        OpSubtract,
        OpMultiply,
        OpDivide,
        OpAccess,

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
