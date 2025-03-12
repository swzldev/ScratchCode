using ScratchCodeCompiler.Scratch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Parsing.AST
{
    internal class BooleanLiteralNode : LiteralNode, IScratchBlockTranslatable
    {
        public string Value { get; }

        public bool IsTrue => Value == "1";

        public BooleanLiteralNode(string value)
        {
            Value = value;
        }

        public override ScratchType GetReturnType() => ScratchType.Boolean;

        public override string GetValue()
        {
            return Value;
        }

        public ScratchBlock ToScratchBlock(ref List<ScratchBlock> blocks)
        {
            // A not block with no inputs is a true literal
            ScratchBlock boolBlock = new(ScratchOpcode.Operator_Not);
            if (!IsTrue)
            {
                ScratchBlock falseBlock = new(ScratchOpcode.Operator_Not);
                blocks.Add(falseBlock);
                boolBlock.Inputs.Add(new("OPERAND", falseBlock));
                falseBlock.Parent = boolBlock;
            }
            return boolBlock;
        }

        public override string ToString()
        {
            return $"BooleanLiteral({Value})";
        }
    }
}
