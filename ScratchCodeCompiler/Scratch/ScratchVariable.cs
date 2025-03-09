using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Scratch
{
    internal class ScratchVariable
    {
        public ScratchId Id { get; }
        public string Name { get; }

        public ScratchVariable(string name)
        {
            Id = new();
            Name = name;
        }
    }
}
