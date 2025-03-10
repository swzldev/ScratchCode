using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Scratch
{
    internal class ScratchField
    {
        public string Name { get; set; }
        public ScratchFieldType Type { get; }
        public ScratchVariable? Variable { get; }

        public ScratchField(string name, ScratchVariable variable)
        {
            Name = name;
            Type = ScratchFieldType.Variable;
            Variable = variable;
        }

        public string ToJson()
        {
            // "VARIABLE":["hi","w2;5DP*ri[?LbZTB}d;5"]
            if (Type == ScratchFieldType.Variable)
            {
                return $"\"{Name}\":[\"{Variable?.Name}\",{Variable?.Id.ToJson()}]";
            }
            return "";
        }
    }
}
