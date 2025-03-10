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
        bool NullLiteral { get; set; } = false;

        public ScratchInput(string name)
        {
            Type = ScratchInputType.Value;
            Name = name;
            Format = ScratchInputFormat.String;
            LiteralValue = "";
            NullLiteral = true;
        }

        public ScratchInput(string name, ScratchInputFormat format, string literalValue)
        {
            Type = ScratchInputType.Value;
            Name = name;
            Format = format;
            LiteralValue = literalValue;
        }

        public ScratchInput(string name, ScratchBlock block, ScratchInputType overrideType = ScratchInputType.Block)
        {
            Type = overrideType;
            Name = name;
            Block = block;
            LiteralValue = "";
        }

        public ScratchInput(string name, ScratchBlock block, ScratchInputFormat defaultFormat)
        {
            Type = ScratchInputType.Expression;
            Name = name;
            Format = defaultFormat;
            Block = block;
            LiteralValue = "";
        }

        public ScratchInput(string name, ScratchInputFormat format, ScratchVariable variable)
        {
            Type = ScratchInputType.Expression;
            Name = name;
            Format = format;
            Variable = variable;
            LiteralValue = "";
        }

        public string ToJson()
        {
            // "STRING2":[3,"Yd.%(U?CL.sF|AKa1v7M",[10,"banana"]]
            if (Type == ScratchInputType.Expression)
            {
                // "VALUE":[3,[12,"myVar","98x7VmWgo0LxQ4lj5pzy"],[4,"534353"]]
                if (Variable != null)
                {
                    return $"\"{Name}\":[{(int)Type},[{(int)ScratchInputFormat.Variable},\"{Variable?.Name}\",{Variable?.Id.ToJson()}],[{(int)Format},\"{LiteralValue}\"]]";
                }
                return $"\"{Name}\":[{(int)Type},{Block?.Id.ToJson()},[{(int)Format},\"{LiteralValue}\"]]";
            }
            // "SUBSTACK2":[2,"eKEwGJ3EX88WeN(7JS7K"]
            else if (Type == ScratchInputType.Block)
            {
                return $"\"{Name}\":[{(int)Type},{Block?.Id.ToJson()}]";
            }
            // "custom_block":[1,"Z8RoQ`N=?7Pc?Va0[|xy"]
            else if (Type == ScratchInputType.CustomBlock)
            {
                return $"\"{Name}\":[1,{Block?.Id.ToJson()}]";
            }
            // "SUBSTACK":[1,null]
            if (NullLiteral)
            {
                return $"\"{Name}\":[{(int)Type},null]";
            }
            // "STRING2":[1,[10,"banana"]]
            return $"\"{Name}\":[{(int)Type},[{(int)Format},\"{LiteralValue}\"]]";
        }
    }
}
