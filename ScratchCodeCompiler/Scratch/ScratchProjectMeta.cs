using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Scratch
{
    internal class ScratchProjectMeta : IScratchJsonable
    {
        public string Semver { get; }
        public string Vm { get; }
        public string Agent { get; }

        public ScratchProjectMeta()
        {
            Semver = "3.0.0";
            Vm = "11.0.0-beta.2";
            Agent = "ScratchCode compiler";
        }

        public string ToJson()
        {
            string json = "{";
            json += $"\"semver\":\"{Semver}\",";
            json += $"\"vm\":\"{Vm}\",";
            json += $"\"agent\":\"{Agent}\"";
            json += "}";
            return json;
        }
    }
}
