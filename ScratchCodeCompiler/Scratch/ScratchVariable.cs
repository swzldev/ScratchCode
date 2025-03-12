using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Scratch
{
    internal class ScratchVariable : IScratchJsonable
    {
        public string Name { get; }
        public ScratchId Id { get; }
        public ScratchVariableType Type { get; }
        public ScratchInputFormat? Format { get; set; }

        public ScratchVariable(string name, ScratchVariableType type)
        {
            Name = name;
            Id = new();
            Type = type;
            Format = null;
        }

        public ScratchVariable(string name, ScratchId id, ScratchVariableType type)
        {
            Name = name;
            Id = id;
            Type = type;
            Format = null;
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
