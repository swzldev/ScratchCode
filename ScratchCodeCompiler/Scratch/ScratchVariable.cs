using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Scratch
{
    internal class ScratchVariable : IScratchJsonable
    {
        public ScratchId Id { get; set; }
        public string Name { get; }

        public ScratchVariable(string name)
        {
            Id = new();
            Name = name;
        }

        public string ToJson()
        {
            string json = $"{Id.ToJson()}:[";
            json += $"\"{Name}\",0"; // 0 is the default value
            json += "]";
            return json;
        }
    }
}
