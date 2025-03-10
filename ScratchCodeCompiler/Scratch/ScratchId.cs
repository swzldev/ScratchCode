using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Scratch
{
    internal class ScratchId : IScratchJsonable
    {
        public string Id { get; }

        public ScratchId(int length = 20, bool useGrammar = true)
        {
            Id = IdGenerator.GenerateRandomId(length, useGrammar);
        }

        public string ToJson()
        {
            return $"\"{Id}\"";
        }
    }
}
