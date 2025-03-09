using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Scratch
{
    internal class ScratchProjectMeta : IScratchJsonable
    {
        public string Semver { get; set; }
        public string Vm { get; set; }
        public string Agent { get; set; }

        public ScratchProjectMeta(string semver, string vm, string agent)
        {
            Semver = semver;
            Vm = vm;
            Agent = agent;
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
