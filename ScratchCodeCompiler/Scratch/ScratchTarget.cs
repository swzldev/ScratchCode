using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Scratch
{
    internal class ScratchTarget : IScratchJsonable
    {
        public bool isStage;
        public string name;
        public List<ScratchVariable> variables;
        public List<int> lists; // TODO: Implement ScratchList
        public List<int> broadcasts; // TODO: Implement ScratchBroadcast
        public List<ScratchBlock> blocks;
        public List<int> comments; // TODO: Implement ScratchComment
        public int currentCostume;
        public List<ScratchCostume> costumes;
        public List<ScratchSound> sounds;
        public int volume;
        public int layerOrder;
        public int tempo;
        public int videoTransparency;
        public ScratchVideoState videoState;
        public int? textToSpeechLanguage = null; // TODO: Implement ScratchTextToSpeechLanguage

        public ScratchTarget(string name, bool isStage)
        {
            this.name = name;
            this.isStage = isStage;
            variables = new();
            lists = new();
            broadcasts = new();
            blocks = new();
            comments = new();
            costumes = new();
            sounds = new();
            volume = 100;
            layerOrder = 0;
            tempo = 60;
            videoTransparency = 50;
            videoState = ScratchVideoState.On;
        }

        public string ToJson()
        {
            string json = "{";
            json += $"\"isStage\":{isStage.ToString().ToLower()},";
            json += $"\"name\":\"{name}\",";
            json += "\"variables\":{";
            for (int i = 0; i < variables.Count; i++)
            {
                json += variables[i].ToJson();
                if (i < variables.Count - 1)
                {
                    json += ",";
                }
            }
            json += "},\"lists\":{},"; // TODO: Implement ScratchList
            json += "\"broadcasts\":{},"; // TODO: Implement ScratchBroadcast
            json += "\"blocks\":{";
            for (int i = 0; i < blocks.Count; i++)
            {
                json += blocks[i].ToJson();
                if (i < blocks.Count - 1)
                {
                    json += ",";
                }
            }
            json += "},\"comments\":{},"; // TODO: Implement ScratchComment
            json += $"\"currentCostume\":{currentCostume},";
            json += "\"costumes\":[";
            for (int i = 0; i < costumes.Count; i++)
            {
                json += costumes[i].ToJson();
                if (i < costumes.Count - 1)
                {
                    json += ",";
                }
            }
            json += "],\"sounds\":[";
            for (int i = 0; i < sounds.Count; i++)
            {
                json += sounds[i].ToJson();
                if (i < sounds.Count - 1)
                {
                    json += ",";
                }
            }
            json += $"],\"volume\":{volume},";
            json += $"\"layerOrder\":{layerOrder},";
            json += $"\"tempo\":{tempo},";
            json += $"\"videoTransparency\":{videoTransparency},";
            json += $"\"videoState\":\"{videoState.ToString().ToLower()}\",";
            json += "\"textToSpeechLanguage\":null"; // TODO: Implement ScratchTextToSpeechLanguage
            json += "}";

            return json;
        }
    }
}
