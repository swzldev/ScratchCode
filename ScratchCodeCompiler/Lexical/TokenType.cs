namespace ScratchCodeCompiler.Lexical
{
    enum TokenType
    {
        Identifier,
        LineTerminator,
        Number,

        // Keywords
        KwIf,
        KwElse,
        KwRepeat,

        // Operators
        OpAssign,
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

        EOF
    }
}
