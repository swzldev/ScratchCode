using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Scratch
{
    internal class ScratchFunctionParameter
    {
        public string Name { get; }
        public ScratchId Id { get; }
        public ScratchBlock Reporter { get; }

        public ScratchFunctionParameter(string name)
        {
            Name = name;
            Id = new();
            // String_Number is default until i add function parameter type info
            Reporter = new(ScratchOpcode.Argument_Reporter_String_Number);
        }
    }
}
