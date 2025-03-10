using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Scratch
{
    internal class ScratchMutation : IScratchJsonable
    {
        public string ProcCode { get; }
        public List<ScratchId> ArgumentIds { get; } = [];
        public List<string> ArgumentNames { get; } = [];
        public List<string> ArgumentDefaults { get; } = [];

        public ScratchMutation(string procCode)
        {
            ProcCode = procCode;
        }

        public string ToJson()
        {
            string json = $"\"tagname\":\"mutation\",\"children\":[],\"proccode\":{ProcCode},\"argumentids\":\"[";
            for (int i = 0; i < ArgumentIds.Count; i++)
            {
                json += $"\\\"{ArgumentIds[i].Id}\\\"";
                if (i < ArgumentIds.Count - 1)
                {
                    json += ",";
                }
            }
            json += "]\",\"argumentnames\":\"[";
            for (int i = 0; i < ArgumentNames.Count; i++)
            {
                json += $"\\\"{ArgumentNames[i]}\\\"";
                if (i < ArgumentNames.Count - 1)
                {
                    json += ",";
                }
            }
            json += "]\",\"argumentdefaults\":\"[";
            for (int i = 0; i < ArgumentDefaults.Count; i++)
            {
                json += $"\\\"{ArgumentDefaults[i]}\\\"";
                if (i < ArgumentDefaults.Count - 1)
                {
                    json += ",";
                }
            }
            json += "]\",\"warp\":\"false\"";
            return json;
        }
    }
}
