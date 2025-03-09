namespace ScratchCodeCompiler.Lexical
{
    enum TokenType
    {
        Identifier,
        Number,

        // Keywords
        KwIf,
        KwElse,
        KwRepeat,

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

        EOF
    }
}
