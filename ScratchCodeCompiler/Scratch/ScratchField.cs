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
        public string Label { get; set; }
        public ScratchVariable? Variable { get; }

        public ScratchField(string name, string label)
        {
            Name = name;
            Label = label;
            Variable = null;
        }

        public ScratchField(string name, ScratchVariable variable)
        {
            Name = name;
            Label = variable.Name;
            Variable = variable;
        }

        public string ToJson()
        {
            // "VARIABLE":["myVar","w2;5DP*ri[?LbZTB}d;5"]
            if (Variable != null)
            {
                return $"\"{Name}\":[\"{Label}\",{Variable?.Id.ToJson()}]";
            }
            // "VALUE":["this is a num input",null]
            else
            {
                return $"\"{Name}\":[\"{Label}\",null]";
            }
        }
    }
}
