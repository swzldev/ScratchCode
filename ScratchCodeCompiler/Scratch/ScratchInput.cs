using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Scratch
{
    internal class ScratchInput : IScratchJsonable
    {
        public ScratchBlock? Block { get; }
        public ScratchVariable? Variable { get; }
        public ScratchInputType Type { get; }
        public ScratchInputFormat Format { get; }
        public string Name { get; set; }
        public string LiteralValue { get; set; }

        public ScratchInput(string name, ScratchInputFormat format, string literalValue)
        {
            Type = ScratchInputType.Value;
            Name = name;
            Format = format;
            LiteralValue = literalValue;
        }

        public ScratchInput(string name, ScratchBlock block, ScratchInputFormat defaultFormat, bool ptr = false)
        {
            if (ptr)
            {
                Type = ScratchInputType.BlockPointer;
            }
            else
            {
                Type = ScratchInputType.Block;
            }
            Name = name;
            Format = defaultFormat;
            Block = block;
            LiteralValue = "grapefruit";
        }

        public ScratchInput(string name, ScratchInputFormat format, ScratchVariable variable)
        {
            Type = ScratchInputType.Block;
            Name = name;
            Format = format;
            Variable = variable;
            LiteralValue = "grapefruit";
        }

        public string ToJson()
        {
            // "STRING2":[3,"Yd.%(U?CL.sF|AKa1v7M",[10,"banana"]]
            if (Type == ScratchInputType.Block)
            {
                return $"\"{Name}\":[{(int)Type},\"{Block?.Id}\",[{(int)Format},\"{LiteralValue}\"]]";
            }
            // "SUBSTACK2":[2,"eKEwGJ3EX88WeN(7JS7K"]
            else if (Type == ScratchInputType.BlockPointer)
            {
                return $"\"{Name}\":[{(int)Type},\"{Block?.Id}\"]";
            }
            // "STRING2":[1,[10,"banana"]]
            return $"\"{Name}\":[{(int)Type},[{(int)Format},\"{LiteralValue}\"]]";
        }
    }
}
