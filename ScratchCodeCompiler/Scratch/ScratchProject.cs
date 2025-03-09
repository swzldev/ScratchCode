using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Scratch
{
    internal class ScratchProject : IScratchJsonable
    {
        public List<ScratchTarget> Targets { get; set; }
        public List<int> Monitors { get; set; } // TODO: Implement ScratchMonitor
        public List<int> Extensions { get; set; } // TODO: Implement ScratchExtension
        public ScratchProjectMeta Meta { get; set; }

        public ScratchProject()
        {
            Targets = new();
            Monitors = new();
            Extensions = new();
            Meta = new();
        }

        public string ToJson()
        {
            string json = "{\"targets\":[";
            for (int i = 0; i < Targets.Count; i++)
            {
                json += Targets[i].ToJson();
                if (i < Targets.Count - 1)
                {
                    json += ",";
                }
            }
            json += "],\"monitors\":[";
            // TODO: Implement ScratchMonitor
            json += "],\"extensions\":[";
            // TODO: Implement ScratchExtension
            json += "],\"meta\":" + Meta.ToJson() + "}";

            return json;
        }
    }
}
