using ScratchCodeCompiler.Lexical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Scratch
{
    internal static class ScratchUtility
    {
        private static List<ScratchBlock> ReturnStack = [];

        public static bool TryGetArithmaticOperatorOpcode(TokenType op, out ScratchOpcode opcode)
        {
            opcode = op switch
            {
                TokenType.OpAdd => ScratchOpcode.Operator_Add,
                TokenType.OpSubtract => ScratchOpcode.Operator_Subtract,
                TokenType.OpMultiply => ScratchOpcode.Operator_Multiply,
                TokenType.OpDivide => ScratchOpcode.Operator_Divide,
                _ => ScratchOpcode.None
            };
            return opcode != ScratchOpcode.None;
        }
    }
}
