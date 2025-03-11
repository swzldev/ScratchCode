using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Scratch
{
    internal class ScratchEvent
    {
        public string Name { get; }
        public string[] Parameters { get; }
        public ScratchOpcode Opcode { get; }

        public ScratchEvent(string name, string[] parameters, ScratchOpcode opcode)
        {
            Name = name;
            Parameters = parameters;
            Opcode = opcode;
        }

        public override string ToString()
        {
            return $"Event({Name}, {string.Join(", ", Parameters)})";
        }
    }
}
