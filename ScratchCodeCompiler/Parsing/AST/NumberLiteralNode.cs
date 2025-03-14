﻿using ScratchCodeCompiler.Scratch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Parsing.AST
{
    internal class NumberLiteralNode : LiteralNode
    {
        public string Value { get; }

        public NumberLiteralNode(string value)
        {
            Value = value;
        }

        public override ScratchType GetReturnType() => ScratchType.Number;

        public override string GetValue()
        {
            return Value;
        }

        public override string ToString()
        {
            return $"NumberLiteral({Value})";
        }
    }
}
