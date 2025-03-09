namespace ScratchCodeCompiler.Lexical
{
    enum TokenType
    {
        Identifier,
        LineTerminator,
        Number,

        OpAssign,
        OpAdd,
        OpSubtract,
        OpMultiply,
        OpDivide,

        EOF
    }
}
